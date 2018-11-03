using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;

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
        public static List<PostContent> getPostFromHtml(string html)
        {
            List<string> post_temps = new List<string>();
            //  System.Text.RegularExpressions.MatchCollection contents = System.Text.RegularExpressions.Regex.Matches(html, @"userContent(.*?)<p>(.*?)<\/p><\/div>").Groups[2].Value;
            // MatchCollection contents = Regex.Matches(html, @"userContent(.*?)<p>(.*?)<\/p><\/div>");

            MatchCollection contents = Regex.Matches(html, @"userContentWrapper(.+)<\/div><\/div><\/div><\/div><\/div><\/div><\/div><\/form>");
            foreach (Match content in contents)
            {
                foreach (Capture capture in content.Captures)
                {
                     post_temps.Add(capture.Value);
                }
            }


            List<PostContent> postContents = new List<PostContent>();
            PostContent postContent = new PostContent();
           
            foreach (string post_temp in post_temps)
            {
                postContent.content =Regex.Match(html, @"userContent(.*?)<p>(.*?)<\/p><\/div>").Groups[2].Value;
                postContent.image = "";
                postContents.Add(postContent);
           }

          return postContents;
        }

        static List<PostContent> postContents = new List<PostContent>();



        //static PostContent postContent = new PostContent();


       
    }


}
