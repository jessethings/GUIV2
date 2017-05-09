using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUI.Scripts
{
    public enum UploadStatus
    {
        None,
        SelectFile,
        GenerateLocal,
        UploadLocal,
        Complete
    }

    public enum ActiveWindow
    {
        Home = 1,
        Upload = 2,
        Report = 3,
        Settings = 4,
        Users = 5
    }

    public static class Utilities
    {
        public const string SAVE_DATA_URL = @"save.tf";
        public static string SAVE_FOLDER = @"C:\Database";
        public static string LOCAL_DB_URL = SAVE_FOLDER + @"\database.sqlite";
        public static string LOCAL_REPORT_URL = SAVE_FOLDER + @"\ConsolidatedReport.xlsx";
    }

    public enum PermissionLevel
    {
        Guest = 2,
        User = 1,
        Admin = 0
    }

    public enum UserUpdateState
    {
        CreateNew = 0,
        UpdateAll = 1,
        IgnorePassword = 2
    }
}
