using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Library.Api.Migrations
{
    public partial class InitialCreation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Books",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Title = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    Author = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    ReleasedYear = table.Column<string>(type: "character varying(5)", maxLength: 5, nullable: false),
                    Pages = table.Column<int>(type: "integer", maxLength: 5, nullable: false),
                    Version = table.Column<int>(type: "integer", maxLength: 5, nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Books", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BookRent",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    PersonId = table.Column<Guid>(type: "uuid", nullable: false),
                    BookId = table.Column<Guid>(type: "uuid", nullable: false),
                    RentedDay = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    DayToReturn = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ReturnedDay = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    BookCondition = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookRent", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BookRent_Books_BookId",
                        column: x => x.BookId,
                        principalTable: "Books",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BookRent_BookId",
                table: "BookRent",
                column: "BookId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BookRent");

            migrationBuilder.DropTable(
                name: "Books");
        }
    }
}
