namespace MonPointOfSaleFinal.Entities.Models
{
    public class OrderDetail
    {
        public int Id { get; set; }
        public int orderId { get; set; }
        public OrderHeader? order { get; set; }
        public int productId { get; set; }
        public Product? product { get; set; }
        public double QTY { get; set; } = 1;
        public double Price { get; set; }
        public double? Discount { get; set; }
    }
}
