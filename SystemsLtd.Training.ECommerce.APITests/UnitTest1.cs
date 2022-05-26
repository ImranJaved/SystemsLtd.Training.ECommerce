using SystemsLtd.Training.ECommerce.API.Controllers;

namespace SystemsLtd.Training.ECommerce.APITests
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            //Arrange

            //Act

            //Assert

            Assert.False(false);
        }

        [Fact]
        public void SumTest()
        {
            //Arrange
            int? a = 10;
            int? b = 20;
            var controller = new ProductController(null, null);

            // Act
            var res = controller.Sum(a, b);
            Assert.Equal(30, res);
        }

        [Fact]
        public void SumTestInvalidParamter()
        {
            //Arrange
            int? a = null;
            int? b = 20;
            var controller = new ProductController(null, null);

            // Act
            var res = controller.Sum(a, b);
            Assert.Equal(null, res);
        }

        [Fact]
        public void SumTestOutofRange()
        {
            //Arrange
            int? a = 1500000000;
            int? b = 1500000000;
            long? c = 3000000000;

            var controller = new ProductController(null, null);

            // Act
            var res = controller.Sum(a, b);
            Assert.NotEqual(c, res);
        }
    }
}