using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Checkout.ShoppingList.Data.Model;

namespace Checkout.ShoppingList.Data
{
    public class ShoppingListContext : DbContext
    {
        public ShoppingListContext(DbContextOptions<ShoppingListContext> options) : base(options) { }

        public DbSet<DrinkOrder> shoppingList { get; set; }

    }
}
