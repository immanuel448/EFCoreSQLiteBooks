using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EFCoreSQLiteBooks.Migrations
{
    /// <inheritdoc />
    public partial class CambiosModelo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AnoPublicacion",
                table: "Libros",
                newName: "AnhoPublicacion");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AnhoPublicacion",
                table: "Libros",
                newName: "AnoPublicacion");
        }
    }
}
