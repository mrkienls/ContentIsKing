using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace ContentIsKing.UI
{
    public static class MainUI
    {

        public static void AddUrl(string url, ListView listView)
        {
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
            ((ListView)staPanel.Parent).Items.Remove(staPanel);

        }
    }
}
