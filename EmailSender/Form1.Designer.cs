namespace EmailSender
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnProcessStop = new System.Windows.Forms.Button();
            this.btnProcessStart = new System.Windows.Forms.Button();
            this.txtStatus = new System.Windows.Forms.TextBox();
            this.lblStatus = new System.Windows.Forms.Label();
            this.txtMsgBox = new System.Windows.Forms.TextBox();
            this.lblBoard = new System.Windows.Forms.Label();
            this.txtBcc = new System.Windows.Forms.TextBox();
            this.lblBcc = new System.Windows.Forms.Label();
            this.txtSubject = new System.Windows.Forms.TextBox();
            this.lblSubject = new System.Windows.Forms.Label();
            this.lblEmailBody = new System.Windows.Forms.Label();
            this.txtEmailBody = new System.Windows.Forms.TextBox();
            this.btnLoad = new System.Windows.Forms.Button();
            this.ofdLoad = new System.Windows.Forms.OpenFileDialog();
            this.SuspendLayout();
            // 
            // btnProcessStop
            // 
            this.btnProcessStop.Enabled = false;
            this.btnProcessStop.Location = new System.Drawing.Point(346, 13);
            this.btnProcessStop.Name = "btnProcessStop";
            this.btnProcessStop.Size = new System.Drawing.Size(75, 23);
            this.btnProcessStop.TabIndex = 16;
            this.btnProcessStop.Text = "Stop";
            this.btnProcessStop.UseVisualStyleBackColor = true;
            this.btnProcessStop.Click += new System.EventHandler(this.btnProcessStop_Click);
            // 
            // btnProcessStart
            // 
            this.btnProcessStart.Location = new System.Drawing.Point(427, 14);
            this.btnProcessStart.Name = "btnProcessStart";
            this.btnProcessStart.Size = new System.Drawing.Size(75, 23);
            this.btnProcessStart.TabIndex = 15;
            this.btnProcessStart.Text = "Start";
            this.btnProcessStart.UseVisualStyleBackColor = true;
            this.btnProcessStart.Click += new System.EventHandler(this.btnProcessStart_Click);
            // 
            // txtStatus
            // 
            this.txtStatus.Location = new System.Drawing.Point(60, 13);
            this.txtStatus.Name = "txtStatus";
            this.txtStatus.ReadOnly = true;
            this.txtStatus.Size = new System.Drawing.Size(280, 20);
            this.txtStatus.TabIndex = 14;
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Location = new System.Drawing.Point(14, 16);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(40, 13);
            this.lblStatus.TabIndex = 13;
            this.lblStatus.Text = "Status:";
            // 
            // txtMsgBox
            // 
            this.txtMsgBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtMsgBox.Location = new System.Drawing.Point(12, 65);
            this.txtMsgBox.Multiline = true;
            this.txtMsgBox.Name = "txtMsgBox";
            this.txtMsgBox.ReadOnly = true;
            this.txtMsgBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtMsgBox.Size = new System.Drawing.Size(490, 184);
            this.txtMsgBox.TabIndex = 12;
            // 
            // lblBoard
            // 
            this.lblBoard.AutoSize = true;
            this.lblBoard.Location = new System.Drawing.Point(14, 48);
            this.lblBoard.Name = "lblBoard";
            this.lblBoard.Size = new System.Drawing.Size(84, 13);
            this.lblBoard.TabIndex = 17;
            this.lblBoard.Text = "Message Board:";
            // 
            // txtBcc
            // 
            this.txtBcc.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtBcc.Location = new System.Drawing.Point(61, 270);
            this.txtBcc.Name = "txtBcc";
            this.txtBcc.ReadOnly = true;
            this.txtBcc.Size = new System.Drawing.Size(360, 20);
            this.txtBcc.TabIndex = 19;
            // 
            // lblBcc
            // 
            this.lblBcc.AutoSize = true;
            this.lblBcc.Location = new System.Drawing.Point(14, 273);
            this.lblBcc.Name = "lblBcc";
            this.lblBcc.Size = new System.Drawing.Size(29, 13);
            this.lblBcc.TabIndex = 18;
            this.lblBcc.Text = "Bcc:";
            // 
            // txtSubject
            // 
            this.txtSubject.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSubject.Location = new System.Drawing.Point(61, 304);
            this.txtSubject.Name = "txtSubject";
            this.txtSubject.ReadOnly = true;
            this.txtSubject.Size = new System.Drawing.Size(360, 20);
            this.txtSubject.TabIndex = 21;
            // 
            // lblSubject
            // 
            this.lblSubject.AutoSize = true;
            this.lblSubject.Location = new System.Drawing.Point(14, 308);
            this.lblSubject.Name = "lblSubject";
            this.lblSubject.Size = new System.Drawing.Size(46, 13);
            this.lblSubject.TabIndex = 20;
            this.lblSubject.Text = "Subject:";
            // 
            // lblEmailBody
            // 
            this.lblEmailBody.AutoSize = true;
            this.lblEmailBody.Location = new System.Drawing.Point(14, 343);
            this.lblEmailBody.Name = "lblEmailBody";
            this.lblEmailBody.Size = new System.Drawing.Size(59, 13);
            this.lblEmailBody.TabIndex = 23;
            this.lblEmailBody.Text = "Email Body";
            // 
            // txtEmailBody
            // 
            this.txtEmailBody.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtEmailBody.Location = new System.Drawing.Point(16, 360);
            this.txtEmailBody.Multiline = true;
            this.txtEmailBody.Name = "txtEmailBody";
            this.txtEmailBody.ReadOnly = true;
            this.txtEmailBody.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtEmailBody.Size = new System.Drawing.Size(486, 197);
            this.txtEmailBody.TabIndex = 22;
            // 
            // btnLoad
            // 
            this.btnLoad.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnLoad.Location = new System.Drawing.Point(427, 270);
            this.btnLoad.Name = "btnLoad";
            this.btnLoad.Size = new System.Drawing.Size(75, 23);
            this.btnLoad.TabIndex = 24;
            this.btnLoad.Text = "Load";
            this.btnLoad.UseVisualStyleBackColor = true;
            this.btnLoad.Click += new System.EventHandler(this.btnLoad_Click);
            // 
            // ofdLoad
            // 
            this.ofdLoad.FileName = "templateMsg.txt";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(514, 570);
            this.Controls.Add(this.btnLoad);
            this.Controls.Add(this.lblEmailBody);
            this.Controls.Add(this.txtEmailBody);
            this.Controls.Add(this.txtSubject);
            this.Controls.Add(this.lblSubject);
            this.Controls.Add(this.txtBcc);
            this.Controls.Add(this.lblBcc);
            this.Controls.Add(this.lblBoard);
            this.Controls.Add(this.btnProcessStop);
            this.Controls.Add(this.btnProcessStart);
            this.Controls.Add(this.txtStatus);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.txtMsgBox);
            this.Name = "Form1";
            this.Text = "Email Sender";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnProcessStop;
        private System.Windows.Forms.Button btnProcessStart;
        private System.Windows.Forms.TextBox txtStatus;
        private System.Windows.Forms.Label lblStatus;
        public System.Windows.Forms.TextBox txtMsgBox;
        private System.Windows.Forms.Label lblBoard;
        private System.Windows.Forms.TextBox txtBcc;
        private System.Windows.Forms.Label lblBcc;
        private System.Windows.Forms.TextBox txtSubject;
        private System.Windows.Forms.Label lblSubject;
        private System.Windows.Forms.Label lblEmailBody;
        public System.Windows.Forms.TextBox txtEmailBody;
        private System.Windows.Forms.Button btnLoad;
        private System.Windows.Forms.OpenFileDialog ofdLoad;
    }
}

