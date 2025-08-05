using System.Reflection;
using Microsoft.AspNetCore.Mvc;

namespace Crypto.Compare.PublicApi.Controllers;

public class HomeController : ControllerBase
{
    /// <summary>
    /// Main page
    /// </summary>
    [HttpGet("")]
    public ActionResult Index()
    {
        var version = typeof(HomeController).Assembly.GetCustomAttribute<AssemblyFileVersionAttribute>().Version;
        return Content($"api.crypto.compare - {version}");
    }
}