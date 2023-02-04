using AuthorsTable2DCMode.Model;
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
using System.Windows.Shapes;

namespace AuthorsTable2DCMode.Views
{
    /// <summary>
    /// Interaction logic for UptadeView.xaml
    /// </summary>
    public partial class UptadeView : Window
    {
        Book books;
        public UptadeView()
        {
            InitializeComponent(Book book);
            books =book;

            name_txt.Text = book.Name;
            pages_txt.Text = book.Pages.ToString();
            yearpress_txt.Text = book.YearPress.ToString();
            comment_txt.Text = book.Comment.ToString();
            Quantity_txt.Text = book.Quantity.ToString();



        }

        private void Btn_Save_Click(object sender, RoutedEventArgs e)
        {
            using (var conn=new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["connString"].ConnectionString;
                conn.Open();

                DataSet set = new DataSet();
                SqlDataAdapter dataAdapter = new();

                SqlCommand sqlCommand = new SqlCommand("ShowAllBooks", conn);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                dataAdapter.SelectCommand= sqlCommand;
                dataAdapter.Fill(set, "Books");


                var paramName = new SqlParameter();
                paramName.ParameterName = "@Name";
                paramName.SqlDbType= SqlDbType.NVarChar;
                paramName.Value = books.Name;


            }
        }

    }
}
