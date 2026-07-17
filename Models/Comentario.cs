using System.ComponentModel.DataAnnotations;

namespace UEventoBackend.Models
{
    public class Comentario
    {

        [Key]
        public int Id { get; set; }

        [Required]
        public string Estudiante { get; set; } = string.Empty;

        [Required]
        public string Contenido { get; set; } = string.Empty;

        [Range(1, 5)] 
        public int Calificacion { get; set; }

        public bool Anonimo { get; set; }

        [Required]
        public int EventoId { get; set; }
    }
}
