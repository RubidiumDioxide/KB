using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace kb_app
{

    public partial class ArmamentInput
    {
        //public event EventHandler inputEntered;

        public ArmamentInput()
        {
            InitializeComponent();
        }

        public void SetTexts(List<string> l)
        {
            var i = 0;

            /*foreach (TextBox t in Extensions.FindVisualChildren<TextBox>(this))
            {
                t.Text = l[i];
                i++;
            }*/

            Name_TextBox.Text = l[0].ToString();
            Caliber_TextBox.Text = l[1].ToString();
            FiringRate_TextBox.Text = l[2].ToString();
            Weight_TextBox.Text = l[3].ToString();  
        }
    }
}
