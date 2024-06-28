﻿// <auto-generated />

using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Greenlight.Modules.Initiatives.Infrastructure.Database.Migrations
{
    /// <inheritdoc />
    public partial class Create_Initiatives_Database : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "initiatives");

            migrationBuilder.CreateTable(
                name: "categories",
                schema: "initiatives",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: false),
                    is_archived = table.Column<bool>(type: "boolean", nullable: false),
                    is_editable = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_categories", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "inbox_message_consumers",
                schema: "initiatives",
                columns: table => new
                {
                    inbox_message_id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_inbox_message_consumers", x => new { x.inbox_message_id, x.name });
                });

            migrationBuilder.CreateTable(
                name: "inbox_messages",
                schema: "initiatives",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    type = table.Column<string>(type: "text", nullable: false),
                    content = table.Column<string>(type: "jsonb", maxLength: 4000, nullable: false),
                    occurred_on_utc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    processed_on_utc = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    error = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_inbox_messages", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "outbox_message_consumers",
                schema: "initiatives",
                columns: table => new
                {
                    outbox_message_id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_outbox_message_consumers", x => new { x.outbox_message_id, x.name });
                });

            migrationBuilder.CreateTable(
                name: "outbox_messages",
                schema: "initiatives",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    type = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    content = table.Column<string>(type: "jsonb", maxLength: 4000, nullable: false),
                    occurred_on_utc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    processed_on_utc = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    error = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_outbox_messages", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "users",
                schema: "initiatives",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    email = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    first_name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    last_name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_users", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "initiatives",
                schema: "initiatives",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    title = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    description = table.Column<string>(type: "text", nullable: false),
                    category_id = table.Column<Guid>(type: "uuid", nullable: true),
                    process_status = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_initiatives", x => x.id);
                    table.ForeignKey(
                        name: "fk_initiatives_categories_category_id",
                        column: x => x.category_id,
                        principalSchema: "initiatives",
                        principalTable: "categories",
                        principalColumn: "id");
                });

            migrationBuilder.InsertData(
                schema: "initiatives",
                table: "categories",
                columns: new[] { "id", "is_archived", "is_editable", "name" },
                values: new object[,]
                {
                    { new Guid("1828388a-64f5-4b98-b51f-a70360dc6991"), false, true, "Employee Learning and Growth" },
                    { new Guid("1a5a152d-07b6-421d-bb99-601a52fb9185"), false, true, "Employee Engagement" },
                    { new Guid("23154431-e9b1-4551-a6f3-a6264154076a"), false, true, "Health and Safety" },
                    { new Guid("a40b6afa-6a58-48b2-af7b-47e968354c96"), false, true, "Internal Processes" },
                    { new Guid("ca000000-0000-0000-0000-000000000000"), false, false, "Category Not Set" },
                    { new Guid("eca54447-b402-45e6-8546-de0d4621bfc3"), false, true, "Customer Service" }
                });

            migrationBuilder.CreateIndex(
                name: "ix_initiatives_category_id",
                schema: "initiatives",
                table: "initiatives",
                column: "category_id");

            migrationBuilder.CreateIndex(
                name: "ix_users_email",
                schema: "initiatives",
                table: "users",
                column: "email",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "inbox_message_consumers",
                schema: "initiatives");

            migrationBuilder.DropTable(
                name: "inbox_messages",
                schema: "initiatives");

            migrationBuilder.DropTable(
                name: "initiatives",
                schema: "initiatives");

            migrationBuilder.DropTable(
                name: "outbox_message_consumers",
                schema: "initiatives");

            migrationBuilder.DropTable(
                name: "outbox_messages",
                schema: "initiatives");

            migrationBuilder.DropTable(
                name: "users",
                schema: "initiatives");

            migrationBuilder.DropTable(
                name: "categories",
                schema: "initiatives");
        }
    }
}