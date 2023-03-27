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

        public DataInfo()
        {
            InitializeComponent();
            MessageBox.Show($"Приветствуем, {Environment.UserName}");
            ShowInfo();
        }

        public void ShowInfo()
        {
            OleDbConnectionStringBuilder straAccess = new OleDbConnectionStringBuilder() //строка подключения к базе access
            {
                Provider = "@Microsoft.ACE.OLEDB.12.0",
                DataSource = "G:/учеба/C#SkillBox/ДЗ №17/DB.accdb"
            };
            OleDbConnection oleDbConnection = new OleDbConnection(straAccess.ConnectionString); //подключение access


            SqlConnectionStringBuilder strCon = new SqlConnectionStringBuilder() //строка подлкючения к базе sql
            {
                DataSource = @"(localdb)\MSSQLLocalDB",
                InitialCatalog = "WorkDataDB",
                IntegratedSecurity = true,
                Pooling = false
            };
            SqlConnection connection = new SqlConnection(strCon.ConnectionString); //подключение sqlDB

            lt_TableData = new DataTable();


        }
    }
}
