﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
        [HttpGet]
        [Route("GetById/{id}")]
        public IActionResult GetById(int id)
        {
            var autor = this._bibliotecaContext.autor.FirstOrDefault(e => e.id_autor == id);

            if (autor == null)
                return NotFound(new { message = "Autor no encontrado" });

            return Ok(autor);
        }
        
    }
}
