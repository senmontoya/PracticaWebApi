using System.ComponentModel.DataAnnotations;
namespace PracticaWebApi.Models
{
    public class libro
    {
        [Key]

        public int id_libro { get; set; }
        public string titulo { get; set; }
        public int? anio_publicacion { get; set; }
        public int? autor_id { get; set; }
        public int? categoria_id { get; set; }
        public string resumen { get; set; }

        public autor Autor { get; set; }

    }
}
