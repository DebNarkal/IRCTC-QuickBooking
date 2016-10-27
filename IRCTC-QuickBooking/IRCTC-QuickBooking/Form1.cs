using System;
using System.Collections.Generic;
using System.Net;
using System.Windows.Forms;
using QuickBookingLogic;

namespace IRCTC_QuickBooking
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            GetStationCodes.PopulateStation();
            //comboBox1.DataSource = StationCodes.Stations;
            doj.MinDate = DateTime.Today;
            doj.MaxDate = doj.MinDate.AddDays(90);
        }



        private void button1_Click(object sender, EventArgs e)
        {
            string uri = "http://etrain.info/ajax.php?q=trains&v=2.8.2";
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(uri);
            req.Method = "POST";
            req.Accept = "application/json";
            req.ContentType = "application/x-www-form-urlencoded; charset=UTF-8";
            req.KeepAlive = false;
            req.Timeout = System.Threading.Timeout.Infinite;

            string postData = string.Format("stn1={0}&stn2={1}", frmStn.Text.Substring(frmStn.Text.IndexOf('-') + 1), toStn.Text.Substring(toStn.Text.IndexOf('-') + 1));
            string[] resultSet = TrainLists.GetTrains(req, postData);


        }

        private void frmStn_TextChanged(object sender, EventArgs e)
        {
            AutoCompleteStringCollection allowedTypes = new AutoCompleteStringCollection();
            List<string> res = new List<string>();
            res.Clear();
            foreach (string stn in StationCodes.Stations)
            {
                if (stn.Contains(frmStn.Text.ToUpper()))
                    res.Add(stn);
            }
            allowedTypes.AddRange(res.ToArray());
            frmStn.AutoCompleteCustomSource = allowedTypes;
            frmStn.AutoCompleteMode = AutoCompleteMode.Suggest;
            frmStn.AutoCompleteSource = AutoCompleteSource.CustomSource;
        }

        private void toStn_TextChanged(object sender, EventArgs e)
        {
            AutoCompleteStringCollection allowedTypes = new AutoCompleteStringCollection();
            List<string> res = new List<string>();
            foreach (string stn in StationCodes.Stations)
            {
                if (stn.Contains(toStn.Text.ToUpper()))
                    res.Add(stn);
            }
            allowedTypes.AddRange(res.ToArray());
            toStn.AutoCompleteCustomSource = allowedTypes;
            toStn.AutoCompleteMode = AutoCompleteMode.Suggest;
            toStn.AutoCompleteSource = AutoCompleteSource.CustomSource;
        }
    }
}
