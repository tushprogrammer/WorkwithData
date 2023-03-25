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
            
            SqlConnectionStringBuilder strCon = new SqlConnectionStringBuilder() //строка подлкючения к базе sql
            {
                DataSource = "@(localdb)/MSSQLLocalDB",
                InitialCatalog = "WorkDataDB",
                IntegratedSecurity = true,
                Pooling = false
            };
            
            OleDbConnectionStringBuilder straAsess = new OleDbConnectionStringBuilder() //строка подключения к базе access
            {
                Provider = "@Microsoft.ACE.OLEDB.12.0",
                DataSource = "G:/учеба/C#SkillBox/ДЗ №17/DB.accdb"                
            };


        }
    }
}
