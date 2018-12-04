using ContentIsKing.DatabaseXML;
using ContentIsKing.MasterK;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Xml.Linq;

namespace ContentIsKing.AddFriend_Comment
{
    public static class addFriend_Xenzuu
    {

        // hen gio add Friend 

        static public void hengio_addFriend_Xen(int phut, ProgressBar pb)
        {
            pb.Minimum = 0;
            pb.Maximum = phut * 60;
            pb.Value = pb.Maximum;

            System.Windows.Threading.DispatcherTimer dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
            dispatcherTimer.Tick += (sender, e) => { dispatcherTimer_Tick_AddFriend(sender, e, pb,pb.Maximum); };
            dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
            dispatcherTimer.Start();

        }

        static private void dispatcherTimer_Tick_AddFriend(object sender, EventArgs e, ProgressBar pb, double max)
        {

            pb.Value--;
            if (pb.Value == 0)
            {
                pb.Value = max;
                string user = Properties.Settings.Default.userXen;
                string pass = Properties.Settings.Default.passXen;
                //Thread threadAddFriendXen = new Thread(() => addFriend_Xen(user, pass));
                //threadAddFriendXen.Start();

                addFriend_Xen(user, pass);
            }


        }



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

                      
                    }

                    MainDatabase.Insert(file, name, id);
                }
            }

        }
        static void addFriendRequest(string id, CookieContainer _cookieJar)
        {
           string s = "https://www.xenzuu.com/do.php?ac=add_friend&id="+ id +"&align=0";
            var client = new RestClient(s);
            var request = new RestRequest(Method.GET);
            client.CookieContainer = _cookieJar;
            IRestResponse response = client.Execute(request);
            string html = response.Content;
        }

        static bool login_Xen(RestClient client,string user, string pass, CookieContainer _cookieJar)
        {
        
            var request = new RestRequest(Method.POST);
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
                return true;
            }
            else  return false;
        }

        static void logout_Xen(RestClient client)
        {
            var request1 = new RestRequest(Method.GET);
            request1.AddHeader("Referer", "https://www.xenzuu.com/index.php?mp=home");
            request1.AddParameter("mp", "home");
            request1.AddParameter("ac", "logout");
            IRestResponse response2 = client.Execute(request1);
        }

        public static void CrawlerFriend_Xen(string user, string pass, string url)
        {
    
     
            var client = new RestClient("https://www.xenzuu.com/index.php");
            CookieContainer _cookieJar = new CookieContainer();
            //login
            if (login_Xen(client,user, pass,_cookieJar))
            {
                crawlerFriends(url, "friendsXen.xml");
                logout_Xen(client);
            }
            
        }

        public static void addFriend_Xen(string user, string pass)
        {
            var client = new RestClient("https://www.xenzuu.com/index.php");
            CookieContainer _cookieJar = new CookieContainer();
            //login
            if (login_Xen(client, user, pass,_cookieJar))
            {
                XElement xl = MainDatabase.readXML("friendsXen.xml");
                string id = xl.Element("PathMedia").Value;
                string name = xl.Element("Content").Value;
                addFriendRequest(id, _cookieJar);
                logout_Xen(client);

                // update TrangThai friend da add =1
                MainDatabase.Update("friendsXen.xml", name, "1");
            }


        }

    }
}
