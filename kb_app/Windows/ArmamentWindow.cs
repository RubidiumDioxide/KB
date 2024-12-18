using kb_back.Tools;

using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

/*using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Xml.Linq;

using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;
using Microsoft.Identity.Client;*/

namespace kb_app.Windows
{
    public partial class ArmamentWindow : BaseWindow
    { 
        public ArmamentWindow() : base()
        {
            InitializeComponent();
            TableRefresh();

            TextBox Name_Texbox = new TextBox() {Text = "Name (string)"};
            TextBox Caliber_Texbox = new TextBox() { Text = "Caliber (double)" };
            TextBox FiringRate_Texbox = new TextBox() { Text = "FiringRate (double)" };
            TextBox Weight_Texbox = new TextBox() { Text = "Weight (double)" };

            StackPanel sp = Input_StackPanel;
            sp.Children.Remove(Enter_Button);
            sp.Children.Add(Name_Texbox);
            sp.Children.Add(Caliber_Texbox);
            sp.Children.Add(FiringRate_Texbox);
            sp.Children.Add(Weight_Texbox);
            sp.Children.Add(Enter_Button);

            Input_GroupBox.Content = sp;
        }

        private protected override void TableRefresh()
        {
            Table.ItemsSource = null;
            Table.ItemsSource = ArmamentTools.LoadTable(MainWindow.db);
        }

        private protected override void InputLayoutUpdate(List<string> _l)
        {
            if (action_type == "none" || action_type == "add" || action_type == "search")
            {
                List<string> l = new List<string> { "Name (string)", "Caliber (float)", "Firing rate (double)", "Weight (double)" };
                SetInputTextBoxes(l);

                Enter_Button.IsEnabled = false;
                Enter_Button.Visibility = Visibility.Collapsed;
            }

            if (action_type == "edit")
            {
                SetInputTextBoxes(_l);

                Enter_Button.IsEnabled = true;
                Enter_Button.Visibility = Visibility.Visible;
            }
        }
    }
}
