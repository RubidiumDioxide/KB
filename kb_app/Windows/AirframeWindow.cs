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
    public class AirframeWindow : BaseWindow
    {
        private TextBox Name_Textbox = new TextBox() { Text = "Name (string)" };
        private TextBox WingProfile_Textbox = new TextBox() { Text = "WingProfile (string)" };
        private TextBox Length_Textbox = new TextBox() { Text = "Length (double)" };
        private TextBox Wingspan_Textbox = new TextBox() { Text = "Wingspan (double)" };

        public AirframeWindow() : base()
        {
            InitializeComponent();
            TableRefresh();

            Add_Button.IsEnabled = false;
            Add_Button.Visibility = Visibility.Collapsed; 
            Delete_Button.IsEnabled = false;
            Delete_Button.Visibility = Visibility.Collapsed;

            StackPanel sp = Input_StackPanel;
            sp.Children.Remove(Enter_Button);
            sp.Children.Add(Name_Textbox);
            sp.Children.Add(WingProfile_Textbox);
            sp.Children.Add(Length_Textbox);
            sp.Children.Add(Wingspan_Textbox);
            sp.Children.Add(Enter_Button);

            Input_GroupBox.Content = sp;
        }

        private protected override void TableRefresh()
        {
            Table.ItemsSource = null;
            Table.ItemsSource = AirframeTools.LoadTable(MainWindow.db);
        }

        private protected override void InputLayoutUpdate(List<string> _l)
        {
            if (action_type == "none" || action_type == "add" || action_type == "search")
            {
                List<string> l = new List<string> { "Name (string)", "WingProfile (string)", "Length (double)", "Wingspan (double)" };
                SetInputTextBoxes(l);

                Name_Textbox.IsEnabled = true;
                Enter_Button.IsEnabled = false;
                Enter_Button.Visibility = Visibility.Collapsed;
            }

            if (this.action_type == "edit")
            {
                SetInputTextBoxes(_l);

                Name_Textbox.IsEnabled = false;
                Enter_Button.IsEnabled = true;
                Enter_Button.Visibility = Visibility.Visible;
            }
        }
    }   
}
