using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PolyBalance.Migrations
{
    /// <inheritdoc />
    public partial class addUniqueConstrainToPartyTypeName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Address",
                table: "Stores");

            migrationBuilder.DropColumn(
                name: "Capacity",
                table: "Stores");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Stores");

            migrationBuilder.AddColumn<string>(
                name: "StoreAddress",
                table: "Stores",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "StoreCapacity",
                table: "Stores",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "StoreName",
                table: "Stores",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_PartyTypes_PartyTypeName",
                table: "PartyTypes",
                column: "PartyTypeName",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_PartyTypes_PartyTypeName",
                table: "PartyTypes");

            migrationBuilder.DropColumn(
                name: "StoreAddress",
                table: "Stores");

            migrationBuilder.DropColumn(
                name: "StoreCapacity",
                table: "Stores");

            migrationBuilder.DropColumn(
                name: "StoreName",
                table: "Stores");

            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "Stores",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Capacity",
                table: "Stores",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Stores",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
