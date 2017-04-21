﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Net;
using System.IO;
using GUI.Scripts;

namespace GUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            LoadData();
            AttemptLogin(true);
        }

        private void LoadData()
        {
            if (File.Exists(Utilities.SAVE_DATA_URL))
            {
                Dictionary<string, string> savedata = new Dictionary<string, string>();
                foreach(string line in File.ReadLines(Utilities.SAVE_DATA_URL, Encoding.UTF8))
                {
                    string[] tmp = line.Split('-');
                    savedata.Add(tmp[0], tmp[1]);
                }
                try
                {
                    if (savedata["RememberMe"] == "true")
                        chkRememberMe.IsChecked = true;
                    if (savedata["Username"] != "" && savedata["Username"] != null)
                        txtUsername.Text = savedata["Username"];
                    if (savedata["Password"] != "" && savedata["Password"] != null)
                        txtPassword.Password = savedata["Password"];
                }
                catch { }
            }
        }

        private void AttemptLogin(bool quiet = false)
        {
            string s = LoginClass.IsAbleToAutologin(txtUsername.Text, txtPassword.Password);
            if (s == "")
                Login(txtUsername.Text);
            else if (!quiet)
                MessageBox.Show(s);
        }

        private void Login(string farmName)
        {
            if (chkRememberMe.IsChecked == true)
                SaveData();
            Uploader u = new Uploader();
            u.SetFarmName(farmName);
            this.Close();
            u.ShowDialog();           
        }

        private void SaveData()
        {
            StreamWriter file = new StreamWriter(Utilities.SAVE_DATA_URL);
            if (chkRememberMe.IsChecked == true)
            {
                file.WriteLine("RememberMe-" + chkRememberMe.IsChecked.ToString());
                file.WriteLine("Username-" + txtUsername.Text);
                file.WriteLine("Password-" + txtPassword.Password);
            }
            file.Close();
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }

        private void butMinimise_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void butClose_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void butLogin_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            AttemptLogin();
        }
    }
}
