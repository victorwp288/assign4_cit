namespace DataLayer
{
    public class OrderDetails
    {
        // Composite Key: OrderId + ProductId
        public int OrderId { get; set; }
        public Order Order { get; set; }          // Navigation property

        public int ProductId { get; set; }
        public Product Product { get; set; }      // Navigation property

        public double UnitPrice { get; set; }    // unitprice
        public int Quantity { get; set; }        // quantity (changed from double to int)
        public float Discount { get; set; }      // discount
    }
}
