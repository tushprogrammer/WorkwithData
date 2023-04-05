using System;
using System.Collections.Generic;
using System.Data;
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
    /// Логика взаимодействия для DetailsClient.xaml
    /// </summary>
    public partial class DetailsClient : Window
    {
        DataTable lt_Data;
        SqlDataAdapter SqlData;
        public DetailsClient()
        {
            InitializeComponent();

        }
        public DetailsClient(DataRowView dataRow) : this()
        {
            string email = dataRow["Email"].ToString();

            
            SqlConnectionStringBuilder str = new SqlConnectionStringBuilder()
            {
                DataSource = @"(localdb)\MSSQLLocalDB",
                InitialCatalog = "WorkDataDB",
                IntegratedSecurity = true,
                Pooling = false
            };
            SqlConnection Connect = new SqlConnection(str.ConnectionString);
            try
            {
                Connect.Open();
                lt_Data = new DataTable();
                SqlData = new SqlDataAdapter();

                string msq = "SELECT * FROM Sales " +
                               "WHERE Email = @Email";
                SqlData.SelectCommand = new SqlCommand(msq, Connect);
                SqlData.SelectCommand.Parameters.Add("@Email", SqlDbType.NVarChar, 255, "Email").Value = email; //установка динамического параметра
                
                SqlData.Fill(lt_Data);
                gridView.DataContext = lt_Data.DefaultView;

            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                throw;
            }
            finally { Connect.Close(); }
            
        }
    }
}
