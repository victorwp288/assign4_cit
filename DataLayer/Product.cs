namespace DataLayer
{
    public class Product
    {
        public int Id { get; set; }                
        public string Name { get; set; }          
        public double UnitPrice { get; set; }     
        public string QuantityPerUnit { get; set; } 
        public int UnitsInStock { get; set; }      

        // Foreign keys
        public int CategoryId { get; set; }
        public Category Category { get; set; }

        public string CategoryName => Category?.Name;

        public ICollection<OrderDetails> OrderDetails { get; set; }
    }
}
