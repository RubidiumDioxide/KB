using kb_back.Tools;

using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using kb_back;

namespace kb_app.Windows
{
    public partial class DepartmentWindow : BaseWindow
    {
        private TextBox Name_TextBox = new TextBox() { Text = "Name (string)" };
        private TextBox Adress_TextBox = new TextBox() { Text = "Adress (string)" };
        private TextBox Director_TextBox = new TextBox() { Text = "Director (int)" };

        public DepartmentWindow() : base()
        {
            InitializeComponent();
            TableRefresh(); 

            Input_StackPanel.Children.Clear();
            Input_StackPanel.Children.Add(Name_TextBox);
            Input_StackPanel.Children.Add(Adress_TextBox);
            Input_StackPanel.Children.Add(Director_TextBox);
            Input_StackPanel.Children.Add(Enter_Button);
            Input_GroupBox.Content = Input_StackPanel;
        }

        private protected override void TableRefresh()
        {
            Table.ItemsSource = null;
            Table.ItemsSource = DepartmentTools.LoadTable(MainWindow.db);
        }

        private protected override void InputLayoutUpdate(List<string> _l)
        {
            if (action_type == "none" || action_type == "add" || action_type == "search")
            {
                List<string> l = new List<string> {"Name (string)", "Adress(string)", "Director (int)"};
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
