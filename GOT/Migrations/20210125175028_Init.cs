using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GOT.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Areas",
                columns: table => new
                {
                    AreaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AreaName = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Areas", x => x.AreaId);
                });

            migrationBuilder.CreateTable(
                name: "Checkpoints",
                columns: table => new
                {
                    CheckpointId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CheckpointName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    XCoords = table.Column<double>(type: "float", nullable: false),
                    YCoords = table.Column<double>(type: "float", nullable: false),
                    AreaId = table.Column<int>(type: "int", nullable: false),
                    CheckpointDescription = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Checkpoints", x => x.CheckpointId);
                    table.ForeignKey(
                        name: "FK_Checkpoints_Areas_AreaId",
                        column: x => x.AreaId,
                        principalTable: "Areas",
                        principalColumn: "AreaId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Paths",
                columns: table => new
                {
                    PathId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PathName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ElevationAB = table.Column<int>(type: "int", nullable: false),
                    DistanceAB = table.Column<int>(type: "int", nullable: false),
                    ElevationBA = table.Column<int>(type: "int", nullable: false),
                    DistanceBA = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CheckpointAId = table.Column<int>(type: "int", nullable: false),
                    CheckpointBId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Paths", x => x.PathId);
                    table.ForeignKey(
                        name: "FK_Paths_Checkpoints_CheckpointAId",
                        column: x => x.CheckpointAId,
                        principalTable: "Checkpoints",
                        principalColumn: "CheckpointId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Paths_Checkpoints_CheckpointBId",
                        column: x => x.CheckpointBId,
                        principalTable: "Checkpoints",
                        principalColumn: "CheckpointId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Areas",
                columns: new[] { "AreaId", "AreaName" },
                values: new object[,]
                {
                    { 1, "Beskidy Zachodnie" },
                    { 2, "Beskidy Wschodnie" },
                    { 3, "Tatry" },
                    { 4, "Sudety" },
                    { 5, "Gory Swietokrzyskie" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Checkpoints_AreaId",
                table: "Checkpoints",
                column: "AreaId");

            migrationBuilder.CreateIndex(
                name: "IX_Paths_CheckpointAId",
                table: "Paths",
                column: "CheckpointAId");

            migrationBuilder.CreateIndex(
                name: "IX_Paths_CheckpointBId",
                table: "Paths",
                column: "CheckpointBId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Paths");

            migrationBuilder.DropTable(
                name: "Checkpoints");

            migrationBuilder.DropTable(
                name: "Areas");
        }
    }
}
