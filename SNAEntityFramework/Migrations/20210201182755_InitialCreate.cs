using Microsoft.EntityFrameworkCore.Migrations;

namespace SNAEntityFramework.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Datasets",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: true),
                    LinksCount = table.Column<int>(type: "INTEGER", nullable: false),
                    UsersCount = table.Column<int>(type: "INTEGER", nullable: false),
                    AvgFriendsCount = table.Column<double>(type: "REAL", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Datasets", x => x.Id);
                    table.UniqueConstraint("AK_Datasets_Name", x => x.Name);
                });

            migrationBuilder.CreateTable(
                name: "Links",
                columns: table => new
                {
                    DatasetId = table.Column<int>(type: "INTEGER", nullable: false),
                    User1Id = table.Column<int>(type: "INTEGER", nullable: false),
                    User2Id = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Links", x => new { x.DatasetId, x.User1Id, x.User2Id });
                    table.ForeignKey(
                        name: "FK_Links_Datasets_DatasetId",
                        column: x => x.DatasetId,
                        principalTable: "Datasets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Datasets",
                columns: new[] { "Id", "AvgFriendsCount", "Description", "LinksCount", "Name", "UsersCount" },
                values: new object[] { 1, 5.3333000000000004, "Sample dataset", 48, "Sample", 18 });

            migrationBuilder.InsertData(
                table: "Links",
                columns: new[] { "DatasetId", "User1Id", "User2Id" },
                values: new object[] { 1, 0, 1 });

            migrationBuilder.InsertData(
                table: "Links",
                columns: new[] { "DatasetId", "User1Id", "User2Id" },
                values: new object[] { 1, 7, 11 });

            migrationBuilder.InsertData(
                table: "Links",
                columns: new[] { "DatasetId", "User1Id", "User2Id" },
                values: new object[] { 1, 8, 9 });

            migrationBuilder.InsertData(
                table: "Links",
                columns: new[] { "DatasetId", "User1Id", "User2Id" },
                values: new object[] { 1, 8, 10 });

            migrationBuilder.InsertData(
                table: "Links",
                columns: new[] { "DatasetId", "User1Id", "User2Id" },
                values: new object[] { 1, 8, 11 });

            migrationBuilder.InsertData(
                table: "Links",
                columns: new[] { "DatasetId", "User1Id", "User2Id" },
                values: new object[] { 1, 9, 10 });

            migrationBuilder.InsertData(
                table: "Links",
                columns: new[] { "DatasetId", "User1Id", "User2Id" },
                values: new object[] { 1, 9, 11 });

            migrationBuilder.InsertData(
                table: "Links",
                columns: new[] { "DatasetId", "User1Id", "User2Id" },
                values: new object[] { 1, 10, 11 });

            migrationBuilder.InsertData(
                table: "Links",
                columns: new[] { "DatasetId", "User1Id", "User2Id" },
                values: new object[] { 1, 12, 13 });

            migrationBuilder.InsertData(
                table: "Links",
                columns: new[] { "DatasetId", "User1Id", "User2Id" },
                values: new object[] { 1, 12, 14 });

            migrationBuilder.InsertData(
                table: "Links",
                columns: new[] { "DatasetId", "User1Id", "User2Id" },
                values: new object[] { 1, 7, 10 });

            migrationBuilder.InsertData(
                table: "Links",
                columns: new[] { "DatasetId", "User1Id", "User2Id" },
                values: new object[] { 1, 12, 15 });

            migrationBuilder.InsertData(
                table: "Links",
                columns: new[] { "DatasetId", "User1Id", "User2Id" },
                values: new object[] { 1, 12, 17 });

            migrationBuilder.InsertData(
                table: "Links",
                columns: new[] { "DatasetId", "User1Id", "User2Id" },
                values: new object[] { 1, 13, 14 });

            migrationBuilder.InsertData(
                table: "Links",
                columns: new[] { "DatasetId", "User1Id", "User2Id" },
                values: new object[] { 1, 13, 15 });

            migrationBuilder.InsertData(
                table: "Links",
                columns: new[] { "DatasetId", "User1Id", "User2Id" },
                values: new object[] { 1, 13, 16 });

            migrationBuilder.InsertData(
                table: "Links",
                columns: new[] { "DatasetId", "User1Id", "User2Id" },
                values: new object[] { 1, 13, 17 });

            migrationBuilder.InsertData(
                table: "Links",
                columns: new[] { "DatasetId", "User1Id", "User2Id" },
                values: new object[] { 1, 14, 15 });

            migrationBuilder.InsertData(
                table: "Links",
                columns: new[] { "DatasetId", "User1Id", "User2Id" },
                values: new object[] { 1, 14, 16 });

            migrationBuilder.InsertData(
                table: "Links",
                columns: new[] { "DatasetId", "User1Id", "User2Id" },
                values: new object[] { 1, 14, 17 });

            migrationBuilder.InsertData(
                table: "Links",
                columns: new[] { "DatasetId", "User1Id", "User2Id" },
                values: new object[] { 1, 15, 16 });

            migrationBuilder.InsertData(
                table: "Links",
                columns: new[] { "DatasetId", "User1Id", "User2Id" },
                values: new object[] { 1, 12, 16 });

            migrationBuilder.InsertData(
                table: "Links",
                columns: new[] { "DatasetId", "User1Id", "User2Id" },
                values: new object[] { 1, 7, 9 });

            migrationBuilder.InsertData(
                table: "Links",
                columns: new[] { "DatasetId", "User1Id", "User2Id" },
                values: new object[] { 1, 7, 8 });

            migrationBuilder.InsertData(
                table: "Links",
                columns: new[] { "DatasetId", "User1Id", "User2Id" },
                values: new object[] { 1, 6, 12 });

            migrationBuilder.InsertData(
                table: "Links",
                columns: new[] { "DatasetId", "User1Id", "User2Id" },
                values: new object[] { 1, 0, 2 });

            migrationBuilder.InsertData(
                table: "Links",
                columns: new[] { "DatasetId", "User1Id", "User2Id" },
                values: new object[] { 1, 0, 3 });

            migrationBuilder.InsertData(
                table: "Links",
                columns: new[] { "DatasetId", "User1Id", "User2Id" },
                values: new object[] { 1, 0, 4 });

            migrationBuilder.InsertData(
                table: "Links",
                columns: new[] { "DatasetId", "User1Id", "User2Id" },
                values: new object[] { 1, 0, 5 });

            migrationBuilder.InsertData(
                table: "Links",
                columns: new[] { "DatasetId", "User1Id", "User2Id" },
                values: new object[] { 1, 0, 6 });

            migrationBuilder.InsertData(
                table: "Links",
                columns: new[] { "DatasetId", "User1Id", "User2Id" },
                values: new object[] { 1, 0, 12 });

            migrationBuilder.InsertData(
                table: "Links",
                columns: new[] { "DatasetId", "User1Id", "User2Id" },
                values: new object[] { 1, 1, 2 });

            migrationBuilder.InsertData(
                table: "Links",
                columns: new[] { "DatasetId", "User1Id", "User2Id" },
                values: new object[] { 1, 1, 3 });

            migrationBuilder.InsertData(
                table: "Links",
                columns: new[] { "DatasetId", "User1Id", "User2Id" },
                values: new object[] { 1, 1, 4 });

            migrationBuilder.InsertData(
                table: "Links",
                columns: new[] { "DatasetId", "User1Id", "User2Id" },
                values: new object[] { 1, 1, 5 });

            migrationBuilder.InsertData(
                table: "Links",
                columns: new[] { "DatasetId", "User1Id", "User2Id" },
                values: new object[] { 1, 2, 3 });

            migrationBuilder.InsertData(
                table: "Links",
                columns: new[] { "DatasetId", "User1Id", "User2Id" },
                values: new object[] { 1, 2, 4 });

            migrationBuilder.InsertData(
                table: "Links",
                columns: new[] { "DatasetId", "User1Id", "User2Id" },
                values: new object[] { 1, 2, 5 });

            migrationBuilder.InsertData(
                table: "Links",
                columns: new[] { "DatasetId", "User1Id", "User2Id" },
                values: new object[] { 1, 3, 4 });

            migrationBuilder.InsertData(
                table: "Links",
                columns: new[] { "DatasetId", "User1Id", "User2Id" },
                values: new object[] { 1, 3, 5 });

            migrationBuilder.InsertData(
                table: "Links",
                columns: new[] { "DatasetId", "User1Id", "User2Id" },
                values: new object[] { 1, 4, 5 });

            migrationBuilder.InsertData(
                table: "Links",
                columns: new[] { "DatasetId", "User1Id", "User2Id" },
                values: new object[] { 1, 6, 7 });

            migrationBuilder.InsertData(
                table: "Links",
                columns: new[] { "DatasetId", "User1Id", "User2Id" },
                values: new object[] { 1, 6, 8 });

            migrationBuilder.InsertData(
                table: "Links",
                columns: new[] { "DatasetId", "User1Id", "User2Id" },
                values: new object[] { 1, 6, 9 });

            migrationBuilder.InsertData(
                table: "Links",
                columns: new[] { "DatasetId", "User1Id", "User2Id" },
                values: new object[] { 1, 6, 10 });

            migrationBuilder.InsertData(
                table: "Links",
                columns: new[] { "DatasetId", "User1Id", "User2Id" },
                values: new object[] { 1, 6, 11 });

            migrationBuilder.InsertData(
                table: "Links",
                columns: new[] { "DatasetId", "User1Id", "User2Id" },
                values: new object[] { 1, 15, 17 });

            migrationBuilder.InsertData(
                table: "Links",
                columns: new[] { "DatasetId", "User1Id", "User2Id" },
                values: new object[] { 1, 16, 17 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Links");

            migrationBuilder.DropTable(
                name: "Datasets");
        }
    }
}
