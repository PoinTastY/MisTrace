using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MisTrace.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class NewStuffToTrySimpleDemo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TraceMilestones_Trace_TraceId",
                schema: "mistrace",
                table: "TraceMilestones");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Trace",
                schema: "mistrace",
                table: "Trace");

            migrationBuilder.RenameTable(
                name: "Trace",
                schema: "mistrace",
                newName: "Traces",
                newSchema: "mistrace");

            migrationBuilder.AddColumn<string>(
                name: "Comments",
                schema: "mistrace",
                table: "TraceMilestones",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Notified",
                schema: "mistrace",
                table: "TraceMilestones",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Notify",
                schema: "mistrace",
                table: "Milestones",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "OrganizationId",
                schema: "mistrace",
                table: "Milestones",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CustomerId",
                schema: "mistrace",
                table: "Traces",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "GlobalIdentifier",
                schema: "mistrace",
                table: "Traces",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<int>(
                name: "OrganizationId",
                schema: "mistrace",
                table: "Traces",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Traces",
                schema: "mistrace",
                table: "Traces",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Traces_CustomerId",
                schema: "mistrace",
                table: "Traces",
                column: "CustomerId");

            migrationBuilder.AddForeignKey(
                name: "FK_TraceMilestones_Traces_TraceId",
                schema: "mistrace",
                table: "TraceMilestones",
                column: "TraceId",
                principalSchema: "mistrace",
                principalTable: "Traces",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Traces_Customers_CustomerId",
                schema: "mistrace",
                table: "Traces",
                column: "CustomerId",
                principalSchema: "mistrace",
                principalTable: "Customers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TraceMilestones_Traces_TraceId",
                schema: "mistrace",
                table: "TraceMilestones");

            migrationBuilder.DropForeignKey(
                name: "FK_Traces_Customers_CustomerId",
                schema: "mistrace",
                table: "Traces");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Traces",
                schema: "mistrace",
                table: "Traces");

            migrationBuilder.DropIndex(
                name: "IX_Traces_CustomerId",
                schema: "mistrace",
                table: "Traces");

            migrationBuilder.DropColumn(
                name: "Comments",
                schema: "mistrace",
                table: "TraceMilestones");

            migrationBuilder.DropColumn(
                name: "Notified",
                schema: "mistrace",
                table: "TraceMilestones");

            migrationBuilder.DropColumn(
                name: "Notify",
                schema: "mistrace",
                table: "Milestones");

            migrationBuilder.DropColumn(
                name: "OrganizationId",
                schema: "mistrace",
                table: "Milestones");

            migrationBuilder.DropColumn(
                name: "CustomerId",
                schema: "mistrace",
                table: "Traces");

            migrationBuilder.DropColumn(
                name: "GlobalIdentifier",
                schema: "mistrace",
                table: "Traces");

            migrationBuilder.DropColumn(
                name: "OrganizationId",
                schema: "mistrace",
                table: "Traces");

            migrationBuilder.RenameTable(
                name: "Traces",
                schema: "mistrace",
                newName: "Trace",
                newSchema: "mistrace");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Trace",
                schema: "mistrace",
                table: "Trace",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TraceMilestones_Trace_TraceId",
                schema: "mistrace",
                table: "TraceMilestones",
                column: "TraceId",
                principalSchema: "mistrace",
                principalTable: "Trace",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
