using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using PracticaWebApi.Models;
using Microsoft.AspNetCore.Mvc;

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
        
    }
}