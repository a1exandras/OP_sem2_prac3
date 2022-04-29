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
    /// Interaction logic for AdminWindow.xaml
    /// </summary>
    public partial class AdminWindow : Window
    {
        public bool hasChanged = false;
        public bool isLoading = false;
        public List<Person> people = new List<Person>();
        public AdminWindow()
        {
            InitializeComponent();
            hasRestrCheck.Click += applyRestrictions;
            GetData gd = new GetData();
            people = gd.GetPeople();
            foreach(Person p in people)
            {
                listBox1.Items.Add(p.Login);
            }
        }

        private void listBox1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (isLoading)
            {
                return;
            }
            Person person = people.Find(x => x.Login == listBox1.SelectedItem.ToString());
            nameBox.Text = person.Name;
            surnameBox.Text = person.Surname;
            loginBox.Text = person.Login;
            passwordBox.Text = person.Password;
            isActiveCheck.IsChecked = person.IsActive;
            hasRestrCheck.IsChecked = person.PassRestr;
            isActiveCheck.IsEnabled = true;
            deleteButton.IsEnabled = true;
            if(person.Login == "ADMIN")
            {
                isActiveCheck.IsEnabled = false;
                deleteButton.IsEnabled = false;
            }
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
        private void smthChanged(object sender, RoutedEventArgs e)
        {
            hasChanged = true;
        }
        private void saveChanges()
        {
            GetData gd = new GetData();
            gd.UpdatePerson(nameBox.Text, surnameBox.Text, loginBox.Text, passwordBox.Text, (bool)isActiveCheck.IsChecked, (bool)hasRestrCheck.IsChecked);
        }

        private void saveButton_Click(object sender, RoutedEventArgs e)
        {
            saveChanges();
            GetData gd = new GetData();
            people = gd.GetPeople();
            isLoading = true;
            listBox1.Items.Clear();
            foreach (Person p in people)
            {
                listBox1.Items.Add(p.Login);
            }
            isLoading = false;
        }

        private void changeButton_Click(object sender, RoutedEventArgs e)
        {
            Person adm = people.Find(x => x.Login == "ADMIN");
            if(adm.Password == pass1Box.Password)
            {
                if (pass2Box.Password == pass3Box.Password)
                {
                    GetData gd = new GetData();
                    gd.UpdatePerson(adm.Name, adm.Surname, "ADMIN", pass2Box.Password, adm.IsActive, adm.PassRestr);
                    people = gd.GetPeople();
                    isLoading = true;
                    listBox1.Items.Clear();
                    foreach (Person p in people)
                    {
                        listBox1.Items.Add(p.Login);
                    }
                    isLoading = false;
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

        private void deleteButton_Click(object sender, RoutedEventArgs e)
        {
            GetData gd = new GetData();
            gd.DeletePerson(loginBox.Text);
            people = gd.GetPeople();
            isLoading = true;
            listBox1.Items.Clear();
            foreach (Person p in people)
            {
                listBox1.Items.Add(p.Login);
            }
            isLoading = false;
        }
        private void applyRestrictions(object sender, RoutedEventArgs e)
        {
            Person per = people.Find(x => x.Login == loginBox.Text);
            GetData gd = new GetData();
            passwordBox.Text = passwordBox.Text.Replace('1', '0');
            gd.UpdatePerson(per.Name, per.Surname, per.Login, per.Password.Replace("1", "0"), per.IsActive, per.PassRestr);
            people = gd.GetPeople();
            isLoading = true;
            listBox1.Items.Clear();
            foreach (Person p in people)
            {
                listBox1.Items.Add(p.Login);
            }
            isLoading = false;
        }
    }
}
