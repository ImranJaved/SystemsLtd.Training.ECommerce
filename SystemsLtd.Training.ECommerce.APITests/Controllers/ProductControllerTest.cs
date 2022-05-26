using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SystemsLtd.Training.ECommerce.API.Controllers;
using SystemsLtd.Training.ECommerce.Model;
using SystemsLtd.Training.ECommerce.Service.Interface;

namespace SystemsLtd.Training.ECommerce.APITests.Controllers
{
    public class ProductControllerTest
    {
        #region
        public List<Product> ProductData = new List<Product>
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
        [Fact]
        public void GetProductsTest()
        {
            //Arrange
            var service = new Mock<IProductService>();
            service.Setup(s => s.GetProducts()).Returns(
                this.ProductData
                );


            var controller = new ProductController(null, service.Object);

            // Act
            var res = controller.GetProducts();

            //Arrange
            Assert.NotNull(res);
            Assert.Equal(3, res.Count());
        }


    }
}
