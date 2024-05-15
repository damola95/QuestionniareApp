using Microsoft.AspNetCore.Mvc;
using QuestionnaireApp.Model;

namespace QuestionnaireApp
{
    public static class ApiResponseHelper
    {
        public static IActionResult CreateResponse<T>(bool success, string message, T data = default, List<string> errors = null)
        {
            return new JsonResult(new ApiResponse<T>(success, message, data, errors));
        }
    }
}
