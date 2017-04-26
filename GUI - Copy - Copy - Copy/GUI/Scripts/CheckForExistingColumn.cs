using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUI.Scripts
{
    public static class CheckForExistingColumn
    {
        //done instead via website
        /*db variables
        static string _server;
        static string _db;
        static string _uid;
        static string _pw;
        static MySqlConnection connection;


        private static void establishConnection()
        {
            _server = "";
            _db = "swartdb";
            _uid = "swartdba";
            _pw = "Heise@2017";
            string constring = $"SERVER={_server};DATABASE={_db};UID={_uid};PASSWORD={_pw};";
            connection = new MySqlConnection(constring);
        }

        private static bool openConnection()
        {
            try
            {
                connection.Open();
                return true;
            }
            catch(MySqlException e)
            {
                Debug.WriteLine(e.ToString());
                return false;
            }
        }

        private static bool closeConnection()
        {
            try
            {
                connection.Close();
                return true;
            }
            catch (MySqlException e)
            {
                Debug.WriteLine(e.ToString());
                return false;
            }
        }

        public static  bool checkForExistingColumnDate(string table, string date, int farmID)
        {
            string sql = $"select date FROM {table} where sdate = '{date}' AND farmid = '{farmID}'";
            establishConnection();
            if (openConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(sql, connection);
                //look in table for existence of a column that matches the date and farm id
                if (cmd.ExecuteScalar() != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            //cannot confirm or deny contents of table so return true
            return true;
        }

        public static bool checkForExistingLabelOrCalc(string table, string rowid)
        {
            string sql = $"select date FROM {table} where row = '{rowid}'";
            establishConnection();
            if (openConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(sql, connection);
                //look in table for existence of a column that matches the row
                if (cmd.ExecuteScalar() != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            //cannot confirm or deny contents of table so return true
            return true;
            */
        }
    }
}
