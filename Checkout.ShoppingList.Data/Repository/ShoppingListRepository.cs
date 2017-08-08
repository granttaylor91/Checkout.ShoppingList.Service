using System;
using System.Collections.Generic;
using Checkout.ShoppingList.Data.Model;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Checkout.ShoppingList.Data
{
    public class ShoppingListRepository : IShoppingListRepository
    {

        private readonly ShoppingListContext _context;

        public ShoppingListRepository(ShoppingListContext context)
        {
            _context = context;
        }

        public void Delete(string name)
        {
            var entity = _context.shoppingList.FirstOrDefault(x => x.Name == name);
            if(entity != null)
            {
                _context.Remove(entity);
                _context.SaveChanges();
            }
        }

        public DrinkOrder Get(string name)
        {
            var result = _context.shoppingList.FirstOrDefault(x => x.Name == name);
            return result;
        }

        public List<DrinkOrder> GetAll()
        {
            var result = _context.shoppingList.ToList();
            return result;
        }

        public void Insert(DrinkOrder entity)
        {
            _context.shoppingList.Add(entity);
            _context.SaveChanges();
        }

        public DrinkOrder Update(DrinkOrder entity)
        {
            DrinkOrder drinkOrder = _context.shoppingList.AsNoTracking().FirstOrDefault(x=>x.Name == entity.Name);
            if (drinkOrder == null)
            {
                return null;
            }
            _context.shoppingList.Update(entity);
            _context.SaveChanges();

            return entity;
        }
    }
}
