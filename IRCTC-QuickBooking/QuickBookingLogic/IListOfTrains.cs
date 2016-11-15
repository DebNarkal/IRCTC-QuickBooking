using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace QuickBookingLogic
{
    public interface IListOfTrains
    {
        string[] trains{get; set;}
        string postData { get; set; }
        void GetTrains();
    }
}
