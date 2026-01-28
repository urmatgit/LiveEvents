using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FSH.Starter.WebApi.Migrations.PostgreSQL.Migrations
{
    /// <inheritdoc />
    public partial class AddSomeEventCollectionToEventCatalog : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SomeEvents",
                schema: "catalog",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: false),
                    OrganizationId = table.Column<Guid>(type: "uuid", nullable: false),
                    MinParticipant = table.Column<int>(type: "integer", nullable: false),
                    MaxParticipant = table.Column<int>(type: "integer", nullable: false),
                    Durations = table.Column<int>(type: "integer", nullable: false),
                    Price = table.Column<double>(type: "numeric(18,2)", nullable: false),
                    EventDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    EventCatalogId = table.Column<Guid>(type: "uuid", nullable: false),
                    TenantId = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false),
                    Created = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    LastModified = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    LastModifiedBy = table.Column<Guid>(type: "uuid", nullable: true),
                    Deleted = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    DeletedBy = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SomeEvents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SomeEvents_EventCatalogs_EventCatalogId",
                        column: x => x.EventCatalogId,
                        principalSchema: "catalog",
                        principalTable: "EventCatalogs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SomeEvents_EventCatalogId",
                schema: "catalog",
                table: "SomeEvents",
                column: "EventCatalogId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SomeEvents",
                schema: "catalog");
        }
    }
}
