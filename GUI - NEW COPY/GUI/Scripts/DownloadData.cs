using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Threading.Tasks;
using Newtonsoft.Json;

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
            string tmp = DownloadData.ReceiveResponse(string.Format("http://swartkat.fossul.com/gui/returnuserfarm?email={0}", email));
            tmp = tmp.Substring(1, tmp.Length - 2);
            tmp = tmp.Replace("\"", "");
            string[] tmp2 = tmp.Split(',');
            Farm f = new Farm();
            f.farmid = int.Parse(tmp2[0]);
            f.farmname = tmp2[1];
            f.area = double.Parse(tmp2[2]);
            f.role = int.Parse(tmp2[3]);
            return f;
        }

        public static Dictionary<int, string> GetWeeklyFarmData(string date)
        {
            string tmp = DownloadData.ReceiveResponse(string.Format("http://swartkat.fossul.com/gui/getdata?fulldate={0}", date));
            //return JsonConvert.DeserializeObject<List<FullFarm>>(tmp);
            Dictionary<int, string> dict = JsonConvert.DeserializeObject<Dictionary<int, string>>(tmp);
            return dict;
        }

    }
}
