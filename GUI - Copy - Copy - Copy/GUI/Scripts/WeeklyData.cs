using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUI.Scripts
{
    public class WeeklyData
    {
        public int FarmID { get; set; }
        public DateTime Sdate { get; set; }
        public string Data { get; set; }

        public WeeklyData(int farmid, DateTime sdate, string data)
        {
            this.FarmID = farmid;
            this.Sdate = sdate;
            this.Data = data;
        }
    }

    public class Paddocks
    {
        public int FarmID { get; set; }
        public DateTime Sdate { get; set; }
        public string PaddockID { get; set; }
        public string Crop { get; set; }
        public double Size { get; set; }

        public Paddocks(int farmid, DateTime sdate, string paddock, string crop, double size)
        {
            this.FarmID = farmid;
            this.Sdate = sdate;
            this.PaddockID = paddock;
            this.Crop = crop;
            this.Size = size;
        }
    }

    public class FarmSupplements
    {
        public int FarmID { get; set; }
        public DateTime Sdate { get; set; }
        public string Cows { get; set; }
        public string Supplements { get; set; }

        public FarmSupplements(int farmid, DateTime sdate, string cows, string supplements)
        {
            this.FarmID = farmid;
            this.Sdate = sdate;
            this.Cows = cows;
            this.Supplements = supplements;
        }
    }

    public class Comments
    {

        public int FarmID { get; set; }
        public DateTime Sdate { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }

        public Comments(int farmid, DateTime sdate, string cate, string desc)
        {
            this.FarmID = farmid;
            this.Sdate = sdate;
            this.Category = cate;
            this.Description = desc;
        }
    }

    public class Labels
    {
        public int Row { get; set; }
        public string Label { get; set; }

        public Labels(int row, string label)
        {
            this.Row = row;
            this.Label = label;
        }
    }

    public class Calculations
    {
        //public int Id { get; set; }
        public int Row { get; set; }
        public string Formula { get; set; }

        public Calculations(int row, string formula)
        {
            //this.Id = id;
            this.Row = row;
            this.Formula = formula;

        }
    }
}
