using Microsoft.EntityFrameworkCore.Migrations;

namespace BlogCore.AccesoDatos.Migrations
{
    public partial class AgregadoCampoDescripcionEnArticulo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "descripción",
                table: "Articulo");

            migrationBuilder.AddColumn<string>(
                name: "descripcion",
                table: "Articulo",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "descripcion",
                table: "Articulo");

            migrationBuilder.AddColumn<string>(
                name: "descripción",
                table: "Articulo",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
