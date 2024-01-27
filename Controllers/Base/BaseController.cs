using Microsoft.AspNetCore.Mvc;
using Online_Survey.Global;
using System.Net;

namespace Online_Survey.Controllers.Base
{
    [ApiController]
    [Route("api/[controller]")]
    public class BaseController : ControllerBase
    {
        protected IActionResult Result<T>(BaseResponse<T> response)
        {
            switch (response.StatusCode)
            {
                case HttpStatusCode.OK:
                    return new OkObjectResult(response);
                case HttpStatusCode.Created:
                    return new CreatedResult(string.Empty, response);
                case HttpStatusCode.Unauthorized:
                    return new UnauthorizedObjectResult(response);
                case HttpStatusCode.BadRequest:
                    return new BadRequestObjectResult(response);
                case HttpStatusCode.NotFound:
                    return new NotFoundObjectResult(response);
                case HttpStatusCode.Accepted:
                    return new AcceptedResult(string.Empty, response);
                case HttpStatusCode.UnprocessableEntity:
                    return new UnprocessableEntityObjectResult(response);
                case HttpStatusCode.UnsupportedMediaType:
                    var BadRequestObjectResult = new BadRequestObjectResult(response);
                    BadRequestObjectResult.StatusCode = (int)HttpStatusCode.UnsupportedMediaType;
                    return BadRequestObjectResult;
                default:
                    return new BadRequestObjectResult(response);
            }
        }
    }
}
