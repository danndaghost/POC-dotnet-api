namespace HelloWorldApi.Application.DTOs
{
    public record GenGeneralDto
    {
        public int Id { get; init; }
        public string? Codigo { get; init; }
        public string? Nombre { get; init; }
        public string? Descripcion { get; init; }
        public string? Valor { get; init; }
        public bool? Activo { get; init; }
        public DateTime? FechaCreacion { get; init; }
        public DateTime? FechaModificacion { get; init; }
    }

    public record CreateGenGeneralDto
    {
        public string Codigo { get; init; } = string.Empty;
        public string Nombre { get; init; } = string.Empty;
        public string? Descripcion { get; init; }
        public string? Valor { get; init; }
        public bool? Activo { get; init; } = true;
    }

    public record UpdateGenGeneralDto
    {
        public string? Nombre { get; init; }
        public string? Descripcion { get; init; }
        public string? Valor { get; init; }
        public bool? Activo { get; init; }
    }
}