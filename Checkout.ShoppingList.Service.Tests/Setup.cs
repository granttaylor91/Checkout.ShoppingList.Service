using Checkout.ShoppingList.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Checkout.ShoppingList.Service.Tests
{
    public class TestHelper
    {
        public DbContextOptions<ShoppingListContext> GetShoppingListContextOptions()
        {
            
            var builder = new DbContextOptionsBuilder<ShoppingListContext>();
            //Creates a new context for each test
            builder.UseInMemoryDatabase(Guid.NewGuid().ToString());
            var options = builder.Options;

            return options;
        }

    }
}
