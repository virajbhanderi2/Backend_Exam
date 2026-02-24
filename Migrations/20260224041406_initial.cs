using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Backend_Exam.Migrations
{
    /// <inheritdoc />
    public partial class initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserName",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "changed_by",
                table: "TicketStatusLogs");

            migrationBuilder.DropColumn(
                name: "new_status",
                table: "TicketStatusLogs");

            migrationBuilder.DropColumn(
                name: "assigned_to",
                table: "Tickets");

            migrationBuilder.RenameColumn(
                name: "RoleID",
                table: "Users",
                newName: "RoleId");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Users",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "ticket_id",
                table: "TicketStatusLogs",
                newName: "TicketId");

            migrationBuilder.RenameColumn(
                name: "old_status",
                table: "TicketStatusLogs",
                newName: "ChangedBy");

            migrationBuilder.RenameColumn(
                name: "changed_at",
                table: "TicketStatusLogs",
                newName: "ChangedAt");

            migrationBuilder.RenameColumn(
                name: "title",
                table: "Tickets",
                newName: "Title");

            migrationBuilder.RenameColumn(
                name: "status",
                table: "Tickets",
                newName: "Status");

            migrationBuilder.RenameColumn(
                name: "priority",
                table: "Tickets",
                newName: "Priority");

            migrationBuilder.RenameColumn(
                name: "created_by",
                table: "Tickets",
                newName: "CreatedBy");

            migrationBuilder.RenameColumn(
                name: "created_at",
                table: "Tickets",
                newName: "CreatedAt");

            migrationBuilder.RenameColumn(
                name: "comment",
                table: "TicketComments",
                newName: "Comment");

            migrationBuilder.RenameColumn(
                name: "user_id",
                table: "TicketComments",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "ticket_id",
                table: "TicketComments",
                newName: "TicketId");

            migrationBuilder.RenameColumn(
                name: "created_at",
                table: "TicketComments",
                newName: "CreatedAt");

            migrationBuilder.AlterColumn<string>(
                name: "Password",
                table: "Users",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Users",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Users",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "NewStatus",
                table: "TicketStatusLogs",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "OldStatus",
                table: "TicketStatusLogs",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Tickets",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "Tickets",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Priority",
                table: "Tickets",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<int>(
                name: "AssignedTo",
                table: "Tickets",
                type: "int",
                nullable: true);

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "MANAGER" },
                    { 2, "SUPPORT" },
                    { 3, "USER" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedAt", "Email", "Name", "Password", "RoleId" },
                values: new object[] { 1, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "viraj@gmail.com", "System Manager", "$2a$11$8PYpXr7FRbfFiMJq3g8oTepq3JBUPUU5d6YSUFbIWsKPGOKwJ6ife", 1 });

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                table: "Users",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_RoleId",
                table: "Users",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_TicketStatusLogs_ChangedBy",
                table: "TicketStatusLogs",
                column: "ChangedBy");

            migrationBuilder.CreateIndex(
                name: "IX_TicketStatusLogs_TicketId",
                table: "TicketStatusLogs",
                column: "TicketId");

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_AssignedTo",
                table: "Tickets",
                column: "AssignedTo");

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_CreatedBy",
                table: "Tickets",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_TicketComments_TicketId",
                table: "TicketComments",
                column: "TicketId");

            migrationBuilder.CreateIndex(
                name: "IX_TicketComments_UserId",
                table: "TicketComments",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Roles_Name",
                table: "Roles",
                column: "Name",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_TicketComments_Tickets_TicketId",
                table: "TicketComments",
                column: "TicketId",
                principalTable: "Tickets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TicketComments_Users_UserId",
                table: "TicketComments",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_Users_AssignedTo",
                table: "Tickets",
                column: "AssignedTo",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_Users_CreatedBy",
                table: "Tickets",
                column: "CreatedBy",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TicketStatusLogs_Tickets_TicketId",
                table: "TicketStatusLogs",
                column: "TicketId",
                principalTable: "Tickets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TicketStatusLogs_Users_ChangedBy",
                table: "TicketStatusLogs",
                column: "ChangedBy",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Roles_RoleId",
                table: "Users",
                column: "RoleId",
                principalTable: "Roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TicketComments_Tickets_TicketId",
                table: "TicketComments");

            migrationBuilder.DropForeignKey(
                name: "FK_TicketComments_Users_UserId",
                table: "TicketComments");

            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_Users_AssignedTo",
                table: "Tickets");

            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_Users_CreatedBy",
                table: "Tickets");

            migrationBuilder.DropForeignKey(
                name: "FK_TicketStatusLogs_Tickets_TicketId",
                table: "TicketStatusLogs");

            migrationBuilder.DropForeignKey(
                name: "FK_TicketStatusLogs_Users_ChangedBy",
                table: "TicketStatusLogs");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Roles_RoleId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_Email",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_RoleId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_TicketStatusLogs_ChangedBy",
                table: "TicketStatusLogs");

            migrationBuilder.DropIndex(
                name: "IX_TicketStatusLogs_TicketId",
                table: "TicketStatusLogs");

            migrationBuilder.DropIndex(
                name: "IX_Tickets_AssignedTo",
                table: "Tickets");

            migrationBuilder.DropIndex(
                name: "IX_Tickets_CreatedBy",
                table: "Tickets");

            migrationBuilder.DropIndex(
                name: "IX_TicketComments_TicketId",
                table: "TicketComments");

            migrationBuilder.DropIndex(
                name: "IX_TicketComments_UserId",
                table: "TicketComments");

            migrationBuilder.DropIndex(
                name: "IX_Roles_Name",
                table: "Roles");

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "NewStatus",
                table: "TicketStatusLogs");

            migrationBuilder.DropColumn(
                name: "OldStatus",
                table: "TicketStatusLogs");

            migrationBuilder.DropColumn(
                name: "AssignedTo",
                table: "Tickets");

            migrationBuilder.RenameColumn(
                name: "RoleId",
                table: "Users",
                newName: "RoleID");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Users",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "TicketId",
                table: "TicketStatusLogs",
                newName: "ticket_id");

            migrationBuilder.RenameColumn(
                name: "ChangedBy",
                table: "TicketStatusLogs",
                newName: "old_status");

            migrationBuilder.RenameColumn(
                name: "ChangedAt",
                table: "TicketStatusLogs",
                newName: "changed_at");

            migrationBuilder.RenameColumn(
                name: "Title",
                table: "Tickets",
                newName: "title");

            migrationBuilder.RenameColumn(
                name: "Status",
                table: "Tickets",
                newName: "status");

            migrationBuilder.RenameColumn(
                name: "Priority",
                table: "Tickets",
                newName: "priority");

            migrationBuilder.RenameColumn(
                name: "CreatedBy",
                table: "Tickets",
                newName: "created_by");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "Tickets",
                newName: "created_at");

            migrationBuilder.RenameColumn(
                name: "Comment",
                table: "TicketComments",
                newName: "comment");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "TicketComments",
                newName: "user_id");

            migrationBuilder.RenameColumn(
                name: "TicketId",
                table: "TicketComments",
                newName: "ticket_id");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "TicketComments",
                newName: "created_at");

            migrationBuilder.AlterColumn<string>(
                name: "Password",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255);

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255);

            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "Users",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "changed_by",
                table: "TicketStatusLogs",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "new_status",
                table: "TicketStatusLogs",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "title",
                table: "Tickets",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255);

            migrationBuilder.AlterColumn<string>(
                name: "status",
                table: "Tickets",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20);

            migrationBuilder.AlterColumn<string>(
                name: "priority",
                table: "Tickets",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(10)",
                oldMaxLength: 10);

            migrationBuilder.AddColumn<int>(
                name: "assigned_to",
                table: "Tickets",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
