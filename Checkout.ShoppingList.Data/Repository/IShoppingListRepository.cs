using Checkout.ShoppingList.Data.Model;
using System;
using System.Collections.Generic;

namespace Checkout.ShoppingList.Data
{
    public interface IShoppingListRepository
    {

        IEnumerable<DrinkOrder> GetAll();
        DrinkOrder Get(string id);
        void Insert(DrinkOrder entity);
        void Update(DrinkOrder entity);
        void Delete(DrinkOrder entity);


    }
}
