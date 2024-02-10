using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Resource.API.Migrations
{
    /// <inheritdoc />
    public partial class TransactionalOutbox : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_TransactionalOutbox",
                table: "TransactionalOutbox");

            migrationBuilder.RenameTable(
                name: "TransactionalOutbox",
                newName: "TransactionalOutboxes");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TransactionalOutboxes",
                table: "TransactionalOutboxes",
                column: "ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_TransactionalOutboxes",
                table: "TransactionalOutboxes");

            migrationBuilder.RenameTable(
                name: "TransactionalOutboxes",
                newName: "TransactionalOutbox");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TransactionalOutbox",
                table: "TransactionalOutbox",
                column: "ID");
        }
    }
}
