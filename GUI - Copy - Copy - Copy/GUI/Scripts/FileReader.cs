using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace GUI.Scripts
{
    public class FileReader
    {
        public bool OpenFile()
        {
            try
            {
                
                //fil
                return true;
            }
            catch
            {
                return false;
            }
        }

        public string ReadFile()
        {
            try
            {
                return "";
            }
            catch (Exception e)
            {
                return e.ToString();
            }
        }

        public string SaveFile()
        {
            try
            {
                
                return "";
            }
            catch (Exception e)
            {
                return e.ToString();
            }
        }

        public bool CloseFile()
        {
            try
            {
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
