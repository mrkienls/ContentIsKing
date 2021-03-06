﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RestSharp;
using System.Net;
using System.Text.RegularExpressions;
using System.IO;

namespace ContentIsKing.MasterK
{
    static class Post_To_Xenzu
    {
        static public void post_xenzu(string content, string pathImage,string user, string pass)
        {

           var client = new RestClient("https://www.xenzuu.com/index.php");
            var request = new RestRequest(Method.POST);
            CookieContainer _cookieJar = new CookieContainer();
            client.CookieContainer = _cookieJar;

            request.AddHeader("content-type", "application/x-www-form-urlencoded");
            request.AddParameter("mp", "home");
            request.AddParameter("ac", "login");
            request.AddParameter("user", user);
            request.AddParameter("pwd", pass);
            request.AddParameter("autologin", "1");

            IRestResponse response = client.Execute(request);


            string html = response.Content;

            // post
            if (html.ToLower().Contains("logout"))
            {

                string dpostid = Regex.Match(html, "dpostid value='(\\d+)'").Groups[1].Value;
                request.AddParameter("max_file_size", "1000000000");
                request.AddParameter("ac", "post_status");
                request.AddParameter("mp", "home");
                request.AddParameter("id_community", "0");
                request.AddParameter("dpostid", dpostid);
                request.AddParameter("txt", content);
                if (pathImage!="")
                {
                    request.AddHeader("content-type", "multipart/form-data");
                    request.AddFile("thefile", pathImage);
                }
                

                // execute the request
                IRestResponse response1 = client.Execute(request);
            }
            else
            {

            }
            var request1 = new RestRequest(Method.GET);
            request1.AddHeader("Referer", "https://www.xenzuu.com/index.php?mp=home");
            request1.AddParameter("mp", "home");
            request1.AddParameter("ac", "logout");
            IRestResponse response2 = client.Execute(request1);
            using (StreamWriter w = File.AppendText("posts.txt"))
            {
                w.WriteLine(content);
            }


        }

    }
}
