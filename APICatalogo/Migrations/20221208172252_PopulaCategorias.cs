using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace APICatalogo.Migrations
{
    public partial class PopulaCategorias : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("insert into Categoria(Nome, ImagemUrl) Values('Bebidas', 'Bebidas.jpg')");
            migrationBuilder.Sql("insert into Categoria(Nome, ImagemUrl) Values('Lanches', 'Lanches.jpg')");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("Delete from Categoria");
        }
    }
}
