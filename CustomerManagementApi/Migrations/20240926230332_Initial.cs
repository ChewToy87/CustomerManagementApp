using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CustomerManagementApi.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    FirstName = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Surname = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "TEXT", nullable: false),
                    PhoneNumber = table.Column<string>(type: "TEXT", nullable: false),
                    Created = table.Column<DateTime>(type: "TEXT", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    Updated = table.Column<DateTime>(type: "TEXT", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "Id", "Created", "Email", "FirstName", "PhoneNumber", "Surname", "Updated" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 9, 27, 0, 3, 31, 794, DateTimeKind.Local).AddTicks(8272), "john.doe@example.com", "John", "1234567890", "Doe", new DateTime(2024, 9, 27, 0, 3, 31, 794, DateTimeKind.Local).AddTicks(8311) },
                    { 2, new DateTime(2024, 9, 27, 0, 3, 31, 794, DateTimeKind.Local).AddTicks(8315), "jane.smith@example.com", "Jane", "0987654321", "Smith", new DateTime(2024, 9, 27, 0, 3, 31, 794, DateTimeKind.Local).AddTicks(8317) },
                    { 3, new DateTime(2024, 9, 27, 0, 3, 31, 794, DateTimeKind.Local).AddTicks(8319), "bob.brown@example.com", "Bob", "1122334455", "Brown", new DateTime(2024, 9, 27, 0, 3, 31, 794, DateTimeKind.Local).AddTicks(8321) },
                    { 4, new DateTime(2024, 9, 27, 0, 3, 31, 794, DateTimeKind.Local).AddTicks(8323), "alice.johnson@example.com", "Alice", "2233445566", "Johnson", new DateTime(2024, 9, 27, 0, 3, 31, 794, DateTimeKind.Local).AddTicks(8324) },
                    { 5, new DateTime(2024, 9, 27, 0, 3, 31, 794, DateTimeKind.Local).AddTicks(8327), "charlie.white@example.com", "Charlie", "3344556677", "White", new DateTime(2024, 9, 27, 0, 3, 31, 794, DateTimeKind.Local).AddTicks(8328) }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Customers");
        }
    }
}
