using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Checkout.ShoppingList.Service.Attributes
{
    //Action Filter to validate models and return a standardized error list. 
    public class ModelValidationAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                var modelState = context.ModelState;
                context.Result = new BadRequestObjectResult(modelState.Values.SelectMany(x => x.Errors.Select(y => y.ErrorMessage)).ToList());
            }
        }

    }
}
