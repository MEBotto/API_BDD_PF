using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API_TrabajoFinal.Models;
using System;
using Microsoft.AspNetCore.Cors;

namespace API_TrabajoFinal.Controllers
{
    [EnableCors("ReglasCors")]
    [Route("api/professors")]
    [ApiController]
    public class ProfesorController : ControllerBase
    {
        public readonly FacultadContext _dbContext;

        public ProfesorController(FacultadContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        [Route("list_all")]
        public IActionResult Lista()
        {
            List<Profesor> listaProfesores = new List<Profesor>();
            try
            {
                listaProfesores = _dbContext.Profesores.Include(c => c.CodDocNavigation).Include(c => c.CodTituloNavigation).ToList();
                foreach (var profesor in listaProfesores)
                {
                    if (profesor.Direccion == null)
                    {
                        profesor.Direccion = "Sin dirección";
                    }
                }
                return StatusCode(StatusCodes.Status200OK, new { message = "ok", response = listaProfesores });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status200OK, new { message = ex.Message, response = listaProfesores });
            }
        }

        [HttpGet]
        [Route("list_by_id/{legajoProfesor:int}")]
        public IActionResult Obtener(int legajoProfesor)
        {
            Profesor profesor = _dbContext.Profesores.Find(legajoProfesor);
            if (profesor == null)
            {
                return BadRequest("Profesor no encontrado");
            }

            try
            {
                profesor = _dbContext.Profesores.Include(c => c.CodDocNavigation).Include(c => c.CodTituloNavigation).Where(p => p.NroLegajoP == legajoProfesor).FirstOrDefault();
                if (profesor.Direccion == null)
                {
                    profesor.Direccion = "Sin dirección";
                }
                return StatusCode(StatusCodes.Status200OK, new { message = "ok", response = profesor });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status200OK, new { message = ex.Message, response = profesor });
            }
        }

        [HttpPost]
        [Route("save")]
        public IActionResult guardar([FromBody] Profesor objeto)
        {
            try
            {
                _dbContext.Profesores.Add(objeto);
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
        public IActionResult actualizar([FromBody] Profesor objeto)
        {
            Profesor profesor = _dbContext.Profesores.Find(objeto.NroLegajoP);
            if (profesor == null)
            {
                return BadRequest("Profesor no encontrado");
            }

            try
            {
                profesor.ApeNomb = objeto.ApeNomb is null ? profesor.ApeNomb : objeto.ApeNomb;
                profesor.NroDoc = objeto.NroDoc is null ? profesor.NroDoc : objeto.NroDoc;
                profesor.Direccion = objeto.Direccion is null ? profesor.Direccion : objeto.Direccion;
                profesor.Email = objeto.Email is null ? profesor.Email : objeto.Email;
                profesor.Telefono = objeto.Telefono is null ? profesor.Telefono : objeto.Telefono;
                profesor.CodDoc = objeto.CodDoc is null ? profesor.CodDoc : objeto.CodDoc;
                profesor.Sexo = objeto.Sexo is null ? profesor.Sexo : objeto.Sexo;
                profesor.FecNac = objeto.FecNac is null ? profesor.FecNac : objeto.FecNac;
                profesor.EstCivil = objeto.EstCivil is null ? profesor.EstCivil : objeto.EstCivil;
                profesor.CodTitulo = objeto.CodTitulo is null ? profesor.CodTitulo : objeto.CodTitulo;

                _dbContext.Profesores.Update(profesor);
                _dbContext.SaveChanges();
                return StatusCode(StatusCodes.Status200OK, new { message = "ok" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status200OK, new { message = ex.Message, error = ex.InnerException?.Message });
            }
        }

        [HttpDelete]
        [Route("delete/{legajoProfesor:int}")]
        public IActionResult eliminar(int legajoProfesor)
        {
            Profesor profesor = _dbContext.Profesores.Find(legajoProfesor);

            if (profesor == null)
            {
                return BadRequest("Profesor no encontrado");
            }

            try
            {
                _dbContext.Profesores.Remove(profesor);
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
