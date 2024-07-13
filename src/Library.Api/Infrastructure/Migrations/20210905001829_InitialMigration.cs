using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Library.Api.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "inventory");

            migrationBuilder.EnsureSchema(
                name: "rentals");

            migrationBuilder.CreateTable(
                name: "books",
                schema: "inventory",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Title = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    Author = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    ReleasedYear = table.Column<string>(type: "character varying(12)", maxLength: 12, nullable: false),
                    Pages = table.Column<int>(type: "integer", nullable: false),
                    Version = table.Column<int>(type: "integer", nullable: false),
                    ISBN = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_books", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "books",
                schema: "rentals",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Title = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    Author = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_books", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "librarians",
                schema: "rentals",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    FirstName = table.Column<string>(type: "text", nullable: true),
                    LastName = table.Column<string>(type: "text", nullable: true),
                    BirthDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Street = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    City = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    Number = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: true),
                    District = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    Cpf = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_librarians", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "locators",
                schema: "rentals",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    FirstName = table.Column<string>(type: "text", nullable: true),
                    LastName = table.Column<string>(type: "text", nullable: true),
                    BirthDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Street = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    City = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    Number = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: true),
                    District = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    Cpf = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_locators", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "penalties",
                schema: "rentals",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    LocatorId = table.Column<Guid>(type: "uuid", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    EndDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Reason = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_penalties", x => x.Id);
                    table.ForeignKey(
                        name: "FK_penalties_locators_LocatorId",
                        column: x => x.LocatorId,
                        principalSchema: "rentals",
                        principalTable: "locators",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "rentals",
                schema: "rentals",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    RentedDay = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    DayToReturn = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    ReturnedDay = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    LocatorId = table.Column<Guid>(type: "uuid", nullable: true),
                    LibrarianId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_rentals", x => x.Id);
                    table.ForeignKey(
                        name: "FK_rentals_librarians_LibrarianId",
                        column: x => x.LibrarianId,
                        principalSchema: "rentals",
                        principalTable: "librarians",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_rentals_locators_LocatorId",
                        column: x => x.LocatorId,
                        principalSchema: "rentals",
                        principalTable: "locators",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "booksrentals",
                schema: "rentals",
                columns: table => new
                {
                    BookId = table.Column<Guid>(type: "uuid", nullable: false),
                    BookRentalId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_booksrentals", x => new { x.BookId, x.BookRentalId });
                    table.ForeignKey(
                        name: "FK_booksrentals_books_BookId",
                        column: x => x.BookId,
                        principalSchema: "rentals",
                        principalTable: "books",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_booksrentals_rentals_BookRentalId",
                        column: x => x.BookRentalId,
                        principalSchema: "rentals",
                        principalTable: "rentals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_booksrentals_BookRentalId",
                schema: "rentals",
                table: "booksrentals",
                column: "BookRentalId");

            migrationBuilder.CreateIndex(
                name: "IX_penalties_LocatorId",
                schema: "rentals",
                table: "penalties",
                column: "LocatorId");

            migrationBuilder.CreateIndex(
                name: "IX_rentals_LibrarianId",
                schema: "rentals",
                table: "rentals",
                column: "LibrarianId");

            migrationBuilder.CreateIndex(
                name: "IX_rentals_LocatorId",
                schema: "rentals",
                table: "rentals",
                column: "LocatorId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "books",
                schema: "inventory");

            migrationBuilder.DropTable(
                name: "booksrentals",
                schema: "rentals");

            migrationBuilder.DropTable(
                name: "penalties",
                schema: "rentals");

            migrationBuilder.DropTable(
                name: "books",
                schema: "rentals");

            migrationBuilder.DropTable(
                name: "rentals",
                schema: "rentals");

            migrationBuilder.DropTable(
                name: "librarians",
                schema: "rentals");

            migrationBuilder.DropTable(
                name: "locators",
                schema: "rentals");
        }
    }
}
