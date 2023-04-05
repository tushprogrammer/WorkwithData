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
        DataRowView row;

        public DataInfo()
        {
            InitializeComponent();
            MessageBox.Show($"Приветствуем, {Environment.UserName}");
            //Information(); //вывод информации об установленных проводниках
            FirstCommand();
        }

        //public void Information()
        //{
        //    DataTable table = new OleDbEnumerator().GetElements();
        //    string inf = "";
        //    foreach (DataRow row in table.Rows)
        //        inf += row["SOURCES_NAME"] + "\n";

        //    MessageBox.Show(inf);
        //}

        /// <summary>
        ///  метод подготовки - вывод первичной таблицы на экран и подготовка базовых команд
        /// </summary>
        public void FirstCommand()
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

            //SqlConnectionStringBuilder strCon = new SqlConnectionStringBuilder() //строка подлкючения к базе sql (по сути тут не нужно)
            //{
            //    DataSource = @"(localdb)\MSSQLLocalDB",
            //    InitialCatalog = "WorkDataDB",
            //    IntegratedSecurity = true,
            //    Pooling = false
            //};
            //SqlConnection connection = new SqlConnection(strCon.ConnectionString); //подключение sqlDB

            try
            {
                oleDbConnection.Open(); //открытие sql базы, которая должна была быть access
                lt_TableData = new DataTable();               
                //инициализация адаптеров
                da = new SqlDataAdapter();
                da_acc = new SqlDataAdapter();
                #region выборка
                    string msd = $"select * from Clients"; //код запроса на общую выборку
                    da_acc.SelectCommand = new SqlCommand(msd, oleDbConnection); //запрос из access базы
                #endregion

                #region вставка                
                msd = @"INSERT INTO Clients (name,  lastname,  middlename, phonenumber, Email) 
                                 VALUES (@name, @lastname, @middlename, @phonenumber, @Email); 
                     SET @id = @@IDENTITY;";
                da_acc.InsertCommand = new SqlCommand(msd, oleDbConnection); //запрос на вставку
                da_acc.InsertCommand.Parameters.Add("@id", SqlDbType.Int, 4, "id").Direction = ParameterDirection.Output;
                da_acc.InsertCommand.Parameters.Add("@name", SqlDbType.NVarChar, 50, "name");
                da_acc.InsertCommand.Parameters.Add("@lastname", SqlDbType.NVarChar, 50, "lastname");
                da_acc.InsertCommand.Parameters.Add("@middlename", SqlDbType.NVarChar, 50, "middlename");
                da_acc.InsertCommand.Parameters.Add("@phonenumber", SqlDbType.NVarChar, 20, "phonenumber");
                da_acc.InsertCommand.Parameters.Add("@Email", SqlDbType.NVarChar, 255, "Email");
                #endregion

                #region обновление
                msd = @"update Clients set 
                            name = @name,
                            lastname = @lastname,
                            middlename = @middlename,
                            phonenumber = @phonenumber,
                            Email = @Email
                                where id = @id;";
                da_acc.UpdateCommand = new SqlCommand(msd, oleDbConnection);
                da_acc.UpdateCommand.Parameters.Add("@id", SqlDbType.Int, 4, "id").SourceVersion = DataRowVersion.Original;
                da_acc.UpdateCommand.Parameters.Add("@name", SqlDbType.NVarChar, 50, "name");
                da_acc.UpdateCommand.Parameters.Add("@lastname", SqlDbType.NVarChar, 50, "lastname");
                da_acc.UpdateCommand.Parameters.Add("@middlename", SqlDbType.NVarChar, 50, "middlename");
                da_acc.UpdateCommand.Parameters.Add("@phonenumber", SqlDbType.NVarChar, 20, "phonenumber");
                da_acc.UpdateCommand.Parameters.Add("@Email", SqlDbType.NVarChar, 255, "Email");

                #endregion

                #region удаление
                msd = "DELETE FROM Clients WHERE ID = @id";

                da_acc.DeleteCommand = new SqlCommand(msd, oleDbConnection);
                da_acc.DeleteCommand.Parameters.Add("@id", SqlDbType.Int, 4, "id");
                #endregion


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

        /// <summary>
        /// Начало редактирования 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GVCellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            row = (DataRowView)gridView.SelectedItem;
            row.BeginEdit();            
        }



        /// <summary>
        /// обработчик ПКМ - добавить
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuItemAddClick(object sender, RoutedEventArgs e)
        {
            DataRow r = lt_TableData.NewRow();
            AddNewClient add = new AddNewClient(r);
            add.ShowDialog();
            if (add.DialogResult.Value)
            {
                lt_TableData.Rows.Add(r); //добавление в таблицу на экране новой строки
                da_acc.Update(lt_TableData); //стандартный метод обновления таблицы, использующий заранее описанный update
                                             //запрос для указанной строки (da_acc)
            }
        }

        /// <summary>
        /// Обработчик ПКМ - удалить
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuItemDeleteClick(object sender, RoutedEventArgs e)
        {
            row = (DataRowView)gridView.SelectedItem; //выбранная строка в данный момент (по которой был ПКМ)
            row.Row.Delete(); //удалить из таблицы указанную строку
            da_acc.Update(lt_TableData); //обновить данные в таблице
        }

        /// <summary>
        /// Обработчик пкм - Посмотреть покупки
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuItemShowSalesClick(object sender, RoutedEventArgs e)
        {
            row = (DataRowView)gridView.SelectedItem; //выбранная строка в данный момент (по которой был ПКМ)
            DetailsClient details = new DetailsClient(row);
            details.Owner = this;
            details.ShowDialog();
            
        }

        /// <summary>
        /// Редактирование записи
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GVCurrentCellChanged(object sender, EventArgs e)
        {
            if (row == null) return;
            row.EndEdit();
            da_acc.Update(lt_TableData);
        }
    }
}
