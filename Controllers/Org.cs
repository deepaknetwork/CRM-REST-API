using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AnOrg.Services;

namespace AnOrg.Controllers
{
    [Route("org/")]
    [ApiController]
    public class Org : ControllerBase
    {
        private readonly OrgService _servie;

        public Org(OrgService servie)
        {
            _servie = servie;
        }

        [HttpGet("start")]
        public ActionResult starting()
        {
            Console.WriteLine("starting");
            _servie.startapp();
            return Ok();
        }
    }
}
