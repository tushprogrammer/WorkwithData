﻿using System;
using System.Collections.Generic;
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
        //string name_user = string.Empty; //имя админа (не понадобилось)
        WorkDataDBEntities context;
        public MainWindow()
        {
            InitializeComponent();
            context = new WorkDataDBEntities();
        }

        private void Log_enter_Click(object sender, RoutedEventArgs e)
        {            
            try
            {
                //поиск совпадений по введеному логину и паролю
                var user = context.UsersSet.Where(w => w.user_nickname == login_box.Text && w.user_password == login_box.Text);     
                
                if (user != null) //если совпадения есть
                {
                    //создание и открытие нового окна
                    DataInfo info = new DataInfo();
                    info.Show();
                    this.Close(); //после успешного входа, это окно уже не требуется
                }                           
            }
            catch (Exception a)
            {
                MessageBox.Show(a.Message); //вывод об ошибке и возврат
                return;
            }
      


        }
    }
}
