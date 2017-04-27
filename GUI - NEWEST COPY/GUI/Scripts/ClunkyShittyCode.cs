using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUI.Scripts
{
    public static class ClunkyShittyCode
    {
        public static string SetUploadPanelText(UploadStatus upst)
        {
            //rtbUploadMessages.FontStyle
            if (upst == UploadStatus.SelectFile)
            {
                return "1. Select your Excel Workbook";
            }
            else if (upst == UploadStatus.GenerateLocal)
            {
                return "2. Process the Workbook";
            }
            else if (upst == UploadStatus.UploadLocal)
            {
                return "3. Upload the data to the server";
            }
            else if (upst == UploadStatus.Complete)
            {
                return "Complete!";
            }
            return "error";
        }
    }
}
