using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace SteveTalksCode.MiddlewareStylesDemo.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GuidCheckerController : ControllerBase
    {
        [HttpGet]
        public string[] Get()
        {
            var reVal = new List<string>();

            reVal.AddRange(
                HttpContext.Items
                    .Where(kvp => kvp.Key?.ToString()?.StartsWith(GuidInstance.ContextKey) ?? false)
                    .Select(k => k.Value?.ToString() ?? "(Null Value)")
            );

            return reVal.ToArray();

        }
    }
}
