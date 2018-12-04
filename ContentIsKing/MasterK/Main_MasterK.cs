using ContentIsKing.MasterK;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using System.Xml;
using System.Xml.Linq;

namespace ContentIsKing.MasterK
{
    static public class Main_MasterK
    {
        //public  Main_MasterK()
        //{

        //}


        // hen gio thuc hien
 
        static public void hengio_post(int phut, ProgressBar pb)
        {
            pb.Minimum = 0;
            pb.Maximum = phut * 60;
            pb.Value = pb.Maximum;
            
            DispatcherTimer dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Tick += (sender, e) => { dispatcherTimer_Tick_Post(sender, e, pb,pb.Maximum); };
            dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
            dispatcherTimer.Start();
  
          
        }

     
        // Post to xenzu
        static private void dispatcherTimer_Tick_Post(object sender, EventArgs e, ProgressBar pb, double max)
        {

            pb.Value--;
      
            if (pb.Value == 0)
            { 
                pb.Value = max;


                string user = Properties.Settings.Default.userXen;
                string pass = Properties.Settings.Default.passXen;
                string path = "db.xml";
                XElement x = DatabaseXML.MainDatabase.readXML(path);
                string content = x.Element("Content").Value;
                string pathImage = x.Element("PathMedia").Value;
                Minds.Post("kienmnm", "Mnm@1234", content, System.AppDomain.CurrentDomain.BaseDirectory + pathImage);
                if (!pathImage.Contains("mp4"))   Minds.webtalk("kienvtls@gmail.com", "Mnm@1234", content, System.AppDomain.CurrentDomain.BaseDirectory + pathImage);
                Post_To_Xenzu.post_xenzu(content, pathImage, user, pass);
                //Thread threadPostXen = new Thread(() => Post_To_Xenzu.post_xenzu(content, pathImage, user, pass));
                //threadPostXen.Start();


                // update trang thai ve 1
                DatabaseXML.MainDatabase.Update(path, content, "1");
                //delete file
                if (File.Exists(pathImage))
                {
                    File.Delete(pathImage);
                }
            }


        }

     
        
       
 


        // het gio crawler 
        static public void hengio_crawler(int phut, ProgressBar pb)
        {
            pb.Minimum = 0;
            pb.Maximum = phut * 60;
            pb.Value = pb.Maximum;

           DispatcherTimer dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Tick += (sender, e) => { dispatcherTimer_Tick_Crawerl(sender, e, pb, pb.Maximum); };
         
            dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
            dispatcherTimer.Start();

        }

        static private void dispatcherTimer_Tick_Crawerl(object sender, EventArgs e,ProgressBar pb,double max)
        {

            //Thread threadPostXen = new Thread(() => Post_To_Xenzu.post_xenzu(content, pathImage, user, pass));
            //threadPostXen.Start();
            pb.Value--;

            if (pb.Value == 0)
            {
                pb.Value = max;
                Thread threadCrawrelFB = new Thread(() => crawlerFB());
                threadCrawrelFB.Start();

            }




        }

        static void crawlerFB()
        {
            string path = "db.xml";

            List<string> URLs = new List<string> { };

            string myXmlString = File.ReadAllText("urls.xml");
            XmlDocument xml = new XmlDocument();
            xml.LoadXml(myXmlString);
            XmlNodeList xnList = xml.SelectNodes("/Posts/Post");
            foreach (XmlNode xn in xnList)
            {
                string url = xn["Content"].InnerText;
                URLs.Add(url);
            }

            foreach (string url in URLs) CrawlerFB.Crawrel(url, path);
        }

    }
}
