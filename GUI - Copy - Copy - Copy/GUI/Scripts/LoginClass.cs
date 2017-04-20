using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using System.Threading.Tasks;

namespace GUI.Scripts
{
    public static class LoginClass
    {
        public static string IsAbleToAutologin(string username, string password)
        {
            try
            {
                string s = AttemptLogin(username, password);
                if (s == "")
                    return "";
                else
                    return s;
            }
            catch (Exception e)
            {
                return e.ToString();
            }
        }

        private static string AttemptLogin(string username, string password)
        {
            if (CheckForInternetConnection())
            {
                string s = CompareDetails(username, password);
                if (s == "")
                    return "";
                else
                    return s;
            }
            else
                return "Your device cannot connect to the internet";
        }

        private static string CompareDetails(string username, string password)
        {
            string user = "Kounga", pass = "Kounga123";
            if (username == user)
                if (password == pass)
                    return "";
                else
                    return "Incorrect password";
            else
                return "Username not found";
        }

        private static bool CheckForInternetConnection()
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
