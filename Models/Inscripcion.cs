using System.Text.Json.Serialization;

namespace UEventoBackend.Models
{
    public class Inscripcion
    {
        public int Id { get; set; }
        public int EventoId { get; set; }

        [JsonIgnore] // Evita ciclos al serializar datos relacionales
        public Evento? Evento { get; set; }

        public string Nombre { get; set; } = string.Empty;
        public string Cedula { get; set; } = string.Empty; // C.I. del estudiante
    }
}
