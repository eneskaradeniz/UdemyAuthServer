using Microsoft.AspNetCore.Mvc;
using SharedLibrary.DTOs;

namespace UdemyAuthServer.API.Controllers
{
    public class CustomControllerBase : ControllerBase
    {
        public IActionResult ActionResultInstance<T>(Response<T> response) where T : class
        {
            return new ObjectResult(response)
            {
                StatusCode = response.StatusCode,
            };
        }
    }
}
