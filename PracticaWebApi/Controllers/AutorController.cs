using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PracticaWebApi.Models;

namespace PracticaWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AutorController : ControllerBase
    {
        private readonly bibliotecaContext _bibliotecaContext;

        public AutorController(bibliotecaContext context)
        {
            _bibliotecaContext = context;
        }
        [HttpGet]
        [Route("GetAllAutores")]
        public IActionResult GetLibros()
        {
            var listado = this._bibliotecaContext.autor.ToList();
            return Ok(listado);
        }

        [HttpPost]
        [Route("Add")]
        public IActionResult GuardarAutor([FromBody] CrearAutorDTO autorDTO)
        {
            try
            {
                if (autorDTO == null)
                    return BadRequest(new { message = "Datos inválidos" });

                var autorNuevo = new autor
                {
                    nombre = autorDTO.nombre,
                    nacionalidad = autorDTO.nacionalidad
                    
                };

                _bibliotecaContext.autor.Add(autorNuevo);
                _bibliotecaContext.SaveChanges();

                return CreatedAtAction(nameof(GetById), new { id = autorNuevo.id_autor }, autorNuevo);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error interno", error = ex.Message });
            }
        }

        [HttpGet]
        [Route("GetById/{id}")]
        public IActionResult GetById(int id)
        {
            try
            {
                var autorConLibros = (from a in _bibliotecaContext.autor
                                      join l in _bibliotecaContext.libro
                                      on a.id_autor equals l.autor_id into libros
                                      where a.id_autor == id
                                      select new
                                      {
                                          AutorId = a.id_autor,
                                          Nombre = a.nombre,
                                          Nacionalidad = a.nacionalidad,
                                          Libros = libros
                                      }).FirstOrDefault();

                if (autorConLibros == null)
                {
                    return NotFound(new { message = "Autor no encontrado", id });
                }

                return Ok(autorConLibros);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "Ocurrió un error interno en el servidor.",
                    error = ex.Message
                });
            }
        }

        [HttpPut]
        [Route("Actualizar/{id}")]
        public IActionResult ActualizarAutor(int id, [FromBody] autor modAutor)
        {

            autor? autorActual = (from e in _bibliotecaContext.autor
                                     where e.id_autor == id
                                     select e).FirstOrDefault();

            if (autorActual == null)
                return BadRequest(new { message = "Datos inválidos" });

            autorActual.nombre = modAutor.nombre;
            autorActual.nacionalidad = modAutor.nacionalidad;
            
            this._bibliotecaContext.Entry(autorActual).State = EntityState.Modified;
            this._bibliotecaContext.SaveChanges();

            return Ok(modAutor);
        }

        [HttpDelete]
        [Route("Delete/{id}")]
        public IActionResult Delete(int id)
        {
            var equipo = _bibliotecaContext.autor.FirstOrDefault(e => e.id_autor == id);

            if (equipo == null)
                return NotFound(new { message = "Equipo no encontrado" });

            _bibliotecaContext.autor.Remove(equipo);
            _bibliotecaContext.SaveChanges();

            return Ok(new { message = "Equipo eliminado correctamente" });
        }

       
        [HttpGet]
        [Route("VerificarAutorConLibros/{id}")]
        public IActionResult VerificarAutorConLibros(int id)
        {
            try
            {
                // Verificamos si existe algún libro asociado al autor
                var autorConLibros = _bibliotecaContext.libro
                    .Any(l => l.autor_id == id); // Verifica si existe al menos un libro del autor

                if (autorConLibros)
                {
                    return Ok(new { message = "El autor tiene libros publicados." });
                }
                else
                {
                    return NotFound(new { message = "El autor no tiene libros publicados." });
                }
            }
            catch (Exception ex)
            {
                // Loguear el error si tienes un sistema de logging
                // Ejemplo: _logger.LogError(ex, "Error al verificar si el autor tiene libros.");

                return StatusCode(500, new
                {
                    message = "Ocurrió un error interno en el servidor.",
                    error = ex.Message
                });
            }
        }
        [HttpGet]
        [Route("GetAutoresConMasLibros")]
        public IActionResult GetAutoresConMasLibros()
        {
            try
            {
                // Realizamos un Join entre autor y libro y contamos los libros por cada autor
                var autoresConMasLibros = (from a in _bibliotecaContext.autor
                                           join l in _bibliotecaContext.libro
                                           on a.id_autor equals l.autor_id into libros
                                           select new
                                           {
                                               AutorId = a.id_autor,
                                               Nombre = a.nombre,
                                               Nacionalidad = a.nacionalidad,
                                               CantidadDeLibros = libros.Count() // Contamos los libros por autor
                                           })
                                            .OrderByDescending(a => a.CantidadDeLibros) // Ordenamos por cantidad de libros
                                            .ToList();

                return Ok(autoresConMasLibros);
            }
            catch (Exception ex)
            {
                // Loguear el error si tienes un sistema de logging
                // Ejemplo: _logger.LogError(ex, "Error al obtener autores con más libros.");

                return StatusCode(500, new
                {
                    message = "Ocurrió un error interno en el servidor.",
                    error = ex.Message
                });
            }
        }




    }
}
