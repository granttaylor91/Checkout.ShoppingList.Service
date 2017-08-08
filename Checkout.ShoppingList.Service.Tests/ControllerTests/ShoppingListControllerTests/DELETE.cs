using Checkout.ShoppingList.Data;
using Checkout.ShoppingList.Service.Controllers;
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
    public class DELETE
    {

        [TestMethod]
        public void Delete_Removes_Existing_Drink_From_Shopping_List()
        {
            //Arrange
            DbContextOptions<ShoppingListContext> options = new TestHelper().GetShoppingListContextOptions();

            var mockData = MockData.LargeShoppingList();

            var expectedResult = mockData.FirstOrDefault();

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
                var result = controller.Get(expectedResult.Name);

                //Assert
                Assert.IsNotNull(result);
                Assert.AreEqual(200, result.StatusCode);
                Assert.AreEqual(result.Value, expectedResult);

            }
        }

    }
}
