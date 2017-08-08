using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Checkout.ShoppingList.Service.Controllers
{
    public class BaseController : Controller
    {


        protected  BadRequestObjectResult FormattedErrorResponse(ModelStateDictionary modelState)
        {
            return new BadRequestObjectResult(modelState.Values.SelectMany(x => x.Errors.Select(y => y.ErrorMessage)).ToList());
        }

    }
}
