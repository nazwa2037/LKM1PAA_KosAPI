using Microsoft.AspNetCore.Mvc;
using KosAPI.Context;
using KosAPI.Models;

namespace KosAPI.Controllers
{
    [ApiController]
    [Route("api/pembayaran")]
    public class PembayaranController : ControllerBase
    {
        private string _constr;

        public PembayaranController(IConfiguration configuration)
        {
            _constr = configuration.GetConnectionString("koneksi");
        }

        [HttpPut("kamar/{id_kamar}")]
        public IActionResult Bayar(int id_kamar, [FromBody] PembayaranRequest req)
        {
            try
            {
                PembayaranContext context = new PembayaranContext(_constr);
                context.BayarKamar(id_kamar, req.jumlah_bayar);

                return Ok(new
                {
                    status = "success",
                    data = "Pembayaran berhasil"
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
    }
}