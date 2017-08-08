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


        /// <summary>
        /// Retrieves all drink orders in the shopping list.
        /// </summary>
        /// <response code="200">Returns the entire shopping list.</response>
        [HttpGet]
        public ObjectResult Get()
        {
            var result = _shoppingListRepository.GetAll();
            return new OkObjectResult(Json(result));
        }


        /// <summary>
        /// Retrieves a single drink order and and it's quantity.
        /// </summary>
        /// <param name="name"></param>
        /// <response code="200">Returns the drink order and it's quantity</response>
        /// <response code="404">If the drink order cannot be found.</response>
        [HttpGet("{name}")]
        public ObjectResult Get(string name)
        {
            var result = _shoppingListRepository.Get(name);

            if(result == null)
            {
                return new NotFoundObjectResult(Json($"Drink: {name} not found on the shopping list."));
            }

            return new OkObjectResult(result);
        }


        /// <summary>
        /// Adds a drink order to the Shopping List.
        /// </summary>
        /// <remarks>
        /// Note: This endpoint will never return a 200 response.
        /// There is an issue in the generated comments that is adding it by default.
        /// </remarks>
        /// <param name="drinkOrder"></param>
        /// <response code="201">Returns created drink order and location (via header).</response>
        /// <response code="400">If the order is null or has an invalid quantity.</response>
        /// <response code="409">If the order already exists in the shopping list.</response>
        [HttpPost]
        [ModelValidation]
        public ObjectResult Post([FromBody]DrinkOrder drinkOrder)
        {
            var result = _shoppingListRepository.Insert(drinkOrder);
            if(result == null)
            {
                var objectResult = new ObjectResult(Json($"Drink {drinkOrder.Name} already exists in the shopping list."));
                objectResult.StatusCode = (int)HttpStatusCode.Conflict;
                return objectResult;
            }

            string returnedUri =  string.Format("{0}://{1}{2}/{3}", 
                this.HttpContext.Request.Scheme, this.HttpContext.Request.Host, this.Request.Path, drinkOrder.Name);

            return Created(returnedUri, drinkOrder);
        }


        /// <summary>
        /// Updates a drink order in the shopping list
        /// </summary>
        /// <remarks>
        /// </remarks>
        /// <param name="drinkOrder"></param>
        /// <response code="200">Returns updated drink order.</response>
        /// <response code="400">If the drink order is null or has an invalid quantity.</response>
        /// <response code="404">If the drink order could not be found.</response>
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


        /// <summary>
        /// Removes a drink order from the shopping list
        /// </summary>
        /// <remarks>
        /// Note: This endpoint will never return a 200 response.
        /// There is an issue in the generated comments that is adding it by default.
        /// </remarks>
        /// <param name="drinkOrder"></param>
        /// <response code="204">Returns regardless, if the drink order existed and was deleted or did not exist.</response>
        [HttpDelete("{name}")]
        public ObjectResult Delete(string name)
        {
            _shoppingListRepository.Delete(name);

            var objectResult = new OkObjectResult(Json("Delete Successful"));
            objectResult.StatusCode = (int)HttpStatusCode.NoContent;
            return objectResult;
        }
    }
}
