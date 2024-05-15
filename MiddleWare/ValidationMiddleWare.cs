using Newtonsoft.Json;
using QuestionnaireApp.Model;
using Microsoft.AspNetCore.Http;
using System.Linq;
using System.Threading.Tasks;
using ApplicationFormApi.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace QuestionnaireApp.MiddleWare
{
    public class ValidationMiddleware
    {
        private readonly RequestDelegate _next;

        public ValidationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var endpoint = context.GetEndpoint();

            if (endpoint != null)
            {
                var actionContextAccessor = context.RequestServices.GetRequiredService<IActionContextAccessor>();
                var actionContext = actionContextAccessor.ActionContext;

                if (actionContext != null && !actionContext.ModelState.IsValid)
                {
                    var errors = actionContext.ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage)).ToList();
                    context.Response.ContentType = "application/json";
                    context.Response.StatusCode = 400;

                    var response = new ApiResponse<object>(false, "Validation failed", null, errors);
                    var jsonResponse = JsonConvert.SerializeObject(response);

                    await context.Response.WriteAsync(jsonResponse);
                    return;
                }
            }

            await _next(context);
        }
    }


}
