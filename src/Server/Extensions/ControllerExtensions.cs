using Microsoft.AspNetCore.Mvc;
using Utils.Errors;
using Utils.Success;
using IResult = Utils.IResult;

namespace Server.Extensions
{
    public static class ControllerExtensions
    {
        public static IActionResult MapWithResult(this ControllerBase controller, IResult result)
        {
            if (result is Success success)
                return controller.StatusCode(success.StatusCode, success.GetValue());
            else if (result is ErrorsCollection errors)
                return controller.StatusCode(errors.StatusCode, errors.Select(e => e.Message));
            else
                return controller.Ok();
        }
    }
}
