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

namespace kb_app.Windows
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static KbDbContext db;
        /*public EmployeeWindow employeeWindow;
        public DepartmentWindow departmentWindow;
        public ProjectWindow projectWindow*/
        public EngineWindow engineWindow;
        public ArmamentWindow armamentWindow;
        public AirframeWindow airframeWindow;
        public AircraftWindow aircraftWindow;

        public MainWindow()
        {
            InitializeComponent();
            Application.Current.MainWindow = this;
            Application.Current.ShutdownMode = ShutdownMode.OnMainWindowClose;
            db = new KbDbContext();
        }

        private void Employee_Button_Click(object sender, RoutedEventArgs e)
        {
        }

        private void Department_Button_Click(object sender, RoutedEventArgs e)
        {
        }

        private void Project_Button_Click(object sender, RoutedEventArgs e)
        {

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