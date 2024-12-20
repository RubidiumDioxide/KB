using kb_back.Tools;

using System.ComponentModel;
using System.Configuration;
using System.DirectoryServices;
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

        private TextBox Armament_Textbox = new TextBox() { Text = "Armament (string)" };

        private Button Show_Button = new Button() { Content = "Show" };
        private Button AddArmament_Button = new Button() { Content = "Add Armament" };
        private Button ClearArmament_Button = new Button() { Content = "Clear Armament" };

        private ShowAircraftWindow showAircraftWindow = new ShowAircraftWindow();

        public AircraftWindow() : base()
        {
            InitializeComponent();
            TableRefresh();

            StackPanel sp = Input_StackPanel;
            sp.Children.Clear();
            sp.Children.Add(Name_Textbox);
            sp.Children.Add(Type_Textbox);
            sp.Children.Add(Crew_Textbox);
            sp.Children.Add(Weight_Textbox);
            sp.Children.Add(Engine_Textbox);
            Input_GroupBox.Content = sp;


            this.grid.Children.Add(Armament_Textbox);
            Armament_Textbox.SetValue(Grid.RowProperty, 2);
            Armament_Textbox.SetValue(Grid.ColumnProperty, 2);
            Armament_Textbox.IsEnabled = false;
            Armament_Textbox.Visibility = Visibility.Collapsed;

            sp.Children.Add(Enter_Button);

            Controls_StackPanel.Children.Add(Show_Button);
            Controls_StackPanel.Children.Add(AddArmament_Button);
            Controls_StackPanel.Children.Add(ClearArmament_Button);
            Show_Button.IsEnabled = true;
            AddArmament_Button.IsEnabled = true;
            Show_Button.IsEnabled = true;
            Show_Button.Click += Show_Button_Click;
            AddArmament_Button.Click += AddArmament_Button_Click;
            ClearArmament_Button.Click += ClearArmament_Button_Click;
        }

        public void Show_Button_Click (object sender, EventArgs e)
        {
            if(Table.SelectedItems.Count > 0)
            {
                dynamic value = Table.SelectedItem;
                name = value.Name;
                
                showAircraftWindow.aircraftName = name;
                showAircraftWindow.Show();
                showAircraftWindow.Focus();
            }
            else
            {
                MessageBox.Show("No cell is selected ");
            }
        }

        public void AddArmament_Button_Click(object sender, EventArgs e)
        {
            if (Table.SelectedItems.Count > 0)
            {
                action_type = "add armament";

                dynamic value = Table.SelectedItem;
                name = value.Name;
                InputLayoutUpdate(null); 
            }
            else
            {
                MessageBox.Show("No cell is selected ");
            }
        }

        public void ClearArmament_Button_Click(object sender, EventArgs e)
        {
            if (Table.SelectedItems.Count > 0)
            {
                dynamic value = Table.SelectedItem;
                name = value.Name;

                try
                {
                    AircraftTools.ClearArmament(MainWindow.db, name);
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
            else
            {
                MessageBox.Show("No cell is selected ");
            }
        }

        private protected override void Enter_Button_Click(object sender, RoutedEventArgs e)
        {
            Input = new List<string>();

            foreach (TextBox t in Extensions.FindVisualChildren<TextBox>(this))
            {
                Input.Add(t.Text);
            }

            if (action_type == "edit")
            {
                try
                {
                    AircraftTools.Edit(MainWindow.db, name, Input);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Failed to edit \n" + ex.Message);
                }
            }
            
            if(action_type == "add armament")
            {
                try
                {
                    AircraftTools.AddArmament(MainWindow.db, name, Armament_Textbox.Text);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Failed to edit \n" + ex.Message);
                }
            }

            action_type = "none";
            InputLayoutUpdate(null);

            TableRefresh();
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

                foreach (TextBox t in Extensions.FindVisualChildren<TextBox>(this))
                {
                    t.IsEnabled = true;
                }

                Armament_Textbox.IsEnabled = false;
                Armament_Textbox.Visibility = Visibility.Collapsed;

                Enter_Button.IsEnabled = false;
                Enter_Button.Visibility = Visibility.Collapsed;
            }

            if (action_type == "add armament")
            {
                List<string> l = new List<string> { "Name (string)", "Type (string)", "Crew (byte)", "Weight (double)", "Engine (string)" };
                SetInputTextBoxes(l);

                foreach (TextBox t in Extensions.FindVisualChildren<TextBox>(this))
                {
                    t.IsEnabled = false;
                }

                Armament_Textbox.IsEnabled = true;
                Armament_Textbox.Visibility = Visibility.Visible;

                Enter_Button.IsEnabled = true;
                Enter_Button.Visibility = Visibility.Visible;
            }

            if (action_type == "edit")
            {
                SetInputTextBoxes(_l);

                foreach (TextBox t in Extensions.FindVisualChildren<TextBox>(this))
                {
                    t.IsEnabled = true;
                }

                Armament_Textbox.IsEnabled = false;
                Armament_Textbox.Visibility = Visibility.Collapsed;

                Enter_Button.IsEnabled = true;
                Enter_Button.Visibility = Visibility.Visible;
            }
        }
    }
}
