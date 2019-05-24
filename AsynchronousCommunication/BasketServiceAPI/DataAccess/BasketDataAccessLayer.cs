using BasketServiceAPI.Models;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace BasketServiceAPI.DataAccess
{
    public class BasketDataAccessLayer : IBasketDataAccessLayer
    {
        private readonly string m_connectionString;

        public BasketDataAccessLayer()
        {
            m_connectionString = "Server=tcp:justassqlserver.database.windows.net,1433;Initial Catalog=BasketDb;Persist Security Info=False;User ID=justas293;Password=;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
        }

        public async Task<IEnumerable<Item>> GetBasketItemsAsync(string basketId)
        {
            List<Item> itemList = new List<Item>();
            using (SqlConnection con = new SqlConnection(m_connectionString))
            {
                SqlCommand cmd = new SqlCommand($"SELECT * FROM Items WHERE BasketId = '{basketId}'", con);
                await con.OpenAsync();
                SqlDataReader reader = await cmd.ExecuteReaderAsync();

                while (await reader.ReadAsync())
                {
                    Item item = new Item();
                    item.Id = reader["Id"].ToString();
                    item.BasketId = reader["BasketId"].ToString();
                    item.ProductId = reader["ProductId"].ToString();

                    itemList.Add(item);
                }
                con.Close();
            }
            return itemList;
        }
        public IEnumerable<Item> GetBasketItems(string basketId)
        {
            List<Item> itemList = new List<Item>();
            using (SqlConnection con = new SqlConnection(m_connectionString))
            {
                SqlCommand cmd = new SqlCommand($"SELECT * FROM Items WHERE BasketId = '{basketId}'", con);
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Item item = new Item();
                    item.Id = reader["Id"].ToString();
                    item.BasketId = reader["BasketId"].ToString();
                    item.ProductId = reader["ProductId"].ToString();

                    itemList.Add(item);
                }
                con.Close();
            }
            return itemList;
        }
    }
}
