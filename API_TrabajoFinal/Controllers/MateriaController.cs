using API_TrabajoFinal.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API_TrabajoFinal.Controllers
{
    [EnableCors("ReglasCors")]
    [Route("api/subjects")]
    [ApiController]
    public class MateriaController : ControllerBase
    {
        public readonly FacultadContext _dbContext;

        public MateriaController(FacultadContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        [Route("list_all")]
        public IActionResult Lista()
        {
            List<Materia> listaMaterias = new List<Materia>();
            try
            {
                listaMaterias = _dbContext.Materias.Include(c => c.NroLegajoPNavigation).ToList();
                return StatusCode(StatusCodes.Status200OK, new { message = "ok", response = listaMaterias });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status200OK, new { message = ex.Message, response = listaMaterias });
            }
        }

        [HttpGet]
        [Route("list_by_id/{codMateria}")]
        public IActionResult Obtener(string codMateria)
        {
            Materia materia = _dbContext.Materias.Find(codMateria);
            if (materia == null)
            {
                return BadRequest("Materia no encontrada");
            }

            try
            {
                materia = _dbContext.Materias.Include(c => c.NroLegajoPNavigation).Where(p => p.CodMateria == codMateria).FirstOrDefault();
                return StatusCode(StatusCodes.Status200OK, new { message = "ok", response = materia });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status200OK, new { message = ex.Message, response = materia });
            }
        }

        [HttpPost]
        [Route("save")]
        public IActionResult guardar([FromBody] Materia objeto)
        {
            try
            {
                var ultimoRegistro = _dbContext.Materias.OrderByDescending(e => e.CodMateria).FirstOrDefault();
                string codUltimoRegistro = ultimoRegistro.CodMateria;
                char[] caracteresABorrar = new char[] { 'M', 'A' };
                string nroUltimoRegistro = string.Join("", codUltimoRegistro.Split(caracteresABorrar));
                int nuevoNumero = int.Parse(nroUltimoRegistro) + 1;
                string nuevoCodigo = $"MA{nuevoNumero:D3}";
                objeto.CodMateria = nuevoCodigo;
                _dbContext.Materias.Add(objeto);
                _dbContext.SaveChanges();
                return StatusCode(StatusCodes.Status200OK, new { message = "ok", codigo = nuevoCodigo });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status200OK, new { message = ex.Message, error = ex.InnerException?.Message });
            }
        }

        [HttpPut]
        [Route("update")]
        public IActionResult actualizar([FromBody] Materia objeto)
        {
            Materia materia = _dbContext.Materias.Find(objeto.CodMateria);
            if (materia == null)
            {
                return BadRequest("Materia no encontrada");
            }

            try
            {
                materia.DescMat = objeto.DescMat is null ? materia.DescMat : objeto.DescMat;
                materia.DescCarrera = objeto.DescCarrera is null ? materia.DescCarrera : objeto.DescCarrera;
                materia.NroLegajoP = objeto.NroLegajoP;

                _dbContext.Materias.Update(materia);
                _dbContext.SaveChanges();
                return StatusCode(StatusCodes.Status200OK, new { message = "ok" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status200OK, new { message = ex.Message, error = ex.InnerException?.Message });
            }
        }

        [HttpDelete]
        [Route("delete/{codMateria}")]
        public IActionResult eliminar(string codMateria)
        {
            Materia materia = _dbContext.Materias.Find(codMateria);

            if (materia == null)
            {
                return BadRequest("Materia no encontrada");
            }

            try
            {
                _dbContext.Materias.Remove(materia);
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
