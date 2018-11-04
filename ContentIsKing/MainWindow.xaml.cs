using ContentIsKing.DatabaseXML;
using ContentIsKing.MasterK;
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
            string path = "db.xml";
            XDocument xdoc = XDocument.Load(path);
     
            InitializeComponent();

            //Main_MasterK mk = new Main_MasterK();

          
            //  MainDatabase.Insert();
            XElement x= MainDatabase.readXML(path);

         //   int[] numbers = { 5, 4, 1, 3, 9, 8, 6, 7, 2, 0 };
          //  int oddNumbers = numbers.Count(n => n % 2 == 1);
        }


      
    }
}
