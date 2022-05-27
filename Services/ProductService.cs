using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using AzureSqlWebapp.Models;

namespace AzureSqlWebapp.Services
{
    public interface IProductService
    {
        List<Product> GetProducts();
    }

    public class ProductService : IProductService
    {
        // private static string db_source = "tusharsql101.database.windows.net";
        // private static string db_user = "tusharostwal99";
        // private static string db_password = "Hridaan@07";
        // private static string db_database = "SqlTushar1001";

        // private static string SqlConnectionString = "Server=tcp:tushar101.database.windows.net,1433;Initial Catalog=appdb;Persist Security Info=False;User ID=tusharostwal99;Password=Azure@123;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

        private IConfiguration Configuration { get; }

        public ProductService(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        private SqlConnection GetConnection()
        {

            var SqlConnectionString = Configuration.GetSection("SqlConnectionString").Get<string>();
            return new SqlConnection(SqlConnectionString);
        }
        public List<Product> GetProducts()
        {
            List<Product> _product_lst = new List<Product>();
            string _statement = "SELECT ProductID,ProductName,Quantity from Products";
            SqlConnection _connection = GetConnection();

            _connection.Open();

            SqlCommand _sqlcommand = new SqlCommand(_statement, _connection);

            using (SqlDataReader _reader = _sqlcommand.ExecuteReader())
            {
                while (_reader.Read())
                {
                    Product _product = new Product()
                    {
                        ProductID = _reader.GetInt32(0),
                        ProductName = _reader.GetString(1),
                        Quantity = _reader.GetInt32(2)
                    };

                    _product_lst.Add(_product);
                }
            }
            _connection.Close();
            return _product_lst;
        }

    }

}