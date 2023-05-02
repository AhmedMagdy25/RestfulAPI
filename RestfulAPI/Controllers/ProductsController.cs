using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using System.Security.Cryptography;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RestfulAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        // GET: api/<ProductsController>

        private string connectionString = "server=127.0.0.1;uid=root;pwd=amP@ssw0rd;database=data";
        [HttpGet]
        public List<string[]> GetProducts()
        {
            List<string[]> products = new();
           
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    MySqlCommand command = new("SELECT * FROM data.products", connection);
                    MySqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        string[] product;
                        string id = "id: " + reader["id"].ToString();
                        string name = "name:" + reader["name"].ToString();
                        string details = "details: " + reader["details"].ToString();
                        string rate = "rate: " + reader["rate"].ToString();
                        string price = "price: " + reader["price"].ToString();
                        string type = "type: " + reader["type"].ToString();

                        product = new string[] { id, name, details, rate, price, type };

                        products.Add(product);
                    }
                    connection.Close();

                    return products;

                }catch (Exception e)
                {
                    List<string[]> err = new()
                    {
                        new string[] { "error: " + e.ToString() }
                    };
                    return err;
                } 
            }
        }
        [HttpPost("{name},{details},{rate}, {price}, {type}")]
        public string AddProduct(string name, string details, string rate, double price, string type)
        {
            using (MySqlConnection connection = new(connectionString))
            {
                try {
                    connection.Open();
                    MySqlCommand command = new("INSERT INTO data.products "
                                               + "VALUES(\""+ new Random().Next().ToString() + "\", \"" + name + "\", \"" + details + "\", \""
                                               + rate + "\", \""+ price + "\", \""+ type +"\");"
                                               , connection);
                    command.ExecuteNonQuery();
                    connection.Close();
                    return "the itme is added successfully";

                }catch(Exception e)
                    {
                    return "error: " + e.ToString();
                }
        }
        }
        [HttpDelete("{id}")]
        public string DeleteProductByID(string id)
        {
            using (MySqlConnection connection = new(connectionString))
            {
                try
                {
                    connection.Open();
                    MySqlCommand command = new("DELETE FROM data.products WHERE id = \""+ id +"\";", connection);
                    command.ExecuteNonQuery();
                    connection.Close();
                    return "the item is deleted successfully";

                }catch(Exception e)
                {
                    return "error: " + e.ToString();
                }
                
            }
        }
    }
}
