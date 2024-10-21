using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class Category
    {
        public int Id { get; set; }            // categoryid
        public string Name { get; set; }       // categoryname
        public string Description { get; set; } // description

        // Navigation property
        public ICollection<Product> Products { get; set; }
    }
}
