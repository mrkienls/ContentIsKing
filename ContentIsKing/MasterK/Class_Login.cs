
using System.Net;
using System.Text.RegularExpressions;
using System.Windows;

namespace CefSharp.MinimalExample.WinForms
{
    public static class Class_Login
    {
         static string[] getFileLogin()
        {

            string url = "https://github.com/mrkienls/valid/blob/master/fb";



           

            //dotnet 4.5.2 moi dung duong Tls12
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
                var webClient = new WebClient();
                var s = webClient.DownloadString(url);
            // string content = Regex.Match(html, "description\" content=\"(\\w+)\"").Groups[1].Value;
            string key = Regex.Match(s, "LC1(.+)\"\\>(.+)\\<\\/td\\>").Groups[2].Value;
            string URLs = Regex.Match(s, "LC2(.+)\"\\>(.+)\\<\\/td\\>").Groups[2].Value;
            string version = Regex.Match(s, "LC3(.+)\"\\>(.+)\\<\\/td\\>").Groups[2].Value;
            string[] content = new string[] { "", "", "" };
            content[0] = key;
            content[1] = URLs;
            content[2] = version;
            return content;
        }

        /*
         *  XU ly khi moi vao chuong trinh:
         1. Check key
         2. Check URLs neu co thi send request - gia lap luot web
         3. Check Version: neu co version moi thi dua ra thong bao cap nhat    
         */
        public static bool ProcessLogin(string keyUser)
        {
            string[] sLogin = new string[] { };
            sLogin = getFileLogin();
            string key = sLogin[0];
            string URLs = sLogin[1];
            string version = sLogin[2];


            //2. Check URLs
            if (URLs!="0") CheckUrls(URLs);

            //3. Check Version
            string old_version = ContentIsKing.Properties.Settings.Default.version ;

            string[] v = Regex.Split(version, ";");
            string new_version = v[0];
            if (CheckVersion(old_version, new_version))
            {
                MessageBox.Show(v[1]);
               

            }
                //1. Check key

                if (CheckLogin(key,keyUser))
            {
            
                return true;
            }
            else
            {
           
                return false;
            }

            
        }

        public static bool CheckLogin(string key, string keyUser)
        {   if (key == keyUser)
                return true;
            else
                return false;
        }

        static void  requestHTTP(string url)
        {
            var webClient = new WebClient();
            var s = webClient.DownloadString(url);
        }

        public static void CheckUrls(string URLs)
        {

            
            string[] urls = Regex.Split(URLs, ";");

     
            foreach (string path in urls)
            {
                requestHTTP(path);
            }

        }

        public static bool CheckVersion(string oldVersion, string newVersion)
        {
            if (oldVersion == newVersion)
                return false;
            else
                return true;
        }

        public static void saveToSettings()
        {
        }
    }



}
