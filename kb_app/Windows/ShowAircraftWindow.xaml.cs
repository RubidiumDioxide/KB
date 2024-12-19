using kb_back.Tools;

using System;
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

namespace kb_app.Windows
{
    public partial class ShowAircraftWindow : Window
    {
        public string aircraftName;

        public ShowAircraftWindow()
        {
            InitializeComponent();
        }

        protected override void OnActivated(EventArgs e)
        {
            base.OnActivated(e);
            ContentRefresh();
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            e.Cancel = true;
            Visibility = Visibility.Hidden;
        }

        private void ContentRefresh()
        {
            string l = EngineTools.GetEngineString(MainWindow.db, aircraftName);

            Engine_TextBlock.Text = l;


            l = AirframeTools.GetAirframeString(MainWindow.db, aircraftName);
            
            Airframe_TextBlock.Text = l;

            AircraftsArmament_Table.ItemsSource = null;
            AircraftsArmament_Table.ItemsSource = ArmamentTools.GetArmamentsList(MainWindow.db, aircraftName);

        }
    }
}
