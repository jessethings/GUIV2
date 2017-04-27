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
        public const string LOCAL_DB_URL = @"C:\Database\database.sqlite";
    }

    public enum PermissionLevel
    {
        Guest = 2,
        User = 1,
        Admin = 0
    }
}
