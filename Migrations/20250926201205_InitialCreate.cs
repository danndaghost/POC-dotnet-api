using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace HelloWorldApi.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "sisa");

            migrationBuilder.CreateTable(
                name: "gen_general",
                schema: "sisa",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    codigo = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    nombre = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    descripcion = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    valor = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    activo = table.Column<bool>(type: "boolean", nullable: true, defaultValue: true),
                    fecha_creacion = table.Column<DateTime>(type: "timestamp with time zone", nullable: true, defaultValueSql: "CURRENT_TIMESTAMP"),
                    fecha_modificacion = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_gen_general", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Messages",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Content = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Author = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Messages", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "gen_general",
                schema: "sisa");

            migrationBuilder.DropTable(
                name: "Messages");
        }
    }
}
