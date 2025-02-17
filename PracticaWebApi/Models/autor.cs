using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PracticaWebApi.Models
{
    public class autor
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        [StringLength(50)]
        public string nombre { get; set; }
        [StringLength(50)]
        public string nacionalidad { get; set; }
    }
}
