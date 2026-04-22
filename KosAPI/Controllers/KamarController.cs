using Microsoft.AspNetCore.Mvc;
using KosAPI.Context;

namespace KosAPI.Controllers
{
    [ApiController]
    [Route("api/kamar")]
    public class KamarController : ControllerBase
    {
        private string _constr;
        public KamarController(IConfiguration configuration)
        {
            _constr = configuration.GetConnectionString("koneksi");
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                KamarContext context = new KamarContext(_constr);
                return Ok(new { status = "success", data = context.GetAll() });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { status = "error", message = ex.Message });
            }
        }
    }
}
