using Microsoft.EntityFrameworkCore.Migrations;

namespace KoronaZakupy.Migrations
{
    public partial class releaseUser2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "35a23980-3be4-4f2b-95e5-ec3ea32d55be");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7c690132-5a63-4f47-8f61-2755974d8f0a");

            migrationBuilder.CreateTable(
                name: "Raitings",
                columns: table => new
                {
                    RaitingId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Value = table.Column<double>(nullable: false),
                    UserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Raitings", x => x.RaitingId);
                    table.ForeignKey(
                        name: "FK_Raitings_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "bdd02d19-3110-42d1-b824-3ae79cb970dd", "6b4b59b7-50f8-4405-8be4-697724fc7836", "Volunteer", "VOLUNTEER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "715f34dc-502e-4f1e-9c35-60abfe948e98", "4bc87622-cce4-4b84-b0f4-f115f498eb6b", "PersonInQuarantine", "PERSONINQUARANTINE" });

            migrationBuilder.CreateIndex(
                name: "IX_Raitings_UserId",
                table: "Raitings",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Raitings");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "715f34dc-502e-4f1e-9c35-60abfe948e98");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "bdd02d19-3110-42d1-b824-3ae79cb970dd");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "7c690132-5a63-4f47-8f61-2755974d8f0a", "6d9a15a8-6f27-4470-ba1b-ebde0a7b743c", "Volunteer", "VOLUNTEER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "35a23980-3be4-4f2b-95e5-ec3ea32d55be", "de7464cf-8051-47b0-b665-6aac36f9496d", "PersonInQuarantine", "PERSONINQUARANTINE" });
        }
    }
}
