namespace Phlo.Api.Models
{
    public class CreateUpdateOrder
    {
        public string CustomerName { get; set; }
        public decimal Price { get; set; }
        public int ProductId { get; set; }
    }
}
