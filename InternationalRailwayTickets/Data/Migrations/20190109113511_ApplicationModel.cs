using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace InternationalRailwayTickets.Data.Migrations
{
    public partial class ApplicationModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CarInstances",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ServiceClass = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarInstances", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Cars",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ServiceClass = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cars", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Routes",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    StartTime = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Routes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Schedule",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    FromDate = table.Column<DateTime>(nullable: false),
                    Discriminator = table.Column<string>(nullable: false),
                    EveryNDay = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Schedule", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Stations",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Trains",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Number = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trains", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PlaceInstances",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Number = table.Column<long>(nullable: false),
                    Level = table.Column<long>(nullable: false),
                    Floor = table.Column<long>(nullable: false),
                    CarId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlaceInstances", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PlaceInstances_CarInstances_CarId",
                        column: x => x.CarId,
                        principalTable: "CarInstances",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Places",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Number = table.Column<long>(nullable: false),
                    Level = table.Column<long>(nullable: false),
                    Floor = table.Column<long>(nullable: false),
                    CarId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Places", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Places_Cars_CarId",
                        column: x => x.CarId,
                        principalTable: "Cars",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TrainInstances",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    StartDate = table.Column<DateTime>(nullable: false),
                    Number = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    RouteId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrainInstances", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TrainInstances_Routes_RouteId",
                        column: x => x.RouteId,
                        principalTable: "Routes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RoutePoints",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    FromStartTime = table.Column<TimeSpan>(nullable: false),
                    StationId = table.Column<Guid>(nullable: false),
                    RouteId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoutePoints", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RoutePoints_Routes_RouteId",
                        column: x => x.RouteId,
                        principalTable: "Routes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RoutePoints_Stations_StationId",
                        column: x => x.StationId,
                        principalTable: "Stations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TrainSchedules",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    FromDate = table.Column<DateTime>(nullable: false),
                    ToDate = table.Column<DateTime>(nullable: false),
                    TrainId = table.Column<Guid>(nullable: false),
                    RouteId = table.Column<Guid>(nullable: false),
                    ScheduleId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrainSchedules", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TrainSchedules_Routes_RouteId",
                        column: x => x.RouteId,
                        principalTable: "Routes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TrainSchedules_Schedule_ScheduleId",
                        column: x => x.ScheduleId,
                        principalTable: "Schedule",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TrainSchedules_Trains_TrainId",
                        column: x => x.TrainId,
                        principalTable: "Trains",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Tickets",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    PassengerName = table.Column<string>(nullable: false),
                    DocumentType = table.Column<string>(nullable: false),
                    DocumentNumber = table.Column<string>(nullable: false),
                    Paid = table.Column<bool>(nullable: false),
                    UserId = table.Column<string>(nullable: false),
                    PlaceInstanceId = table.Column<Guid>(nullable: false),
                    FromPointId = table.Column<Guid>(nullable: false),
                    ToPointId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tickets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tickets_RoutePoints_FromPointId",
                        column: x => x.FromPointId,
                        principalTable: "RoutePoints",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Tickets_PlaceInstances_PlaceInstanceId",
                        column: x => x.PlaceInstanceId,
                        principalTable: "PlaceInstances",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Tickets_RoutePoints_ToPointId",
                        column: x => x.ToPointId,
                        principalTable: "RoutePoints",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Tickets_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TrainCarInstances",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Number = table.Column<long>(nullable: false),
                    TrainId = table.Column<Guid>(nullable: false),
                    CarId = table.Column<Guid>(nullable: false),
                    FromPointId = table.Column<Guid>(nullable: false),
                    ToPointId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrainCarInstances", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TrainCarInstances_CarInstances_CarId",
                        column: x => x.CarId,
                        principalTable: "CarInstances",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TrainCarInstances_RoutePoints_FromPointId",
                        column: x => x.FromPointId,
                        principalTable: "RoutePoints",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TrainCarInstances_RoutePoints_ToPointId",
                        column: x => x.ToPointId,
                        principalTable: "RoutePoints",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TrainCarInstances_TrainInstances_TrainId",
                        column: x => x.TrainId,
                        principalTable: "TrainInstances",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TrainCars",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Number = table.Column<long>(nullable: false),
                    TrainScheduleId = table.Column<Guid>(nullable: false),
                    CarId = table.Column<Guid>(nullable: false),
                    FromPointId = table.Column<Guid>(nullable: false),
                    ToPointId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrainCars", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TrainCars_Cars_CarId",
                        column: x => x.CarId,
                        principalTable: "Cars",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TrainCars_RoutePoints_FromPointId",
                        column: x => x.FromPointId,
                        principalTable: "RoutePoints",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TrainCars_RoutePoints_ToPointId",
                        column: x => x.ToPointId,
                        principalTable: "RoutePoints",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TrainCars_TrainSchedules_TrainScheduleId",
                        column: x => x.TrainScheduleId,
                        principalTable: "TrainSchedules",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PlaceInstances_CarId",
                table: "PlaceInstances",
                column: "CarId");

            migrationBuilder.CreateIndex(
                name: "IX_Places_CarId",
                table: "Places",
                column: "CarId");

            migrationBuilder.CreateIndex(
                name: "IX_RoutePoints_RouteId",
                table: "RoutePoints",
                column: "RouteId");

            migrationBuilder.CreateIndex(
                name: "IX_RoutePoints_StationId",
                table: "RoutePoints",
                column: "StationId");

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_FromPointId",
                table: "Tickets",
                column: "FromPointId");

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_PlaceInstanceId",
                table: "Tickets",
                column: "PlaceInstanceId");

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_ToPointId",
                table: "Tickets",
                column: "ToPointId");

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_UserId",
                table: "Tickets",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_TrainCarInstances_CarId",
                table: "TrainCarInstances",
                column: "CarId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TrainCarInstances_FromPointId",
                table: "TrainCarInstances",
                column: "FromPointId");

            migrationBuilder.CreateIndex(
                name: "IX_TrainCarInstances_ToPointId",
                table: "TrainCarInstances",
                column: "ToPointId");

            migrationBuilder.CreateIndex(
                name: "IX_TrainCarInstances_TrainId",
                table: "TrainCarInstances",
                column: "TrainId");

            migrationBuilder.CreateIndex(
                name: "IX_TrainCars_CarId",
                table: "TrainCars",
                column: "CarId");

            migrationBuilder.CreateIndex(
                name: "IX_TrainCars_FromPointId",
                table: "TrainCars",
                column: "FromPointId");

            migrationBuilder.CreateIndex(
                name: "IX_TrainCars_ToPointId",
                table: "TrainCars",
                column: "ToPointId");

            migrationBuilder.CreateIndex(
                name: "IX_TrainCars_TrainScheduleId",
                table: "TrainCars",
                column: "TrainScheduleId");

            migrationBuilder.CreateIndex(
                name: "IX_TrainInstances_RouteId",
                table: "TrainInstances",
                column: "RouteId");

            migrationBuilder.CreateIndex(
                name: "IX_TrainSchedules_RouteId",
                table: "TrainSchedules",
                column: "RouteId");

            migrationBuilder.CreateIndex(
                name: "IX_TrainSchedules_ScheduleId",
                table: "TrainSchedules",
                column: "ScheduleId");

            migrationBuilder.CreateIndex(
                name: "IX_TrainSchedules_TrainId",
                table: "TrainSchedules",
                column: "TrainId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Places");

            migrationBuilder.DropTable(
                name: "Tickets");

            migrationBuilder.DropTable(
                name: "TrainCarInstances");

            migrationBuilder.DropTable(
                name: "TrainCars");

            migrationBuilder.DropTable(
                name: "PlaceInstances");

            migrationBuilder.DropTable(
                name: "TrainInstances");

            migrationBuilder.DropTable(
                name: "Cars");

            migrationBuilder.DropTable(
                name: "RoutePoints");

            migrationBuilder.DropTable(
                name: "TrainSchedules");

            migrationBuilder.DropTable(
                name: "CarInstances");

            migrationBuilder.DropTable(
                name: "Stations");

            migrationBuilder.DropTable(
                name: "Routes");

            migrationBuilder.DropTable(
                name: "Schedule");

            migrationBuilder.DropTable(
                name: "Trains");
        }
    }
}
