using GcpeProductsAPI.Controllers;
using GcpeProductsAPI.Models;
using GcpeProductsAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace BookStoreApp.Tests
{
    [TestClass]
    public class TestProductsController
    {
        [TestMethod]
        public void GetProduct_ReturnProductSameID()
        {
            //arrange
            // these two lines are the moq equivalent of this:
            // var context = new TestStoreAppContext();
            // context.Products.Add(GetDemoProduct());

            var mockContext = new Mock<IBookStoreAppContext>();
            mockContext.Setup(x => x.Get(3)).Returns(GetDemoProduct());

            // act
            // sut (system under test)
            var sut = new ProductsController(mockContext.Object);

            // assert

            // try to get an object that doesn't exist and we should get a not found
            var result = sut.Get(1);
            Assert.IsInstanceOfType(result, typeof(NotFoundObjectResult));

            var httpResult = sut.Get(3) as OkObjectResult;
            var product = httpResult.Value as Product;

            // trying to get a product that exists should return our demo object
            // this could have been done in a better way than comparing field by field...but time
            Assert.AreEqual(product.Id, GetDemoProduct().Id);
            Assert.AreEqual(product.Name, GetDemoProduct().Name);
            Assert.AreEqual(product.Price, GetDemoProduct().Price);
        }

        [TestMethod]
        public void DeleteProduct_OnlyDeleteIfFound()
        {
            //arrange
            var mockContext = new Mock<IBookStoreAppContext>();
            mockContext.Setup(x => x.Get(3)).Returns(GetDemoProduct());

            // act
            // sut (system under test)
            var sut = new ProductsController(mockContext.Object);

            // assert

            // deleting an object should return an OK result
            var result = sut.Delete(3);
            Assert.IsInstanceOfType(result, typeof(OkResult));

            // note: this test could be more thorough to test for attempting to delete an object that doesn't exist
            // but because we're using a fake list in the API, this was not implemented, but its something that would make
            // test more complete.
        }

        Product GetDemoProduct()
        {
            return new Product() { Id = 3, Name = "Demo name", Price = 5 };
        }

    }
}
