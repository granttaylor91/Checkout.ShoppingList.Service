using System;
using System.Collections.Generic;
using System.Text;

namespace Checkout.ShoppingList.Data.Model
{
    public class ShoppingList
    {

        //Use the drink name as a key and the order quantity as a value.
        //potentially refactor.
        IEnumerable<DrinkOrder> DrinkOrders { get; set; }
    }
}
