using System.Configuration;
using System.Linq;

namespace EmailSender
{
    internal static class Config
    {
        internal static readonly bool IsDebug, IsShowTemplate, ExcelHasHeader;
        internal static readonly string FileExcelPath, FileExcel, FileTemplateMsg, AttachmentPath, EmailUserAlias, EmailUser, EmailPW, EmailServer;
        internal static readonly int NumOfCheckLogDate, EmailPort;

        static Config()
        {
            IsDebug = GetConfiguration(nameof(IsDebug), "N") == "Y";
            IsShowTemplate = GetConfiguration(nameof(IsShowTemplate), "N") == "Y";
            NumOfCheckLogDate = int.Parse(GetConfiguration(nameof(NumOfCheckLogDate), "7"));
            FileExcelPath = GetConfiguration(nameof(FileExcelPath), "email//");
            if (!FileExcelPath.EndsWith("//")) FileExcelPath += "//";
            FileExcel = GetConfiguration(nameof(FileExcel), "templateExcel.xlsx");
            ExcelHasHeader = GetConfiguration(nameof(ExcelHasHeader), "Y") == "Y";
            FileTemplateMsg = GetConfiguration(nameof(FileTemplateMsg), "templateMsg.txt");
            AttachmentPath = GetConfiguration(nameof(AttachmentPath), "attachment//");
            if (!AttachmentPath.EndsWith("//")) AttachmentPath += "//";
            EmailUserAlias = GetConfiguration(nameof(EmailUserAlias), "alias");
            EmailUser = GetConfiguration(nameof(EmailUser), "");
            EmailPW = GetConfiguration(nameof(EmailPW), "");
            EmailServer = GetConfiguration(nameof(EmailServer), "smtp.live.com");
            EmailPort = int.Parse(GetConfiguration(nameof(EmailPort), "587"));
        }

        private static string GetConfiguration(string name, string defValue)
        {
            return ConfigurationManager.AppSettings.AllKeys.Contains(name) ? ConfigurationManager.AppSettings[name] : defValue;
        }
    }
}
