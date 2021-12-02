using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentEnrollment.Api.Utils;
using StudentEnrollment.Core;

namespace StudentEnrollment.API.Controllers
{
    public class BaseController : Controller
    {
        protected new IActionResult Ok()
        {
            return base.Ok(Envelope.Ok());
        }

        protected IActionResult Ok<T>(T result)
        {
            return base.Ok(Envelope.Ok(result));
        }

        protected IActionResult Error(string errorMessage)
        {
            return BadRequest(Envelope.Error(errorMessage));
        }
        protected IActionResult Created(string response)
        {
            return StatusCode(StatusCodes.Status201Created, Envelope.Ok(response));
        }

        protected IActionResult FromResult(Result result)
        {
            return result.Sucess ? Created(result.Response.ToString()) : Error(result.Response.ToString());
        }

         protected IActionResult OkResult(Result result)
        {
            return result.Sucess ? Ok(result.Response.ToString()) : Error(result.Response.ToString());
        }
    }
}