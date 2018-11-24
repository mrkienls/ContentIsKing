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

            string s = "iSocialNetwork Version " + Properties.Settings.Default.version;
  

            
        
            
            this.Title = s;
           MainUI.LoadUrl(listUrls);
        

       

            string key = Properties.Settings.Default.key;
            bool pass = Class_Login.ProcessLogin(key);
            if (pass)
            {
                var tab = tabMain.Items[1] as TabItem;
                tab.Focus();
            }
            else
            {
                foreach (var tabItem in tabMain.Items)
                {
                    (tabItem as TabItem).IsEnabled = false;
                }
                var tab = tabMain.Items[0] as TabItem;
                tab.IsEnabled = true;
            }

        }

        private void Hyperlink_RequestNavigate(object sender,
                                       System.Windows.Navigation.RequestNavigateEventArgs e)
        {
            System.Diagnostics.Process.Start(e.Uri.AbsoluteUri);
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
            MainUI.AddUrl(txtUrlAdd.Text, listUrls);
        }

        private void cmdCheckKey(object sender, RoutedEventArgs e)
        {
            bool pass = Class_Login.ProcessLogin(txtKey.Text);
            if (pass)
            {
                MessageBox.Show("Login Success");
                foreach (var tabItem in tabMain.Items)
                {
                    (tabItem as TabItem).IsEnabled = true;
                }

                // save key
                Properties.Settings.Default.key = txtKey.Text;
                Properties.Settings.Default.Save();
            }
            else
            {
                MessageBox.Show("key chua dung. Lien he kiennguyen.mobe@gmail.com hoac facebook  https://www.facebook.com/kiennguyenmobe  de lay key");
                foreach (var tabItem in tabMain.Items)
                {
                    (tabItem as TabItem).IsEnabled = false;
                }
                var tab = tabMain.Items[0] as TabItem;
                tab.IsEnabled = true;
            }
        }
    }
}
