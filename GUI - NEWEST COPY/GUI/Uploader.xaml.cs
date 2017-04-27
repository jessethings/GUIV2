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
using Newtonsoft.Json;
using FortunaExcelProcessing.ConsilidatedReport;

namespace GUI
{
    /// <summary>
    /// Interaction logic for Uploader.xaml
    /// </summary>
    public partial class Uploader : Window
    {
        string url;
        bool isProcessing;
        string UserEmail;
        Farm farm;
        bool uploadIsComplete = false;

        Dictionary<string, int> farms;

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


            //thingy to make combo thingy thingy
            DateTime date = new DateTime(2017, 06, 1);
            for (int i = 1; i <= 365; i++)
            {
                cboColsonidatedReportDate.Items.Add(date);
                date.AddDays(7);
            }

        }

        public void SetFarm(Farm farm)
        {
            this.farm = farm;
            labFarmNameMenu.Content = farm.farmname;
            labFarmNameHome.Content = farm.farmname;
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
            {
                butGenerateLocalDatabase.IsEnabled = true;
                butSelectFile.IsEnabled = false;
            }

            labUploadInstruction.Content = ClunkyShittyCode.SetUploadPanelText(upst + 1);
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

                if (farm != null)
                {
                    try
                    {
                        ManageData.ProcessFile(url, farm);
                    }
                    catch (Exception ie)
                    {
                        //MessageBox.Show(ie.ToString());
                        MessageBox.Show("If the excel workbook you are currently trying to upload is open, please close it before proceeding.\nIf it is already closed, please contact your local administrator and report this error.\nError: Fortuna#293");
                        tmp = true;
                    }
                    if (!tmp)
                        UpdateUploadStatus(UploadStatus.GenerateLocal);
                }
                else
                    MessageBox.Show("You are not currently assigned to any farm.");
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
            if (weekly != null && !isProcessing)
            {
                isProcessing = true;
                weekly = ManageData.WeeklyLoadData();
                comments = ManageData.CommentsLoadData();
                try { paddocks = ManageData.PaddocksLoadData(); } catch (Exception ie) { }
                farmsupp = ManageData.FarmSuppLoadData();
                labels = ManageData.LabelLoadData();
                calculations = ManageData.CalcLoadData();

                try
                {
                    this.Cursor = Cursors.Wait;
                    //UploadData(new WeeklyData(1, 42, DateTime.Now, "HelloWorld"));
                    //UploadDataGet(new WeeklyData(1, 42, DateTime.Now, "HelloWorld"));

                    foreach (WeeklyData wdata in weekly)
                    {
                        UploadData.UploadDataGet(wdata);
                    }

                    this.Cursor = Cursors.Arrow;
                }
                catch (Exception ie)
                {
                    MessageBox.Show(ie.ToString());
                    tmp = true;
                }
            }
            if (farmsupp != null && !isProcessing)
            {
                try
                {
                    this.Cursor = Cursors.Wait;
                    foreach (FarmSupplements fsdata in farmsupp)
                    {
                        UploadData.UploadFarmSuppGet(fsdata);
                    }
                    this.Cursor = Cursors.Arrow;
                }
                catch (Exception ie)
                {
                    MessageBox.Show(ie.ToString());
                    tmp = true;
                }
            }
            if (paddocks != null && !isProcessing)
            {
                try
                {
                    this.Cursor = Cursors.Wait;
                    foreach (Paddocks pdata in paddocks)
                    {
                        UploadData.UploadPaddocksGet(pdata);
                    }
                    this.Cursor = Cursors.Arrow;
                }
                catch (Exception ie)
                {
                    MessageBox.Show(ie.ToString());
                    tmp = true;
                }
            }
            if (paddocks != null && !isProcessing)
            {
                try
                {
                    this.Cursor = Cursors.Wait;
                    foreach (Comments cdata in comments)
                    {
                        UploadData.UploadCommentsGet(cdata);
                    }
                    this.Cursor = Cursors.Arrow;
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
            DisableEnableWindows((ActiveWindow)aw);
        }

        private void DisableEnableWindows(ActiveWindow aw)
        {
            if (uploadIsComplete)
            {
                uploadIsComplete = false;
                labUploadInstruction.Content = ClunkyShittyCode.SetUploadPanelText(UploadStatus.None);
            }

            panHome.Visibility = Visibility.Hidden;
            panReport.Visibility = Visibility.Hidden;
            panSettings.Visibility = Visibility.Hidden;
            panUpload.Visibility = Visibility.Hidden;
            panUsers.Visibility = Visibility.Hidden;

            if (aw == ActiveWindow.Home)
            {
                panHome.Visibility = Visibility.Visible;

            }
            else if (aw == ActiveWindow.Report)
            {
                panReport.Visibility = Visibility.Visible;
            }
            else if (aw == ActiveWindow.Upload)
            {
                panUpload.Visibility = Visibility.Visible;
            }
            else if (aw == ActiveWindow.Settings)
            {
                panSettings.Visibility = Visibility.Visible;
            }
            else if (aw == ActiveWindow.Users)
            {
                panUsers.Visibility = Visibility.Visible;
                LoadAdminMenu();
            }
        }

        private void LoadAdminMenu()
        {
            panModifyUser.Visibility = Visibility.Hidden;
            panModifyFarm.Visibility = Visibility.Hidden;

            lstUsers.Items.Clear();

            foreach (User u in DownloadData.GetAllUsers())
            {
                lstUsers.Items.Add(u);
            }

            cboModuserfarmlist.Items.Clear();
            lstFarms.Items.Clear();
            farms = null;

            try
            {
                foreach (FullFarm ff in DownloadData.GetAllFullFarms())
                {
                    cboModuserfarmlist.Items.Add(ff.name);
                    lstFarms.Items.Add(ff);
                }
            }
            catch
            {
                MessageBox.Show("There are currently no farms available right now");
            }

            cboModuserRoles.Items.Clear();

            for (PermissionLevel p = PermissionLevel.Admin; p < PermissionLevel.Guest + 1; p++)
                cboModuserRoles.Items.Add(p);
        }

        private void butMenuLogout_Click(object sender, RoutedEventArgs e)
        {
            if (File.Exists(Utilities.SAVE_DATA_URL))
                File.Delete(Utilities.SAVE_DATA_URL);
            Application.Current.Shutdown();
        }

        private void butGenerateConsolidatedReport1_Click(object sender, RoutedEventArgs e)
        {
            //set temp location
        }

        private void butReloadAllUsers_Click(object sender, RoutedEventArgs e)
        {
            LoadAdminMenu();
        }

        private void butModifyFarm_Click(object sender, RoutedEventArgs e)
        {
            panModifyFarm.Visibility = Visibility.Visible;
            ContentMainAdmin.Visibility = Visibility.Hidden;
            try
            {
                FullFarm ff = (FullFarm)lstFarms.SelectedItems[0];
                txtModuserFarmarea.Text = ff.area + "";
                txtModuserFarmid.Text = ff.id + "";
                txtModuserFarmname.Text = ff.name;
            }
            catch
            {
                panModifyFarm.Visibility = Visibility.Hidden;
                ContentMainAdmin.Visibility = Visibility.Visible;
                MessageBox.Show("Please select a farm");
            }
        }

        private void butModifyUser_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                User u = (User)lstUsers.SelectedItems[0];

                try
                {
                    Farm f = DownloadData.GetUserFarm(u.email);
                    cboModuserRoles.SelectedIndex = f.role;
                    cboModuserfarmlist.SelectedValue = f.farmname;
                }
                catch
                {
                }
                try
                {
                    txtModuserEmail.Text = u.email;
                    txtModuserID.Text = u.id + "";
                    txtModuserName.Text = u.name;
                    txtModuserPassword.Password = "";
                    txtModuserPasswordConfirm.Password = "";


                    panModifyUser.Visibility = Visibility.Visible;
                    ContentMainAdmin.Visibility = Visibility.Hidden;
                }
                catch
                {
                    MessageBox.Show("Error: Fortuna#326");
                }
            }
            catch
            {
                MessageBox.Show("Please select a user account first");
            }
        }

