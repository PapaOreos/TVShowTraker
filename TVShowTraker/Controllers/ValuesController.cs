using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TVShowTraker.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private static readonly string[] values = new[]
        {
            "Value1","Value2","Value3","Value4"
        };

        [HttpGet]
        [Authorize]
        [Route("values")]
        public IActionResult Values()
        {
            return Ok(values);
        }
    }
}
