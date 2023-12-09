namespace DictionaryManagement_Common
{
    public static class SD
    {
        public enum SelectDictionaryScope
        {
            All,
            ArchiveOnly,
            NotArchiveOnly
        }

        public enum UpdateMode
        {
            Update,
            MoveToArchive,
            RestoreFromArchive
        }

        public enum FactoryMode
        {
            NKNH,
            KOS
        }

        public enum MessageBoxMode
        {
            On,
            Off
        }

        public enum LoginReturnMode
        {
            LoginOnly,
            NameOnly,
            LoginAndName
        }

        public static string AppVersion = "";
        public static string? AppFactoryMode = "КOC";
        public static string AdminRoleName = "Администратор";
        public static string SyncUserADGroupsIntervalInMinutesSettingName = "SyncUserADGroupsIntervalInMinutes";

        public static int MaxAllowedExcelRows = 1048576;

        public enum EditMode
        {
            CreateBasedOnRow = 1,
            Create = 2,
            Edit = 3
        }

        public static string ReportDownloadPathSettingName = "ReportDownloadPath";
        public static string ReportUploadPathSettingName = "ReportUploadPath";
        public static string ReportInputSheetSettingName = "ReportInputSheet";
        public static string ReportOutputSheetSettingName = "ReportOutputSheet";
        public static string ReportStartEndDateSheetSettingName = "ReportStartEndDateSheet";
        public static string ReportReasonLiabrarySheetSettingName = "ReportReasonLiabrarySheet";
        public static string ReportTagLibrarySheetSettingName = "ReportTagLibrary";
        public static string ReportTemplatePathSettingName = "ReportTemplatePath";
        public static string ExcelWorkBookProtectionPassword = "sirreport";

        public static String RemoveInvalidCharsFromFilename(this String file_name, int? maxFileNameLength = 200)
        {
            foreach (Char invalid_char in Path.GetInvalidFileNameChars())
            {
                file_name = file_name.Replace(oldValue: invalid_char.ToString(), newValue: "_");
            }

            if(maxFileNameLength!=null)
            {
                if ((file_name.Length - (int)maxFileNameLength) > 0)
                    file_name = file_name.Substring(file_name.Length - (int) maxFileNameLength);
            }

            return file_name;
        }
    }
}
