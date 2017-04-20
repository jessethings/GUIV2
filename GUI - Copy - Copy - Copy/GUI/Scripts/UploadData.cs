using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace GUI.Scripts
{
    class UploadData
    {
        public static void UploadGet(string injectedUrl)
        {
            //http://swartkat.fossul.com/data/insertdata?farmid=42
            string url = injectedUrl;
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(url);
            request.Method = "GET";
            String test = String.Empty;
            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            {
                Stream dataStream = response.GetResponseStream();
                StreamReader reader = new StreamReader(dataStream);
                test = reader.ReadToEnd();
                reader.Close();
                dataStream.Close();
            }
        }

        public static void UploadDataGet(WeeklyData wdata)
        {
            string url = string.Format("http://swartkat.fossul.com/data/insertdata?farmid={0}&sdate={1}&data={2}", wdata.FarmID, wdata.Sdate.ToString("yyyy-MM-dd"), wdata.Data);
            UploadGet(url);
        }

        public static void UploadPaddocksGet(Paddocks padd)
        {
            string url = string.Format("http://swartkat.fossul.com/data/insertdata?farmid={0}&sdate={1}&data={2}", wdata.FarmID, wdata.Sdate.ToString("yyyy-MM-dd"), wdata.Data);
            UploadGet(url);
        }

        public static void UploadFarmSuppGet(FarmSupplements sup)
        {
            string url = string.Format("http://swartkat.fossul.com/data/insertdata?farmid={0}&sdate={1}&data={2}", wdata.FarmID, wdata.Sdate.ToString("yyyy-MM-dd"), wdata.Data);
            UploadGet(url);
        }

        public static void UploadCommentsGet(Comments com)
        {
            string url = string.Format("http://swartkat.fossul.com/data/insertdata?farmid={0}&sdate={1}&data={2}", wdata.FarmID, wdata.Sdate.ToString("yyyy-MM-dd"), wdata.Data);
            UploadGet(url);
        }

        public static void UploadLabelsGet(Labels label)
        {
            string url = string.Format("http://swartkat.fossul.com/data/insertdata?farmid={0}&sdate={1}&data={2}", wdata.FarmID, wdata.Sdate.ToString("yyyy-MM-dd"), wdata.Data);
            UploadGet(url);
        }

        public static void UploadCalculationGet(Calculations calc)
        {
            string url = string.Format("http://swartkat.fossul.com/data/insertdata?farmid={0}&sdate={1}&data={2}", wdata.FarmID, wdata.Sdate.ToString("yyyy-MM-dd"), wdata.Data);
            UploadGet(url);
        }
    }
}
