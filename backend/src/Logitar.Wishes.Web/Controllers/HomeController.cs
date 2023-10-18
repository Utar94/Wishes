using Microsoft.AspNetCore.Mvc;

namespace Logitar.Wishes.Web.Controllers;

[ApiExplorerSettings(IgnoreApi = true)]
[Route("")]
public class HomeController : ControllerBase
{
  private readonly Version _version;

  public HomeController(IConfiguration configuration)
  {
    _version = new Version(configuration.GetValue<string>("Version") ?? string.Empty);
  }

  [HttpGet]
  public ActionResult Index() => Ok($"Wishes API v{_version}");
}
