using System;
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
using System.Windows.Shapes;
using System.IO;
using Microsoft.Win32;
using System.Threading;
using System.Data.SQLite;
using FortunaExcelProcessing;
using GUI.Scripts;

namespace GUI
{
    /// <summary>
    /// Interaction logic for Uploader.xaml
    /// </summary>
    public partial class Uploader : Window
    {
        string url;
        bool isProcessing;

        List<WeeklyData> currentData;

        public Uploader()
        {
            InitializeComponent();
            DisableEnableWindows(ActiveWindow.Home);
        }

        private void UpdateUploadStatus(UploadStatus upst)
        {
            butSelectFile.IsEnabled = true;
            butUploadMemoryData.IsEnabled = false;
            butReadLocalDatabase.IsEnabled = false;
            butGenerateLocalDatabase.IsEnabled = false;

            if (upst == UploadStatus.Complete)
            {

            }
            else if (upst == UploadStatus.ReadLocal)
                butUploadMemoryData.IsEnabled = true;
            else if (upst == UploadStatus.GenerateLocal)
                butReadLocalDatabase.IsEnabled = true;
            else if (upst == UploadStatus.SelectFile)
                butGenerateLocalDatabase.IsEnabled = true;
        }

        private void butClose_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void butMinimise_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void click_SelectInputFile(object sender, RoutedEventArgs e)
        {
            if (!isProcessing)
            {
                isProcessing = true;
                OpenFileDialog fd = new OpenFileDialog();
                fd.Filter = "Excel Workbooks|*.xlsx";
                if (fd.ShowDialog() == true)
                {
                    url = fd.FileName;
                    UpdateUploadStatus(UploadStatus.SelectFile);
                    currentData = new List<WeeklyData>();
                }
                else
                    UpdateUploadStatus(UploadStatus.None);
            }
            isProcessing = false;
        }

        private void click_GenerateLocalDatabase(object sender, RoutedEventArgs e)
        { 
            if (!(string.IsNullOrEmpty(url)) && url.Substring(url.Length - 5) == ".xlsx" && !isProcessing)
            {
                isProcessing = true;
                bool tmp = false;
                try
                {
                    ManageData.ProcessFile(url);
                }
                catch (Exception ie)
                {
                    MessageBox.Show(ie.ToString());
                    tmp = true;
                }
                if (!tmp)
                    UpdateUploadStatus(UploadStatus.GenerateLocal);
            }
            else
            {
                MessageBox.Show("Incorrect File or Directory");
                MessageBox.Show(url);
            }
            isProcessing = false;
        }

        private void click_ReadLocalDatabase(object sender, RoutedEventArgs e)
        {
            currentData = ManageData.LoadData();
            if (currentData.Count > 0 && !isProcessing)
            {
                isProcessing = true;
                UpdateUploadStatus(UploadStatus.ReadLocal);
            }
            isProcessing = false;
        }

        private void butUploadMemoryData_Click(object sender, RoutedEventArgs e)
        {
            if (currentData != null && !isProcessing)
            {
                isProcessing = true;
                bool tmp = false;
                try
                {
                    this.Cursor = Cursors.Wait;
                    //UploadData(new WeeklyData(1, 42, DateTime.Now, "HelloWorld"));
                    //UploadDataGet(new WeeklyData(1, 42, DateTime.Now, "HelloWorld"));

                    foreach (WeeklyData wdata in currentData)
                    {
                        ManageData.UploadDataGet(wdata);
                    }

                    this.Cursor = Cursors.Arrow;
                }
                catch (Exception ie)
                {
                    MessageBox.Show(ie.ToString());
                    tmp = true;
                }
                if (!tmp)
                    UpdateUploadStatus(UploadStatus.UploadLocal);
            }
            isProcessing = false;
        }

        private void button_Copy4_Click(object sender, RoutedEventArgs e)
        {
            //leave me here pls
        }

        private void panTitleBar_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }

        private void SwitchWindows(object sender, RoutedEventArgs e)
        {
            Button b = sender as Button;
            int aw = int.Parse(b.Tag.ToString());
            DisableEnableWindows((ActiveWindow) aw);
        }

        private void DisableEnableWindows(ActiveWindow aw)
        {
            if (aw == ActiveWindow.Home)
            {
                panHome.Visibility = Visibility.Visible;
                panNews.Visibility = Visibility.Hidden;
                panSettings.Visibility = Visibility.Hidden;
                panUpload.Visibility = Visibility.Hidden;
            }
            else if (aw == ActiveWindow.Report)
            {
                panNews.Visibility = Visibility.Visible;
                panHome.Visibility = Visibility.Hidden;
                panSettings.Visibility = Visibility.Hidden;
                panUpload.Visibility = Visibility.Hidden;
            }
            else if (aw == ActiveWindow.Upload)
            {
                panUpload.Visibility = Visibility.Visible;
                panHome.Visibility = Visibility.Hidden;
                panNews.Visibility = Visibility.Hidden;
                panSettings.Visibility = Visibility.Hidden;
            }
            else if (aw == ActiveWindow.Settings)
            {
                panSettings.Visibility = Visibility.Visible;
                panHome.Visibility = Visibility.Hidden;
                panNews.Visibility = Visibility.Hidden;
                panUpload.Visibility = Visibility.Hidden;
            }
        }
    }
}