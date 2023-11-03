using Localization.Localize;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace Localization.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly IStringLocalizer<Resource> _stringLocalizer;

        public ValuesController(IStringLocalizer<Resource> stringLocalizer)
        {
            _stringLocalizer = stringLocalizer;
        }

        [HttpGet]
        public string Get()
        {
            //var language = Request.Headers["Accept-Language"].ToString();
            var value = _stringLocalizer["ProductNotFound"].Value;
            return value;
        }
    }
}
