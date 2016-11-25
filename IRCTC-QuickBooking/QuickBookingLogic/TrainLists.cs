using HtmlAgilityPack;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace QuickBookingLogic
{
    public class TrainLists:IListOfTrains
    {
        //void GetTrains(HttpWebRequest req, string postData)
        //{
        //    string result;
        //    byte[] credentials = Encoding.UTF8.GetBytes(postData);
        //    req.ContentLength = credentials.Length;
        //    using (Stream stWrite = req.GetRequestStream())
        //    {
        //        stWrite.Write(credentials, 0, credentials.Length);
        //    }
        //    HttpWebResponse resp = (HttpWebResponse)req.GetResponse();
        //    using (Stream read = resp.GetResponseStream())
        //    {
        //        using (StreamReader reading = new StreamReader(read))
        //        {
        //            result = reading.ReadToEnd();
        //        }
        //    }
        //    JObject jsondata = (JObject)JsonConvert.DeserializeObject(result);
        //    string trainInfo = jsondata["data"].ToString();
        //    string[] trains = trainInfo.Split(';');
        //}
        public List<string> trains { get; set; }
        public string postData { get; set; }
        public void GetTrains()
        {            
            //string uri = "http://etrain.info/ajax.php?q=trains&v=2.8.2";
            //HttpWebRequest req = (HttpWebRequest)WebRequest.Create(uri);
            //req.Method = "POST";
            //req.Accept = "application/json";
            //req.ContentType = "application/x-www-form-urlencoded; charset=UTF-8";
            //req.KeepAlive = false;
            //req.Timeout = System.Threading.Timeout.Infinite;
            //byte[] credentials = Encoding.UTF8.GetBytes(postData);
            //req.ContentLength = credentials.Length;
            //using (Stream stWrite = req.GetRequestStream())
            //{
            //    stWrite.Write(credentials, 0, credentials.Length);
            //}
            //HttpWebResponse resp = (HttpWebResponse)req.GetResponse();
            //using (Stream read = resp.GetResponseStream())
            //{
            //    using (StreamReader reading = new StreamReader(read))
            //    {
            //        result = reading.ReadToEnd();
            //    }
            //}
            //JObject jsondata = (JObject)JsonConvert.DeserializeObject(result);
            //string trainInfo = jsondata["data"].ToString();
            //trains = trainInfo.Split(';');
            string result;
            HtmlDocument doc = new HtmlDocument();
            string uri = "http://railwayapi.com/getTrainsBetween_old.php";
            trains = new List<string>();
            //postData = "pnrQ=HWH&dest=PNBE";
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(uri);
            req.Method = "POST";
            req.Accept = "application/json";
            req.ContentType = "application/x-www-form-urlencoded; charset=UTF-8";
            req.KeepAlive = false;
            req.Timeout = System.Threading.Timeout.Infinite;
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
            doc.LoadHtml(result);
            var node = doc.DocumentNode.SelectSingleNode("(//table//tbody)[3]");
            var nodes = node.ChildNodes;
            foreach (var nd in nodes)
            {
                if (nd.Name == "tr")
                {
                    trains.Add(nd.InnerText.Trim().Replace("\t", ""));
                }
            }
        }
    }
}
