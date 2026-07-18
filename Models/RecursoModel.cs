using System.ComponentModel.DataAnnotations;

namespace UEventoBackend.Models
{
    public class RecursoModel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Nombre { get; set; } = string.Empty;

        [Required]
        public string Tipo { get; set; } = string.Empty;

        public int Cantidad { get; set; }

        public bool Disponible { get; set; }

        public string Descripcion { get; set; } = string.Empty;
    }
}