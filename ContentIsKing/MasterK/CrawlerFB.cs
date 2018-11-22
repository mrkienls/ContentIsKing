using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;

namespace ContentIsKing.MasterK
{
    class CrawlerFB
    {

        static public void Crawrel(string url, string path_saved)
        {
         
            string html = GetDataFromUrl.getHTML(url);

            
            List<PostContent> postContents = GetDataFromUrl.getPostFromHtml(html);

            string noidung = "";
            string pathImageSaved = "";
            string pathVideoSaved = "";
            foreach (PostContent c in postContents)
            {
                noidung = c.content;
                noidung = Regex.Replace(noidung, "<.*?>|&.*?;", "");
                noidung = Regex.Replace(noidung, "#(.*?)(\\w|\\d)+","");

               




                string urlImage = c.image;
                if (urlImage != "")
                { pathImageSaved = GetDataFromUrl.DownloadImage(urlImage); }
                else
                { pathImageSaved = ""; }

                string urlVideo = c.video;
                if (urlVideo != "" && urlVideo !=null && urlVideo!=" ")
                { pathVideoSaved = GetDataFromUrl.DownloadVideo(urlVideo); }
                else
                { pathVideoSaved = ""; }

                string pathMedia = "";
                if (pathVideoSaved!="") { pathMedia = pathVideoSaved; } else { pathMedia = pathImageSaved; }
                // save to db xml
                if (pathMedia!="")
                {
                    DatabaseXML.MainDatabase.saveXML(path_saved, noidung, pathMedia);
                    using (StreamWriter w = File.AppendText("crawler.txt"))
                    {
                        w.WriteLine(url);
                    }
                }
                
            }
           
        }
    }
}
