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
        ReadLocal,
        UploadLocal,
        Complete
    }

    public enum ActiveWindow
    {
        Home = 1,
        Upload = 2,
        Report = 3,
        Settings = 4
    }
}
