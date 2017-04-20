using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUI.Scripts
{
    public class WeeklyData
    {
        public int Id { get; set; }
        public int FarmID { get; set; }
        public DateTime Sdate { get; set; }
        public string Data { get; set; }

        public WeeklyData(int id, int farmid, DateTime sdate, string data)
        {
            this.Id = id;
            this.FarmID = farmid;
            this.Sdate = sdate;
            this.Data = data;
        }
    }
}
