namespace HelloWorldApi.Domain.Entities
{
    public class GenGeneral
    {
        public int Id { get; set; }
        public string? Codigo { get; set; }
        public string? Nombre { get; set; }
        public string? Descripcion { get; set; }
        public string? Valor { get; set; }
        public bool? Activo { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public DateTime? FechaModificacion { get; set; }

        // Constructor por defecto requerido por EF
        public GenGeneral() { }

        // Constructor con parámetros básicos
        public GenGeneral(string codigo, string nombre, string? descripcion = null, string? valor = null)
        {
            Codigo = codigo;
            Nombre = nombre;
            Descripcion = descripcion;
            Valor = valor;
            Activo = true;
            FechaCreacion = DateTime.UtcNow;
        }
    }
}