namespace DataLayer
{
    public class Product
    {
        public int Id { get; set; }                // productid
        public string Name { get; set; }           // productname
        public double UnitPrice { get; set; }      // unitprice
        public string QuantityPerUnit { get; set; } // quantityperunit
        public int UnitsInStock { get; set; }      // unitsinstock (changed from short to int)

        // Foreign keys
        public int CategoryId { get; set; }
        public Category Category { get; set; }

        // Added to match the unit test expectations
        public string CategoryName => Category?.Name;

        // Navigation property
        public ICollection<OrderDetails> OrderDetails { get; set; }
    }
}
