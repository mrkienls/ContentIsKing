using ContentIsKing.MasterK;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
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
        static public void hengio_post(int phut)
        {
            System.Windows.Threading.DispatcherTimer dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
            dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick_Post);
            dispatcherTimer.Interval = new TimeSpan(0, phut, 0);
            dispatcherTimer.Start();

        }

        // Post to xenzu
        static private void dispatcherTimer_Tick_Post(object sender, EventArgs e)
        {
            string user = "nguyenthuylinhls";
            string pass = "cstd1234";
            string path = "db.xml";
            XElement x = DatabaseXML.MainDatabase.readXML(path);
            string content = x.Element("Content").Value;
            string pathImage = x.Element("PathMedia").Value;
            Post_To_Xenzu.post_xenzu(content, pathImage, user, pass);

            // update trang thai ve 1
            DatabaseXML.MainDatabase.Update(path, content, "1");
            //delete file
            if (File.Exists(pathImage))
            {
                File.Delete(pathImage);
            }
        }



        // het gio crawler 
        static public void hengio_crawler(int phut)
        {
            System.Windows.Threading.DispatcherTimer dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
            dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick_Crawerl);
            dispatcherTimer.Interval = new TimeSpan(0, phut, 0);
            dispatcherTimer.Start();

        }

        static private void dispatcherTimer_Tick_Crawerl(object sender, EventArgs e)
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
