using kb_back;
using kb_back.Tools;

using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kb_app.Windows
{
    public partial class ProjectWindow : BaseWindow 
    {
        private int id; 

        private TextBox Id_TextBox = new TextBox() { Text = "Id (int)" };
        private TextBox Name_TextBox = new TextBox() { Text = "Name (string)" };
        private TextBox Aircraft_TextBox = new TextBox() { Text = "Aircraft (string)" };
        private TextBox Department_TextBox = new TextBox() { Text = "Department (string)" };
        private TextBox Status_TextBox = new TextBox() { Text = "Status (string)" };
        private TextBox DateBegan_TextBox = new TextBox() { Text = "DateBegan (DateOnly)" };
        private TextBox DateFinished_TextBox = new TextBox() { Text = "DateFinished (DateOnly)" };
        private TextBox ChiefDesigner_TextBox = new TextBox() { Text = "ChiefDesigner (int)" };

        public ProjectWindow() : base()
        {
            InitializeComponent();
            TableRefresh();

            Input_StackPanel.Children.Clear();
            Input_StackPanel.Children.Add(Id_TextBox);
            Input_StackPanel.Children.Add(Name_TextBox);
            Input_StackPanel.Children.Add(Aircraft_TextBox);
            Input_StackPanel.Children.Add(Department_TextBox);
            Input_StackPanel.Children.Add(Status_TextBox);
            Input_StackPanel.Children.Add(DateBegan_TextBox);
            Input_StackPanel.Children.Add(DateFinished_TextBox);
            Input_StackPanel.Children.Add(ChiefDesigner_TextBox);
            Input_StackPanel.Children.Add(Enter_Button); 
            Input_GroupBox.Content = Input_StackPanel; 
        }

        private protected override void Edit_Button_Click(object sender, RoutedEventArgs e)
        {
            if (Table.SelectedItems.Count > 0)
            {
                action_type = "edit";

                //get Name of the selected row and it's value list 
                dynamic value = Table.SelectedItem;
                id = value.Id;
                List<string> l = value.GetValues();

                InputLayoutUpdate(l);
            }
            else
            {
                MessageBox.Show("No cell is selected ");
            }
        }

        private protected override void Delete_Button_Click(object sender, RoutedEventArgs e)
        {
            action_type = "delete";
            InputLayoutUpdate(null);

            if (Table.SelectedItems.Count != 0)
            {
                dynamic value = Table.SelectedItem;

                try
                {
                    ProjectTools.Delete(MainWindow.db, value.Id);
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
                    ProjectTools.Edit(MainWindow.db, id, Input);
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
            Table.ItemsSource = ProjectTools.LoadTable(MainWindow.db); 
        }

        private protected override void InputLayoutUpdate(List<string> _l)
        {
            List<string> l = new List<string> { "Id (int)", "Name (string)", "Aircraft (string)", "Department (string)", "Status (string)", "DateBegan (DateOnly)", "DateFinished (DateOnly)", "ChiefDesigner (int)" };

            if (action_type == "none" || action_type == "search" || action_type == "add")
            {
                SetInputTextBoxes(l);

                Id_TextBox.IsEnabled = true;
                DateBegan_TextBox.IsEnabled = true;
                DateFinished_TextBox.IsEnabled = true;

                Enter_Button.IsEnabled = false;
                Enter_Button.Visibility = Visibility.Collapsed;
            }

            if (action_type == "edit")
            {
                SetInputTextBoxes(_l);

                Id_TextBox.IsEnabled = false;
                DateBegan_TextBox.IsEnabled = false;
                DateFinished_TextBox.IsEnabled = false;

                Enter_Button.IsEnabled = true;
                Enter_Button.Visibility = Visibility.Visible;
            }
        }
    }
}
