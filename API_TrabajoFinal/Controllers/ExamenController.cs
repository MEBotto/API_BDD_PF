using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API_TrabajoFinal.Models;
using System;
using Microsoft.AspNetCore.Cors;

namespace API_TrabajoFinal.Controllers
{
    [EnableCors("ReglasCors")]
    [Route("api/exams")]
    [ApiController]
    public class ExamenController : ControllerBase
    {
        public readonly FacultadContext _dbContext;

        public ExamenController(FacultadContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        [Route("list_all")]
        public IActionResult Lista()
        {
            List<Examen> listaExamenes = new List<Examen>();
            try
            {
                listaExamenes = _dbContext.Examenes.Include(c => c.NroLegajoANavigation).Include(c => c.CodMatNavigation).Include(c => c.CodTurnoNavigation).ToList();
                return StatusCode(StatusCodes.Status200OK, new { message = "ok", response = listaExamenes });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status200OK, new { message = ex.Message, response = listaExamenes });
            }
        }

        [HttpGet]
        [Route("list_one/{legajoAlumno:int}/{codMateria}/{codTurno}/{year}")]
        public IActionResult Obtener(int legajoAlumno, string codMateria, string codTurno, string year)
        {
            Examen examen = _dbContext.Examenes.Where(p => p.NroLegajoA == legajoAlumno && p.CodMat == codMateria && p.CodTurno == codTurno && p.Año == year).FirstOrDefault();
            if (examen == null)
            { 
                return BadRequest("Examen no encontrado");
            }

            try
            {
                examen = _dbContext.Examenes.Where(p => p.NroLegajoA == legajoAlumno && p.CodMat == codMateria && p.CodTurno == codTurno && p.Año == year).FirstOrDefault();
                return StatusCode(StatusCodes.Status200OK, new { message = "ok", response = examen });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status200OK, new { message = ex.Message, response = examen });
            }
        }

        [HttpPost]
        [Route("save")]
        public IActionResult guardar([FromBody] Examen objeto)
        {
            try
            {
                _dbContext.Examenes.Add(objeto);
                _dbContext.SaveChanges();
                return StatusCode(StatusCodes.Status200OK, new { message = "ok" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status200OK, new { message = ex.Message, error = ex.InnerException?.Message });
            }
        }

        [HttpPut]
        [Route("update")]
        public IActionResult actualizar([FromBody] Examen objeto)
        {
            Examen examen = _dbContext.Examenes.Where(p => p.NroLegajoA == objeto.NroLegajoA && p.CodMat == objeto.CodMat && p.CodTurno == objeto.CodTurno && p.Año == objeto.Año).FirstOrDefault();
            if (examen == null)
            {
                return BadRequest("Profesor no encontrado");
            }

            try
            {
                examen.NroLegajoA = objeto.NroLegajoA is null ? examen.NroLegajoA : objeto.NroLegajoA;
                examen.CodMat = objeto.CodMat is null ? examen.CodMat : objeto.CodMat;
                examen.CodTurno = objeto.CodTurno is null ? examen.CodTurno : objeto.CodTurno;
                examen.Año = objeto.Año is null ? examen.Año : objeto.Año;
                examen.Nota = objeto.Nota is null ? examen.Nota : objeto.Nota;
                examen.FechaInscripcion = objeto.FechaInscripcion is null ? examen.FechaInscripcion : objeto.FechaInscripcion;

                _dbContext.Examenes.Update(examen);
                _dbContext.SaveChanges();
                return StatusCode(StatusCodes.Status200OK, new { message = "ok" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status200OK, new { message = ex.Message, error = ex.InnerException?.Message });
            }
        }

        [HttpDelete]
        [Route("delete/{legajoAlumno:int}/{codMateria}/{codTurno}/{year}")]
        public IActionResult eliminar(int legajoAlumno, string codMateria, string codTurno, string year)
        {
            Examen examen = _dbContext.Examenes.Where(p => p.NroLegajoA == legajoAlumno && p.CodMat == codMateria && p.CodTurno == codTurno && p.Año == year).FirstOrDefault();

            if (examen == null)
            {
                return BadRequest("Examen no encontrado");
            }

            try
            {
                _dbContext.Examenes.Remove(examen);
                _dbContext.SaveChanges();
                return StatusCode(StatusCodes.Status200OK, new { message = "ok" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status200OK, new { message = ex.Message, error = ex.InnerException?.Message });
            }
        }
    }
}
