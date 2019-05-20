using Microsoft.Extensions.Configuration;
using ProductServiceAPI.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace ProductServiceAPI.DataAccess
{
    public class ProductDataAccessLayer : IProductDataAccessLayer
    {
        private readonly string m_connectionString;

        public ProductDataAccessLayer(IConfiguration configuration)
        {
            m_connectionString = configuration.GetConnectionString("ProductDb");
        }

        public IEnumerable<Product> GetProducts()
        {
            List<Product> productList = new List<Product>();
            using (SqlConnection con = new SqlConnection(m_connectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM Products", con);
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Product product = new Product();
                    product.Id = reader["Id"].ToString();
                    product.Name = reader["Name"].ToString();
                    product.Price = Convert.ToDecimal(reader["Price"]);
                    product.Description = reader["Description"].ToString();
                    product.CategoryId = Convert.ToInt32(reader["CategoryId"]);

                    productList.Add(product);
                }
                con.Close();
            }
            return productList;
        }

        public Product GetProductById(string productId)
        {
            Product product = new Product();
            using (SqlConnection con = new SqlConnection(m_connectionString))
            {
                SqlCommand cmd = new SqlCommand($"SELECT * FROM Products WHERE Id = '{productId}'", con);
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    product.Id = reader["Id"].ToString();
                    product.Name = reader["Name"].ToString();
                    product.Price = Convert.ToDecimal(reader["Price"]);
                    product.Description = reader["Description"].ToString();
                    product.CategoryId = Convert.ToInt32(reader["CategoryId"]);
                }
                con.Close();
            }
            return product;
        }

        public void InsertProduct(Product product)
        {
            using (SqlConnection con = new SqlConnection(m_connectionString))
            {
                SqlParameter paramId = new SqlParameter("Id", product.Id);
                SqlParameter paramName = new SqlParameter("Name", product.Name);
                SqlParameter paramDescription = new SqlParameter("Description", product.Description);
                SqlParameter paramPrice = new SqlParameter("Price", product.Price);
                SqlParameter paramCategory = new SqlParameter("CategoryId", product.CategoryId);

                var cmd = new SqlCommand("INSERT INTO Products VALUES (@Id, @Name, @Description, @Price, @CategoryId)", con);
                cmd.Parameters.Add(paramId);
                cmd.Parameters.Add(paramName);
                cmd.Parameters.Add(paramDescription);
                cmd.Parameters.Add(paramPrice);
                cmd.Parameters.Add(paramCategory);
                con.Open();

                cmd.ExecuteNonQuery();
                con.Close();
            }
        }

        public void DeleteProduct(string productId)
        {
            using (SqlConnection con = new SqlConnection(m_connectionString))
            {
                SqlParameter paramId = new SqlParameter("Id", productId);

                var cmd = new SqlCommand("DELETE Products WHERE Id = @Id", con);
                cmd.Parameters.Add(paramId);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }

        public void UpdateProduct(Product product)
        {
            using (SqlConnection con = new SqlConnection(m_connectionString))
            {
                SqlParameter paramId = new SqlParameter("Id", product.Id);
                SqlParameter paramName = new SqlParameter("Name", product.Name);
                SqlParameter paramDescription = new SqlParameter("Description", product.Description);
                SqlParameter paramPrice = new SqlParameter("Price", product.Price);
                SqlParameter paramCategory = new SqlParameter("CategoryId", product.CategoryId);

                var cmd = new SqlCommand("UPDATE Products SET Name = @Name, Description = @Description, Price = @Price, CategoryId = @CategoryId WHERE Id = @Id", con);
                cmd.Parameters.Add(paramId);
                cmd.Parameters.Add(paramName);
                cmd.Parameters.Add(paramDescription);
                cmd.Parameters.Add(paramPrice);
                cmd.Parameters.Add(paramCategory);
                con.Open();

                cmd.ExecuteNonQuery();
                con.Close();
            }
        }
    }
}
