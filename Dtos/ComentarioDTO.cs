namespace UEventoBackend.Dtos
{
    public class ComentarioDTO
    {
         
        
            public string Estudiante { get; set; } = string.Empty;
            public string Contenido { get; set; } = string.Empty;
            public int Calificacion { get; set; }
            public bool Anonimo { get; set; }
            public int EventoId { get; set; }
        }
    }



