using ContentIsKing.DatabaseXML;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Xml;
using System.Xml.Linq;

namespace ContentIsKing.UI
{
    public static class MainUI
    {
        // load du lieu tu db len listview
        public static void LoadUrl(ListView lst)
        {
      
            string myXmlString  = File.ReadAllText("urls.xml");
            XmlDocument xml = new XmlDocument();
            xml.LoadXml(myXmlString); 

            XmlNodeList xnList = xml.SelectNodes("/Posts/Post");
            foreach (XmlNode xn in xnList)
            {
                string url = xn["Content"].InnerText;
               
                AddUrl(url, lst);

            }

        }

        public static void AddUrl(string url, ListView listView)
        {
            // save to db
            MainDatabase.saveXML("urls.xml", url, "");

            StackPanel stPanelUrl = new StackPanel();
            TextBox txtUrl = new TextBox();
            txtUrl.Text = url;
            Button btnDeleteUrl = new Button();
            btnDeleteUrl.Click += DeleteUrl;
            btnDeleteUrl.Content = "Xoa";
            stPanelUrl.Orientation = Orientation.Horizontal;
            stPanelUrl.Children.Add(txtUrl);
            stPanelUrl.Children.Add(btnDeleteUrl);
            listView.Items.Add(stPanelUrl);

          
        }

        static void DeleteUrl(object sender, RoutedEventArgs e)
        {
        //https://stackoverflow.com/questions/13741349/how-to-make-a-usercontrol-remove-itself-at-runtime-in-wpf
            Button btn = sender as Button ;
            StackPanel staPanel = btn.Parent as StackPanel;



            string content = staPanel.Children[0].ToString().Substring(33);
        

            // delete
            XDocument xdoc = XDocument.Load("urls.xml");
            xdoc.Element("Posts").Elements("Post").Where(x => x.Element("Content").Value == content).Remove();
            xdoc.Save("urls.xml");

            ((ListView)staPanel.Parent).Items.Remove(staPanel);
        }


    }
}
