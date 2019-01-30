using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace MSSQLDBtoXML
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {


        //Exclude table
        /* string[] ttables = new string[] {
             "Actuator","ActuatorConditionResult","Costing",
             "CurveCondition","LineItem","LineItemAttribute",
             "OperatingCondition","OperatingConditionResult",
             "OperatingConditionSizingSummary","OpertingConditionDataSheet",
             "PricingSummary","PricingSummaryNotes","ProductCharacteristicsCurve",
             "ProductCostSummary","ProductCostSummaryNonList","ProductDataSheet",
             "ProductFaceToFaceDrawing","ProductSizingSummary","ProjectHeader",
             "ProjectMember","ProjectPreference","ProjectSystem","ProjectUnit","ProposalContents",
             "ProposalFileTable","ResultSizingSummary","RevisionList","UserUnit","UserPreference","UserGroup","User","UnitType",
             "Unit","constant","ActuatorTable","dtoUserDetails","sysdiagrams","tmpattributeconfig"
         };*/


        private LoaderViewModel loaderMsg;
        public MainWindowViewModel()
        {
            Host = @"";
            Database = ""; 
            UserName = "";
            FileName = "";

            CloseCommand = new RelayCommand(isExecute, Close);
            BrowseCommand = new RelayCommand(isExecute, Browse);
            ExecuteCommand = new RelayCommand(isExecuteable, Execute);
            TestCommand = new RelayCommand(isExecuteable, TestConnection);
        }

        private void TestConnection(object obj)
        {
            var pwd = obj as PasswordBox;
            Password = pwd.Password;

            if (isConnected())
            {
                MessageBox.Show("Connection success");
            }
            else
            {
                MessageBox.Show("Connection failed, please try again");
            }
        }

        private bool isConnected()
        {
            string cns = $"Data Source={Host};Initial Catalog={Database};User id={UserName};Password={Password};";
            SqlConnection cn = new SqlConnection(cns);
            try
            {
                cn.Open();
                return true;
            }
            catch
            {
                return false;
            }
            finally
            {
                cn.Close();
            }
        }

        private async void Execute(object obj)
        {

            var pwd = obj as PasswordBox;
            Password = pwd.Password;
            if (Password.Length == 0)
            {
                MessageBox.Show("Please Put Password");
                return;
            }
            //testing dolo..
            if (!isConnected())
            {
                MessageBox.Show("Connection failed, please try again");
                return;
            }

            LoadingBar wnd = new LoadingBar();
            loaderMsg = new LoaderViewModel();
            wnd.DataContext = loaderMsg;
            wnd.Show();
            await Task.Run(() => Transfer());
            wnd.Close();
            MessageBox.Show("Done");
        }
        //taro di system sychronize..
        private void Transfer()
        {
            try
            {
            // Set Configuration
                string cns = $"Data Source={Host};Initial Catalog={Database};User id={UserName};Password={Password};";
                SqlConnection conn = new SqlConnection(cns);

                    // Get Table Names
                conn.Open();

                var tbSchemas = conn.GetSchema("Tables", new string[] { null, null, null, "BASE TABLE" });

                conn.Close();

                List<string> tables = new List<string>();
                loaderMsg.StatusMessages = $"Getting Table List... ";

                // Add Master Table 
                foreach (var tb in tbSchemas.Rows)
                {
                    var drw = (DataRow)tb;
                    var ctb = drw["TABLE_NAME"].ToString();
                    /*if (ttables.Contains(ctb))
                    {
                        continue;
                    }*/
                    tables.Add(ctb);
                }

                SqlCommand cmd;

                // Get Version and Last Update
                List<Tuple<string, string, DateTime>> readerResult = new List<Tuple<string, string, DateTime>>();

                DataSet ds = new DataSet("SmartSiz");
                SqlDataAdapter adapter;
                foreach (var tb in tables)
                {
                    loaderMsg.StatusMessages = $"Processing {tb}...";
                    cmd = new SqlCommand(String.Format("SELECT * FROM [dbo].[{0}]", tb), conn);
                    adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(ds, tb);
                }

                ds.WriteXml(FileName, XmlWriteMode.WriteSchema);

            }catch (Exception ex)
             {
                    MessageBox.Show(ex.Message);
             }
        }

        private bool isExecuteable(object obj)
        {

            //String plainStr = new System.Net.NetworkCredential(string.Empty, Password).Password;

            bool   hasil = (Host.Length != 0) && (Database.Length != 0) && (UserName.Length != 0) && (FileName.Length != 0);
            return hasil;
        }

        private void Browse(object obj)
        {
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.Filter = "xml files|*.xml|All Files |*.*";
            if (dlg.ShowDialog()==true)
            {
                FileName = dlg.FileName;
            }
        }

        private bool isExecute(object obj)
        {
            return true;
        }

        private void Close(object obj)
        {
            Window wnd = obj as Window;
            wnd.Close();
        }

        #region Property
        private string _host;

        public string Host
        {
            get { return _host; }
            set { _host = value;
                onPropertyChanged("Host");
            }
        }

        private string _database;

        public string Database
        {
            get { return _database; }
            set {
                _database = value;
                onPropertyChanged("Database");
            }
        }

        private string _username;

        public string UserName
        {
            get { return _username; }
            set { _username = value;
                onPropertyChanged("UserName");
            }
        }

        private string _password;

        public string Password
        {
            get { return _password; }
            set { _password = value;
                onPropertyChanged("Password");
            }
        }

        private string _filename;

        public string FileName
        {
            get { return _filename; }
            set { _filename = value;
                onPropertyChanged("FileName");
            }
        }


        private ICommand _browseCommand;

        public ICommand BrowseCommand
        {
            get { return _browseCommand; }
            set {
                _browseCommand = value;
                onPropertyChanged("BrowseCommand");
            }
        }

        private ICommand _closeCommand;

        public ICommand CloseCommand
        {
            get { return _closeCommand; }
            set { _closeCommand = value;
                onPropertyChanged("CloseCommand");
            }
        }

        private ICommand _executeCommand;

        public ICommand ExecuteCommand
        {
            get { return _executeCommand; }
            set
            {
                _executeCommand = value;
                onPropertyChanged("ExecuteCommand");
            }
        }


        private ICommand _testCommand;

        public ICommand TestCommand
        {
            get { return _testCommand; }
            set
            {
                _testCommand = value;
                onPropertyChanged("TestCommand");
            }
        }

        #endregion


        #region "Notify Implementation"

        public event PropertyChangedEventHandler PropertyChanged;

        public void onPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #endregion

    }


    #region Command Class
    public class RelayCommand : ICommand
    {
        private Predicate<object> _canExecute;
        private Action<object> _execute;

        public RelayCommand(Predicate<object> canExecute, Action<object> execute)
        {
            this._canExecute = canExecute;
            this._execute = execute;
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object parameter)
        {
            return _canExecute(parameter);
        }

        public void Execute(object parameter)
        {
            _execute(parameter);
        }
    }

    #endregion
}
