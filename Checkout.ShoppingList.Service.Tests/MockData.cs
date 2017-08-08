using Checkout.ShoppingList.Data.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Checkout.ShoppingList.Service.Tests
{
    public static class MockData
    {


        public static List<DrinkOrder> LargeShoppingList(){

            return new List<DrinkOrder>
            {
                new DrinkOrder{Name = "Pepsi", Quantity = 1},
                new DrinkOrder{Name = "Coca Cola", Quantity = 2},
                new DrinkOrder{Name = "Water", Quantity = 3},
                new DrinkOrder{Name = "Coffee", Quantity = 5},
                new DrinkOrder{Name = "Orange Juice", Quantity = 2},
                new DrinkOrder{Name = "Milk", Quantity = 6},
                new DrinkOrder{Name = "Whiskey", Quantity = 1}
            };
        }


    }
}
