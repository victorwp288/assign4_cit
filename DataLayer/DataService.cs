using System;
using System.Collections.Generic;
using System.Linq;
using DataLayer.Data;
using Microsoft.EntityFrameworkCore;

namespace DataLayer
{
    public class DataService
    {
        // 1. Get a single order by ID
        public Order GetOrder(int id)
        {
            using var db = new NorthwindContext();
            var order = db.Orders
                .Include(o => o.OrderDetails)
                    .ThenInclude(od => od.Product)
                        .ThenInclude(p => p.Category)
                .FirstOrDefault(o => o.Id == id);
            return order;
        }

        // 2. Get orders by shipping name
        public List<Order> GetOrdersByShippingName(string shipName)
        {
            using var db = new NorthwindContext();
            var orders = db.Orders
                .Where(o => o.ShipName.Contains(shipName))
                .Select(o => new Order
                {
                    Id = o.Id,
                    Date = o.Date,
                    ShipName = o.ShipName,
                    ShipCity = o.ShipCity
                })
                .ToList();
            return orders;
        }

        // 3. List all orders
        public List<Order> GetOrders()
        {
            using var db = new NorthwindContext();
            var orders = db.Orders
                .Select(o => new Order
                {
                    Id = o.Id,
                    Date = o.Date,
                    ShipName = o.ShipName,
                    ShipCity = o.ShipCity
                })
                .ToList();
            return orders;
        }

        // 4. Get details for a specific order ID
        public List<OrderDetails> GetOrderDetailsByOrderId(int orderId)
        {
            using var db = new NorthwindContext();
            var details = db.OrderDetails
                .Include(od => od.Product)
                .ThenInclude(p => p.Category)
                .Where(od => od.OrderId == orderId)
                .ToList();
            return details;
        }

        // 5. Get details for a specific product ID
        public List<OrderDetails> GetOrderDetailsByProductId(int productId)
        {
            using var db = new NorthwindContext();
            var details = db.OrderDetails
                .Include(od => od.Order)
                .Include(od => od.Product)
                .Where(od => od.ProductId == productId)
                .OrderBy(od => od.OrderId)
                .ToList();
            return details;
        }

        // 6. Get a single product by ID
        public Product GetProduct(int id)
        {
            using var db = new NorthwindContext();
            var product = db.Products
                .Include(p => p.Category)
                .FirstOrDefault(p => p.Id == id);
            return product;
        }

        // 7. Get a list of products containing a substring
        public List<ProductNameCategoryDTO> GetProductByName(string substring)
        {
            using var db = new NorthwindContext();
            var products = db.Products
                .Include(p => p.Category)
                .Where(p => p.Name.Contains(substring))
                .Select(p => new ProductNameCategoryDTO
                {
                    ProductName = p.Name,
                    CategoryName = p.Category.Name
                })
                .ToList();

            return products;
        }


        // 8. Get products by category ID
        public List<Product> GetProductByCategory(int categoryId)
        {
            using var db = new NorthwindContext();
            var products = db.Products
                .Include(p => p.Category)
                .Where(p => p.CategoryId == categoryId)
                .OrderBy(p => p.Id) 
                .ToList();
            return products;
        }

        // 9. Get category by ID
        public Category GetCategory(int id)
        {
            using var db = new NorthwindContext();
            var category = db.Categories
                .FirstOrDefault(c => c.Id == id);
            return category;
        }

        // 10. Get all categories
        public List<Category> GetCategories()
        {
            using var db = new NorthwindContext();
            var categories = db.Categories
                .OrderBy(c => c.Id)
                .ToList();
            return categories;
        }

        // 11. Add a new category
        public Category CreateCategory(string name, string description)
        {
            using var db = new NorthwindContext();

            // Generate a new unique Id
            int newId = db.Categories.Any() ? db.Categories.Max(c => c.Id) + 1 : 1;
            Console.WriteLine($"Generated newId: {newId}");

            var category = new Category
            {
                Id = newId,
                Name = name,
                Description = description
            };

            Console.WriteLine($"Before SaveChanges: category.Id = {category.Id}");
            db.Categories.Add(category);
            db.SaveChanges();
            Console.WriteLine($"After SaveChanges: category.Id = {category.Id}");

            return category;
        }

        // 12. Update category
        public bool UpdateCategory(int id, string name, string description)
        {
            using var db = new NorthwindContext();
            var category = db.Categories.Find(id);
            if (category == null)
                return false;

            category.Name = name;
            category.Description = description;
            db.SaveChanges();
            return true;
        }

        // 13. Delete category
        public bool DeleteCategory(int id)
        {
            using var db = new NorthwindContext();
            var category = db.Categories.Find(id);
            if (category == null)
                return false;

            db.Categories.Remove(category);
            try
            {
                db.SaveChanges();
                return true;
            }
            catch (DbUpdateException ex)
            {
                Console.WriteLine($"Error deleting category: {ex.Message}");
                throw;
            }
        }
    }

    // DTO for requirement 7
    public class ProductNameCategoryDTO
    {
        public string ProductName { get; set; }
        public string CategoryName { get; set; }
    }
}
