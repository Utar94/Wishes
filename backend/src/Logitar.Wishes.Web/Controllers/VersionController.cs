using Logitar.Wishes.Web.Models.Version;
using Microsoft.AspNetCore.Mvc;

namespace Logitar.Wishes.Web.Controllers;

[ApiController]
[Route("version")]
public class VersionController : ControllerBase
{
  private readonly Version _version;

  public VersionController(IConfiguration configuration)
  {
    _version = new Version(configuration.GetValue<string>("Version") ?? string.Empty);
  }

  [HttpGet]
  public ActionResult<ApplicationVersion> Get() => Ok(new ApplicationVersion(_version));
}
