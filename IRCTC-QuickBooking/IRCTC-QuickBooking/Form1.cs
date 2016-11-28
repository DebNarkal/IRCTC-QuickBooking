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
        AutoCompleteStringCollection allowedTypes = new AutoCompleteStringCollection();

        public Form1()
        {
            GetStationCodes.PopulateStation();
            InitializeComponent();
            doj.MinDate = DateTime.Today;
            doj.MaxDate = doj.MinDate.AddDays(90);
            doj.Value = DateTime.Today;
            waitTimeSpan = TimeSpan.FromMilliseconds(50);
            for (int i = 1; i < 7; i++)
            {
                this.dataGridViewPassengerDetails.Rows.Add(i.ToString());
            }
            allowedTypes.AddRange(StationCodes.Stations);
            frmStn.AutoCompleteCustomSource = allowedTypes;
            frmStn.AutoCompleteSource = AutoCompleteSource.CustomSource;
            frmStn.AutoCompleteMode = AutoCompleteMode.Suggest;
            toStn.AutoCompleteCustomSource = allowedTypes;
            toStn.AutoCompleteSource = AutoCompleteSource.CustomSource;
            toStn.AutoCompleteMode = AutoCompleteMode.Suggest;
        }



        private void buttonTrainList_Click(object sender, EventArgs e)
        {
            //string postDatas = string.Format("stn1={0}&stn2={1}", frmStn.Text.Substring(frmStn.Text.IndexOf('-') + 1), toStn.Text.Substring(toStn.Text.IndexOf('-') + 1));
            string postDatas = string.Format("pnrQ={0}&dest={1}", frmStn.Text.Substring(frmStn.Text.IndexOf('-') + 1), toStn.Text.Substring(toStn.Text.IndexOf('-') + 1));
            List<string> listOfTrains = new List<string>();
            pleaseWait1.Visible = true;
            buttonTrainList.Enabled = false;
            Task trainFillup = new Task(() =>
                {
                    TrainsForTheDay getListOfTrains = new TrainsForTheDay(new TrainLists() { postData = postDatas }, doj.Value.DayOfWeek.ToString());

                    listOfTrains = getListOfTrains.trainLists();
                }
            );
            trainFillup.ContinueWith((antecedent) =>
            {
                AutoCompleteStringCollection trainHints = new AutoCompleteStringCollection();
                trainHints.AddRange(listOfTrains.ToArray());
                textBoxTrains.Clear();

                textBoxTrains.AutoCompleteCustomSource = trainHints;
                textBoxTrains.AutoCompleteSource = AutoCompleteSource.CustomSource;
                textBoxTrains.AutoCompleteMode = AutoCompleteMode.Suggest;
                pleaseWait1.Visible = false;
                buttonTrainList.Enabled = true;
            }, TaskScheduler.FromCurrentSynchronizationContext()
            );
            trainFillup.Start();

        }

        private void buttonBookNow_Click(object sender, EventArgs e)
        {
            if (Browser == null)
            {
                Browser = new FirefoxDriver();
                Waiter = new WebDriverWait(Browser, TimeSpan.FromMinutes(5));
            }
            Browser.Url = "https://www.irctc.co.in/eticketing/loginHome.jsf";
            Browser.FindElementById("usernameId").SendKeys(textBoxUserName.Text.Trim());
            Browser.FindElementByName("j_password").SendKeys(textBoxPassword.Text.Trim());
            Waiter.Until(ExpectedConditions.ElementExists(By.Name("jpform:fromStation")));
            Browser.FindElementByName("jpform:fromStation").SendKeys(frmStn.Text.Trim());
            Browser.FindElementById("jpform:toStation").SendKeys(toStn.Text.Trim());
            Browser.FindElementById("jpform:journeyDateInputDate").SendKeys(doj.Value.ToString("dd-MM-yyyy"));
            Browser.FindElementById("jpform:jpsubmit").Click();
            Waiter.Until(ExpectedConditions.ElementExists(By.Name("quota")));
            Browser.FindElementByXPath("//td[text()='TATKAL']").FindElement(By.TagName("input")).Click();
            Waiter.Until(ExpectedConditions.ElementExists(By.XPath("//td/a[contains(text(),'12333')]/ancestor::tr/td[16]/a[contains(text(),'SL')]")));
            Browser.FindElementByXPath("//td/a[contains(text(),'12333')]/ancestor::tr/td[16]/a[contains(text(),'SL')]").Click();
            Waiter.Until(ExpectedConditions.ElementExists(By.LinkText("Book Now")));
            Browser.FindElementByLinkText("Book Now").Click();
        }
    }
}
