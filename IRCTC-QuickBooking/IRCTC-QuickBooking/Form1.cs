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
        List<string> journeyClass = new List<string>();

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
                //this.dataGridViewPassengerDetails.Rows[i-1].Cells[4].Value = "No Preference";
            }
            allowedTypes.AddRange(StationCodes.Stations);
            frmStn.AutoCompleteCustomSource = allowedTypes;
            frmStn.AutoCompleteSource = AutoCompleteSource.CustomSource;
            frmStn.AutoCompleteMode = AutoCompleteMode.Suggest;
            toStn.AutoCompleteCustomSource = allowedTypes;
            toStn.AutoCompleteSource = AutoCompleteSource.CustomSource;
            toStn.AutoCompleteMode = AutoCompleteMode.Suggest;
            comboBoxJourneyClass.Items.AddRange(new string[] { "1A", "2A", "3A", "CC", "SL", "2S" });
        }



        private void buttonTrainList_Click(object sender, EventArgs e)
        {
            //string postDatas = string.Format("stn1={0}&stn2={1}", frmStn.Text.Substring(frmStn.Text.IndexOf('-') + 1), toStn.Text.Substring(toStn.Text.IndexOf('-') + 1));
            if (frmStn.Text == "" || toStn.Text == "")
            {
                MessageBox.Show("Please select start and destination");
                return;
            }
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
            if (textBoxPassword.Text == "" || textBoxUserName.Text == "" || frmStn.Text == "" || toStn.Text == "")
            {
                MessageBox.Show("Please fill the necessary details");
                return;
            }
            if (dataGridViewPassengerDetails.Rows[0].Cells[1].Value == null || string.IsNullOrWhiteSpace(dataGridViewPassengerDetails.Rows[0].Cells[1].Value.ToString()))
            {
                MessageBox.Show("Please enter aleast one passenger detail");
                return;
            }
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
            string travelQuota = "";
            if (radioButtonGen.Checked)
            {
                travelQuota = radioButtonGen.Text;
            }
            else if (radioButtonLadies.Checked)
            {
                travelQuota = radioButtonLadies.Text;
            }
            else if (radioButtonPhyHand.Checked)
            {
                travelQuota = radioButtonPhyHand.Text;
            }
            else if (radioButtonPremTat.Checked)
            {
                travelQuota = radioButtonPremTat.Text;
            }
            else if (radioButtonTatkal.Checked)
            {
                travelQuota = radioButtonTatkal.Text;
            }
            else
            {
                travelQuota = "GENERAL";
            }
            string quota = getBookNowText(travelQuota);
            Browser.FindElementByXPath(String.Format("//td[text()='{0}']", travelQuota)).FindElement(By.TagName("input")).Click();
            string trainNumber = textBoxTrains.Text.Trim().Substring(0, 5);
            string journeyClass = comboBoxJourneyClass.Text;
            Waiter.Until(ExpectedConditions.ElementExists(By.XPath(string.Format("//td/a[contains(text(),'{0}')]/ancestor::tr/td[16]/a[contains(text(),'{1}')]", trainNumber, journeyClass))));
            Browser.FindElementByXPath(string.Format("//td/a[contains(text(),'{0}')]/ancestor::tr/td[16]/a[contains(text(),'{1}')]", trainNumber, journeyClass)).Click();
            Waiter.Until(ExpectedConditions.ElementExists(By.LinkText("Book Now")));
            var bookNowElement = Browser.FindElementByLinkText("Book Now");
            if (bookNowElement.GetAttribute("id").Substring(12) == "0")
            {
                bookNowElement.Click();
            }



            try
            {
                Browser.SwitchTo().Alert().Accept();
            }
            catch(NoAlertPresentException ex)
            {
                
            }
            var passengerDetailNames = Browser.FindElementsByClassName("psgn-name");
            var passengerDetailAges = Browser.FindElementsByClassName("psgn-age");
            var passengerDetailGender = Browser.FindElementsByClassName("psgn-gender");
            var passengerBerthChoice = Browser.FindElementsByClassName("psgn-berth-choice");
            var passengerSeniorCitizen = Browser.FindElementsByClassName("psgn-concopt");
            Waiter.Until(ExpectedConditions.ElementExists(By.ClassName("psgn-name")));
            if (textBoxBoardingPoint.Visible)
            {
                Browser.FindElementById("addPassengerForm:boardingStation").SendKeys(textBoxBoardingPoint.Text.Trim());
            }
            for (int i = 0; i < 6; i++)
            {
                if (dataGridViewPassengerDetails.Rows[i].Cells[1].Value == null || string.IsNullOrWhiteSpace(dataGridViewPassengerDetails.Rows[i].Cells[1].Value.ToString()))
                {
                    break;
                }
                passengerDetailNames[i].SendKeys(dataGridViewPassengerDetails.Rows[i].Cells[1].Value.ToString());
                passengerDetailAges[i].SendKeys(dataGridViewPassengerDetails.Rows[i].Cells[2].Value.ToString());
                passengerDetailGender[i].SendKeys(dataGridViewPassengerDetails.Rows[i].Cells[3].Value.ToString());
                passengerBerthChoice[i].SendKeys(dataGridViewPassengerDetails.Rows[i].Cells[4].Value.ToString());
                if (Convert.ToBoolean(dataGridViewPassengerDetails.Rows[i].Cells[5].Value))
                {
                    passengerSeniorCitizen[i].Click();
                }
            }
            //var dropDownBerthChoice = new SelectElement(passengerBerthChoice[0]);
            //dropDownBerthChoice.SelectByText("SIDE LOWER");
            //passengerDetailNames[0].SendKeys("D Mondal");
        }

        private string getBookNowText(string travelQuota)
        {
            switch (travelQuota)
            {
                case "GENERAL":
                    return "GN";
                case "PREMIUM TATKAL":
                    return "PT";
                case "PHYSICALLY HANDICAP":
                    return "HP";
                case "TATKAL":
                    return "CK";
                case "LADIES":
                    return "LD";
            }
            return
                "GN";
        }

        private void label8_Click(object sender, EventArgs e)
        {
            label8.Visible = false;
            label9.Visible = true;
            textBoxBoardingPoint.Visible = true;
            textBoxBoardingPoint.AutoCompleteCustomSource = allowedTypes;
            textBoxBoardingPoint.AutoCompleteSource = AutoCompleteSource.CustomSource;
            textBoxBoardingPoint.AutoCompleteMode = AutoCompleteMode.Suggest;
            textBoxBoardingPoint.Text = frmStn.Text;
        }

        private void comboBoxJourneyClass_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataGridViewComboBoxCell dgvcc;
            List<string> popCell = new List<string>();

            if (comboBoxJourneyClass.Text == "CC" || comboBoxJourneyClass.Text == "2S")
            {
                popCell.AddRange(new string[] { "No Preference", "WINDOW SIDE" });
            }
            else
            {
                popCell.AddRange(new string[] { "No Preference", "LOWER", "MIDDLE", "UPPER", "SIDE LOWER", "SIDE UPPER" });
            }
            //dataGridViewPassengerDetails.Rows[0].Cells[4] = dgvcc;
            //dataGridViewPassengerDetails.Rows[1].Cells[4] = dgvcc;
            for (int i = 0; i < 6; ++i)
            {
                dgvcc = new DataGridViewComboBoxCell();
                dgvcc.Items.AddRange(popCell.ToArray());
                dataGridViewPassengerDetails.Rows[i].Cells[4] = dgvcc;
                dataGridViewPassengerDetails.Rows[i].Cells[4].Value = "No Preference";
                dataGridViewPassengerDetails.Rows[i].Cells[5].ToolTipText = "Please carry a valid ID proof for senior citizen";
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
