using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API_TrabajoFinal.Models;
using System;
using Microsoft.AspNetCore.Cors;

namespace API_TrabajoFinal.Controllers
{
    [EnableCors("ReglasCors")]
    [Route("api/students")]
    [ApiController]
    public class AlumnoController : ControllerBase
    {
        public readonly FacultadContext _dbContext;

        public AlumnoController(FacultadContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        [Route("list_all")]
        public IActionResult Lista()
        {
            List<Alumno> listaAlumnos = new List<Alumno>();
            try
            {
                listaAlumnos = _dbContext.Alumnos.Include(c => c.CodDocNavigation).ToList();
                return StatusCode(StatusCodes.Status200OK, new { message = "ok", response = listaAlumnos });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status200OK, new { message = ex.Message, response = listaAlumnos });
            }
        }

        [HttpGet]
        [Route("list_by_id/{legajoAlumno:int}")]
        public IActionResult Obtener(int legajoAlumno)
        {
            Alumno alumno = _dbContext.Alumnos.Find(legajoAlumno);
            if (alumno == null)
            {
                return BadRequest("Producto no encontrado");
            }

            try
            {
                alumno = _dbContext.Alumnos.Include(c => c.CodDocNavigation).Where(p => p.NroLegajoA == legajoAlumno).FirstOrDefault();
                return StatusCode(StatusCodes.Status200OK, new { message = "ok", response = alumno });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status200OK, new { message = ex.Message, response = alumno });
            }
        }

        [HttpPost]
        [Route("save")]
        public IActionResult guardar([FromBody] Alumno objeto)
        {
            try
            {
                _dbContext.Alumnos.Add(objeto);
                _dbContext.SaveChanges();
                return StatusCode(StatusCodes.Status200OK, new { message = "ok" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status200OK, new { message = ex.Message , error = ex.InnerException?.Message });
            }
        }

        [HttpPut]
        [Route("update")]
        public IActionResult actualizar([FromBody] Alumno objeto)
        {
            Alumno alumno = _dbContext.Alumnos.Find(objeto.NroLegajoA);
            if (alumno == null)
            {
                return BadRequest("Producto no encontrado");
            }

            try
            {
                alumno.ApeNomb = objeto.ApeNomb is null ? alumno.ApeNomb : objeto.ApeNomb;
                alumno.NroDoc = objeto.NroDoc is null ? alumno.NroDoc : objeto.NroDoc;
                alumno.Direccion = objeto.Direccion is null ? alumno.Direccion : objeto.Direccion;
                alumno.Email = objeto.Email is null ? alumno.Email : objeto.Email;
                alumno.Telefono = objeto.Telefono is null ? alumno.Telefono : objeto.Telefono;
                alumno.CodDoc = objeto.CodDoc is null ? alumno.CodDoc : objeto.CodDoc;
                alumno.Sexo = objeto.Sexo is null ? alumno.Sexo : objeto.Sexo;
                alumno.FecNac = objeto.FecNac is null ? alumno.FecNac : objeto.FecNac;
                alumno.EstCivil = objeto.EstCivil is null ? alumno.EstCivil : objeto.EstCivil;

                _dbContext.Alumnos.Update(alumno);
                _dbContext.SaveChanges();
                return StatusCode(StatusCodes.Status200OK, new { message = "ok" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status200OK, new { message = ex.Message, error = ex.InnerException?.Message });
            }
        }

        [HttpDelete]
        [Route("delete/{legajoAlumno:int}")]
        public IActionResult eliminar(int legajoAlumno)
        {
            Alumno alumno = _dbContext.Alumnos.Find(legajoAlumno);

            if (alumno == null)
            {
                return BadRequest("Producto no encontrado");
            }

            try
            {
                _dbContext.Alumnos.Remove(alumno);
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
