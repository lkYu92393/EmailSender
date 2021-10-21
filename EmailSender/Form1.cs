using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

using ExcelDataReader;
using MimeKit;

namespace EmailSender
{

    public partial class Form1 : Form
    {
        public string ErrorMsg;
        private CancellationTokenSource source;
        private Dictionary<string, string> templateDict = new Dictionary<string, string>(); // field, value
        private DataTable excelDT;

        #region "Logging"
        //Setter
        public void ChangeProgramStatus(String status) { txtStatus.Text = status; }
        public void WriteTextBox(String message)
        {
            txtMsgBox.AppendText($"{message}{Environment.NewLine}");
        }
        public void writeLogFile(String message)
        {
            string filePath = Application.StartupPath + "//Log//" + "Log" + System.DateTime.Now.ToString("yyyy.MM.dd") + ".txt";
            StreamWriter writer = new StreamWriter(filePath, true, Encoding.Unicode);
            writer.WriteLine(message);
            writer.Close();
        }
        #endregion

        #region constructor and initialization

        public Form1()
        {
            InitializeComponent();

            ErrorMsg = "";
            Init();
        }

        private void Init()
        {
            ofdLoad.InitialDirectory = Directory.GetCurrentDirectory();
            if (Config.FileExcel.ToLower().EndsWith("xlsx"))
            {
                excelDT = ReadExcel(Config.FileExcelPath + Config.FileExcel);
            }
            else if (Config.FileExcel.ToLower().EndsWith("csv"))
            {
                excelDT = ReadCsv(Config.FileExcelPath + Config.FileExcel);
            }
            ReadTemplate();

            if (Config.IsShowTemplate)
                FillForm();
            else
                HideForm();            

            if (String.IsNullOrEmpty(ErrorMsg))
            {
                ChangeProgramStatus("Ready");
                txtMsgBox.Text = "Ready" + Environment.NewLine;
                btnProcessStart.Select();
            }
            else
            {
                ChangeProgramStatus("Error");
                WriteTextBox(ErrorMsg);
                btnProcessStart.Enabled = false;
            }
        }

