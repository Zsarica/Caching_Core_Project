using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CustomSqlServerCache.Migrations
{
    /// <inheritdoc />
    public partial class new_initial3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Caches",
                columns: table => new
                {
                    Key = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    CacheItem = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Caches", x => x.Key);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Caches");
        }
    }
}
