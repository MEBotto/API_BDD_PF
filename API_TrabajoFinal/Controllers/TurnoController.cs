using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API_TrabajoFinal.Models;
using System;
using Microsoft.AspNetCore.Cors;

namespace API_TrabajoFinal.Controllers
{
    [EnableCors("ReglasCors")]
    [Route("api/shifts")]
    [ApiController]
    public class TurnoController : ControllerBase
    {
        public readonly FacultadContext _dbContext;

        public TurnoController(FacultadContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        [Route("info")]
        public IActionResult infoPorTurno()
        {
            List<Materia> listaTurnosInfo = new List<Materia>();
            try
            {
                listaTurnosInfo = _dbContext.Materias.Include(t => t.NroLegajoPNavigation.CodTituloNavigation).ToList();

                return StatusCode(StatusCodes.Status200OK, new { message = "ok", response = listaTurnosInfo });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status200OK, new { message = ex.Message, response = new { } });
            }
        }
    }
}