        // return whole excel as table, including header column
        private DataTable ReadExcel(string filePath) //copy from https://github.com/ExcelDataReader/ExcelDataReader
        {
            DataTable dt;
            if (!File.Exists(filePath))
            {
                ErrorMsg = "Can't find excel file.";
                writeLogFile(String.Format("{0}: Can't find email template file ({1})", DateTime.Now.ToString(), Config.FileExcel));
                return new DataTable();
            }
            using (var stream = File.Open(filePath, FileMode.Open, FileAccess.Read))
            {
                // Auto-detect format, supports:
                //  - Binary Excel files (2.0-2003 format; *.xls)
                //  - OpenXml Excel files (2007 format; *.xlsx, *.xlsb)
                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {
                    // 2. Use the AsDataSet extension method
                    dt = reader.AsDataSet().Tables[0];
                }
            }
            if (Config.ExcelHasHeader)
            {
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    dt.Columns[j].ColumnName = dt.Rows[0][j].ToString();
                }
                dt.Rows.RemoveAt(0);
            }
            else
            {
                if (dt.Columns.Count == ConfigurationManager.AppSettings["excelColumns"].Split('|').Count())
                {
                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        dt.Columns[j].ColumnName = ConfigurationManager.AppSettings["excelColumns"].Split('|')[j];
                    }
                }
            }
            return dt;
        }

        // return whole excel as table, including header column
        private DataTable ReadCsv(string filePath)
        {
            DataTable dt = new DataTable();
            if (!File.Exists(filePath))
            {
                ErrorMsg = "Can't find file.";
                writeLogFile(String.Format("{0}: Can't find email template file ({1})", DateTime.Now.ToString(), Config.FileExcel));
                return dt;
            }
            string[] lines = File.ReadAllLines(filePath);
            int columns = lines[0].Split(',').Length;
            for (int i = 0; i < columns; i++) dt.Columns.Add($"Column{i.ToString()}");
            foreach (string line in lines)
            {
                DataRow dr = dt.NewRow();
                string[] cols = line.Split(',');
                for (int j = 0; j < cols.Length; j++)
                {
                    dr[j] = cols[j];
                }
            }
            if (Config.ExcelHasHeader)
            {
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    dt.Columns[i].ColumnName = dt.Rows[0][i].ToString();
                }
                dt.Rows.RemoveAt(0);
            }
            return dt;
        }

        // read txt file and get data according to given format. Hard code. Prone to error.
        private bool ReadTemplate(string filePath = "")
        {
            if (String.IsNullOrEmpty(filePath)) filePath = Config.FileTemplateMsg;
            if (!File.Exists(filePath))
            {
                ErrorMsg += "Template File not found.";
                writeLogFile(String.Format("{0}: Can't find email template file ({1})", DateTime.Now.ToString(), filePath));
                return false;
            }
            string[] templateLines = File.ReadAllLines(filePath);
            int count = 0;
            StringBuilder msgBuilder = new StringBuilder();
            foreach (string line in templateLines)
            {
                if (count > 1)
                {
                    msgBuilder.Append(line + "<br>");
                }
                else if (line.IndexOf(": ") > -1)
                {
                    string key = line.Substring(0, line.IndexOf(": ")).ToLower();
                    string value = line.Substring(line.IndexOf(": ") + 2, line.Length - key.Length - 2);
                    templateDict.Add(key, value);
                }
                else if (line.StartsWith("-"))
                {
                    count++;
                }
            }
            templateDict.Add("body", msgBuilder.ToString());
            return true;
        }

        private void FillForm()
        {
            if (templateDict.ContainsKey("bcc"))
            {
                txtBcc.Text = templateDict["bcc"];
            }
            txtSubject.Text = templateDict["subject"];
            txtEmailBody.Text = templateDict["body"].Replace("<br>",Environment.NewLine);
        }

        private void HideForm()
        {
            lblBcc.Visible = false;
            lblSubject.Visible = false;
            lblEmailBody.Visible = false;
            txtBcc.Visible = false;
            txtSubject.Visible = false;
            txtEmailBody.Visible = false;
            btnLoad.Visible = false;
            this.Size = new Size(txtMsgBox.Size.Width + 40, txtMsgBox.Size.Height + 115);
            txtMsgBox.Location = new Point(12, 60);
            txtMsgBox.Anchor = (AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Bottom | AnchorStyles.Left);
        }

        #endregion

        #region email function

        //connect to email server, loop through target and send, disconnect after sending all, enable to button, write to status
        //cancellation check before each email
        private async Task SendEmailTask(CancellationToken token)
        {
            bool isTimeout = false;
            using (var client = new MailKit.Net.Smtp.SmtpClient())
            {
                ChangeProgramStatus("Connecting to mail server...");
                Task taskConnect = ConnectionToSmtp(client);
                for (int i = 0; i < 150; i++)
                {
                    if (taskConnect.Status == TaskStatus.RanToCompletion) break;
                    await Task.Delay(100);
                }                

                if (taskConnect.Status == TaskStatus.RanToCompletion)
                    ChangeProgramStatus("Connected.");
                else
                {
                    isTimeout = true;
                    source.Cancel();
                }
                try
                {
                    foreach (DataRow dr in excelDT.Rows)
                    {
                        if (token.IsCancellationRequested) token.ThrowIfCancellationRequested(); //to catch block
                        if (CheckSentLog(dr[1].ToString(), templateDict["subject"].Replace("[BrandName]", dr[0].ToString()))) continue;

                        MimeMessage message = BuildMessage(dr);
                        WriteTextBox(String.Format("Ready to send to {0}", dr[1].ToString()));

                        try
                        {
                            await client.SendAsync(message);
                            writeLogFile(String.Format("{0}: Email sent to {1}|{2}).", DateTime.Now.ToString(), dr[1].ToString(), templateDict["subject"].Replace("[BrandName]", dr[0].ToString())));
                        }
                        catch (Exception ex)
                        {
                            writeLogFile(String.Format("{0}: Error sending email to {1} ({2})", DateTime.Now.ToString(), dr[1].ToString(), ex.Message));
                        }
                    }
                }
                catch (OperationCanceledException)
                {
                    if (!isTimeout)
                        WriteTextBox("Cancelled by user." + Environment.NewLine);
                }
                client.Disconnect(true);
            }

            if (isTimeout)
            {
                ChangeProgramStatus("Connection Timeout");
            }
            else if (source.IsCancellationRequested)
            {
                ChangeProgramStatus("Cancelled");
                btnProcessStart.Enabled = true;
            }
            else
            {
                ChangeProgramStatus("Completed");
                File.Move(Config.FileExcelPath + Config.FileExcel, Config.FileExcelPath + Config.FileExcel.Replace(".xlsx", String.Format("_OK_{0}.xlsx", DateTime.Now.ToString("yyyyMMdd_HHmmss"))));
            }

            btnProcessStop.Enabled = false;
            btnProcessStart.Select();
            source.Dispose();
        }

        private async Task ConnectionToSmtp(MailKit.Net.Smtp.SmtpClient client)
        {
            client.Connect(Config.EmailServer, 587, false);
            client.Authenticate(Config.EmailUser, Config.EmailPW);
        }

        private MimeMessage BuildMessage(DataRow dr)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(Config.UserAlias, Config.EmailUser));
            message.To.Add(new MailboxAddress(dr[0].ToString(), dr[1].ToString()));
            if (templateDict.Keys.Contains<string>("bcc"))
            {
                message.Bcc.Add(MailboxAddress.Parse(templateDict["bcc"]));
            }
            message.Subject = templateDict["subject"];

            var builder = new BodyBuilder();
            builder.TextBody = templateDict["body"];
            builder.HtmlBody = templateDict["body"];
            if (!String.IsNullOrEmpty(dr[2].ToString()))
            {
                string[] attachments = dr[2].ToString().Split('|');
                foreach (string part in attachments)
                {
                    builder.Attachments.Add(Config.AttachmentPath + part);
                }
            }
            message.Body = builder.ToMessageBody();

            return message;
        }
        
        // check log of past 1 week to see if duplicated. Time can be altered in app.settings
        private bool CheckSentLog(string email, string subject)
        {
            for (int i = 0; i <= Config.NumOfCheckLogDate; i++)
            {
                string filePath = Application.StartupPath + "//Log//" + "Log" + System.DateTime.Now.AddDays(-7 + i).ToString("yyyy.MM.dd") + ".txt";
                if (File.Exists(filePath))
                {
                    string[] lines = File.ReadAllLines(filePath);
                    foreach (string line in lines)
                    {
                        if (line.IndexOf(email + "|" + subject) > -1)
                        {
                            writeLogFile(String.Format("{0}: Skip sending email to {1} as duplicated found in {2}", DateTime.Now.ToString(), email, System.DateTime.Now.AddDays(-7 + i).ToString("yyyy.MM.dd")));
                            WriteTextBox(String.Format("Skipped {0}", email));
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        #endregion

        #region Event Handler

        private void btnProcessStart_Click(object sender, EventArgs e)
        {
            btnProcessStart.Enabled = false;
            btnProcessStop.Enabled = true;
            ChangeProgramStatus("Running...");
            btnProcessStop.Select();

            source = new CancellationTokenSource();
            CancellationToken token = source.Token;
            SendEmailTask(token);
        }

        private void btnProcessStop_Click(object sender, EventArgs e)
        {
            if (source != null)
                source.Cancel();
        }
        private void btnLoad_Click(object sender, EventArgs e)
        {
            if (ofdLoad.ShowDialog() == DialogResult.OK)
            {
                string dialogPath = ofdLoad.FileName;
                templateDict.Clear();
                ReadTemplate(dialogPath);
            }
        }

        #endregion

    }

    //Entry Point
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
