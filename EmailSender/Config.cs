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
            NumOfCheckLogDate = int.Parse(GetConfiguration(nameof(NumOfCheckLogDate), "7"));
            IsShowTemplate = GetConfiguration(nameof(IsShowTemplate), "N") == "Y";
            FileExcelPath = GetConfiguration(nameof(FileExcelPath), "email//");
            if (!FileExcelPath.EndsWith("//")) FileExcelPath += "//";
            FileExcel = GetConfiguration(nameof(FileExcel), "templateExcel.xlsx");
            ExcelHasHeader = GetConfiguration(nameof(ExcelHasHeader), "Y") == "Y";
            FileTemplateMsg = GetConfiguration(nameof(FileTemplateMsg), "templateMsg.txt");
            AttachmentPath = GetConfiguration(nameof(AttachmentPath), "attachment//");
            if (!AttachmentPath.EndsWith("//")) AttachmentPath += "//";
            UserAlias = GetConfiguration(nameof(UserAlias), "alias");
            User = GetConfiguration(nameof(User), "");
            Password = GetConfiguration(nameof(Password), "");
            EmailServer = GetConfiguration(nameof(EmailServer), "smtp.live.com");
            EmailPort = int.Parse(GetConfiguration(nameof(EmailPort), "587"));
        }

        private static string GetConfiguration(string name, string defValue)
        {
            return ConfigurationManager.AppSettings.AllKeys.Contains(name) ? ConfigurationManager.AppSettings[name] : defValue;
        }
    }
}
