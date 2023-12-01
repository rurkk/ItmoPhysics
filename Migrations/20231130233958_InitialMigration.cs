using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ItmoPhysics.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Abscissa",
                columns: table => new
                {
                    AbscissaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Abscissa", x => x.AbscissaId);
                });

            migrationBuilder.CreateTable(
                name: "CurveData",
                columns: table => new
                {
                    CurveDataId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FileId = table.Column<int>(type: "int", nullable: false),
                    AbscissaId = table.Column<int>(type: "int", nullable: false),
                    OrdinateId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CurveData", x => x.CurveDataId);
                });

            migrationBuilder.CreateTable(
                name: "Ordinates",
                columns: table => new
                {
                    OrdinateId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ordinates", x => x.OrdinateId);
                });

            migrationBuilder.CreateTable(
                name: "UsersFiles",
                columns: table => new
                {
                    FileId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FilePath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsersFiles", x => x.FileId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Abscissa");

            migrationBuilder.DropTable(
                name: "CurveData");

            migrationBuilder.DropTable(
                name: "Ordinates");

            migrationBuilder.DropTable(
                name: "UsersFiles");
        }
    }
}
