using kb_back;
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
    /// <summary>
    /// Логика взаимодействия для ArmamentWindow.xaml
    /// </summary>
    public abstract partial class BaseWindow : Window
    {
        private protected List<string> Input;
        private protected string name;
        private protected string action_type = "none";

        private protected void Add_Button_Click(object sender, RoutedEventArgs e)
        {
            action_type = "add";

            Input = new List<string>();

            foreach (TextBox t in Extensions.FindVisualChildren<TextBox>(this.Input_StackPanel))
            {
                Input.Add(t.Text);
            }

            try
            {
                if (this.GetType() == typeof(ArmamentWindow))
                {
                    ArmamentTools.Add(MainWindow.db, Input);
                }
                if (this.GetType() == typeof(EngineWindow))
                {
                    EngineTools.Add(MainWindow.db, Input);
                }
                if (this.GetType() == typeof(AircraftWindow))
                {
                    AircraftTools.Add(MainWindow.db, Input);
                }
                if(this.GetType() == typeof(EmployeeWindow))
                {
                    EmployeeTools.Add(MainWindow.db, Input);
                }
                if (this.GetType() == typeof(DepartmentWindow))
                {
                    DepartmentTools.Add(MainWindow.db, Input);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to add \n" + ex.Message);
            }

            TableRefresh();
        }

        private protected virtual void Edit_Button_Click(object sender, RoutedEventArgs e)
        {
            if (Table.SelectedItems.Count > 0)
            {
                action_type = "edit";
                dynamic value = Table.SelectedItem;
                name = value.Name;
                List<string> l = value.GetValues();

                InputLayoutUpdate(l);
            }
            else
            {
                MessageBox.Show("No cell is selected ");
            }
        }

        private protected virtual void Delete_Button_Click(object sender, RoutedEventArgs e)
        {
            action_type = "delete";
            InputLayoutUpdate(null);

            if (Table.SelectedItems.Count > 0)
            {
                dynamic value = Table.SelectedItem;

                try
                {
                    if (this.GetType() == typeof(ArmamentWindow))
                    {
                        ArmamentTools.Delete(MainWindow.db, value.Name);
                    }
                    if (this.GetType() == typeof(EngineWindow))
                    {
                        EngineTools.Delete(MainWindow.db, value.Name);
                    }
                    if (this.GetType() == typeof(AircraftWindow))
                    {
                        AircraftTools.Delete(MainWindow.db, value.Name);
                    }
                    if (this.GetType() == typeof(DepartmentWindow))
                    {
                        DepartmentTools.Delete(MainWindow.db, value.Name);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Failed to delete \n" + ex.Message);
                }
            }
            else
            {
                MessageBox.Show("No cell is selected ");
            }

            action_type = "none";
            InputLayoutUpdate(null);

            TableRefresh();
        }

        private protected virtual void Enter_Button_Click(object sender, RoutedEventArgs e)
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
                    if (this.GetType() == typeof(ArmamentWindow))
                    {
                        ArmamentTools.Edit(MainWindow.db, name, Input);
                    }
                    if (this.GetType() == typeof(EngineWindow))
                    {
                        EngineTools.Edit(MainWindow.db, name, Input);
                    }
                    if (this.GetType() == typeof(AircraftWindow))
                    {
                        AircraftTools.Edit(MainWindow.db, name, Input);
                    }
                    if (this.GetType() == typeof(AirframeWindow))
                    {
                        AirframeTools.Edit(MainWindow.db, name, Input);
                    }
                    if (this.GetType() == typeof(DepartmentWindow))
                    {
                        DepartmentTools.Edit(MainWindow.db, name, Input);
                    }
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

        private protected virtual void Search_Button_Click(object sender, RoutedEventArgs e)
        {
            action_type = "search";

            Input = new List<string>();

            foreach (TextBox t in Extensions.FindVisualChildren<TextBox>(this))
            {
                Input.Add(t.Text);
            }

            Table.ItemsSource = null;
            if (this.GetType() == typeof(ArmamentWindow))
            {
                Table.ItemsSource = ArmamentTools.Search(MainWindow.db, Input);
            }
            if (this.GetType() == typeof(EngineWindow))
            {
                Table.ItemsSource = EngineTools.Search(MainWindow.db, Input);
            }
            if (this.GetType() == typeof(AircraftWindow))
            {
                Table.ItemsSource = AircraftTools.Search(MainWindow.db, Input);
            }
            if (this.GetType() == typeof(AirframeWindow))
            {
                Table.ItemsSource = AirframeTools.Search(MainWindow.db, Input);
            }
            if (this.GetType() == typeof(EmployeeWindow))
            {
                Table.ItemsSource = EmployeeTools.Search(MainWindow.db, Input);
            }
            if (this.GetType() == typeof(DepartmentWindow))
            {
                Table.ItemsSource = DepartmentTools.Search(MainWindow.db, Input);
            }
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            e.Cancel = true;
            Visibility = Visibility.Hidden;

            action_type = "none";

            InputLayoutUpdate(null);
        }

        protected override void OnActivated(EventArgs e)
        {
            TableRefresh();
        }

        private protected abstract void TableRefresh();

        private protected abstract void InputLayoutUpdate(List<string> _l);

        private protected void SetInputTextBoxes(List<string> l)
        {
            int i = 0;
            foreach (TextBox t in Extensions.FindVisualChildren<TextBox>(Input_GroupBox))
            {
                t.Text = l[i];
                i++;
            }
        }
    }
}
