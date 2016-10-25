using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Web;

namespace IRCTC_QuickBooking
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            GetStationCodes.PopulateStation();
            //comboBox1.DataSource = StationCodes.Stations;
            dateTimePicker1.MinDate = DateTime.Today;
        }



        private void button1_Click(object sender, EventArgs e)
        {
            string result;
            //string uri = "http://enquiry.indianrail.gov.in/ntes/NTES?action=getTrnBwStns&stn1=HWH&stn2=ASR&trainType=ALL&zccfucvv5s=1ac96bjlg0";
            string uri = "http://etrain.info/ajax.php?q=trains&v=2.8.2";

            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(uri);
            req.Method = "POST";
            req.Accept = "application/json";
            req.ContentType = "application/x-www-form-urlencoded; charset=UTF-8";
            req.KeepAlive = false;
            req.Timeout = System.Threading.Timeout.Infinite;

            string postData = string.Format("stn1={0}&stn2={1}", frmStn.Text.Substring(frmStn.Text.IndexOf('-') + 1), toStn.Text.Substring(toStn.Text.IndexOf('-') + 1));
            byte[] credentials = Encoding.UTF8.GetBytes(postData);
            req.ContentLength = credentials.Length;
            using (Stream stWrite = req.GetRequestStream())
            {
                stWrite.Write(credentials, 0, credentials.Length);
            }
            HttpWebResponse resp = (HttpWebResponse)req.GetResponse();
            using (Stream read = resp.GetResponseStream())
            {
                using (StreamReader reading = new StreamReader(read))
                {
                    result = reading.ReadToEnd();
                }
            }
            JObject jsondata = (JObject)JsonConvert.DeserializeObject(result);
            string trainInfo = jsondata["data"].ToString();
            string[] trains = trainInfo.Split(';');
            //string day = dateTimePicker1.Text;
            //ComboBox cb = new ComboBox();
            //cb.Items.Add("1A");
            //((DataGridViewComboBoxColumn)dataGridView1.Columns["MyDataGridColumnName"]).DataSource = cb.Items;
        }



        private void comboBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
        }

        private void frmStn_TextChanged(object sender, EventArgs e)
        {
            AutoCompleteStringCollection allowedTypes = new AutoCompleteStringCollection();
            List<string> res = new List<string>();
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
