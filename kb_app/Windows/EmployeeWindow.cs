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
    public partial class EmployeeWindow : BaseWindow 
    {
        private int id;

        private TextBox Id_TextBox = new TextBox() { Text = "Id (int)" };
        private TextBox Surname_TextBox = new TextBox() { Text = "Surname (string)" };
        private TextBox FirstName_TextBox = new TextBox() { Text = "FirstName (string)" };
        private TextBox LastName_TextBox = new TextBox() { Text = "LastName (string)" };
        private TextBox DateOfBirth_TextBox = new TextBox() { Text = "DateOfBirth (DateOnly)" };
        private TextBox Position_TextBox = new TextBox() { Text = "Position (string)" };
        private TextBox Department_TextBox = new TextBox() { Text = "Department (string)" };
        private TextBox YearsOfExperience_TextBox = new TextBox() { Text = "YearsOfExperience (byte)" };
        private TextBox CurrentProject_TextBox = new TextBox() { Text = "CurrentProject (int)" };
        private TextBox Salary_TextBox = new TextBox() { Text = "Salary (decimal)" }; 

        public EmployeeWindow() : base()
        {
            InitializeComponent();
            TableRefresh();

            Input_StackPanel.Children.Clear();
            Input_StackPanel.Children.Add(Id_TextBox);
            Input_StackPanel.Children.Add(Surname_TextBox);
            Input_StackPanel.Children.Add(FirstName_TextBox);
            Input_StackPanel.Children.Add(LastName_TextBox);
            Input_StackPanel.Children.Add(DateOfBirth_TextBox);
            Input_StackPanel.Children.Add(Position_TextBox);
            Input_StackPanel.Children.Add(Department_TextBox);
            Input_StackPanel.Children.Add(YearsOfExperience_TextBox);
            Input_StackPanel.Children.Add(CurrentProject_TextBox);
            Input_StackPanel.Children.Add(Salary_TextBox);
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
                    EmployeeTools.Delete(MainWindow.db, value.Id);
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
                    EmployeeTools.Edit(MainWindow.db, id, Input);
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
            Table.ItemsSource = EmployeeTools.LoadTable(MainWindow.db);
        }

        private protected override void InputLayoutUpdate(List<string> _l)
        {
            if (action_type == "none" || action_type == "add" || action_type == "search")
            {
                List<string> l = new List<string> { "Id (int)", "Surname (string)", "FirstName (string)", "LastName (string)", "DateOfBirth (DateOnly)", "Position (string)", "Department (string)", "YearsOfExperience (byte)", "CurrentProject (int)", "Salary (decimal)" };
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
