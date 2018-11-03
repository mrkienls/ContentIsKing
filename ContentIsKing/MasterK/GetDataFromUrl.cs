using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
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
            MatchCollection contents = Regex.Matches(html, @"userContentWrapper(.*?)<\/form>");
            
            foreach (Match content in contents)
            {
                foreach (Capture capture in content.Captures)
                {
                     post_temps.Add(capture.Value);
                }
            }

            List<PostContent> postContents = new List<PostContent>();
            
            string s = "";
            foreach (string post_temp in post_temps)
            {
                PostContent postContent = new PostContent();
                postContent.content =Regex.Match(post_temp, @"userContent(.*?)<p>(.*?)<\/p><\/div>").Groups[2].Value;
                s = Regex.Match(post_temp, "src=\"(.*?)alt(.*?)src=\"(.*?)\"").Groups[3].Value;
                postContent.image = s.Replace("amp;", "");
                postContents.Add(postContent);
           }
          return postContents;
        }
    
        /*Download image from urlImage to PC*/
        public static string DownloadImage(string urlImage)
        {
            string path="temp.jpg";

            HttpWebRequest lxRequest = (HttpWebRequest)WebRequest.Create(urlImage);

            // returned values are returned as a stream, then read into a string
            string lsResponse = string.Empty;
            using (HttpWebResponse lxResponse = (HttpWebResponse)lxRequest.GetResponse())
            {
                using (BinaryReader reader = new BinaryReader(lxResponse.GetResponseStream()))
                {
                    Byte[] lnByte = reader.ReadBytes(1 * 1024 * 1024 * 10);
                    using (FileStream lxFS = new FileStream(path, FileMode.Create))
                    {
                        lxFS.Write(lnByte, 0, lnByte.Length);
                    }
                }
            }
       

            return path;
        }

    }

    public class PostContent
    {
        public string content { get; set; }
        public string image { get; set; }
        public string video { get; set; }
    }
}
