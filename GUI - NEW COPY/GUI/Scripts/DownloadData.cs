using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Web.Script.Serialization;

namespace GUI.Scripts
{
    public static class DownloadData
    {
        public static string ReceiveResponse(string url)
        {
            WebClient wc = new WebClient();
            byte[] raw = wc.DownloadData(url);
            return Encoding.UTF8.GetString(raw);
        }

        public static List<User> GetAllUsers()
        {
            string tmp = DownloadData.ReceiveResponse("http://swartkat.fossul.com/gui/getusers");
            return JsonConvert.DeserializeObject<List<User>>(tmp);
        }

        public static List<FullFarm> GetAllFullFarms()
        {
            string tmp = DownloadData.ReceiveResponse("http://swartkat.fossul.com/gui/getfarms");
            return JsonConvert.DeserializeObject<List<FullFarm>>(tmp);
        }

        public static Farm GetUserFarm(string email)
        {
            string tmp = DownloadData.ReceiveResponse(string.Format("http://swartkat.fossul.com/gui/getusersfarm?email={0}", email));
            tmp = tmp.Substring(1, tmp.Length - 2);
            tmp = tmp.Replace("\"", "");
            string[] tmp2 = tmp.Split(',');
            Farm f = new Farm();
            f.farmid = int.Parse(tmp2[0]);
            f.farmname = tmp2[1];
            f.area = double.Parse(tmp2[2]);
            return f;
        }

        public static Dictionary<int, string> GetWeeklyFarmData(string date)
        {
            JavaScriptSerializer ser = new JavaScriptSerializer();
            var JSONObj = ser.Deserialize<Dictionary<string, string>[]>(ReceiveResponse(string.Format("http://swartkat.fossul.com/gui/getdata?fulldate={0}", date))); //JSON decoded
            Dictionary<int, string> tmp = new Dictionary<int, string>();
            foreach (Dictionary<string, string> map in JSONObj)
            {
                int id = int.Parse(map["farmid"]);
                string data = map["data"];
                tmp.Add(id, data);
            }
            return tmp;
        }

        public static PermissionLevel GetUserRole(string userEmail)
        {
            string tmp = ReceiveResponse(string.Format("http://swartkat.fossul.com/gui/getuserrole?email={0}", userEmail));
            return (PermissionLevel)(int.Parse(tmp));
        }

        public static List<tmpdate> GetWeeklyDataDates(int userID)
        {
            try
            {
                string tmp = DownloadData.ReceiveResponse(string.Format("http://swartkat.fossul.com/gui/getfarmdates?userid={0}", userID));
                return JsonConvert.DeserializeObject<List<tmpdate>>(tmp);
            }
            catch
            {
                return null;
            }

            /*JavaScriptSerializer ser = new JavaScriptSerializer();
            //return ser.Deserialize<List<string>>(ReceiveResponse(string.Format("http://swartkat.fossul.com/gui/getfarmdates?userid={0}", userID))); //JSON decoded
            return JsonConvert.DeserializeObject<List<DateTime>>(string.Format("http://swartkat.fossul.com/gui/getfarmdates?userid={0}", userID));*/
        }
    }
}
