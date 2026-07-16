namespace UEventoBackend.Models
{
    public class Evento
    {
        public int Id { get; set; }
        public int OrganizadorId { get; set; }
        public string Titulo { get; set; } = string.Empty;

        // Relación con TipoEvento
        public int TipoEventoId { get; set; }
        public TipoEvento? TipoEvento { get; set; }

        public string Fecha { get; set; } = string.Empty; // Mantenido como string para mapear directamente con el datepicker de Angular
        public string Detalles { get; set; } = string.Empty;
        public string Imagen { get; set; } = string.Empty; // Almacenará la cadena base64 o URL

        // Relación: Un evento contiene múltiples inscripciones
        public ICollection<Inscripcion> Inscripciones { get; set; } = new List<Inscripcion>();
    }
}
