using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace GUI.Scripts
{
    public static class LoginClass
    {
        public static bool AttemptLogin(string email, string password)
        {
            string tmp = DownloadData.ReceiveResponse(string.Format("http://swartkat.fossul.com/gui/finduser?email={0}&password={1}", email, password));
            tmp = tmp.Substring(1, tmp.Length - 2);
            return bool.Parse(tmp);
        }

        public static bool CheckForInternetConnection()
        {
            try
            {
                using (var client = new WebClient())
                {
                    using (var stream = client.OpenRead("http://www.google.com"))
                    {
                        return true;
                    }
                }
            }
            catch
            {
                return false;
            }
        }
    }
}
