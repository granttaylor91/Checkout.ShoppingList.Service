using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Checkout.ShoppingList.Data.Model;

namespace Checkout.ShoppingList.Data
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options) { }

        //TODO: fix this
        public DbSet<ShoppingList.Data.Model.ShoppingList> shoppingList { get; set; }

    }
}
