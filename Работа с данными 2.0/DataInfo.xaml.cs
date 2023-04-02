using System;
using System.Collections.Generic;
using System.Data;
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
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Работа_с_данными_2._0
{
    /// <summary>
    /// Логика взаимодействия для DataInfo.xaml
    /// </summary>
    public partial class DataInfo : Window

    {
        DataTable lt_TableData; //таблица для хранения данных
        SqlDataAdapter da; //передатчик данных из sql запросов в таблицу
        SqlDataAdapter da_acc; //передатчик данных из sql запросов access (ныне sql) в таблицу

        public DataInfo()
        {
            InitializeComponent();
            MessageBox.Show($"Приветствуем, {Environment.UserName}");
            //Information(); //вывод информации об установленных проводниках
            ShowInfo();
        }

        //public void Information()
        //{
        //    DataTable table = new OleDbEnumerator().GetElements();
        //    string inf = "";
        //    foreach (DataRow row in table.Rows)
        //        inf += row["SOURCES_NAME"] + "\n";

        //    MessageBox.Show(inf);
        //}
        public void ShowInfo()
        {

            //старый вариант (ныне не работает, даже после переустановке офиса,
            //но оставлен, ибо вдруг на другом компьютере заработает
            //OleDbConnectionStringBuilder straAccess = new OleDbConnectionStringBuilder() //строка подключения к базе access
            //{
            //    Provider = "@Microsoft.ACE.OLEDB.12.0",
            //    DataSource = "G:/учеба/C#SkillBox/ДЗ №17/DB.accdb"
            //};
            //OleDbConnection oleDbConnection = new OleDbConnection(straAccess.ConnectionString); //подключение access
            
            SqlConnectionStringBuilder straAccess = new SqlConnectionStringBuilder()
            {
                DataSource = @"(localdb)\MSSQLLocalDB",
                InitialCatalog = "Fake_access-SQLDB",
                IntegratedSecurity = true,
                Pooling = false
            };
            SqlConnection oleDbConnection = new SqlConnection(straAccess.ConnectionString);

            SqlConnectionStringBuilder strCon = new SqlConnectionStringBuilder() //строка подлкючения к базе sql
            {
                DataSource = @"(localdb)\MSSQLLocalDB",
                InitialCatalog = "WorkDataDB",
                IntegratedSecurity = true,
                Pooling = false
            };
            SqlConnection connection = new SqlConnection(strCon.ConnectionString); //подключение sqlDB

            try
            {
                oleDbConnection.Open(); //открытие sql базы, которая должна была быть access
                lt_TableData = new DataTable();               
                //инициализация адаптеров
                da = new SqlDataAdapter();
                da_acc = new SqlDataAdapter();

                string msd = $"select * from Клиенты"; //код запроса на общую выборку
                da_acc.SelectCommand = new SqlCommand(msd, oleDbConnection); //запрос из access базы
                da_acc.Fill(lt_TableData); //заполнение переменной таблицы данными из БД
                gridView.DataContext = lt_TableData.DefaultView;
            }
            catch (Exception e )
            {
                MessageBox.Show(e.Message);
                
            }
            finally
            {
                oleDbConnection.Close();
            }         
            
            
        }

        private void MenuItemAddClick(object sender, RoutedEventArgs e)
        {

        }
    }
}
