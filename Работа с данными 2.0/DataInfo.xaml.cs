using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Remoting.Contexts;
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
        ClientsDBEntities context;
        Clients row;

        public DataInfo()
        {
            InitializeComponent();
            context = new ClientsDBEntities();
            MessageBox.Show($"Приветствуем, {Environment.UserName}");            
            FirstCommand();
        }


        /// <summary>
        ///  Метод вывода первичной таблицы на экран
        /// </summary>
        public void FirstCommand()
        {                                  
            try
            {
                //выгрузка всей таблицы и вывод на основной грид
                context.Clients.Load();              
                gridView.DataContext = context.Clients.Local.ToBindingList<Clients>(); 
            }
            catch (Exception e )
            {
                MessageBox.Show(e.Message);
            }
        }

        /// <summary>
        /// Начало редактирования (получение строки до редактирвания)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GVCellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            row = (Clients)gridView.SelectedItem;            
        }



        /// <summary>
        /// обработчик ПКМ - добавить
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuItemAddClick(object sender, RoutedEventArgs e)
        {
            //DataRow r = lt_TableData.NewRow();
            //AddNewClient add = new AddNewClient(r);
            //add.ShowDialog();
            //if (add.DialogResult.Value)
            //{
            //    lt_TableData.Rows.Add(r); //добавление в таблицу на экране новой строки
            //    da_acc.Update(lt_TableData); //стандартный метод обновления таблицы, использующий заранее описанный update
            //                                 //запрос для указанной строки (da_acc)
            //}
        }

        /// <summary>
        /// Обработчик ПКМ - удалить
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuItemDeleteClick(object sender, RoutedEventArgs e)
        {
            //row = (DataRowView)gridView.SelectedItem; //выбранная строка в данный момент (по которой был ПКМ)
            //row.Row.Delete(); //удалить из таблицы указанную строку
            //da_acc.Update(lt_TableData); //обновить данные в таблице
        }

        /// <summary>
        /// Обработчик пкм - Посмотреть покупки
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuItemShowSalesClick(object sender, RoutedEventArgs e)
        {
            row = (Clients)gridView.SelectedItem; //выбранная строка в данный момент (по которой был ПКМ)
            DetailsClient details = new DetailsClient(row);
            details.Owner = this;
            details.ShowDialog();
            
        }

        /// <summary>
        /// Редактирование записи (строка уже с внесёнными изменениями)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GVCurrentCellChanged(object sender, EventArgs e)
        {
            if (row == null) return;
            var client = context.Clients.FirstOrDefault(w => w.id == row.id); //на выходе поиска - первый элемент, попавший под условие
            client.name = row.name;
            client.lastname = row.lastname;
            client.middlename = row.middlename; //ругался на отчество, потому что ввод обязателен = ключ, а только отчество, потому что его только и меняли
            client.Email = row.Email;
            client.phonenumber = row.phonenumber;



            context.SaveChanges();
            //row.EndEdit();
            //da_acc.Update(lt_TableData);
        }
    }
}
