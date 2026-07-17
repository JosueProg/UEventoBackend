using System.ComponentModel.DataAnnotations;

namespace UEventoBackend.Dtos
{
    /// <summary>
    /// Datos de entrada para el login de organizadores.
    /// Reemplaza en el servidor la lógica cliente `buscarCredenciales(email, password)`.
    /// </summary>
    public class LoginRequest
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string Password { get; set; } = string.Empty;
    }
}
