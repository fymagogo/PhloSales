namespace Phlo.Api.Models
{
    public class ProductSold
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal MaxPrice { get; set; }
        public decimal MinPrice { get; set; }
        public int NumberOfOrders { get; set; }
    }
}
