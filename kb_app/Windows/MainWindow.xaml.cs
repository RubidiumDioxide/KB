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

//using Microsoft.EntityFrameworkCore;       remove the usage of EF to kb_back if possible

using System.Data;
using kb_back;
using Microsoft.EntityFrameworkCore;

namespace kb_app.Windows
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public LoginWindow loginWindow;
        public string connectionString = "";
        private bool isLoginWindowOpened = false; 
        private string mode = "engineer"; 

        public static KbDbContext db;
        public EmployeeWindow employeeWindow;
        public DepartmentWindow departmentWindow;
        public ProjectWindow projectWindow;
        public EngineWindow engineWindow;
        public ArmamentWindow armamentWindow;
        public AirframeWindow airframeWindow;
        public AircraftWindow aircraftWindow;

        public MainWindow()
        {
            InitializeComponent();
            Application.Current.MainWindow = this;
            Application.Current.ShutdownMode = ShutdownMode.OnMainWindowClose;

            loginWindow = new LoginWindow();
            loginWindow.inputEntered += new EventHandler(loginWindowEntered);
            isLoginWindowOpened = true;
            loginWindow.Show();
            loginWindow.Focus();

            Employee_Button.IsEnabled = false; 
            Department_Button.IsEnabled = false; 
            Project_Button.IsEnabled = false; 
            Aircraft_Button.IsEnabled = false; 
            Engine_Button.IsEnabled = false;
            Armament_Button.IsEnabled = false; 
            Airframe_Button.IsEnabled = false; 
        }

        private void loginWindowEntered(object sender, EventArgs e)
        {
            mode = loginWindow.username;

            this.connectionString = "Server=WIN-4E7JKGBR3SV\\SQLEXPRESS;Database=kb_DB;TrustServerCertificate=True;Encrypt=False;user id=" + loginWindow.username + ";password=" + loginWindow.password + ";";

            try
            {
                db = new KbDbContext(connectionString);
                db.Aircraft.Load(); //purely to see if it throws an exception
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                return; 
            }

            loginWindow.Close();
            isLoginWindowOpened = false;

            if (mode == "engineer")
            {
                Aircraft_Button.IsEnabled = true; 
                Engine_Button.IsEnabled = true; 
                Armament_Button.IsEnabled = true; 
                Airframe_Button.IsEnabled = true; 
  
            }
            if(mode == "manager") 
            {
                Employee_Button.IsEnabled = true;
                Department_Button.IsEnabled = true;
                Project_Button.IsEnabled = true;
            }
        }

        private void Employee_Button_Click(object sender, RoutedEventArgs e)
        {
            if (employeeWindow == null)
            {
                employeeWindow = new EmployeeWindow();
            }

            employeeWindow.Show();
            employeeWindow.Focus();
        }

        private void Department_Button_Click(object sender, RoutedEventArgs e)
        {
            if (departmentWindow == null)
            {
                departmentWindow = new DepartmentWindow();
            }

            departmentWindow.Show();
            departmentWindow.Focus();
        }

        private void Project_Button_Click(object sender, RoutedEventArgs e)
        {
            if (projectWindow == null)
            {
                projectWindow = new ProjectWindow();
            }

            projectWindow.Show();
            projectWindow.Focus();
        }

        private void Engine_Button_Click(object sender, RoutedEventArgs e)
        {
            if (engineWindow == null)
            {
                engineWindow = new EngineWindow();
            }

            engineWindow.Show();
            engineWindow.Focus();
        }

        private void Armament_Button_Click(object sender, RoutedEventArgs e)
        {
            if (armamentWindow == null)
            {
                armamentWindow = new ArmamentWindow();
            }

            armamentWindow.Show();
            armamentWindow.Focus();
        }

        private void Airframe_Button_Click(object sender, RoutedEventArgs e)
        {
            if (airframeWindow == null)
            {
                airframeWindow = new AirframeWindow();
            }

            airframeWindow.Show();
            airframeWindow.Focus();
        }
        private void Aircraft_Button_Click(object sender, RoutedEventArgs e)
        {
            if (aircraftWindow == null)
            {
                aircraftWindow = new AircraftWindow();
            }

            aircraftWindow.Show();
            aircraftWindow.Focus();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}