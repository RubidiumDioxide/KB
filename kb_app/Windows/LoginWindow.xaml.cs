using Azure.Identity;
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
    public partial class LoginWindow : Window
    {
        public event EventHandler inputEntered;
        public string username;
        public string password;

        public LoginWindow()
        {
            InitializeComponent();
        }

        public void Login_Button_Click(object sender, RoutedEventArgs e)
        {
            username = Username_Textbox.Text;
            password = Password_Passwordbox.Password;

            inputEntered?.Invoke(this, EventArgs.Empty);
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            e.Cancel = true;
            Visibility = Visibility.Collapsed;
        }
    }
}
