using Microsoft.AspNetCore.Mvc;

namespace Commander.Controllers
{

  // Name base:  api/tests
  [Route("api/[controller]")]
  [ApiController]
  public class TestsController : ControllerBase
  {

    [HttpGet]
    public ActionResult<string> testNewController()
    {
      return Ok("New Controller Tested Successfully!!!");
    }

  }

}