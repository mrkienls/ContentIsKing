using ContentIsKing.DatabaseXML;
using ContentIsKing.MasterK;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;

namespace ContentIsKing.AddFriend_Comment
{
    public static class addFriend_Xenzuu
    {

        static void crawlerFriends(string url, string file)
        {
           
            string html1 = GetDataFromUrl.getHTML(url);
            string name = "";
            string id = "";

            MatchCollection contents = Regex.Matches(html1, "href='pr\\/(.*?)'(.*?)id_user=(\\d+)(.*?)ac=do_follow");


            foreach (Match content in contents)
            {
                foreach (Capture capture in content.Captures)
                {
                    string s = capture.Value;
                    if (s.Contains("user_add"))
                    {
                        name = Regex.Match(s, "href='pr\\/(.*?)'(.*?)id_user=(\\d+)").Groups[1].Value;
                        id = Regex.Match(s, "href='pr\\/(.*?)'(.*?)id_user=(\\d+)").Groups[3].Value;

                        // addfirend:   get https://www.xenzuu.com/do.php?ac=add_friend&id=231992&align=0
                    }

                    MainDatabase.Insert(file, name, id);
                }
            }
        }
        public static void addFriend_Xen()
        {
            string user = "nguyenthuylinhls";
            string pass = "cstd1234";
            //login
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
                crawlerFriends("https://www.xenzuu.com/fl188228", "friends.xml");


            }

            
        }
       
    }
}
