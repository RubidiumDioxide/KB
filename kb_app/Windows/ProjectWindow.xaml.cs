using kb_back;
using kb_back.Tools;
using System;
using System.Data;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace kb_app
{
    public partial class ProjectWindow : Window
    {
        public ProjectWindow()
        {
            InitializeComponent();
            ProjectTableRefresh();
        }
        private void Add_Project_Button_Click(object sender, RoutedEventArgs e)
        {

        }

        protected override void OnClosing(CancelEventArgs e)
        {
            e.Cancel = true;
            this.Visibility = Visibility.Hidden;
        }

        public void ProjectTableRefresh()
        {
            this.Project_Table.ItemsSource = null; 
            this.Project_Table.ItemsSource = ProjectTools.LoadTable(MainWindow.db);
        }
    }
}
