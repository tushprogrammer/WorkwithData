using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.Entity;
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
        WorkDataDBEntities context;

        public DetailsClient()
        {
            InitializeComponent();
            context = new WorkDataDBEntities();
        }
        public DetailsClient(Clients clientnow) : this()
        {
            string email = clientnow.Email;
            try
            {
                context.SalesSet.Load();
                var msd = context.SalesSet.Where(w => w.Email == email);
                gridView.DataContext = new ObservableCollection<Sales>(msd);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                throw;
            }
            
            
        }
    }
}
