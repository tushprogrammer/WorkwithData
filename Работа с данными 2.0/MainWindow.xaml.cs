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
        string name_user = string.Empty; //имя админа (не понадобилось)
        public MainWindow()
        {
            InitializeComponent(); 
            
            
            


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
                SqlCommand com = new SqlCommand($"SELECT * FROM users WHERE ((([user_nickname]) ='{login_box.Text}') " +
                    $"AND(([user_password]) = '{password_box.Password}'))", connection); //выборка логина и пароля
                SqlDataReader reader = com.ExecuteReader();
                
                if (!reader.Read()) //если по запросу вообще ничего не нашел
                {
                    MessageBox.Show("Проверьте правильность написания логина и пароля"); //
                    return;
                }
                reader.Close();

                //reader = com.ExecuteReader(); //обновление запроса, так как после первой проверки он уже типо "прочитан"
                //while (reader.Read())//если понадобится сохранить логин и пароль, что по сути бессмыслено
                //                     //так как если прошлая проверка пройдена, значит введеный пользователь имеет доступ на вход
                //                     //уже сам разочаровался в этом коде, но пусть будет
                //{
                //    name_user = Convert.ToString(reader[3]);                    
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
                //создание и открытие нового окна
                DataInfo info = new DataInfo();
                info.Show();
                this.Close(); //после успешного входа, это окно не требуется


            }


        }
    }
}
