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
        private const string CSTRING = @"Data Source=" + Utilities.LOCAL_DB_URL + ";Version=3;";

        public static string ProcessFile(string url, Farm myfarm)
        {
            string retunString = "";
            //try
           // {
                if (File.Exists(Utilities.LOCAL_DB_URL))
                    File.Delete(Utilities.LOCAL_DB_URL);
                FarmIdentificationManagement fid = new FarmIdentificationManagement();
                ProcessData f = new ProcessData(url);
                f.createSQLiteDB();
                fid.CreateFarmTable();
                fid.EditTable(myfarm.farmid, myfarm.farmname, myfarm.area);
                f.CreateWorkBook();

                f.processSheet("Input Page");
                f.processSheet("Weekly Comments");
                f.processSheet("Weekly Data");
                f.processSheet("paddocks");

                //try
                //{
                //    f.processSheet("Input Page");
                //}
                //catch
                //{
                //    retunString += "An 'Input Page' page was not found." + Environment.NewLine;
                //}
                //try
                //{
                //    f.processSheet("Weekly Comments");
                //}
                //catch
                //{
                //    retunString += "A 'Comments' page was not found." + Environment.NewLine;
                //}
                //try
                //{
                //    f.processSheet("Weekly Data");
                //}
                //catch
                //{
                //    retunString += "A 'Weekly Data' page was not found." + Environment.NewLine;
                //}
                //try
                //{
                //    f.processSheet("paddocks");
                //}
                //catch
                //{ 
                //    retunString += "A 'Paddocks' page was not found." + Environment.NewLine;
                //}

                return retunString;
            /*}
            catch
            {
                return "The database is currently open, please close the application that has it open.";
            }*/
        }

        public static List<WeeklyData> WeeklyLoadData()
        {
            List<WeeklyData> list = new List<WeeklyData>();
            using (SQLiteConnection conn = new SQLiteConnection(CSTRING))
            {
                string query = "select farmid,date,data from Datas";
                conn.Open();
                SQLiteCommand cmd = new SQLiteCommand(query, conn);
                SQLiteDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    int farmid = reader.GetInt32(0);
                    DateTime sdate = reader.GetDateTime(1);
                    string data = reader.GetString(2);
                    list.Add(new WeeklyData(farmid, sdate, data));
                }
                conn.Close();
            }
            return list;
        }

        public static List<Comments> CommentsLoadData()
        {
            List<Comments> list = new List<Comments>();
            using (SQLiteConnection conn = new SQLiteConnection(CSTRING))
            {
                string query = "select farmid,sdate,category,description from Comments";
                conn.Open();
                SQLiteCommand cmd = new SQLiteCommand(query, conn);
                SQLiteDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    int farmid = reader.GetInt32(0);
                    DateTime sdate = reader.GetDateTime(1);
                    string category = reader.GetString(2);
                    string description = reader.GetString(3);
                    list.Add(new Comments(farmid, sdate, category, description));
                }
                conn.Close();
            }
            return list;
        }

        public static List<FarmSupplements> FarmSuppLoadData()
        {
            List<FarmSupplements> list = new List<FarmSupplements>();
            using (SQLiteConnection conn = new SQLiteConnection(CSTRING))
            {
                string query = "select farmid,sdate,cows,supplements from farmSupplements";
                conn.Open();
                SQLiteCommand cmd = new SQLiteCommand(query, conn);
                SQLiteDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    int farmid = reader.GetInt32(0);
                    string date = reader.GetString(1);
                    DateTime sdate = DateTime.Parse(date);
                    string cows = reader.GetString(2);
                    string supp = reader.GetString(3);
                    list.Add(new FarmSupplements(farmid, sdate, cows, supp));
                }
                conn.Close();
            }
            return list;
        }

        public static List<Calculations> CalcLoadData()
        {
            List<Calculations> list = new List<Calculations>();
            using (SQLiteConnection conn = new SQLiteConnection(CSTRING))
            {
                string query = "select row,formula from Formulae";
                conn.Open();
                SQLiteCommand cmd = new SQLiteCommand(query, conn);
                SQLiteDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    int row = reader.GetInt32(0);
                    string formula = reader.GetString(1);
                    list.Add(new Calculations(row, formula));
                }
                conn.Close();
            }
            return list;
        }

        public static List<Labels> LabelLoadData()
        {
            List<Labels> list = new List<Labels>();
            using (SQLiteConnection conn = new SQLiteConnection(CSTRING))
            {
                string query = "select id,label from Labels";
                conn.Open();
                SQLiteCommand cmd = new SQLiteCommand(query, conn);
                SQLiteDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    int row = reader.GetInt32(0);
                    string label = reader.GetString(1);
                    list.Add(new Labels(row, label));
                }
                conn.Close();
            }
            return list;
        }

        public static List<Paddocks> PaddocksLoadData()
        {
            List<Paddocks> list = new List<Paddocks>();
            using (SQLiteConnection conn = new SQLiteConnection(CSTRING))
            {
                string query = "select farmid,sdate,paddockid,crop,paddock from farmSupplements";
                conn.Open();
                SQLiteCommand cmd = new SQLiteCommand(query, conn);
                SQLiteDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    int farmid = reader.GetInt32(0);
                    DateTime sdate = reader.GetDateTime(1);
                    string paddockID = reader.GetString(2);
                    string crop = reader.GetString(3);
                    double size = reader.GetDouble(4);
                    list.Add(new Paddocks(farmid, sdate, paddockID, crop, size));
                }
                conn.Close();
            }
            return list;
        }
    }
}
