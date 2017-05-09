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
    // <summary>
    // Interaction logic for Uploader.xaml
    // </summary>
    public partial class Uploader : Window
    {
        string url;
        bool isProcessing;
        Farm myFarm;
        int myID;
        PermissionLevel myRole;
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

            /*DateTime date = new DateTime(2016, 06, 6);
            for (int i = 1; i <= 52; i++)
            {
                cboColsonidatedReportDate.Items.Add(date.ToString("yyyy-MM-dd"));
                date = date.AddDays(7);
            }*/

            List<User> userList = DownloadData.GetAllUsers();
            List<DateTime> dateList = new List<DateTime>();
            foreach(User u in userList)
            {
                List<tmpdate> tmp;
                tmp = DownloadData.GetWeeklyDataDates(u.id);
                try
                {
                    tmp = DownloadData.GetWeeklyDataDates(u.id);
                }
                catch
                {
                    continue;
                }
                if (tmp == null)
                    continue;

                foreach (tmpdate dt in tmp)
                {
                    //MessageBox.Show(dt + "");
                    if (dateList.Contains(dt.sdate))
                        continue;
                    else
                    {
                        dateList.Add(dt.sdate);
                        cboColsonidatedReportDate.Items.Add(dt.sdate.ToString("yyyy-MM-dd"));
                    }
                }
            }

            txtbxSaveDir.Text = Utilities.SAVE_FOLDER;
        }

        public void SetFarm(Farm farm)
        {
            this.myFarm = farm;
            labFarmNameMenu.Content = farm.farmname;
            labFarmNameHome.Content = farm.farmname;
        }

        public void SetRole(PermissionLevel role)
        {
            myFarm = new Farm();
            myRole = role;
            SetupPermissions();
        }

        public void SetupPermissions()
        {
            if (myFarm.farmid < 1)
            {
                myFarm.farmname = "No Farm Assigned";
                myFarm.area = 0;
                myFarm.farmid = 0;
            }

            if (myRole == PermissionLevel.User)
            {
                butMenuSettings.IsEnabled = true;
                butMenuUpload.IsEnabled = true;
                butMenuDownload.IsEnabled = true;
            }
            else if (myRole == PermissionLevel.Admin)
            {
                butMenuAdmin.IsEnabled = true;
                butMenuSettings.IsEnabled = true;
                butMenuUpload.IsEnabled = true;
                butMenuDownload.IsEnabled = true;
            }
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

                if (myFarm != null)
                {
                    string res = ManageData.ProcessFile(url, myFarm);
                    if (res != "" && res != null)
                        MessageBox.Show(res);

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
            MessageBox.Show("Process Complete!");
        }

        private void butUploadMemoryData_Click(object sender, RoutedEventArgs e)
        {
            bool tmp = false;
            if (weekly != null && !isProcessing)
            {
                isProcessing = true;
                weekly = ManageData.WeeklyLoadData();
                comments = ManageData.CommentsLoadData();
                try { paddocks = ManageData.PaddocksLoadData(); } catch { }
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
            if (farmsupp != null)
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
            if (paddocks != null)
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
            if (comments != null)
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
            MessageBox.Show("Process Complete!");
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
                LoadAdmin();                                        //reset the administration views
            }
        }

        private void LoadAdmin()
        {
            panModifyUser.Visibility = Visibility.Hidden;           //Hide the modification panels as they are not needed yet
            panModifyFarm.Visibility = Visibility.Hidden;           //

            lstFarms.Items.Clear();                                 //Clean the list views
            lstUsers.Items.Clear();                                 //

            cboModuserRoles.Items.Clear();                          //Clean the combo boxes associated with modifying the users/farms
            cboModuserfarmlist.Items.Clear();                       //

            foreach (User u in DownloadData.GetAllUsers())          //get all users currently on server
                lstUsers.Items.Add(u);                              //add them into the list view

            cboModuserfarmlist.Items.Add("NONE");                   //Make the default/first option be nil
            foreach (FullFarm ff in DownloadData.GetAllFullFarms()) //get all farms currently on server
            {
                lstFarms.Items.Add(ff);                             //add the farms into the list view
                cboModuserfarmlist.Items.Add(ff.name);              //add the farm names into the combo box on modifying farms panel
            }

            for (PermissionLevel p = PermissionLevel.Admin; p < PermissionLevel.Guest + 1; p++) //increment through all the roles possible
                cboModuserRoles.Items.Add(p);                       //add the roles to the combo box for the user to select on modify user
        }



        private void butMenuLogout_Click(object sender, RoutedEventArgs e)
        {
            if (File.Exists(Utilities.SAVE_DATA_URL))
                File.Delete(Utilities.SAVE_DATA_URL);
            Application.Current.Shutdown();
        }

        private void butReloadAllUsers_Click(object sender, RoutedEventArgs e)
        {
            LoadAdmin();                                            //Reset/update the data/views on the administration pages
        }

        private void butModifyFarm_Click(object sender, RoutedEventArgs e)
        {
            panModifyFarm.Visibility = Visibility.Visible;          //Disable the main content and only display modify farm
            ContentMainAdmin.Visibility = Visibility.Hidden;        //
            try
            {
                FullFarm ff = (FullFarm)lstFarms.SelectedItems[0];  //get the first selected row from the farm list view and assign it to the textboxes as predefined text
                txtModuserFarmarea.Text = ff.area + "";             //
                txtModuserFarmid.Text = ff.id + "";                 //
                txtModuserFarmname.Text = ff.name;                  //
            }
            catch
            {
                panModifyFarm.Visibility = Visibility.Hidden;       //if something goes wrong, it is likely that the farm is not selected and returns back to the main content
                ContentMainAdmin.Visibility = Visibility.Visible;   //
                MessageBox.Show("Please select a farm");            //
            }
        }

        private void butModifyUser_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                User u = (User)lstUsers.SelectedItems[0];           //get the first selected row from the user list view and assign it to the textboxes as predefined text
                try
                {
                    Farm f = DownloadData.GetUserFarm(u.email);     //retrieve the farm details fromt the server that are assigned to the email address supplied
                    cboModuserRoles.SelectedIndex = (int)DownloadData.GetUserRole(u.email);//input the received data into the input fields
                    cboModuserfarmlist.SelectedIndex = f.farmid;    //
                }
                catch
                {
                    MessageBox.Show("Unable to get a farm assigned to this user");//return error if it fails
                }
                try
                {
                    txtModuserEmail.Text = u.email;                 //Set predefined values from the selected account in the list
                    txtModuserID.Text = u.id + "";                  //
                    txtModuserName.Text = u.name;                   //
                    txtModuserPassword.Password = "";               //Clear these textboxes to keep consistent
                    txtModuserPasswordConfirm.Password = "";        //


                    panModifyUser.Visibility = Visibility.Visible;  //Display the modification form if it made it this far
                    ContentMainAdmin.Visibility = Visibility.Hidden;//
                }
                catch
                {
                    MessageBox.Show("Error: Fortuna#326");          //Not sure what could cause this error however, during testing shit did go wrong here. Use as point of reference
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
            if (txtModuserPassword.Password == txtModuserPasswordConfirm.Password)//ensure the passwords match up
            {
                User u2 = new User();                               //establish temporary user object which will contain the old pre-edited values
                User u = new User();                                //establish temporary user associated with the new changes
                u.name = txtModuserName.Text;                       //assign values that are found in the input fields to the new user object
                u.password = txtModuserPassword.Password;           //
                u.email = txtModuserEmail.Text;                     //
                
                try
                {
                    u2 = (User)lstUsers.SelectedItems[0];           //put the old data from the list view into the old user object
                }
                catch
                {
                    MessageBox.Show("Please select a user");        
                }

                if (txtModuserPassword.Password != "" && txtModuserName.Text != "" && txtModuserEmail.Text != "")//ensure there is no empty data being uploaded
                {
                    if (txtModuserID.Text == "" || txtModuserID.Text == null)//is the user being created or modified? this textfield is read only so this dictates which of the two it is
                    {
                        UploadData.CreateUser(u);                   //attempt to create a new user
                        MessageBox.Show("User Created");
                    }
                    else
                    {
                        UploadData.UpdateUser(u2.email, u);         //attempt to modify an existing user
                        try
                        {
                            UploadData.AssignFarm(cboModuserfarmlist.SelectedIndex, u2.email);//update the farm assigned to this user
                            MessageBox.Show("Complete");
                        }
                        catch
                        {
                            MessageBox.Show("An error ocurred with assigning the farm id to this user");//report the error if any occur
                        }
                    }
                    UploadData.UpdatePermissions(txtModuserEmail.Text, cboModuserRoles.SelectedIndex);//update the user's role
                }
                else
                    MessageBox.Show("Please fill in all fields");
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

        private void butGenerateConsolidatedReport1_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("error");
        }

        private void butGenerateConsolidatedReport_Click(object sender, RoutedEventArgs e)
        {
            string date = cboColsonidatedReportDate.SelectedValue.ToString();
            Dictionary<int, string> dict = DownloadData.GetWeeklyFarmData(date);

            processConsolidated p = new processConsolidated();
            if (File.Exists(Utilities.LOCAL_REPORT_URL))
            {
                try
                {
                    File.Delete(Utilities.LOCAL_REPORT_URL);
                }
                catch
                {
                    MessageBox.Show("The previous report is currently open in another application. Please close the application that is using this file.");
                }
            }
            DateTime dt = DateTime.Parse(date);

            try
            {
                Directory.CreateDirectory(Utilities.SAVE_FOLDER);
                p.createWorkBook(Utilities.LOCAL_REPORT_URL, date, dict);
            }
            catch
            {
                MessageBox.Show("Something went wrong when attempting to create your consolidated report.");
            }
            MessageBox.Show("Process Complete!");
        }

        private void butOpenColsolidatedReport_Click(object sender, RoutedEventArgs e)
        {
            if (Directory.Exists(Utilities.SAVE_FOLDER))
                System.Diagnostics.Process.Start(Utilities.SAVE_FOLDER);
            else
                MessageBox.Show("There is currently no data available at this location");
        }

        private void butMenuWebsite_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.fortunagroup.net.nz/");
        }
    }
}