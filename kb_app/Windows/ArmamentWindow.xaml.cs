using kb_back;
using kb_back.Tools;
using kb_back.Entities;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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
using System.Xml.Linq;

using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;
using Microsoft.Identity.Client;

namespace kb_app
{
    /// <summary>
    /// Логика взаимодействия для ArmamentWindow.xaml
    /// </summary>
    public partial class ArmamentWindow : Window
    {
        //private ArmamentInput armamentInput = null;
        private List<string> Input;
        private string name;
        private string action_type = "none"; 

        public ArmamentWindow()
        {
            InitializeComponent();
            ArmamentTableRefresh();
        }

        public void Add_Button_Click(object sender, RoutedEventArgs e)
        {
            action_type = "add";

            InputLayoutUpdate(null);

            Input = new List<string>();

            foreach (TextBox t in Extensions.FindVisualChildren<TextBox>(this))
            {
                Input.Add(t.Text);
            }

            try
            {
                ArmamentTools.Add(MainWindow.db, Input);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to add an armament \n" + ex.Message);
            }
        }

        public void Edit_Button_Click(object sender, RoutedEventArgs e)
        { 
            if (Armament_Table.SelectedItems.Count > 0)
            {
                action_type = "edit";

                //get Name of the selected row and it's value list 
                dynamic value = Armament_Table.SelectedItem;
                name = value.Name;
                List<string> l = value.GetValues();

                InputLayoutUpdate(l);

                //armamentInput.SetTexts(l);
            }
            else
            {
                MessageBox.Show("No cell is selected ");
            }
        }

        public void Delete_Button_Click(object sender, RoutedEventArgs e)
        {
            action_type = "delete";

            InputLayoutUpdate(null);

            if (Armament_Table.SelectedItems.Count > 0)
            {
                //get Name of the selected row 
                dynamic value = Armament_Table.SelectedItem;
                //delete function
                try
                {
                    ArmamentTools.Delete(MainWindow.db, value.Name);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Failed to delete an Armament \n" + ex.Message);
                }
            }
            else
            {
                MessageBox.Show("No cell is selected ");
            }

            ArmamentTableRefresh();
        }

        public void Enter_Button_Click(object sender, RoutedEventArgs e)
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
                    ArmamentTools.Edit(MainWindow.db, name, Input);
                }
                catch(Exception ex)
                {
                    MessageBox.Show("Failed to edit an armament \n" + ex.Message);
                }
            }

            ArmamentTableRefresh();
        }

        private void Search_Button_Click(object sender, RoutedEventArgs e)
        {
            action_type = "search";

            InputLayoutUpdate(null);

            Input = new List<string>();

            foreach (TextBox t in Extensions.FindVisualChildren<TextBox>(this))
            {
                Input.Add(t.Text);
            }

            Armament_Table.ItemsSource = null;
            Armament_Table.ItemsSource = ArmamentTools.Search(MainWindow.db, Input);
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            e.Cancel = true;
            Visibility = Visibility.Hidden;
            
            action_type = "none"; 
            
            InputLayoutUpdate(null); 
        }

        public void ArmamentTableRefresh()
        {
            Armament_Table.ItemsSource = null;
            Armament_Table.ItemsSource = ArmamentTools.LoadTable(MainWindow.db);
        }

        public void InputLayoutUpdate(List<string> _l)
        {
            if (action_type == "none" || action_type == "add" || action_type == "search")
            {
                List<string> l = new List<string> { "Name (string)", "Caliber (float)", "Firing rate (float)", "Weight (float)"};
                SetInputTextBoxes(l);

                Enter_Button.IsEnabled = false;
                Enter_Button.Visibility = Visibility.Collapsed;
            }

            if(action_type == "edit")
            {
                SetInputTextBoxes(_l);

                Enter_Button.IsEnabled = true;
                Enter_Button.Visibility = Visibility.Visible;
            }
        }

        public void SetInputTextBoxes(List<string> l)
        {
            int i = 0;
            foreach (TextBox t in Extensions.FindVisualChildren<TextBox>(this.Input_GroupBox))
            {
                t.Text = l[i];
                i++;
            }
        }
    }
}
