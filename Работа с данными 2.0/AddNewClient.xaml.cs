using System;
using System.Collections.Generic;
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

namespace Работа_с_данными_2._0
{
    /// <summary>
    /// Логика взаимодействия для AddNewClient.xaml
    /// </summary>
    public partial class AddNewClient : Window
    {
        public AddNewClient()
        {
            InitializeComponent();
        }
        public AddNewClient(Clients new_client) : this()
        {
            //события после нажатия кнопки
            add.Click += delegate
            {
                //проверка на заполненность 
                if (NewName.Text == string.Empty ||
                NewLastname.Text == string.Empty ||
                NewMiddlename.Text == string.Empty ||
                NewEmail.Text == string.Empty)
                {
                    MessageBox.Show("Заполните все обязательные поля");
                    return;
                }

                new_client.name = NewName.Text;
                new_client.lastname = NewLastname.Text;
                new_client.middlename = NewMiddlename.Text;
                if (NewPhonenumber.Text != string.Empty)
                {
                    new_client.phonenumber = NewPhonenumber.Text;
                }                
                new_client.Email = NewEmail.Text;
                this.DialogResult = true;
            };
        }
   
    }
}
