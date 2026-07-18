using System.ComponentModel.DataAnnotations;

namespace UEventoBackend.Models
{
    public class PonenteModel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Nombre { get; set; } = string.Empty;

        [Required]
        public string Especialidad { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        public string Correo { get; set; } = string.Empty;

        public string Telefono { get; set; } = string.Empty;

        public string Institucion { get; set; } = string.Empty;

        public string Biografia { get; set; } = string.Empty;
    }
}