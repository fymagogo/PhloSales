namespace Phlo.Api.Models
{
    public class ProductSold
    {
        public string Name { get; set; }
        public decimal MaxPrice { get; set; }
        public decimal MinPrice { get; set; }
        public int NumberOfOrders { get; set; }
    }
}
