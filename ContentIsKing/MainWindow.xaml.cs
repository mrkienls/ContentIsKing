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

            Minds.http_post("content","path","kienmnm","Mnm@1234");
                // init
            //string s = "MNM " + Properties.Settings.Default.version;
            //this.Title = s;
            //txtMinuteCrawlerFB.Text = "540";
            //txtMinuteAddFriends_Xen.Text = "60";
            //txtMinutesPost_Xen.Text = "360";

            //txtUserXen.Text = Properties.Settings.Default.userXen;
            //txtPassXen.Password = Properties.Settings.Default.passXen;

            //MainUI.LoadUrl(listUrls);
            //// end init



            //string key = Properties.Settings.Default.key;
            //bool pass = Class_Login.ProcessLogin(key);
            //if (pass)
            //{
            //    var tab = tabMain.Items[1] as TabItem;
            //    tab.Focus();
            //}
            //else
            //{
            //    foreach (var tabItem in tabMain.Items)
            //    {
            //        (tabItem as TabItem).IsEnabled = false;
            //    }
            //    var tab = tabMain.Items[0] as TabItem;
            //    tab.IsEnabled = true;
            //}

        }

        

        private void Hyperlink_RequestNavigate(object sender,
                                       System.Windows.Navigation.RequestNavigateEventArgs e)
        {
            System.Diagnostics.Process.Start(e.Uri.AbsoluteUri);
        }
        //crawler

        //private void HandleCheck(object sender, RoutedEventArgs e)
        //{
        //    MessageBox.Show("Button is Checked");
        //}

        //private void HandleUnchecked(object sender, RoutedEventArgs e)
        //{
        //    MessageBox.Show("Button is unchecked.");
        //}

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            int minute = Convert.ToInt32(txtMinuteCrawlerFB.Text);
            Main_MasterK.hengio_crawler(minute, pbCrawlerFB);

          
         //   txtMessageCrawlerFB.Text = "Scheduled crawler FB after " + txtMinuteCrawlerFB.Text + " minutes.";


            cmdCrawlerFB.IsEnabled = false;
            listUrls.IsEnabled = false;
            buttonAddUrl.IsEnabled = false;

        }

        //post
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
           

        }

        private void cmdAddUrl(object sender, RoutedEventArgs e)
        {
            MainUI.AddUrl(txtUrlAdd.Text, listUrls);
            txtUrlAdd.Text = "";
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

        private void cmdSaveUserXen(object sender, RoutedEventArgs e)
        {
            
            Properties.Settings.Default.userXen = txtUserXen.Text;
            Properties.Settings.Default.passXen = txtPassXen.Password;
            Properties.Settings.Default.Save();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            string user = Properties.Settings.Default.userXen;
            string pass = Properties.Settings.Default.passXen;
            string url = txtUrlFriend_Xen.Text;
            addFriend_Xenzuu.CrawlerFriend_Xen(user, pass, url);
            string s = "";
            s += MainDatabase.GetCout("friendsXen.xml", "TrangThai", "0").ToString();
            s += " users is waiting you.";
            txtMessageFriendsCount.Text = s;
        }

        private void cmdAddFriend_Xen(object sender, RoutedEventArgs e)
        {
            

            int minute = Convert.ToInt32(txtMinuteAddFriends_Xen.Text);
            addFriend_Xenzuu.hengio_addFriend_Xen(minute,pbAddFriend_xen);

            cmdCrawlerFriend_Xen.IsEnabled = false;
            buttonAddFriend_Xen.IsEnabled = false;
        }

        private void Click_Post(object sender, RoutedEventArgs e)
        {
            int minute = Convert.ToInt32(txtMinutesPost_Xen.Text);
              Main_MasterK.hengio_post(minute,pbSchedulePost);
            btnSchedulePost_Xen.IsEnabled = false;
            
        }
    }
}
