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

                string url = "https://www.xenzuu.com/fl188228";
                string html1 = GetDataFromUrl.getHTML(url);

                MatchCollection contents = Regex.Matches(html1, "href='pr\\/(.*?)'(.*?)id_user=(\\d+)");
      
             
                foreach (Match content in contents)
                {
                    foreach (Capture capture in content.Captures)
                    {
                        //string s = capture.Grou
                    }
                }
            }

            
        }
       
    }
}
