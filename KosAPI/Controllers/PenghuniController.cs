using Microsoft.AspNetCore.Mvc;
using KosAPI.Context;
using KosAPI.Models;

namespace KosAPI.Controllers
{
    [ApiController]
    [Route("api/penghuni")]
    public class PenghuniController : ControllerBase
    {
        private string _constr;
        public PenghuniController(IConfiguration configuration)
        {
            _constr = configuration.GetConnectionString("koneksi");
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                PenghuniContext context = new PenghuniContext(_constr);
                return Ok(new { status = "success", data = context.GetAll() });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { status = "error", message = ex.Message });
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            try
            {
                PenghuniContext context = new PenghuniContext(_constr);
                var data = context.GetById(id);
                if (data == null) return NotFound(new { status = "error", message = "data tidak ditemukan" });
                return Ok(new { status = "success", data = data });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { status = "error", message = ex.Message });
            }
        }

        [HttpPost]
        public IActionResult Insert([FromBody] Penghuni p)
        {
            try
            {
                if (p == null)
                    return BadRequest(new { status = "error", message = "data kosong" });

                PenghuniContext context = new PenghuniContext(_constr);
                context.Insert(p);

                return Ok(new
                {
                    status = "success",
                    data = "berhasil tambah penghuni"
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    status = "error",
                    message = ex.Message
                });
            }
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] Penghuni p)
        {
            try
            {
                PenghuniContext context = new PenghuniContext(_constr);
                context.Update(id, p);
                return Ok(new { status = "success", data = "berhasil update" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { status = "error", message = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                PenghuniContext context = new PenghuniContext(_constr);
                context.Delete(id);
                return Ok(new { status = "success", data = "berhasil soft delete" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { status = "error", message = ex.Message });
            }
        }
    }
}
