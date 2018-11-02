using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;


namespace ContentIsKing.MasterK
{
    public static class GetDataFromUrl
    {
        /*Lay ma html tu url*/
        public static string getHTML(string url)
        {
            HttpWebRequest myRequest = (HttpWebRequest)WebRequest.Create(url);
            myRequest.Method = "GET";
            myRequest.UserAgent = "Mozilla/5.0 (Windows NT 6.2; rv:63.0) Gecko/20100101 Firefox/63.0";

            WebResponse myResponse = myRequest.GetResponse();
            System.IO.StreamReader sr = new System.IO.StreamReader(myResponse.GetResponseStream(), System.Text.Encoding.UTF8);
            string result = sr.ReadToEnd();
            sr.Close();
            myResponse.Close();

            return result;
        }

        /*Lay noi dung va hinh anh POSTs tu HTML*/
        public static string[] getPostFromHtml(string html)
        {
           var content = System.Text.RegularExpressions.Regex.Match(html, "text\\%22\\%3A\\%22(.+)\\%22\\%2C\\%22ranges").Groups[1].Value;
            string[] result = new string[] { };
            return result;
        }
    }
}
