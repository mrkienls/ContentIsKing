﻿using ContentIsKing.DatabaseXML;
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

        }

        //crawler
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //Main_MasterK.hengio_crawler(360);
            // sau 2 gio post 1 lan
            //Main_MasterK.hengio_post(180);


            /*test*/

            
            
           CrawlerFB.Crawrel("https://www.facebook.com/pg/CoongDDieencos.102/posts/", "db.xml");


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
