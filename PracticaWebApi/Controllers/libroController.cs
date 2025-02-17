using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using PracticaWebApi.Models;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.Intrinsics.Arm;

namespace PracticaWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class libroController : ControllerBase
    {
        private readonly bibliotecaContext _bibliotecaContexto;

        public libroController(bibliotecaContext bibliotecaContexto)
        {
            _bibliotecaContexto = bibliotecaContexto;
        }

        ///<summary> 
        ///EndPoint para obtener todos los libros
        ///</summary>
        ///<returns></returns>

        [HttpGet]
        [Route("GetAll")]
        public IActionResult Get()
        {
            List<libro> libros = (from l in _bibliotecaContexto.libro select l).ToList();

            if (libros.Count == 0)
            {
                return NotFound();
            }
            return Ok(libros);
        }

        ///<summary>
        ///EndPoint para retornar libro por su id, incluyendo el nombre del autor
        ///</summary>
        ///<param name="id"></param>
        ///<returns></returns>
        ///
        [HttpGet]
        [Route("GetById/{id}")]
        public IActionResult GetById(int id)
        {
            libro? libro = (from l in _bibliotecaContexto.libro
                            join a in _bibliotecaContexto.autor on l.autor_id equals a.id_autor
                            where l.id_libro == id
                            select new libro
                            {
                                titulo = l.titulo,
                                autor_id = a.id_autor,
                            }).FirstOrDefault();
            if (libro == null)
            {
                return NotFound();
            }
            return Ok(libro);
        }

        [HttpPost]
        [Route("Add")]
        public IActionResult GuardarLibro([FromBody] libro libro)
        {
            try
            {
                _bibliotecaContexto.libro.Add(libro);
                _bibliotecaContexto.SaveChanges();
                return Ok();
            }
            catch (Exception bug)
            {
                return BadRequest(bug.Message);
            }
        }

        [HttpPut]
        [Route("Update/{id}")]
        public IActionResult ActualizaLibro(int id, [FromBody] libro libro)
        {
            try
            {
                libro? libroActual = (from l in _bibliotecaContexto.libro where l.id_libro == id select l).FirstOrDefault();
                if (libroActual == null)
                {
                    return NotFound();
                }
                libroActual.titulo = libro.titulo;
                libroActual.anio_publicacion = libro.anio_publicacion;
                libroActual.autor_id = libro.autor_id;
                libroActual.categoria_id = libro.categoria_id;
                libroActual.resumen = libro.resumen;
                _bibliotecaContexto.Entry(libroActual).State = EntityState.Modified;
                _bibliotecaContexto.SaveChanges();
                return Ok();
            }
            catch (Exception bug)
            {
                return BadRequest(bug.Message);
            }
        }

        [HttpDelete]
        [Route("Delete/{id}")]
        public IActionResult EliminarLibro(int id)
        {
            try
            {
                libro? libro = (from l in _bibliotecaContexto.libro where l.id_libro == id select l).FirstOrDefault();
                if (libro == null)

                    return NotFound();

                _bibliotecaContexto.libro.Attach(libro);
                _bibliotecaContexto.libro.Remove(libro);
                _bibliotecaContexto.SaveChanges();
                return Ok();
            }
            catch (Exception bug)
            {
                return BadRequest(bug.Message);
            }
        }

        [HttpGet]
        [Route("GetLibroMasReciente")]
        public IActionResult libroMasReciente()
        {
            List<libro> libros = (from l in _bibliotecaContexto.libro where l.anio_publicacion > 2005 select l).ToList();

            if (libros.Count == 0)
            {
                return NotFound();
            }
            return Ok(libros);
        }

        [HttpGet]
        [Route("GetTotalLibros")]
        public IActionResult TotalLibros()
        {
            var libros = (from l in _bibliotecaContexto.libro
                          join a in _bibliotecaContexto.autor
                          on l.autor_id equals a.id_autor
                          group l by new { a.id_autor, a.nombre } into g
                          select new
                          {
                              AutorId = g.Key.id_autor,
                              NombreAutor = g.Key.nombre,
                              TotalLibros = g.Count()
                          }).ToList();

            return Ok(libros);
        }
    }
    
}