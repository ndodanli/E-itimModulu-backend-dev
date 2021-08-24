using Entities.Dtos;
using Entities.Dtos.ErrorDtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace EgitimModuluApp.Filter
{
    public class ValidationFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                ErrorDto errorDto = new ErrorDto();
                foreach (var item in context.ModelState)
                {
                    errorDto.Errors.Add(item.Key, item.Value.Errors.Select(err => err.ErrorMessage).ToArray());
                }
                context.Result = new BadRequestObjectResult(errorDto.Errors);
            }
        }
    }
}
