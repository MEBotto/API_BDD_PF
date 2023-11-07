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
            List<Turno> listaTurnosInfo2 = new List<Turno>();
            try
            {
                listaTurnosInfo2 = _dbContext.Turnos.Include(t => t.Planificacions).ThenInclude(p => p.CodMatNavigation).ThenInclude(p => p.NroLegajoPNavigation).ToList();
                foreach (var turno in listaTurnosInfo2)
                {
                    foreach (var planificacion in turno.Planificacions)
                    {
                        if (planificacion.CodMatNavigation.NroLegajoPNavigation.Direccion == null)
                        {
                            planificacion.CodMatNavigation.NroLegajoPNavigation.Direccion = "Sin dirección";
                        }
                    }
                }

                return StatusCode(StatusCodes.Status200OK, new { message = "ok", response = listaTurnosInfo2 });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status200OK, new { message = ex.Message, response = new { } });
            }
        }

        [HttpGet]
        [Route("students")]
        public IActionResult alumnosIncriptosPorTurno()
        {
            List<Turno> listaEstudiantesPorTurno = new List<Turno>();
            try
            {
                listaEstudiantesPorTurno = _dbContext.Turnos.Include(t => t.Examenes).ThenInclude(e => e.NroLegajoANavigation).ToList();
                foreach (var turno in listaEstudiantesPorTurno)
                {
                    foreach (var examen in turno.Examenes)
                    {
                        if (examen.NroLegajoANavigation.Direccion == null)
                        {
                            examen.NroLegajoANavigation.Direccion = "Sin dirección";
                        }
                    }
                }
                return StatusCode(StatusCodes.Status200OK, new { message = "ok", response = listaEstudiantesPorTurno });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status200OK, new { message = ex.Message, response = new { } });
            }
        }
    }
}
