using Microsoft.Extensions.Logging;
using SystemsLtd.Training.ECommerce.Repository.Interface;
using SystemsLtd.Training.ECommerce.Model;

namespace SystemsLtd.Training.ECommerce.Repository
{
    public class ProductRepositoryStatic : IProductRepository
    {
        #region Properties
        private readonly ILogger<ProductRepositoryStatic> Logger;
        private static List<Product> Products = new List<Product>()
            {
                new Product()
                {
                    ProductId =1,
                    ProductName="Dell Laptop",
                    ProductDescription = "Dell Laptop E6410",
                    PurchasePrice = 1000.50m,
                    SalesPrice = 1200.00m,
                    Active = true,
                    CategoryId = 1
                },
                new Product()
                {
                    ProductId =2,
                    ProductName="HP Laptop",
                    ProductDescription = "HP Laptop E6410",
                    PurchasePrice = 1000.50m,
                    SalesPrice = 1200.00m,
                    Active = true,
                    CategoryId = 1
                },
                new Product()
                {
                    ProductId =3,
                    ProductName="Lenovo Laptop",
                    ProductDescription = "Lenovo Laptop E6410",
                    PurchasePrice = 1000.50m,
                    SalesPrice = 1200.00m,
                    Active = true,
                    CategoryId = 1
                }
            };
        #endregion

        #region Constractor
        public ProductRepositoryStatic(ILogger<ProductRepositoryStatic> logger)
        {
            Logger = logger;
        }
        #endregion


        #region Public Methods
        public IEnumerable<Product> GetProducts()
        {
            return ProductRepositoryStatic.Products;
        }
        public Product GetProduct(int id)
        {
            return ProductRepositoryStatic.Products.FirstOrDefault(x => x.ProductId == id);
        }

        public IEnumerable<Product> GetAllProducts(Product product)
        {
            var products = ProductRepositoryStatic.Products;

            if (product != null)
            {
                if (!string.IsNullOrWhiteSpace(product.ProductName))
                {
                    products = products.Where(x => x.ProductName.Contains(product.ProductName, StringComparison.CurrentCultureIgnoreCase)).ToList();
                }

                if (product.CategoryId > 0)
                {
                    products = products.Where(x => x.CategoryId == product.CategoryId).ToList();
                }

                products = products.Where(x => x.Active == product.Active).ToList();
            }

            return products;
        }

        public int AddProduct(Product product)
        {
            product.ProductId = ProductRepositoryStatic.Products.Count() + 1;

            ProductRepositoryStatic.Products.Add(product);

            return product.ProductId;
        }

        public bool UpdateProduct(Product product)
        {
            var existingProduct = this.GetProduct(product.ProductId);

            if (existingProduct != null)
            {
                existingProduct.ProductName = product.ProductName;
                existingProduct.ProductDescription = product.ProductDescription;
                existingProduct.PurchasePrice = product.PurchasePrice;
                existingProduct.SalesPrice = product.SalesPrice;
                existingProduct.Active = product.Active;
                existingProduct.CategoryId = product.CategoryId;
                return true;
            }
            else
            {
                return false;
            }
        }


        public bool DeleteProduct(Product product)
        {
            var existingProduct = this.GetProduct(product.ProductId);

            return ProductRepositoryStatic.Products.Remove(existingProduct);
        }

        #endregion
    }
}
