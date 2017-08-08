using Checkout.ShoppingList.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Checkout.ShoppingList.Service;
using Checkout.ShoppingList.Service.Controllers;
using Microsoft.AspNetCore.Mvc;
using Checkout.ShoppingList.Data.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace Checkout.ShoppingList.Service.Tests.ControllerTests.ShoppingListControllerTests
{
    [TestClass]
    public class GET
    {
        [TestMethod]
        public void Get_All_Return_Expected_List()
        {
            //Arrange
            DbContextOptions<ShoppingListContext> options = new TestHelper().GetShoppingListContextOptions();

            var mockData = MockData.LargeShoppingList();
            var expectedResult = new OkObjectResult(mockData);

            using (var context = new ShoppingListContext(options))
            {
                context.AddRange(mockData);
                context.SaveChanges();
            };

            using (var context = new ShoppingListContext(options))
            {
                IShoppingListRepository mockRepo = new ShoppingListRepository(context);
                var controller = new ShoppingListController(mockRepo);

                //Act
                var result = controller.Get();
                
                //Assert
                Assert.IsNotNull(result);
                Assert.AreEqual(200, result.StatusCode);
                CollectionAssert.Equals(expectedResult.Value, result.Value);
            }
        }


        [TestMethod]
        public void Get_All_If_EmptyList_Return_EmptyList()
        {
            //Arrange
            DbContextOptions<ShoppingListContext> options = new TestHelper().GetShoppingListContextOptions();

            using (var context = new ShoppingListContext(options))
            {
                IShoppingListRepository mockRepo = new ShoppingListRepository(context);
                var controller = new ShoppingListController(mockRepo);
                var expectedResult = new OkObjectResult(new List<DrinkOrder>());

                //Act
                var result = controller.Get() as OkObjectResult;

                //Assert
                Assert.IsNotNull(result);
                Assert.AreEqual(200, result.StatusCode);
                CollectionAssert.Equals(expectedResult.Value, result.Value);
            }
        }

        [TestMethod]
        public void Get_Item_Returns_Expected_Item()
        {
            //Arrange
            DbContextOptions<ShoppingListContext> options = new TestHelper().GetShoppingListContextOptions();

            var mockData = MockData.LargeShoppingList();
            var expectedObject = mockData.FirstOrDefault();
            var expectedResult = new OkObjectResult(expectedObject);

            using (var context = new ShoppingListContext(options))
            {
                context.AddRange(mockData);
                context.SaveChanges();
            };

            using (var context = new ShoppingListContext(options))
            {
                IShoppingListRepository mockRepo = new ShoppingListRepository(context);
                var controller = new ShoppingListController(mockRepo);

                //Act
                var result = controller.Get(expectedObject.Name) as OkObjectResult;

                //Assert
                Assert.IsNotNull(result);
                Assert.AreEqual(200, result.StatusCode);
                Assert.IsTrue(expectedResult.Value.Equals(result.Value));
            }

        }


        [TestMethod]
        public void Get_Non_Existant_Item_Returns_Not_Found()
        {
            //Arrange
            DbContextOptions<ShoppingListContext> options = new TestHelper().GetShoppingListContextOptions();

            var mockData = MockData.LargeShoppingList();

            using (var context = new ShoppingListContext(options))
            {
                context.AddRange(mockData);
                context.SaveChanges();
            };

            using (var context = new ShoppingListContext(options))
            {
                IShoppingListRepository mockRepo = new ShoppingListRepository(context);
                var controller = new ShoppingListController(mockRepo);

                //Act
                var result = controller.Get("Does Not Exist");

                //Assert
                Assert.IsNotNull(result);
                Assert.AreEqual(404, result.StatusCode);
                Assert.AreEqual(result.Value, "Drink: Does Not Exist not found on the shopping list.");
            }

        }


    }
}
