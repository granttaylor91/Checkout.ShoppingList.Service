using Checkout.ShoppingList.Data;
using Checkout.ShoppingList.Data.Model;
using Checkout.ShoppingList.Service.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Checkout.ShoppingList.Service.Tests.ControllerTests.ShoppingListControllerTests
{
    [TestClass]
    public class POST
    {
        [TestMethod]
        public void Post_To_Empty_List_Successfully_Adds_Item()
        {
            //Arrange
            DbContextOptions<ShoppingListContext> options = new TestHelper().GetShoppingListContextOptions();

            var expectedObject = new DrinkOrder
            {
                Name = "Pepsi",
                Quantity = 1
            };

            var expectedResult = new CreatedResult("", expectedObject);

            using(var context = new ShoppingListContext(options))
            {
                IShoppingListRepository mockRepo = new ShoppingListRepository(context);

                var controller = new ShoppingListController(mockRepo);
                controller.ControllerContext = new ControllerContext();
                controller.ControllerContext.HttpContext = new DefaultHttpContext();

                //Act
                var result = controller.Post(expectedObject) as CreatedResult;

                //Assert
                Assert.IsNotNull(result);
                Assert.AreEqual(201, result.StatusCode);
                //Can't effectively mock HttpRequest, and don't really want to go down that rabbit hole so just check
                //it's not null.
                Assert.IsNotNull(result.Location);
                Assert.AreEqual(result.Value, expectedResult.Value);
            }
        }

        [TestMethod]
        public void Post_Existing_Item_Returns_Conflict()
        {
            //Arrange
            DbContextOptions<ShoppingListContext> options = new TestHelper().GetShoppingListContextOptions();

            var mockData = MockData.LargeShoppingList();
          
            var testObject = mockData.FirstOrDefault();

            using (var context = new ShoppingListContext(options))
            {
                context.AddRange(mockData);
                context.SaveChanges();
            };

            using (var context = new ShoppingListContext(options))
            {
                IShoppingListRepository mockRepo = new ShoppingListRepository(context);

                var controller = new ShoppingListController(mockRepo);
                controller.ControllerContext = new ControllerContext();
                controller.ControllerContext.HttpContext = new DefaultHttpContext();

                //Act
                var result = controller.Post(testObject);

                //Assert
                Assert.IsNotNull(result);
                Assert.AreEqual(409, result.StatusCode);
                Assert.AreEqual(result.Value, $"Drink {testObject.Name} already exists in the shopping list.");
            }
        }
    }
}
