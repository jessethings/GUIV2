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
        string FarmName;
        bool uploadIsComplete = false;

        //turn into a factory
        List<WeeklyData> weekly;
        List<Comments> comments;
        List<Paddocks> paddocks;
        List<FarmSupplements> farmsupp;
        //only need to be run once
        List<Labels> labels;
        List<Calculations> calculations;

        public Uploader()
        {
            InitializeComponent();
            DisableEnableWindows(ActiveWindow.Home);
        }

        public void SetFarmName(string name)
        {
            if (name == "" || name == null)
                FarmName = "Not Found";
            FarmName = name;
            labFarmNameHome.Content = FarmName;
            labFarmNameMenu.Content = FarmName;
        }

        private void UpdateUploadStatus(UploadStatus upst)
        {
            butSelectFile.IsEnabled = true;
            butUploadMemoryData.IsEnabled = false;
            butGenerateLocalDatabase.IsEnabled = false;

            if (upst == UploadStatus.Complete)
            {

            }
            else if (upst == UploadStatus.GenerateLocal)
                butUploadMemoryData.IsEnabled = true;
            else if (upst == UploadStatus.SelectFile)
                butGenerateLocalDatabase.IsEnabled = true;

            labUploadInstruction.Content = ClunkyShittyCode.SetUploadPanelText(upst +1);
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
                    weekly = new List<WeeklyData>();
                    comments = new List<Comments>();
                    paddocks = new List<Paddocks>();
                    farmsupp = new List<FarmSupplements>();
                    labels = new List<Labels>();
                    calculations = new List<Calculations>();
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

        private void butUploadMemoryData_Click(object sender, RoutedEventArgs e)
        {
            bool tmp = false;

            //populate the lists
            weekly = ManageData.WeeklyLoadData();
            comments = ManageData.CommentsLoadData();
            try { paddocks = ManageData.PaddocksLoadData(); } catch (Exception ie) { }
            farmsupp = ManageData.FarmSuppLoadData();
            labels = ManageData.LabelLoadData();
            calculations = ManageData.CalcLoadData();
            Console.WriteLine($"{weekly.Count} {comments.Count} {paddocks.Count} {farmsupp.Count} {labels.Count} {calculations.Count}");

           if (weekly != null && !isProcessing)
            {
                isProcessing = true;
                try
                {
                    this.Cursor = Cursors.Wait;
                    foreach (WeeklyData wdata in weekly)
                    {
                        UploadData.UploadDataGet(wdata);
                    }                
                }
                catch (Exception ie)
                {
                    MessageBox.Show(ie.ToString());
                    tmp = true;
                }
            }
            if (farmsupp != null)
            {
                try
                {
                    this.Cursor = Cursors.Wait;
                    foreach (FarmSupplements fsdata in farmsupp)
                    {
                        UploadData.UploadFarmSuppGet(fsdata);
                    }
                }
                catch (Exception ie)
                {
                    MessageBox.Show(ie.ToString());
                    tmp = true;
                }
            }
            if (paddocks != null)
            {
                try
                {
                    this.Cursor = Cursors.Wait;
                    foreach (Paddocks pdata in paddocks)
                    {
                        UploadData.UploadPaddocksGet(pdata);
                    }
                }
                catch (Exception ie)
                {
                    MessageBox.Show(ie.ToString());
                    tmp = true;
                }
            }
            if (paddocks != null)
            {
                try
                {
                    this.Cursor = Cursors.Wait;
                    foreach (Comments cdata in comments)
                    {
                        UploadData.UploadCommentsGet(cdata);
                    }
                }
                catch (Exception ie)
                {
                    MessageBox.Show(ie.ToString());
                    tmp = true;
                }
            }
            if (labels != null)
            {
                try
                {
                    this.Cursor = Cursors.Wait;
                    foreach (Labels ldata in labels)
                    {
                        UploadData.UploadLabelsGet(ldata);
                    }
                }
                catch (Exception ie)
                {
                    MessageBox.Show(ie.ToString());
                    tmp = true;
                }
            }
            if (calculations != null)
            {
                try
                {
                    this.Cursor = Cursors.Wait;
                    foreach (Calculations cdata in calculations)
                    {
                        UploadData.UploadCalculationGet(cdata);
                    }
                }
                catch (Exception ie)
                {
                    MessageBox.Show(ie.ToString());
                    tmp = true;
                }
            }
            if (!tmp)
            {
                UpdateUploadStatus(UploadStatus.UploadLocal);
                uploadIsComplete = true;
            }
            this.Cursor = Cursors.Arrow;
            isProcessing = false;
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
            if (uploadIsComplete)
            {
                uploadIsComplete = false;
                labUploadInstruction.Content = ClunkyShittyCode.SetUploadPanelText(UploadStatus.None);
            }

            if (aw == ActiveWindow.Home)
            {
                panHome.Visibility = Visibility.Visible;
                panReport.Visibility = Visibility.Hidden;
                panSettings.Visibility = Visibility.Hidden;
                panUpload.Visibility = Visibility.Hidden;
            }
            else if (aw == ActiveWindow.Report)
            {
                panReport.Visibility = Visibility.Visible;
                panHome.Visibility = Visibility.Hidden;
                panSettings.Visibility = Visibility.Hidden;
                panUpload.Visibility = Visibility.Hidden;
            }
            else if (aw == ActiveWindow.Upload)
            {
                panUpload.Visibility = Visibility.Visible;
                panHome.Visibility = Visibility.Hidden;
                panReport.Visibility = Visibility.Hidden;
                panSettings.Visibility = Visibility.Hidden;
            }
            else if (aw == ActiveWindow.Settings)
            {
                panSettings.Visibility = Visibility.Visible;
                panHome.Visibility = Visibility.Hidden;
                panReport.Visibility = Visibility.Hidden;
                panUpload.Visibility = Visibility.Hidden;
            }
        }

        private void butMenuLogout_Click(object sender, RoutedEventArgs e)
        {
            if (File.Exists(Utilities.SAVE_DATA_URL))
                File.Delete(Utilities.SAVE_DATA_URL);
            Application.Current.Shutdown();

        }
    }
}