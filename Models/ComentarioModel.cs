namespace UEventoBackend.Models
{
    public class ComentarioModel
    {
       public int id {get;set;}
        public string estudiante {get;set; }
        public string contenido {get;set;}
        public int calificacion {get;set; }
        public bool anonimo {get;set; }
        public int eventoId {get;set; }

    }
}
