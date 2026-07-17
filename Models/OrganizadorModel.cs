using System.ComponentModel.DataAnnotations;

namespace UEventoBackend.Models
{
    /// <summary>
    /// Entidad Organizador. Coincide 1:1 con la interfaz `Organizador`
    /// del frontend Angular (src/app/models/organizador.model.ts).
    /// Las propiedades van en PascalCase; ASP.NET Core las serializa
    /// automáticamente a camelCase en el JSON (nombre, email, etc.).
    /// </summary>
    public class OrganizadorModel
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(120)]
        public string Nombre { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        [MaxLength(150)]
        public string Email { get; set; } = string.Empty;

        [Required]
        [MaxLength(200)]
        public string Password { get; set; } = string.Empty;

        [MaxLength(100)]
        public string Facultad { get; set; } = string.Empty;

        [MaxLength(100)]
        public string Cargo { get; set; } = string.Empty;

        [MaxLength(20)]
        public string Telefono { get; set; } = string.Empty;

        public bool Activo { get; set; } = true;
    }
}
