namespace DataLayer
{
    public class OrderDetails
    {
        public int OrderId { get; set; }
        public Order Order { get; set; }          

        public int ProductId { get; set; }
        public Product Product { get; set; }      

        public double UnitPrice { get; set; }    
        public int Quantity { get; set; }        
        public float Discount { get; set; }  
    }
}
