using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RestfulAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        // GET: api/<ProductsController>

        [HttpGet]
        public List<List<string>> GetProducts()
        {
            List<List<string>> products = new();
           
            string connectionString = "server=127.0.0.1;uid=root;pwd=amP@ssw0rd;database=data";
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                MySqlCommand command = new("SELECT id, name, details, rate, price " +
                    "FROM data.products", connection);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    List<string> product = new();
                    string id = "id: " + reader["id"].ToString();
                    string name = "name:" + reader["name"].ToString();
                    string details = "details: " + reader["details"].ToString();
                    string rate = "rate: " + reader["rate"].ToString();
                    string price = "price: " + reader["price"].ToString();

                    product.Add(id);
                    product.Add(name);
                    product.Add(details);
                    product.Add(rate);
                    product.Add(price);

                    products.Add(product);
                }
                connection.Close();
                return products;
            }
        }
        [HttpGet("{name}")]
        public List<string> GetProductByName(string name)
        {
            List<string> product = new();
            string connectionString = "server=127.0.0.1;uid=root;pwd=amP@ssw0rd;database=data";
            using (MySqlConnection connection = new(connectionString))
            {
                connection.Open();
                MySqlCommand command = new("SELECT id, details, rate, price " +
                    "FROM data.products WHERE name = \""+name+"\"", connection);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    string id = "id: " + reader["id"].ToString();
                    string details = "details: " + reader["details"].ToString();
                    string rate = "rate: " + reader["rate"].ToString();
                    string price = "price: " + reader["price"].ToString();

                    product.Add(id);
                    product.Add(details);
                    product.Add(rate);
                    product.Add(price);

                }
                connection.Close();
                return product;
            }
        }

    }
}
