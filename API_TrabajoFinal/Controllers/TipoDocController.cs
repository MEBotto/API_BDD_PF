using API_TrabajoFinal.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API_TrabajoFinal.Controllers
{
    [EnableCors("ReglasCors")]
    [Route("api/type_docs")]
    [ApiController]
    public class TipoDocController : ControllerBase
    {
        public readonly FacultadContext _dbContext;

        public TipoDocController(FacultadContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        [Route("list_all")]
        public IActionResult Lista()
        {
            List<TipoDoc> listaTipoDocs = new List<TipoDoc>();
            try
            {
                listaTipoDocs = _dbContext.TipoDocs.ToList();
                return StatusCode(StatusCodes.Status200OK, new { message = "ok", response = listaTipoDocs });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status200OK, new { message = ex.Message, response = listaTipoDocs });
            }
        }
    }
}
