using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Holiberry.Api.Common.Response;
using Holiberry.Api.Extensions;

namespace Holiberry.Api.ActionResults
{
    public class ValidationResult : IActionResult
    {
        public async Task ExecuteResultAsync(ActionContext context)
        {
            var modelStateEntries = context.ModelState.Where(m => m.Value.Errors.Count > 0).ToArray();

            var errors = new List<APIError>();
            if (modelStateEntries.Any())
            {
                if (modelStateEntries.Length == 1 && modelStateEntries[0].Value.Errors.Count == 1 && modelStateEntries[0].Key == string.Empty)
                {
                    errors.Add(new APIError("error", modelStateEntries[0].Value.Errors[0].ErrorMessage));
                }
                else
                {
                    var allErrors = modelStateEntries.SelectMany(entry => entry.Value.Errors,
                                                                (parent, child) => new APIError(parent.Key, child.ErrorMessage));
                    errors.AddRange(allErrors);
                }
            }


            context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            var response = new APIErrorResponse
            {
                Message = "Błąd walidacji",
                StatusCode = 400,
                Errors = errors
            };

            await context.HttpContext.Response.WriteJsonAsync(response, "application/problem+json");
        }
    }
}
