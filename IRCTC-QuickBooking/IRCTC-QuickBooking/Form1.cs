using System;
using System.Collections.Generic;
using System.Net;
using System.Windows.Forms;
using QuickBookingLogic;
using System.Threading.Tasks;
using System.Threading;
//using Selenium;
//using Selenium.Internal;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;


namespace IRCTC_QuickBooking
{
    public partial class Form1 : Form
    {
        TimeSpan waitTimeSpan = new TimeSpan();
        FirefoxDriver Browser;
        WebDriverWait Waiter;
        public Form1()
        {
            GetStationCodes.PopulateStation();
            InitializeComponent();
            doj.MinDate = DateTime.Today;
            doj.MaxDate = doj.MinDate.AddDays(90);
            doj.Value = DateTime.Today;
            //avlTrains.Text = "Select Train";
            waitTimeSpan = TimeSpan.FromMilliseconds(50);
            
        }



        private void button1_Click(object sender, EventArgs e)
        {
            //string postDatas = string.Format("stn1={0}&stn2={1}", frmStn.Text.Substring(frmStn.Text.IndexOf('-') + 1), toStn.Text.Substring(toStn.Text.IndexOf('-') + 1));
            ////string[] resultSet = TrainLists.GetTrains(req, postData);
            //TrainsForTheDay getListOfTrains = new TrainsForTheDay(new TrainLists() { postData = postDatas }, doj.Value.DayOfWeek.ToString());
            //List<string> listOfTrains = getListOfTrains.trainLists();
            //avlTrains.Enabled = true;
            //foreach (var train in listOfTrains)
            //{
            //    string[] traindet = train.Split(',');
            //    avlTrains.Items.Add(traindet[0] + "-" + traindet[1] + "-" + traindet[2]);
            //}
            if (Browser == null)
            {
                Browser = new FirefoxDriver();
                Waiter = new WebDriverWait(Browser, TimeSpan.FromMinutes(5));
            }
            Browser.Url = "https://www.irctc.co.in/eticketing/loginHome.jsf";
            Browser.FindElementById("usernameId").SendKeys("Bibek009");
            Browser.FindElementByName("j_password").SendKeys("Bgs556");
            Waiter.Until(ExpectedConditions.ElementExists(By.Name("jpform:fromStation")));
            Browser.FindElementByName("jpform:fromStation").SendKeys("ASANSOL JN - ASN");
            Browser.FindElementById("jpform:toStation").SendKeys("PATNA JN - PNBE");
            Browser.FindElementById("jpform:journeyDateInputDate").SendKeys(doj.Value.ToString("dd-MM-yyyy"));
            Browser.FindElementById("jpform:jpsubmit").Click();
            Browser.FindElementByXPath("//td/a[contains(text(),'12333')]/ancestor::tr/td[16]/a[contains(text(),'SL')]").Click();

        }
        object obj = new object();
        private void frmStn_TextChanged(object sender, EventArgs e)
        {
            try
            {
                AutoCompleteStringCollection allowedTypes = new AutoCompleteStringCollection();
                List<string> res = new List<string>();
                //frmStn.AutoCompleteCustomSource = null;
                Thread.Sleep(waitTimeSpan);
                lock (obj)
                {
                    res.Clear();
                    allowedTypes.Clear();
                    foreach (string stn in StationCodes.Stations)
                    {
                        if (stn.Contains(frmStn.Text.ToUpper()))
                            res.Add(stn);
                    }
                }
                lock (obj)
                {
                    allowedTypes.AddRange(res.ToArray());
                }
                frmStn.AutoCompleteCustomSource = allowedTypes;
                frmStn.AutoCompleteMode = AutoCompleteMode.Suggest;
                frmStn.AutoCompleteSource = AutoCompleteSource.CustomSource;
            }
            catch (Exception ex2)
            {
                
                
            }
        }

        private void toStn_TextChanged(object sender, EventArgs e)
        {
            try
            {
                AutoCompleteStringCollection allowedTypes = new AutoCompleteStringCollection();
                List<string> res = new List<string>();
                //toStn.AutoCompleteCustomSource = null;
                Thread.Sleep(waitTimeSpan);
                lock (obj)
                {
                    foreach (string stn in StationCodes.Stations)
                    {
                        if (stn.Contains(toStn.Text.ToUpper()))
                            res.Add(stn);
                    }
                }
                lock (obj)
                {
                    allowedTypes.AddRange(res.ToArray());
                }
                toStn.AutoCompleteCustomSource = allowedTypes;
                toStn.AutoCompleteMode = AutoCompleteMode.Suggest;
                toStn.AutoCompleteSource = AutoCompleteSource.CustomSource;
            }
            catch(Exception exc)
            {

            }

        }
    }
}
