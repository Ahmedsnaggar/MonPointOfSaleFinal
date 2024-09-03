namespace MonPointOfSaleFinal.Entities.Models
{
    public class OrderHeader
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; } = DateTime.Now;
        public int? customerId { get; set; }
        public Customer? customer { get; set; }
        public int? storeId { get; set; }
        public Store? store { get; set; }
    }
}
