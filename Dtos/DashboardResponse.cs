namespace UEventoBackend.Dtos
{
    /// <summary>
    /// Un conteo agrupado (por facultad, por cargo, etc.).
    /// </summary>
    public record ConteoPorGrupo(string Grupo, int Cantidad);

    /// <summary>
    /// Estadísticas del módulo de Organizadores para el dashboard.
    /// </summary>
    public class DashboardResponse
    {
        public int Total { get; set; }
        public int Activos { get; set; }
        public int Inactivos { get; set; }
        public List<ConteoPorGrupo> PorFacultad { get; set; } = new();
        public List<ConteoPorGrupo> PorCargo { get; set; } = new();
    }
}
