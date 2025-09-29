using Microsoft.EntityFrameworkCore;
using HelloWorldApi.Domain.Entities;

namespace HelloWorldApi.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Message> Messages { get; set; }
        public DbSet<GenGeneral> GenGeneral { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configuración para la tabla gen_general en el esquema sisa
            modelBuilder.Entity<GenGeneral>(entity =>
            {
                entity.ToTable("gen_general", "sisa");
                
                entity.HasKey(e => e.Id);
                
                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .IsRequired();
                
                entity.Property(e => e.Codigo)
                    .HasColumnName("codigo")
                    .HasMaxLength(50);
                    
                entity.Property(e => e.Nombre)
                    .HasColumnName("nombre")
                    .HasMaxLength(200);
                    
                entity.Property(e => e.Descripcion)
                    .HasColumnName("descripcion")
                    .HasMaxLength(500);
                    
                entity.Property(e => e.Valor)
                    .HasColumnName("valor")
                    .HasMaxLength(500);
                    
                entity.Property(e => e.Activo)
                    .HasColumnName("activo")
                    .HasDefaultValue(true);
                    
                entity.Property(e => e.FechaCreacion)
                    .HasColumnName("fecha_creacion")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");
                    
                entity.Property(e => e.FechaModificacion)
                    .HasColumnName("fecha_modificacion");
            });

            // Configuración para Message (mantenemos compatibilidad)
            modelBuilder.Entity<Message>(entity =>
            {
                entity.HasKey(e => e.Id);
                
                entity.Property(e => e.Content)
                    .IsRequired()
                    .HasMaxLength(1000);
                    
                entity.Property(e => e.CreatedAt)
                    .IsRequired();
            });
        }
    }
}