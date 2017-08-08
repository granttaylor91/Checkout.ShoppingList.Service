using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Checkout.ShoppingList.Data;
using Checkout.ShoppingList.Data.Model;
using Checkout.ShoppingList.Service.Attributes;
using System.Net;

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
        public ObjectResult Get()
        {
            var result = _shoppingListRepository.GetAll();
            return new OkObjectResult(result);
        }

        
        [HttpGet("{name}")]
        public ObjectResult Get(string name)
        {
            var result = _shoppingListRepository.Get(name);

            if(result == null)
            {
                return new NotFoundObjectResult($"Drink: {name} not found on the shopping list.");
            }

            return new OkObjectResult(result);
        }

        [HttpPost]
        [ModelValidation]
        public ObjectResult Post([FromBody]DrinkOrder drinkOrder)
        {
            var result = _shoppingListRepository.Insert(drinkOrder);
            if(result == null)
            {
                var objectResult = new ObjectResult($"Drink {drinkOrder.Name} already exists in the shopping list.");
                objectResult.StatusCode = (int)HttpStatusCode.Conflict;
                return objectResult;
            }

            string returnedUri =  string.Format("{0}://{1}{2}/{3}", 
                this.HttpContext.Request.Scheme, this.HttpContext.Request.Host, this.Request.Path, drinkOrder.Name);

            return Created(returnedUri, drinkOrder);
        }

 
        [HttpPut]
        [ModelValidation]
        public ObjectResult Put([FromBody]DrinkOrder drinkOrder)
        {
           
            var result = _shoppingListRepository.Update(drinkOrder);

            if(result == null)
            {
                return new NotFoundObjectResult($"Drink: {drinkOrder.Name} not found on the shopping list.");
            }

            return new OkObjectResult(result);
        }

        [HttpDelete]
        public ObjectResult Delete(string name)
        {
            _shoppingListRepository.Delete(name);

            return new OkObjectResult(null);
        }
    }
}
