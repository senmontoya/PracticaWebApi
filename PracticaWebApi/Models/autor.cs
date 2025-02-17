using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PracticaWebApi.Models
{
    public class autor
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id_autor { get; set; }
        [StringLength(50)]
        public string nombre { get; set; }
        [StringLength(50)]
        public string nacionalidad { get; set; }

    }
    public class CrearAutorDTO
    {
        [Required]
        [StringLength(50)]
        public string nombre { get; set; }
        [Required]
        [StringLength(50)]
        public string nacionalidad { get; set; }
    }
}
