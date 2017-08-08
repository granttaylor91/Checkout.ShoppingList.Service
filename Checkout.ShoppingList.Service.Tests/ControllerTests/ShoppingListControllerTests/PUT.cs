using Checkout.ShoppingList.Data;
using Checkout.ShoppingList.Data.Model;
using Checkout.ShoppingList.Service.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Checkout.ShoppingList.Service.Tests.ControllerTests.ShoppingListControllerTests
{
    [TestClass]
    public class PUT
    {
        [TestMethod]
        public void Put_To_Existing_Item_Updates_Quantity()
        {
            //Arrange
            DbContextOptions<ShoppingListContext> options = new TestHelper().GetShoppingListContextOptions();

            var mockData = MockData.LargeShoppingList();

            using (var context = new ShoppingListContext(options))
            {
                context.AddRange(mockData);
                context.SaveChanges();
                context.Dispose();
            };

            var expectedObject = new DrinkOrder
            {
                Name = "Pepsi",
                Quantity = 4
            };

            var expectedResult = new OkObjectResult(expectedObject);

            using (var context = new ShoppingListContext(options))
            {
                IShoppingListRepository mockRepo = new ShoppingListRepository(context);

                var controller = new ShoppingListController(mockRepo);

                //Act
                var result = controller.Put(expectedObject) as OkObjectResult;

                //Assert
                Assert.IsNotNull(result);
                Assert.AreEqual(200, result.StatusCode);
                Assert.AreEqual(result.Value, expectedResult.Value);
                //Check context is updated
                Assert.AreEqual(expectedObject.Quantity, context.shoppingList.FirstOrDefault(x => x.Name == expectedObject.Name).Quantity);

            }
        }


        [TestMethod]
        public void Put_To_Non_Existing_Item_Returns_Not_Found()
        {
            DbContextOptions<ShoppingListContext> options = new TestHelper().GetShoppingListContextOptions();

            var mockData = MockData.LargeShoppingList();

            using (var context = new ShoppingListContext(options))
            {
                context.AddRange(mockData);
                context.SaveChanges();
                context.Dispose();
            };

            var expectedObject = new DrinkOrder
            {
                Name = "Does Not Exist",
                Quantity = 4
            };

            var expectedResult = new OkObjectResult(expectedObject);

            using (var context = new ShoppingListContext(options))
            {
                IShoppingListRepository mockRepo = new ShoppingListRepository(context);

                var controller = new ShoppingListController(mockRepo);

                //Act
                var result = controller.Put(expectedObject) as NotFoundObjectResult;

                //Assert
                Assert.IsNotNull(result);
                Assert.AreEqual(404, result.StatusCode);
                Assert.AreEqual(result.Value, "Drink: Does Not Exist not found on the shopping list.");
            }
        }

        [TestMethod]
        public void Put_Model_Error_Returns_Invalid()
        {
            DbContextOptions<ShoppingListContext> options = new TestHelper().GetShoppingListContextOptions();

            var mockData = MockData.LargeShoppingList();

            using (var context = new ShoppingListContext(options))
            {
                context.AddRange(mockData);
                context.SaveChanges();
                context.Dispose();
            };

            var expectedObject = new DrinkOrder
            {
                Name = "Pepsi",
                Quantity = -1
            };

            using (var context = new ShoppingListContext(options))
            {
                IShoppingListRepository mockRepo = new ShoppingListRepository(context);

                var controller = new ShoppingListController(mockRepo);
                controller.ModelState.AddModelError("Test", "Error");
                var expectedErrorResult = new List<string> { "Error" };

                //Act
                var result = controller.Put(expectedObject);

                //Assert
                Assert.IsNotNull(result);
                Assert.AreEqual(400, result.StatusCode);
                CollectionAssert.AreEqual(result.Value as List<string>, expectedErrorResult);
            }
        }


    }
}

