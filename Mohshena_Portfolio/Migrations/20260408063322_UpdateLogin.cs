using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mohshena_Portfolio.Migrations
{
    /// <inheritdoc />
    public partial class UpdateLogin : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Phone",
                table: "ContactMessages",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Phone",
                table: "ContactMessages");
        }
    }
}
