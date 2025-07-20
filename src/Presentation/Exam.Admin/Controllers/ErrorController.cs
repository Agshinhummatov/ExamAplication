using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace ExamAplication.Admin.Controllers
{
    public class ErrorController : Controller
    {
        [Route("Error")]
        public IActionResult Error()
        {
            var exceptionFeature = HttpContext.Features.Get<IExceptionHandlerPathFeature>();
            ViewBag.ErrorMessage = exceptionFeature?.Error?.Message ?? "An unknown error occurred.";
            return View();
        }

        [Route("Error/{statusCode}")]
        public IActionResult HttpStatusCodeHandler(int statusCode)
        {
            switch (statusCode)
            {
                case 404:
                    ViewBag.ErrorMessage = "Page not found";
                    break;
                case 403:
                    ViewBag.ErrorMessage = "Access denied";
                    break;
                default:
                    ViewBag.ErrorMessage = "Unexpected error";
                    break;
            }

            return View("Error");
        }

    }
}
