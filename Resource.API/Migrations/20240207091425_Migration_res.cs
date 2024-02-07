using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Resource.API.Migrations
{
    /// <inheritdoc />
    public partial class Migration_res : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ResourceDb",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Grade = table.Column<int>(type: "int", nullable: false),
                    Own = table.Column<int>(type: "int", nullable: false),
                    Craftable = table.Column<int>(type: "int", nullable: false),
                    Material1 = table.Column<int>(type: "int", nullable: true),
                    Material2 = table.Column<int>(type: "int", nullable: true),
                    Material3 = table.Column<int>(type: "int", nullable: true),
                    Material1Q = table.Column<int>(type: "int", nullable: false),
                    Material2Q = table.Column<int>(type: "int", nullable: false),
                    Material3Q = table.Column<int>(type: "int", nullable: false),
                    LMD = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResourceDb", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ResourceDb_ResourceDb_Material3",
                        column: x => x.Material3,
                        principalTable: "ResourceDb",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ResourceDb_Material3",
                table: "ResourceDb",
                column: "Material3");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ResourceDb");
        }
    }
}
