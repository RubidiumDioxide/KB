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
    public partial class AircraftWindow : BaseWindow
    {
        private TextBox Name_Textbox = new TextBox() { Text = "Name (string)" };
        private TextBox Type_Textbox = new TextBox() { Text = "Type (string)" };
        private TextBox Crew_Textbox = new TextBox() { Text = "Crew (byte)" };
        private TextBox Weight_Textbox = new TextBox() { Text = "Weight (double)" };
        private TextBox Engine_Textbox = new TextBox() { Text = "Engine (string)" };

        public AircraftWindow() : base()
        {
            InitializeComponent();
            TableRefresh();

            StackPanel sp = Input_StackPanel;
            sp.Children.Remove(Enter_Button);
            sp.Children.Add(Name_Textbox);
            sp.Children.Add(Type_Textbox);
            sp.Children.Add(Crew_Textbox);
            sp.Children.Add(Weight_Textbox);
            sp.Children.Add(Engine_Textbox);
            sp.Children.Add(Enter_Button);

            Input_GroupBox.Content = sp;
        }

        private protected override void TableRefresh()
        {
            Table.ItemsSource = null;
            Table.ItemsSource = AircraftTools.LoadTable(MainWindow.db);
        }

        private protected override void InputLayoutUpdate(List<string> _l)
        {
            if (action_type == "none" || action_type == "add" || action_type == "search")
            {
                List<string> l = new List<string> { "Name (string)", "Type (string)", "Crew (byte)", "Weight (double)", "Engine (string)" };
                SetInputTextBoxes(l);

                Name_Textbox.IsEnabled = true;

                Enter_Button.IsEnabled = false;
                Enter_Button.Visibility = Visibility.Collapsed;
            }

            if (action_type == "edit")
            {
                SetInputTextBoxes(_l);

                Name_Textbox.IsEnabled = false;

                Enter_Button.IsEnabled = true;
                Enter_Button.Visibility = Visibility.Visible;
            }
        }
    }
}
