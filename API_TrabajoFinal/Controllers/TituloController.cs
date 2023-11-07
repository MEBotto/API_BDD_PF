using API_TrabajoFinal.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API_TrabajoFinal.Controllers
{
    [EnableCors("ReglasCors")]
    [Route("api/degree")]
    [ApiController]
    public class TituloController : ControllerBase
    {
        public readonly FacultadContext _dbContext;

        public TituloController(FacultadContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        [Route("list_all")]
        public IActionResult Lista()
        {
            List<Titulo> listaTitulos = new List<Titulo>();
            try
            {
                listaTitulos = _dbContext.Titulos.ToList();
                return StatusCode(StatusCodes.Status200OK, new { message = "ok", response = listaTitulos });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status200OK, new { message = ex.Message, response = listaTitulos });
            }
        }
    }
}
