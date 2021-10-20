using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailSender
{
    public static class Config
    {
        public static bool IsShowTemplate, ExcelHasHeader;
        public static string FileExcelPath, FileExcel, FileTemplateMsg, AttachmentPath, UserAlias, User, Password, EmailServer;
        public static int NumOfCheckLogDate, EmailPort;

        static Config()
        {
            NumOfCheckLogDate = ConfigurationManager.AppSettings.AllKeys.Contains("NumOfCheckLogDate") ? int.Parse(ConfigurationManager.AppSettings["NumOfCheckLogDate"]) : 7;
            IsShowTemplate = ConfigurationManager.AppSettings.AllKeys.Contains("ShowTemplate") ? ConfigurationManager.AppSettings["ShowTemplate"] == "Y" : false;
            FileExcelPath = ConfigurationManager.AppSettings.AllKeys.Contains("FileExcelPath") ? ConfigurationManager.AppSettings["FileExcelPath"] : "email//";
            if (!FileExcelPath.EndsWith("//")) FileExcelPath += "//";
            FileExcel = ConfigurationManager.AppSettings.AllKeys.Contains("FileExcel") ? ConfigurationManager.AppSettings["FileExcel"] : "templateExcel.xlsx";
            ExcelHasHeader = ConfigurationManager.AppSettings.AllKeys.Contains("ExcelHasHeader") ? ConfigurationManager.AppSettings["ExcelHasHeader"] == "Y" : true;
            FileTemplateMsg = ConfigurationManager.AppSettings.AllKeys.Contains("FileTemplateMsg") ? ConfigurationManager.AppSettings["FileTemplateMsg"] : "templateMsg.txt";
            AttachmentPath = ConfigurationManager.AppSettings.AllKeys.Contains("AttachmentPath") ? ConfigurationManager.AppSettings["AttachmentPath"] : "attachment//";
            if (!AttachmentPath.EndsWith("//")) AttachmentPath += "//";
            UserAlias = ConfigurationManager.AppSettings.AllKeys.Contains("UserName") ? ConfigurationManager.AppSettings["UserName"] : "alias";
            User = ConfigurationManager.AppSettings.AllKeys.Contains("EmailUser") ? ConfigurationManager.AppSettings["EmailUser"] : "";
            Password = ConfigurationManager.AppSettings.AllKeys.Contains("EmailPW") ? ConfigurationManager.AppSettings["EmailPW"] : "";
            EmailServer = ConfigurationManager.AppSettings.AllKeys.Contains("EmailPW") ? ConfigurationManager.AppSettings["EmailServer"] : "smtp.live.com";
            EmailPort = ConfigurationManager.AppSettings.AllKeys.Contains("EmailPort") ? int.Parse(ConfigurationManager.AppSettings["EmailPort"]) : 587;
        }
    }
}
