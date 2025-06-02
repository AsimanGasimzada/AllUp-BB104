using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AllUp_BB104.Migrations
{
    /// <inheritdoc />
    public partial class ChangeProductTags : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ProductTags_ProductId",
                table: "ProductTags");

            migrationBuilder.CreateIndex(
                name: "IX_ProductTags_ProductId_TagId",
                table: "ProductTags",
                columns: new[] { "ProductId", "TagId" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ProductTags_ProductId_TagId",
                table: "ProductTags");

            migrationBuilder.CreateIndex(
                name: "IX_ProductTags_ProductId",
                table: "ProductTags",
                column: "ProductId");
        }
    }
}
