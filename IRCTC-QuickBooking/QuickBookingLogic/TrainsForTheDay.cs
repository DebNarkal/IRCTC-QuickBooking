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
                string[] trainDetails = trainInfo.Split(',');
                switch (_day)
                {
                    case "Sunday":
                        if (trainDetails[8] == "1")
                            trains.Add(trainInfo);
                        break;
                    case "Monday":
                        if (trainDetails[9] == "1")
                            trains.Add(trainInfo);
                        break;
                    case "Tuesday":
                        if (trainDetails[10] == "1")
                            trains.Add(trainInfo);
                        break;
                    case "Wednesday":
                        if (trainDetails[11] == "1")
                            trains.Add(trainInfo);
                        break;
                    case "Thursday":
                        if (trainDetails[12] == "1")
                            trains.Add(trainInfo);
                        break;
                    case "Friday":
                        if (trainDetails[13] == "1")
                            trains.Add(trainInfo);
                        break;
                    case "Saturday":
                        if (trainDetails[14] == "1")
                            trains.Add(trainInfo);
                        break;
                }
            }
            return trains;
        }
    }
}
