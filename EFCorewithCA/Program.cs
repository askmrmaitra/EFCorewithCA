using EFCorewithCA.data;

namespace EFCorewithCA
{
    internal class Program
    {
        //UI 
        static void Main(string[] args)
        {
            Console.WriteLine("Understanding EF Core...");
            Console.WriteLine("****************************");
            Console.WriteLine("Saving Data to Table...");
            int Status = SaveData();
            Console.WriteLine(Status);
            if (Status == 1)
            {
                Console.WriteLine("Data Saved...");
                Console.WriteLine("Press Any Key to Display the Data");
                Console.ReadKey();
                Display();
                Console.WriteLine("Let us update the Data...");
                int ProductID = 1;
                string ProductName = "HP Laptop";
                int ProductPrice = 75000;
                UpdateProduct(ProductID, ProductName, ProductPrice);
                Console.WriteLine("Product updated....");
                Display();
                Console.WriteLine("Do you want to Remove the Product?");
                Console.ReadKey();
                DeleteProduct(ProductID);
                Console.WriteLine("Product Removed...Press Any Key to terminate");
                Console.ReadKey();
            }
            else
            {
                Console.WriteLine("OOPS!!! Data has not been saved...");
            }
        }
        public static int SaveData()
        {
            using (var dbContext = new AppDbContext())
            {
                var newProduct = new Product { Name = "Laptop", Price = 50000 };
                dbContext.Products.Add(newProduct);
                int Status = dbContext.SaveChanges();
                return Status;
            }

        }
        public static void UpdateProduct(int productId, string newName, int newPrice)
        {
            using (var dbContext = new AppDbContext())
            {
                var product = dbContext.Products.Find(productId);

                if (product != null)
                {
                    product.Name = newName;
                    product.Price = newPrice;

                    dbContext.SaveChanges();
                }
            }
        }

        public static void DeleteProduct(int productId)
        {
            using (var dbContext = new AppDbContext())
            {
                var product = dbContext.Products.Find(productId);

                if (product != null)
                {
                    dbContext.Products.Remove(product);
                    dbContext.SaveChanges();
                }
            }
        }

        public static void Display()
        {
            using (var dbContext = new AppDbContext())
            {
                var products = dbContext.Products.ToList(); // Retrieve all products

                // Display the products
                foreach (var product in products)
                {
                    Console.WriteLine($"Product ID: {product.ProductId}, Name: {product.Name}, Price: {product.Price}");
                }
            }

        }
    }
}
