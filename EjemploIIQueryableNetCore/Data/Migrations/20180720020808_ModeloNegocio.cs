using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace EjemploIIQueryableNetCore.Data.Migrations
{
    public partial class ModeloNegocio : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "UserNameIndex",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUserRoles_UserId",
                table: "AspNetUserRoles");

            migrationBuilder.DropIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles");

            migrationBuilder.CreateTable(
                name: "Asignatura",
                columns: table => new
                {
                    IdAsignatura = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Nombre = table.Column<string>(maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Asignatura", x => x.IdAsignatura);
                });

            migrationBuilder.CreateTable(
                name: "Estudiante",
                columns: table => new
                {
                    IdEstudiante = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Nombre = table.Column<string>(maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Estudiante", x => x.IdEstudiante);
                });

            migrationBuilder.CreateTable(
                name: "EstudianteAsignatura",
                columns: table => new
                {
                    IdEstudianteAsignatura = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    IdAsignatura = table.Column<int>(nullable: false),
                    IdEstudiante = table.Column<int>(nullable: false),
                    Nota = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EstudianteAsignatura", x => x.IdEstudianteAsignatura);
                    table.ForeignKey(
                        name: "FK_EstudianteAsignatura_Asignatura_IdAsignatura",
                        column: x => x.IdAsignatura,
                        principalTable: "Asignatura",
                        principalColumn: "IdAsignatura",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EstudianteAsignatura_Estudiante_IdEstudiante",
                        column: x => x.IdEstudiante,
                        principalTable: "Estudiante",
                        principalColumn: "IdEstudiante",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_EstudianteAsignatura_IdAsignatura",
                table: "EstudianteAsignatura",
                column: "IdAsignatura");

            migrationBuilder.CreateIndex(
                name: "IX_EstudianteAsignatura_IdEstudiante",
                table: "EstudianteAsignatura",
                column: "IdEstudiante");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                table: "AspNetUserTokens",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                table: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "EstudianteAsignatura");

            migrationBuilder.DropTable(
                name: "Asignatura");

            migrationBuilder.DropTable(
                name: "Estudiante");

            migrationBuilder.DropIndex(
                name: "UserNameIndex",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_UserId",
                table: "AspNetUserRoles",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName");
        }
    }
}
