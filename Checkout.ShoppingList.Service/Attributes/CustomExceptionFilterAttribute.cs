using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Checkout.ShoppingList.Service.Attributes
{
    //Action Filter to validate models and return a standardized error list. 
    public class CustomExceptionFilterAttribute : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            //TODO: Log exception
            var jsonResult = new JsonResult(new { error = "An Internal Server Error has ocurred." });
            jsonResult.StatusCode = 500;
            context.Result = jsonResult;
        }
    }
}
