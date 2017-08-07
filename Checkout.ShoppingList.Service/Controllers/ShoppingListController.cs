using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Checkout.ShoppingList.Data;
using Checkout.ShoppingList.Data.Model;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Checkout.ShoppingList.Service.Controllers
{
    [Route("api/[controller]")]
    public class ShoppingListController : Controller
    {
        private readonly IShoppingListRepository _shoppingListRepository;

        public ShoppingListController(IShoppingListRepository shoppingListRepository)
        {
            _shoppingListRepository = shoppingListRepository;
        }


        // GET: api/ShoppingList
        [HttpGet]
        public async Task<ObjectResult> Get()
        {
            _shoppingListRepository.GetAll();

            throw new NotImplementedException();
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public async Task<ObjectResult> Get(string id)
        {
            _shoppingListRepository.Get(id);

            throw new NotImplementedException();
        }

        // POST api/values
        [HttpPost]
        public async Task<ObjectResult> Post([FromBody]DrinkOrder drinkOrder)
        {
            _shoppingListRepository.Insert(drinkOrder);

            throw new NotImplementedException();
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public async Task<ObjectResult> Put([FromBody]DrinkOrder drinkOrder)
        {
            _shoppingListRepository.Update(drinkOrder);
            
            throw new NotImplementedException();
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public async Task<ObjectResult> Delete([FromBody]DrinkOrder drinkOrder)
        {
            _shoppingListRepository.Delete(drinkOrder);

            throw new NotImplementedException();
        }
    }
}
