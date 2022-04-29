using System;
using System.Data.SqlClient;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace OP_sem2_prac3
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public int counter = 3;
        public List<Person> people = new List<Person>();
        public MainWindow()
        {
            InitializeComponent();
            GetData gd = new GetData();
            people = gd.GetPeople();

            foreach(Person p in people)
            {
                cBox.Items.Add(p.Login);
            }
        }

        private void exitButton_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }

        private void regButton_Click(object sender, RoutedEventArgs e)
        {
            Registren nw = new Registren();
            Hide();
            nw.Show();
        }

        private void confButton_Click(object sender, RoutedEventArgs e)
        {
            string sel = cBox.SelectedItem.ToString();
            Person per = people.Find(x => x.Login == sel);
            if (per.IsActive == false)
            {
                mssgBlock.Text = "This User is Blocked by Admin!";
            }
            else
            {
                if (per.Password + "" == tBox.Password.ToString())
                {
                    if (sel == "ADMIN")
                    {
                        AdminWindow nw = new AdminWindow();
                        Hide();
                        nw.Show();
                    }
                    else
                    {
                        UserWindow nw = new UserWindow(per);
                        Hide();
                        nw.Show();
                    }
                }
                else
                {
                    counter--;
                    mssgBlock.Text = "Incorrect Password! Remaining Attempts: " + counter.ToString();
                }
                if (counter == 0)
                {
                    System.Windows.Application.Current.Shutdown();
                }
            }
        }

        private void cBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            counter = 3;
        }
    }
}
