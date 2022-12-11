namespace Phlo.Api.Models
{
    public class Order : Entity
    {
        public string CustomerName { get; set; }
        public decimal Price { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
    }
}
