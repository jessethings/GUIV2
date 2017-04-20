using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Microsoft.Win32;
using System.Threading;
using System.Data.SQLite;
using FortunaExcelProcessing;
using System.Runtime.InteropServices;
using System.Net;

namespace GUI.Scripts
{
    public static class ManageData
    {
        private const string CSTRING = @"Data Source=C:\Database\database.sqlite;Version=3;";

        public static void ProcessFile(string url)
        {
            if (File.Exists(@"C:\Database\database.sqlite"))
                File.Delete(@"C:\Database\database.sqlite");
            FarmIdentificationManagement fid = new FarmIdentificationManagement();
            ProcessData f = new ProcessData(url);
            //ProcessData f = new ProcessData(@"C:\Users\2015001518\Downloads\Kounga Weekly Farm Report.xlsx");
            f.createSQLiteDB();
            fid.CreateFarmTable();
            fid.EstablishData("Kounga", "11/9/2001");
            f.CreateWorkBook();
            f.processSheet("Input Page");
            f.processSheet("Weekly Comments");
            f.processSheet("Weekly Data");
            f.processSheet("paddocks");
        }

        public static List<WeeklyData> LoadData()
        {
            List<WeeklyData> list = new List<WeeklyData>();
            using (SQLiteConnection conn = new SQLiteConnection(CSTRING))
            {
                string query = "select id,farmid,date,data from Datas";
                conn.Open();
                SQLiteCommand cmd = new SQLiteCommand(query, conn);
                SQLiteDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    int id = reader.GetInt32(0);
                    int farmid = reader.GetInt32(1);
                    DateTime sdate = reader.GetDateTime(2);
                    string data = reader.GetString(3);
                    list.Add(new WeeklyData(id, farmid, sdate, data));
                }
                conn.Close();
            }
            return list;
        }

        public static void UploadDataGet(WeeklyData wdata)
        {
            //http://swartkat.fossul.com/data/insertdata?farmid=42
            string url = string.Format("http://swartkat.fossul.com/data/insertdata?farmid={0}&sdate={1}&data={2}", wdata.FarmID, wdata.Sdate.ToString("yyyy-MM-dd"), wdata.Data);
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
                //MessageBox.Show(test + "\n");
                //int result = JsonConvert.DeserializeObject<Dictionary<string, dynamic>>(test);
            }
            //DeserializeObject(test...)
        }
    }
}
