using BasketServiceAPI.DataAccess;
using BasketServiceAPI.Models;
using System.Collections.Generic;

namespace BasketServiceAPI.Services
{
    public class BasketService
    {
        private readonly IBasketDataAccessLayer m_basketDataAccessLayer;

        public BasketService(IBasketDataAccessLayer basketDataAccessLayer)
        {
            m_basketDataAccessLayer = basketDataAccessLayer;
        }

        public IEnumerable<Item> GetBasketItems(string id)
        {
            return m_basketDataAccessLayer.GetBasketItems(id);
        }
    }
}
