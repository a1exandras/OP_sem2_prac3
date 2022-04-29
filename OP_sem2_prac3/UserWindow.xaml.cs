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
    /// Interaction logic for UserWindow.xaml
    /// </summary>
    public partial class UserWindow : Window
    {
        public bool hasChanged = false;
        public Person person;
        public UserWindow(Person per)
        {
            InitializeComponent();
            person = per;
            fillTextBlocks();
        }

        private void backButton_Click(object sender, RoutedEventArgs e)
        {
            if (hasChanged)
            {
                saveChanges();
            }
            MainWindow nw = new MainWindow();
            Hide();
            nw.Show();
        }

        private void deleteButton_Click(object sender, RoutedEventArgs e)
        {
            GetData gd = new GetData();
            gd.DeletePerson(person.Login);
            backButton_Click(sender, e);
        }

        private void saveButton_Click(object sender, RoutedEventArgs e)
        {
            saveChanges();
            fillTextBlocks();
        }

        private void changeButton_Click(object sender, RoutedEventArgs e)
        {
            if (person.Password == pass1Box.Password)
            {
                if (pass2Box.Password == pass3Box.Password)
                {
                    GetData gd = new GetData();
                    gd.UpdatePerson(person.Name, person.Surname, person.Login, pass2Box.Password, person.IsActive, person.PassRestr);
                    person.Password = pass2Box.Password;
                    fillTextBlocks();
                    pass1Box.Clear();
                    pass2Box.Clear();
                    pass3Box.Clear();
                }
                else
                {
                    mssgBlock.Text = "New Passwords are Different";
                }
            }
            else
            {
                mssgBlock.Text = "Incorrect Current Password";
            }
        }

        private void fillTextBlocks()
        {
            nameBox.Text = person.Name;
            surnameBox.Text = person.Surname;
            loginBox.Text = person.Login;
            passwordBox.Text = person.Password;
        }
        private void smthChanged(object sender, RoutedEventArgs e)
        {
            hasChanged = true;
        }
        private void saveChanges()
        {
            GetData gd = new GetData();
            gd.UpdatePerson(nameBox.Text, surnameBox.Text, person.Login, person.Password, person.IsActive, person.PassRestr);
            person.Name = nameBox.Text;
            person.Surname = surnameBox.Text;
        }
    }
}
