using CefSharp.MinimalExample.WinForms;
using ContentIsKing.AddFriend_Comment;
using ContentIsKing.DatabaseXML;
using ContentIsKing.MasterK;
using ContentIsKing.UI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml;
using System.Xml.Linq;

namespace ContentIsKing
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {


         


            InitializeComponent();


           MainUI.LoadUrl(listUrls);
            foreach (var tabItem in tabMain.Items)
            {
                (tabItem as TabItem).IsEnabled = false;
            }

            var tab = tabMain.Items[0] as TabItem;
            tab.IsEnabled = true;

            bool s = Class_Login.ProcessLogin("key","u","p");


        }

        //crawler
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //Main_MasterK.hengio_crawler(360);
            // sau 2 gio post 1 lan
            //  Main_MasterK.hengio_post(2);


            /*test*/



            //   CrawlerFB.Crawrel("https://www.facebook.com/pg/DienQuanEntertainment/posts", "db.xml");
            // addFriend_Xenzuu.CrawlerFriend_Xen();
            addFriend_Xenzuu.addFriend_Xen();

        }

        //post
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
           

        }

        private void cmdAddUrl(object sender, RoutedEventArgs e)
        {
            //  MessageBox.Show(txtUrlAdd.Text);
            MainUI.AddUrl(txtUrlAdd.Text, listUrls);
        }
        

    }
}
