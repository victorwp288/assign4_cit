﻿using DataLayer;

public class Order
{
    public int Id { get; set; }              
    public DateTime Date { get; set; }        
    public DateTime Required { get; set; }    
    public DateTime? Shipped { get; set; }    
    public decimal Freight { get; set; }      
    public string ShipName { get; set; }      
    public string ShipCity { get; set; }      

    public ICollection<OrderDetails> OrderDetails { get; set; }
}
