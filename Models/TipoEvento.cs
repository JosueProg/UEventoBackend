using System.Diagnostics;
using System.Text.Json.Serialization;

namespace UEventoBackend.Models
{
    public class TipoEvento
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;

        // Un tipo de evento puede pertenecer a muchos eventos
        [JsonIgnore] // Evita ciclos de serialización infinita
        public ICollection<Evento> Eventos { get; set; } = new List<Evento>();
    }
}
