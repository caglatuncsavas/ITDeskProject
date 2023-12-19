﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ITDeskServer.Migrations
{
    /// <inheritdoc />
    public partial class mg3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_AppUserRoles_RoleId",
                table: "AppUserRoles",
                column: "RoleId");

            migrationBuilder.AddForeignKey(
                name: "FK_AppUserRoles_Roles_RoleId",
                table: "AppUserRoles",
                column: "RoleId",
                principalTable: "Roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppUserRoles_Roles_RoleId",
                table: "AppUserRoles");

            migrationBuilder.DropIndex(
                name: "IX_AppUserRoles_RoleId",
                table: "AppUserRoles");
        }
    }
}
