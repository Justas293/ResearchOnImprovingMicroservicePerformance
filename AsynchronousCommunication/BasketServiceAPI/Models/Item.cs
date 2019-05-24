using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BasketServiceAPI.Models
{
    public class Item
    {
        public string Id { get; set; }
        public string BasketId { get; set; }
        public string ProductId { get; set; }
    }
}
