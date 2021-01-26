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
                name: "Trips",
                columns: table => new
                {
                    TripId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Score = table.Column<int>(type: "int", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsApproved = table.Column<bool>(type: "bit", nullable: false),
                    IsCompleted = table.Column<bool>(type: "bit", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trips", x => x.TripId);
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
                    IsFromAToB = table.Column<bool>(type: "bit", nullable: false),
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

            migrationBuilder.CreateTable(
                name: "PathTrips",
                columns: table => new
                {
                    PathTripID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Order = table.Column<int>(type: "int", nullable: false),
                    PathId = table.Column<int>(type: "int", nullable: false),
                    TripId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PathTrips", x => x.PathTripID);
                    table.ForeignKey(
                        name: "FK_PathTrips_Paths_PathId",
                        column: x => x.PathId,
                        principalTable: "Paths",
                        principalColumn: "PathId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PathTrips_Trips_TripId",
                        column: x => x.TripId,
                        principalTable: "Trips",
                        principalColumn: "TripId",
                        onDelete: ReferentialAction.Cascade);
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

            migrationBuilder.InsertData(
                table: "Trips",
                columns: new[] { "TripId", "Description", "EndDate", "IsApproved", "IsCompleted", "Score", "StartDate" },
                values: new object[,]
                {
                    { 1, null, new DateTime(2020, 11, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), false, false, 10, new DateTime(2020, 11, 19, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, null, new DateTime(2020, 11, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), false, false, 20, new DateTime(2020, 11, 20, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 3, null, new DateTime(2020, 11, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), false, false, 30, new DateTime(2020, 11, 21, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 4, null, new DateTime(2020, 11, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), false, false, 40, new DateTime(2020, 11, 22, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 5, null, new DateTime(2020, 11, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), false, false, 50, new DateTime(2020, 11, 23, 0, 0, 0, 0, DateTimeKind.Unspecified) }
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

            migrationBuilder.CreateIndex(
                name: "IX_PathTrips_PathId",
                table: "PathTrips",
                column: "PathId");

            migrationBuilder.CreateIndex(
                name: "IX_PathTrips_TripId",
                table: "PathTrips",
                column: "TripId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PathTrips");

            migrationBuilder.DropTable(
                name: "Paths");

            migrationBuilder.DropTable(
                name: "Trips");

            migrationBuilder.DropTable(
                name: "Checkpoints");

            migrationBuilder.DropTable(
                name: "Areas");
        }
    }
}
