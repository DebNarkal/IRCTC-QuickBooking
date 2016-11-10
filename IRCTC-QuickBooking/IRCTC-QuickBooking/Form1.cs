using System;
using System.Collections.Generic;
using System.Net;
using System.Windows.Forms;
using QuickBookingLogic;
using System.Threading.Tasks;
using System.Threading;

namespace IRCTC_QuickBooking
{
    public partial class Form1 : Form
    {
        TimeSpan waitTimeSpan = new TimeSpan();
        public Form1()
        {
            GetStationCodes.PopulateStation();
            InitializeComponent();
            doj.MinDate = DateTime.Today;
            doj.MaxDate = doj.MinDate.AddDays(90);
            doj.Value = DateTime.Today;
            avlTrains.Text = "Select Train";
            waitTimeSpan = TimeSpan.FromMilliseconds(50);
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
            List<string> listOfTrains = TrainsForTheDay.trainLists(resultSet, doj.Value.DayOfWeek.ToString());
            avlTrains.Enabled = true;
            foreach (var train in listOfTrains)
            {
                string[] traindet = train.Split(',');
                avlTrains.Items.Add(traindet[0] + "-" + traindet[1] + "-" + traindet[2]);
            }

        }
        private void frmStn_TextChanged(object sender, EventArgs e)
        {
            AutoCompleteStringCollection allowedTypes = new AutoCompleteStringCollection();
            List<string> res = new List<string>();
            res.Clear();
            allowedTypes.Clear();
            Thread.Sleep(waitTimeSpan);
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
            res.Clear();
            allowedTypes.Clear();
            Thread.Sleep(waitTimeSpan);

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
