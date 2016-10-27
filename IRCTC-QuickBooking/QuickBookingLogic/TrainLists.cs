using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Net;
using System.Text;

namespace QuickBookingLogic
{
    public static class TrainLists
    {
        public static string[] GetTrains(HttpWebRequest req, string postData)
        {
            string result;
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
            return trains;
        }
    }
}
