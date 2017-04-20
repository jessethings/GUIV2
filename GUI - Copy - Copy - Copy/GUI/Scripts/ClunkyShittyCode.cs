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
                return "";
            }
            else if (upst == UploadStatus.GenerateLocal)
            {
                return "";
            }
            else if (upst == UploadStatus.ReadLocal)
            {
                return "";
            }
            else if (upst == UploadStatus.UploadLocal)
            {
                return "";
            }
            else if (upst == UploadStatus.Complete)
            {
                return "";
            }
            return "error";
        }
    }
}
