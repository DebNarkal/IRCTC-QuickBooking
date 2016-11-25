using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickBookingLogic
{
    public class TrainsForTheDay
    {
        public IListOfTrains trainListss;
        public string _day;
        public TrainsForTheDay(IListOfTrains listOfTrains,string day)
        {
            trainListss = listOfTrains;
            _day = day;
        }
        public List<string> trainLists()
        {
            trainListss.GetTrains();
            List<string> trains = new List<string>();
            foreach (var trainInfo in trainListss.trains)
            {
                string[] trainDetails = trainInfo.Split('\n');
                trainDetails = trainDetails.Where(str => str.Trim() != "").ToArray();
                switch (_day)
                {
                    case "Monday":
                        if (trainDetails[6] == "Y")
                            trains.Add(trainDetails[0].Trim() + " " + trainDetails[1].Trim());
                        break;
                    case "Tuesday":
                        if (trainDetails[7] == "Y")
                            trains.Add(trainDetails[0].Trim() + " " + trainDetails[1].Trim());
                        break;
                    case "Wednesday":
                        if (trainDetails[8] == "Y")
                            trains.Add(trainDetails[0].Trim() + " " + trainDetails[1].Trim());
                        break;
                    case "Thursday":
                        if (trainDetails[9] == "Y")
                            trains.Add(trainDetails[0].Trim() + " " + trainDetails[1].Trim());
                        break;
                    case "Friday":
                        if (trainDetails[10] == "Y")
                            trains.Add(trainDetails[0].Trim() + " " + trainDetails[1].Trim());
                        break;
                    case "Saturday":
                        if (trainDetails[11] == "Y")
                            trains.Add(trainDetails[0].Trim() + " " + trainDetails[1].Trim());
                        break;
                    case "Sunday":
                        if (trainDetails[12] == "Y")
                            trains.Add(trainDetails[0].Trim() + " " + trainDetails[1].Trim());
                        break;
                }
            }
            return trains;
        }
    }
}
