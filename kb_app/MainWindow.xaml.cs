using kb_back;
using kb_back.Entities;
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

namespace kb_app
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static KbDbContext db;
        public EmployeeWindow employeeWindow;
        public DepartmentWindow departmentWindow;
        public ProjectWindow projectWindow;
        public EngineWindow engineWindow;
        public ArmamentWindow armamentWindow;
        public AirframeWindow airframeWindow;

        public MainWindow()
        {
            InitializeComponent();
            Application.Current.MainWindow = this;
            Application.Current.ShutdownMode = ShutdownMode.OnMainWindowClose;
            db = new KbDbContext();
            //db.Employees.Load();
            //db.Projects.Load();
            //db.BookReaders.Load();
        }

        private void Employee_Button_Click(object sender, RoutedEventArgs e)
        {
            if (employeeWindow == null)
            {
                employeeWindow = new EmployeeWindow();
            }

            employeeWindow.Show();
            //employeeWindow.LinkEvents();
            employeeWindow.Focus();
        }

        private void Department_Button_Click(object sender, RoutedEventArgs e)
        {
            if (departmentWindow == null)
            {
                departmentWindow = new DepartmentWindow();
            }

            departmentWindow.Show();
            //employeeWindow.LinkEvents();
            departmentWindow.Focus();
        }

        private void Project_Button_Click(object sender, RoutedEventArgs e)
        {
            if (projectWindow == null)
            {
                projectWindow = new ProjectWindow();
            }

            projectWindow.Show();
            //projectWindow.LinkEvents();
            projectWindow.Focus();
        }

        private void Engine_Button_Click(object sender, RoutedEventArgs e)
        {
            if (engineWindow == null)
            {
                engineWindow = new EngineWindow();
            }

            engineWindow.Show();
            //employeeWindow.LinkEvents();
            engineWindow.Focus();
        }

        private void Armament_Button_Click(object sender, RoutedEventArgs e)
        {
            if (armamentWindow == null)
            {
                armamentWindow = new ArmamentWindow();
            }

            armamentWindow.Show();
            //employeeWindow.LinkEvents();
            armamentWindow.Focus();
        }

        private void Airframe_Button_Click(object sender, RoutedEventArgs e)
        {
            if (airframeWindow == null)
            {
                airframeWindow = new AirframeWindow();
            }

            airframeWindow.Show();
            //employeeWindow.LinkEvents();
            airframeWindow.Focus();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}