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
            try
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
            catch
            {
                return "";
            }
           

         
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
                s= Regex.Match(post_temp, @"userContent(.*?)<p>(.*?)<\/p>").Groups[2].Value;

                postContent.content = s;

                s = Regex.Match(post_temp, "scaledImageFitWidth img\" src=\"(.*?)\"").Groups[1].Value;
                postContent.image = s.Replace("amp;", "");
                string p = "<a href=\"(.*?)href=\"(.*?)\\/\\?__xts__";
                string sVideo = "";
                sVideo = Regex.Match(post_temp, p).Groups[2].Value;
                if (sVideo != "") {
                    if (!sVideo.Contains("facebook.com")) 
                    postContent.video = "https://www.facebook.com" + sVideo;
                }
                postContents.Add(postContent);
           }
          return postContents;
        }
    
        /*Download image from urlImage to PC*/
        public static string DownloadImage(string urlImage)
        {
            string path=@"media\" + DateTime.Now.ToString("yyyyMMddTHHmmss.fff") + ".jpg";

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


        public static string DownloadVideo(string urlVideo)
        {

            string path = "";
            string html = GetDataFromUrl.getHTML(urlVideo);

           string sd = Regex.Match(html, "sd_src_no_ratelimit:\"(.*?)\"").Groups[1].Value;
           string hd = Regex.Match(html, "hd_src:\"(.*?)\"").Groups[1].Value;
            string pathUrl = " ";

            // ut tien lay toc do sd, vi hd xenzu bi giat(chua co tinh nang tuy chon toc do)
            if (sd != "") { pathUrl = sd; } else { pathUrl = hd; }
                
           if (pathUrl!="" )
                using (var client = new WebClient())
            {
                    path = @"media\" + DateTime.Now.ToString("yyyyMMddTHHmmss.fff") + ".mp4";
                    client.DownloadFile(pathUrl, path);
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
