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

        public static void CreateUser(User user)
        {
            UploadGet(string.Format("http://swartkat.fossul.com/gui/createuser?username={0}&email={1}&password={2}",user.name,user.email,user.password));
        }

        public static void UpdateUser(string email, User user)
        {
            UploadGet(string.Format("http://swartkat.fossul.com/gui/modifyuser?email={0}&newname={1}&newemail={2}&newpassword={3}", email, user.name, user.email, user.password));
        }

        public static void UpdateUser(string email, User user, bool updatePassword)
        {
            if (updatePassword)
                UploadGet(string.Format("http://swartkat.fossul.com/gui/modifyuser?email={0}&newname={1}&newemail={2}&newpassword={3}", email, user.name, user.email, user.password));
            else
                UploadGet(string.Format("http://swartkat.fossul.com/gui/modifyuser?email={0}&newname={1}&newemail={2}", email, user.name, user.email));
        }

        public static void CreateFarm(Farm farm)
        {
            UploadGet(string.Format("http://swartkat.fossul.com/gui/createmodfarm?id={0}&name={1}&area={2}", farm.farmname, farm.farmname, farm.area));
        }

        public static void UpdateFarm(Farm farm)
        {
            UploadGet(string.Format("http://swartkat.fossul.com/gui/createmodfarm?id={0}&name={1}&area={2}", farm.farmid,farm.farmname,farm.area));
        }

        public static void UpdatePermissions(string email, int permission)
        {
            UploadGet(string.Format("http://swartkat.fossul.com/gui/assignrole?email={0}&role={1}", email, (int)permission));
        }

        public static void AssignFarm(int farmid, string email)
        {
            UploadGet(string.Format("http://swartkat.fossul.com/gui/assignfarm?farmid={0}&email={1}", farmid, email));
        }

        public static void UploadDataGet(WeeklyData wdata)
        {
            string url = string.Format("http://swartkat.fossul.com/data/insertdata?farmid={0}&sdate={1}&data={2}", wdata.FarmID, wdata.Sdate.ToString("yyyy-MM-dd"), wdata.Data);
            UploadGet(url);
        }

        public static void UploadPaddocksGet(Paddocks padd)
        {
            string url = string.Format("http://swartkat.fossul.com/data/insertlabels?farmid={0}&sdate={1}&paddockid={2}&crop={3}&size={4}", padd.FarmID, padd.Sdate.ToString("yyyy-MM-dd"), padd.PaddockID, padd.Crop, padd.Size);
            UploadGet(url);
        }

        public static void UploadFarmSuppGet(FarmSupplements sup)
        {
            string url = string.Format("http://swartkat.fossul.com/data/insertfarmsupplements?farmid={0}&sdate={1}&cows={2}&supplements={3}", sup.FarmID, sup.Sdate.ToString("yyyy-MM-dd"), sup.Cows, sup.Supplements);
            UploadGet(url);
        }

        public static void UploadCommentsGet(Comments com)
        {
            string url = string.Format("http://swartkat.fossul.com/data/insertcomments?farmid={0}&sdate={1}&category={2}&description={3}", com.FarmID, com.Sdate.ToString("yyyy-MM-dd"), com.Category, com.Description);
            UploadGet(url);
        }

        public static void UploadLabelsGet(Labels label)
        {
            string url = string.Format("http://swartkat.fossul.com/data/insertlabels?row={0}&label={1}", label.Row, label.Label);
            UploadGet(url);
        }

        public static void UploadCalculationGet(Calculations calc)
        {
            string url = string.Format("http://swartkat.fossul.com/data/insertcalc?row={0}&formula={1}", calc.Row, calc.Formula);
            UploadGet(url);
        }
    }
}
