using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EventManager.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddOrganizerEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Events_AspNetUsers_OrganizerId",
                table: "Events");

            migrationBuilder.AlterColumn<string>(
                name: "CityName",
                table: "Locations",
                type: "character varying(75)",
                maxLength: 75,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "Plan",
                table: "Events",
                type: "character varying(600)",
                maxLength: 600,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(150)",
                oldMaxLength: 150);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Events",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(75)",
                oldMaxLength: 75);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Events",
                type: "character varying(255)",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(150)",
                oldMaxLength: 150);

            migrationBuilder.CreateTable(
                name: "Organizers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    OrganizerName = table.Column<string>(type: "character varying(75)", maxLength: 75, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Organizers", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Organizers_Id",
                table: "Organizers",
                column: "Id",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Events_Organizers_OrganizerId",
                table: "Events",
                column: "OrganizerId",
                principalTable: "Organizers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Events_Organizers_OrganizerId",
                table: "Events");

            migrationBuilder.DropTable(
                name: "Organizers");

            migrationBuilder.AlterColumn<string>(
                name: "CityName",
                table: "Locations",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(75)",
                oldMaxLength: 75);

            migrationBuilder.AlterColumn<string>(
                name: "Plan",
                table: "Events",
                type: "character varying(150)",
                maxLength: 150,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(600)",
                oldMaxLength: 600);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Events",
                type: "character varying(75)",
                maxLength: 75,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Events",
                type: "character varying(150)",
                maxLength: 150,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(255)",
                oldMaxLength: 255);

            migrationBuilder.AddForeignKey(
                name: "FK_Events_AspNetUsers_OrganizerId",
                table: "Events",
                column: "OrganizerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
