using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BA.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly IAntiforgery _antiForgeryService;

        public ValuesController(IAntiforgery antiForgeryService)
        {

            _antiForgeryService = antiForgeryService;
        }
        // GET api/values
        [HttpGet]
        public IActionResult GetToken()
        {
            var token = _antiForgeryService.GetTokens(HttpContext).RequestToken;
            HttpContext.Response.Cookies.Append("XSRF-TOKEN", token, new CookieOptions { HttpOnly = false });
            return new StatusCodeResult(StatusCodes.Status200OK);
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
