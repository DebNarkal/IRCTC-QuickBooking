using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace IRCTC_QuickBooking
{
    public class GetStationCodes
    {
        public static void PopulateStation()
        {
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create("https://www.irctc.co.in/eticketing/StationLinguisticNames/76v?hl=en");
            HttpWebResponse res = (HttpWebResponse)req.GetResponse();
            string response;
            using (Stream s = res.GetResponseStream())
            {
                using (StreamReader reader = new StreamReader(s))
                    response = reader.ReadToEnd();
            }
            response = response.Remove(0, response.IndexOf('[') + 1);
            response = response.Remove(response.IndexOf(']'));
            //response = response.Remove(response.IndexOf('"'), 1);
            response = response.Replace("\"", "");
            StationCodes.Stations = response.Split(',');
        }
    }
}
