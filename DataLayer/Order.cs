using DataLayer;

public class Order
{
    public int Id { get; set; }               // orderid
    public DateTime Date { get; set; }        // orderdate
    public DateTime Required { get; set; }    // requireddate
    public DateTime? Shipped { get; set; }    // shippeddate
    public decimal Freight { get; set; }      // freight
    public string ShipName { get; set; }      // shipname
    public string ShipCity { get; set; }      // shipcity

    // Navigation property
    public ICollection<OrderDetails> OrderDetails { get; set; }
}
