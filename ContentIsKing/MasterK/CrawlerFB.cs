﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

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
            foreach (PostContent c in postContents)
            {
                noidung = c.content;
                noidung = Regex.Replace(noidung, "<.*?>|&.*?;", "");


                string urlImage = c.image;
                if (urlImage != "")
                { pathImageSaved = GetDataFromUrl.DownloadImage(urlImage); }
                else
                { pathImageSaved = ""; }

                // save to db xml
                DatabaseXML.MainDatabase.saveXML(path_saved, noidung, pathImageSaved);
            }
        }
    }
}