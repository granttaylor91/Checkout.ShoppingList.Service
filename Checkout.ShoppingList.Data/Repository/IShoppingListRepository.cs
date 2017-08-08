using Checkout.ShoppingList.Data.Model;
using System;
using System.Collections.Generic;

namespace Checkout.ShoppingList.Data
{
    public interface IShoppingListRepository
    {
        List<DrinkOrder> GetAll();

        DrinkOrder Get(string name);

        DrinkOrder Insert(DrinkOrder entity);

        DrinkOrder Update(DrinkOrder entity);

        void Delete(string name);
    }
}
