using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EventSiteAPI.Migrations
{
    public partial class InitDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "EventSite");

            migrationBuilder.CreateTable(
                name: "Campus",
                schema: "EventSite",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Campus", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "City",
                schema: "EventSite",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true),
                    ZipCode = table.Column<string>(type: "char(5)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_City", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Reveller",
                schema: "EventSite",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    LastName = table.Column<string>(type: "text", nullable: true),
                    FirstName = table.Column<string>(type: "text", nullable: true),
                    Phone = table.Column<string>(type: "text", nullable: true),
                    Mail = table.Column<string>(type: "text", nullable: true),
                    Password = table.Column<string>(type: "text", nullable: true),
                    IsAdmin = table.Column<bool>(type: "boolean", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    CampusId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reveller", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reveller_Campus_CampusId",
                        column: x => x.CampusId,
                        principalSchema: "EventSite",
                        principalTable: "Campus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Place",
                schema: "EventSite",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Street = table.Column<string>(type: "text", nullable: true),
                    CityId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Place", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Place_City_CityId",
                        column: x => x.CityId,
                        principalSchema: "EventSite",
                        principalTable: "City",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Event",
                schema: "EventSite",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true),
                    BeginTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Duration = table.Column<TimeSpan>(type: "interval", nullable: false),
                    SubscribeDeadline = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    MaxMembers = table.Column<int>(type: "integer", nullable: false),
                    Infos = table.Column<string>(type: "text", nullable: true),
                    Status = table.Column<string>(type: "text", nullable: false),
                    PlaceId = table.Column<Guid>(type: "uuid", nullable: false),
                    CampusId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Event", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Event_Campus_CampusId",
                        column: x => x.CampusId,
                        principalSchema: "EventSite",
                        principalTable: "Campus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Event_Place_PlaceId",
                        column: x => x.PlaceId,
                        principalSchema: "EventSite",
                        principalTable: "Place",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Event_Reveller_CreatorId",
                        column: x => x.CreatorId,
                        principalSchema: "EventSite",
                        principalTable: "Reveller",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EventReveller",
                schema: "EventSite",
                columns: table => new
                {
                    RevellerId = table.Column<Guid>(type: "uuid", nullable: false),
                    EventId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventReveller", x => new { x.EventId, x.RevellerId });
                    table.ForeignKey(
                        name: "FK_EventReveller_Event_EventId",
                        column: x => x.EventId,
                        principalSchema: "EventSite",
                        principalTable: "Event",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EventReveller_Reveller_RevellerId",
                        column: x => x.RevellerId,
                        principalSchema: "EventSite",
                        principalTable: "Reveller",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Event_CampusId",
                schema: "EventSite",
                table: "Event",
                column: "CampusId");

            migrationBuilder.CreateIndex(
                name: "IX_Event_CreatorId",
                schema: "EventSite",
                table: "Event",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_Event_PlaceId",
                schema: "EventSite",
                table: "Event",
                column: "PlaceId");

            migrationBuilder.CreateIndex(
                name: "IX_EventReveller_RevellerId",
                schema: "EventSite",
                table: "EventReveller",
                column: "RevellerId");

            migrationBuilder.CreateIndex(
                name: "IX_Place_CityId",
                schema: "EventSite",
                table: "Place",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_Reveller_CampusId",
                schema: "EventSite",
                table: "Reveller",
                column: "CampusId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EventReveller",
                schema: "EventSite");

            migrationBuilder.DropTable(
                name: "Event",
                schema: "EventSite");

            migrationBuilder.DropTable(
                name: "Place",
                schema: "EventSite");

            migrationBuilder.DropTable(
                name: "Reveller",
                schema: "EventSite");

            migrationBuilder.DropTable(
                name: "City",
                schema: "EventSite");

            migrationBuilder.DropTable(
                name: "Campus",
                schema: "EventSite");
        }
    }
}