        private void butModuserSaveChanges_Click(object sender, RoutedEventArgs e)
        {
            //try
            //{
            if (txtModuserPassword.Password == txtModuserPasswordConfirm.Password)
            {
                User u2 = new User();
                try
                {
                    u2 = (User)lstUsers.SelectedItems[0];
                }
                catch { }
                User u = new User();
                u.name = txtModuserName.Text;
                u.password = txtModuserPassword.Password;
                u.email = txtModuserEmail.Text;
                if (txtModuserPassword.Password != "" && txtModuserName.Text != "" && txtModuserEmail.Text != "")
                {
                    if (txtModuserID.Text == "" || txtModuserID.Text == null)
                        UploadData.CreateUser(u);
                    else
                    {
                        UploadData.UpdateUser(u2.email, u);
                        try
                        {
                            UploadData.AssignFarm(farms[(string)cboModuserfarmlist.SelectedValue], u2.email);
                        }
                        catch { }
                    }
                }
                else
                    MessageBox.Show("Please fill in all fields");
                UploadData.UpdatePermissions(txtModuserEmail.Text, cboModuserRoles.SelectedIndex);

            }
            /*}
            catch
            {
                MessageBox.Show("Error: Fortuna#324");
            }*/
        }

        private void butModuserBack_Click(object sender, RoutedEventArgs e)
        {
            panModifyUser.Visibility = Visibility.Hidden;
            panModifyFarm.Visibility = Visibility.Hidden;
            ContentMainAdmin.Visibility = Visibility.Visible;
        }

        private void butModuserCreateUser_Click(object sender, RoutedEventArgs e)
        {
            txtModuserEmail.Text = "";
            txtModuserID.Text = "";
            txtModuserName.Text = "";
            txtModuserPassword.Password = "";
            txtModuserPasswordConfirm.Password = "";

            panModifyUser.Visibility = Visibility.Visible;
            ContentMainAdmin.Visibility = Visibility.Hidden;
        }

        private void butModuserSaveChangesFarm_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Farm f = new Farm();
                if (txtModuserFarmid.Text == "" || txtModuserFarmid.Text == null)
                {
                    f.area = double.Parse(txtModuserFarmarea.Text);
                    f.farmname = txtModuserFarmname.Text;
                    UploadData.CreateFarm(f);
                }
                else
                {
                    f.farmid = int.Parse(txtModuserFarmid.Text);
                    f.area = double.Parse(txtModuserFarmarea.Text);
                    f.farmname = txtModuserFarmname.Text;
                    UploadData.UpdateFarm(f);
                }
            }
            catch
            {
                MessageBox.Show("Error: Fortuna#325");
            }
        }

        private void butModifyUser_Click_1(object sender, RoutedEventArgs e)
        {
            txtModuserFarmarea.Text = "";
            txtModuserFarmid.Text = "";
            txtModuserFarmname.Text = "";

            panModifyFarm.Visibility = Visibility.Visible;
            ContentMainAdmin.Visibility = Visibility.Hidden;
        }

        private void cboColsonidatedReportDate_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //ignore dis
        }

        private void butGenerateConsolidatedReport_Click(object sender, RoutedEventArgs e)
        {
            DateTime date = (DateTime)cboColsonidatedReportDate.SelectedValue;

            processConsolidated p = new processConsolidated();

            p.createWorkBook("",date, dict);
        }
    }
}