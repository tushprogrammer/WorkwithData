using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Data.SqlClient;
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

namespace Работа_с_данными_2._0
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            
            
            
            OleDbConnectionStringBuilder straAccess = new OleDbConnectionStringBuilder() //строка подключения к базе access
            {
                Provider = "@Microsoft.ACE.OLEDB.12.0",
                DataSource = "G:/учеба/C#SkillBox/ДЗ №17/DB.accdb"                
            };


        }

        private void Log_enter_Click(object sender, RoutedEventArgs e)
        {
            SqlConnectionStringBuilder strCon = new SqlConnectionStringBuilder() //строка подлкючения к базе sql
            {
                DataSource = @"(localdb)\MSSQLLocalDB",
                InitialCatalog = "WorkDataDB",
                IntegratedSecurity = true,
                Pooling = false
            };
            SqlConnection connection = new SqlConnection(strCon.ConnectionString); //создание подключения на основе строки
            MessageBox.Show($"Строка подключения: {strCon.ConnectionString}"); //вывод строки подключения

            try
            {
                connection.Open();
                SqlCommand com = new SqlCommand($"SELECT * FROM users WHERE ((([username]) ='{login_box.Text}') " +
                    $"AND(([user_password]) = '{password_box.Text}'))", connection); //выборка логина и пароля
                SqlDataReader reader = com.ExecuteReader();
                string log, pass;
                if (!reader.Read()) //если по запросу вообще ничего не нашел
                {
                    MessageBox.Show("Проверьте правильность написания логина и пароля"); //
                    return;
                }
                reader.Close();

                //reader = com.ExecuteReader(); //обновление запроса, так как после первой проверки он уже типо "прочитан"
                //while (reader.Read())//если понадобится сохранить логин и пароль, что по сути бессмыслено
                //    //так как если прошлая проверка пройдена, значит введеный пользователь имеет доступ на вход
                //    //уже сам разочаровался в этом коде, но пусть будет
                //{
                //    log = Convert.ToString(reader[1]);
                //    pass = Convert.ToString(reader[2]);
                //}
                
            }
            catch (Exception a)
            {
                MessageBox.Show(a.Message); //вывод об ошибке и возврат
                return;
            }
            finally
            {
                connection.Close();//закрыть содединение и высвободить ресурсы


            }


        }
    }
}
