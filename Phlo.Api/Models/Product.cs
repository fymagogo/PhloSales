using System.Collections.Generic;

namespace Phlo.Api.Models
{
    public class Product : Entity
    {
        public string Name { get; set; }
        public List<Order> Orders { get; set; }
    }
}
