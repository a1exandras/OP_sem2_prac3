using System;
using System.Collections.Generic;
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

namespace OP_sem2_prac3
{
    /// <summary>
    /// Interaction logic for Registren.xaml
    /// </summary>
    public partial class Registren : Window
    {
        public Registren()
        {
            InitializeComponent();
        }

        private void signInButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow nw = new MainWindow();
            Hide();
            nw.Show();
        }

        private void confButton_Click(object sender, RoutedEventArgs e)
        {
            mssgBlock.Text = "";
            if(pass1Box.Password == pass2Box.Password)
            {
                try
                {
                    GetData gd = new GetData();
                    gd.AddPerson(loginBox.Text, pass1Box.Password);
                    loginBox.Text = "";
                    pass1Box.Password = "";
                    pass2Box.Password = "";
                }
                catch
                {
                    mssgBlock.Text =  "There is Already a User with this login in the System";
                }
            }

        }
    }
}
