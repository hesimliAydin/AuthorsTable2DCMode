using AuthorsTable2DCMode.Model;
using AuthorsTable2DCMode.Views;
using System;
using System.Collections.Generic;
using System.Configuration;
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
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Linq;

namespace AuthorsTable2DCMode
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        
        DataSet set;
        SqlDataAdapter adapter;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Btn_Add_Click(object sender, RoutedEventArgs e)
        {
            AddView addView = new AddView();
            addView.ShowDialog();
        }

        private void Btn_Uptade_Click(object sender, RoutedEventArgs e)
        {
            var result = myDataGrid.SelectedItem;
            var obj= result as DataRowView;
            var objects = obj.Row.ItemArray;


            Book book = new((int)objects[0], objects[1], (int)objects[2], (int)objects[3], objects[4], (int)objects[5]);
            UptadeView uptadeView = new(book);
            uptadeView.ShowDialog();

        }

        private void Btn_Delete_Click(object sender, RoutedEventArgs e)
        {
            var result = myDataGrid.SelectedItem;
            var obj = result as DataRowView;
            var book = obj.Row.ItemArray;

            using (var conn=new SqlConnection())
            {
                adapter = new SqlDataAdapter();
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["connString"].ConnectionString;
                conn.Open();

                SqlCommand sqlCommand = new SqlCommand("ShowAllBooks", conn);
                sqlCommand.CommandType = CommandType.StoredProcedure;

                adapter.SelectCommand= sqlCommand;
                adapter.Fill(set, "Books");


                sqlCommand = new SqlCommand("DeleteBooks", conn);
                sqlCommand.CommandType = CommandType.StoredProcedure;


                sqlCommand.Parameters.Add(new SqlParameter()
                {
                    DbType=DbType.Int32,
                    ParameterName="@bookId",
                    Value = book[0]
                });

                adapter.UpdateCommand= sqlCommand;

                adapter.Update(set, "Books");
                adapter.UpdateCommand.ExecuteNonQuery();

                adapter.Fill(set, "Books");
            }


        }
    }
}
