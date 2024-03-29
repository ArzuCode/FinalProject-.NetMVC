﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Rent_a_Car_.Net.Migrations
{
    /// <inheritdoc />
    public partial class Migr : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "About",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HeaderDesc = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Desc = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_About", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Surname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Bios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Contact = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WorkTime = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bios", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Brands",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    isDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Brands", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Colors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    isDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Colors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "IdentityUser",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IdentityUser", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Teams",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Surname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Position = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    isDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Teams", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Cars",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ModelYear = table.Column<int>(type: "int", nullable: false),
                    DailyPrice = table.Column<double>(type: "float", nullable: false),
                    FuelType = table.Column<int>(type: "int", nullable: false),
                    TransmissionType = table.Column<int>(type: "int", nullable: false),
                    PassengerCount = table.Column<int>(type: "int", nullable: false),
                    BrandId = table.Column<int>(type: "int", nullable: false),
                    ColorId = table.Column<int>(type: "int", nullable: false),
                    isDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cars", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cars_Brands_BrandId",
                        column: x => x.BrandId,
                        principalTable: "Brands",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Cars_Colors_ColorId",
                        column: x => x.ColorId,
                        principalTable: "Colors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CarImages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsMain = table.Column<bool>(type: "bit", nullable: false),
                    CarId = table.Column<int>(type: "int", nullable: false),
                    isDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarImages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CarImages_Cars_CarId",
                        column: x => x.CarId,
                        principalTable: "Cars",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Rentals",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RentDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ReturnDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    InvoiceNo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    isPaid = table.Column<bool>(type: "bit", nullable: false),
                    CarId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    isDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rentals", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Rentals_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Rentals_Cars_CarId",
                        column: x => x.CarId,
                        principalTable: "Cars",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "About",
                columns: new[] { "Id", "Desc", "HeaderDesc", "ImageUrl", "Title" },
                values: new object[] { 1, "Rent A Car şirkəti yeni yaranmasına baxmayaraq, ölkədə ən böyük avtomobil qarajına sahibdir. Bizim bütün avtomobillərimiz  bütün standartlara cavab verən, ekoloji tələblərə riayət edən yeni avtomobillərdir. Bizim şirkət avtomobil icarəsi bazarına yeni daxil olduğu üçün sərfəli qiymətlərlə digər şirkətləri üstələyir. Kirayə maşın bazarında çox şirkətin olmasına baxmayaraq keyfiyyətli və təhlükəsiz şirkət azlıq təşkil edir.", "Əziz şəhərimizin qonaqları sizi “Rent A Car” şirkəti olaraq salamlayırıq.", "hero_2.jpg", "BİZ KİMİK" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "1", null, "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "Bios",
                columns: new[] { "Id", "Contact", "Email", "ImageUrl", "Location", "WorkTime" },
                values: new object[] { 1, "+994 77 710 19 98", "arzuem@code.edu.az", "logo.png", "Nizami küçəsi 203B, AF Business House, 2-ci mərtəbə, Baku 1000, Azerbaijan", "Bazar ertəsi-Şənbə 9:00pm - 5:00pm Bazar:Bağlıdır" });

            migrationBuilder.InsertData(
                table: "Brands",
                columns: new[] { "Id", "CreatedAt", "DeletedAt", "ImageUrl", "Name", "UpdatedAt", "isDeleted" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 3, 9, 15, 53, 31, 980, DateTimeKind.Local).AddTicks(3208), null, "mercedes.png", "Mercedes", null, false },
                    { 2, new DateTime(2024, 3, 9, 15, 53, 31, 980, DateTimeKind.Local).AddTicks(3286), null, "hyundai.png", "Hyundai", null, false },
                    { 3, new DateTime(2024, 3, 9, 15, 53, 31, 980, DateTimeKind.Local).AddTicks(3305), null, "toyota.png", "Toyota", null, false },
                    { 4, new DateTime(2024, 3, 9, 15, 53, 31, 980, DateTimeKind.Local).AddTicks(3400), null, "bmw.png", "BMW", null, false },
                    { 5, new DateTime(2024, 3, 9, 15, 53, 31, 980, DateTimeKind.Local).AddTicks(3419), null, "nissan.png", "Nissan", null, false },
                    { 6, new DateTime(2024, 3, 9, 15, 53, 31, 980, DateTimeKind.Local).AddTicks(3449), null, "mitsubishi.png", "Mitsubishi", null, false },
                    { 7, new DateTime(2024, 3, 9, 15, 53, 31, 980, DateTimeKind.Local).AddTicks(3467), null, "kia.png", "Kia", null, false },
                    { 8, new DateTime(2024, 3, 9, 15, 53, 31, 980, DateTimeKind.Local).AddTicks(3483), null, "chevrolet.png", "Chevrolet", null, false },
                    { 9, new DateTime(2024, 3, 9, 15, 53, 31, 980, DateTimeKind.Local).AddTicks(3501), null, "lexus.png", "Lexus", null, false },
                    { 10, new DateTime(2024, 3, 9, 15, 53, 31, 980, DateTimeKind.Local).AddTicks(3525), null, "tesla.png", "Tesla", null, false }
                });

            migrationBuilder.InsertData(
                table: "Colors",
                columns: new[] { "Id", "CreatedAt", "DeletedAt", "Name", "UpdatedAt", "isDeleted" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 3, 9, 15, 53, 31, 980, DateTimeKind.Local).AddTicks(3566), null, "Qara", null, false },
                    { 2, new DateTime(2024, 3, 9, 15, 53, 31, 980, DateTimeKind.Local).AddTicks(3591), null, "Ağ", null, false },
                    { 3, new DateTime(2024, 3, 9, 15, 53, 31, 980, DateTimeKind.Local).AddTicks(3609), null, "Qəhvəyi", null, false },
                    { 4, new DateTime(2024, 3, 9, 15, 53, 31, 980, DateTimeKind.Local).AddTicks(3625), null, "Boz", null, false },
                    { 5, new DateTime(2024, 3, 9, 15, 53, 31, 980, DateTimeKind.Local).AddTicks(3641), null, "Qırmızı", null, false },
                    { 6, new DateTime(2024, 3, 9, 15, 53, 31, 980, DateTimeKind.Local).AddTicks(3659), null, "Mavi", null, false }
                });

            migrationBuilder.InsertData(
                table: "IdentityUser",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "1", 0, "c3cb0485-8c93-4b4b-99e4-7a2e3d2af3f6", "admin@admin.com", true, false, null, "ADMIN@ADMIN.COM", "ADMIN@ADMIN.COM", "AQAAAAIAAYagAAAAEL8BI7HJphEef1hrOy/Xg7zd5Nq5vGi6rZUruFFD6SnMAY92boTDmNz0NTPNPWD48A==", null, false, "0edc1e1a-a38a-4e23-a194-e3e8ecdfe22c", false, "admin@admin.com" });

            migrationBuilder.InsertData(
                table: "Teams",
                columns: new[] { "Id", "CreatedAt", "DeletedAt", "ImageUrl", "Name", "Position", "Surname", "UpdatedAt", "isDeleted" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 3, 9, 15, 53, 31, 980, DateTimeKind.Local).AddTicks(5485), null, "Arzu.jpg", "Arzu", "CEO", "Mahmudov", null, false },
                    { 2, new DateTime(2024, 3, 9, 15, 53, 31, 980, DateTimeKind.Local).AddTicks(5513), null, "Murad.jpg", "Murad", "CFO", "Qarayev", null, false },
                    { 3, new DateTime(2024, 3, 9, 15, 53, 31, 980, DateTimeKind.Local).AddTicks(5530), null, "Riyad.jpg", "Riyad", "Developer", "Nağıyev", null, false },
                    { 4, new DateTime(2024, 3, 9, 15, 53, 31, 980, DateTimeKind.Local).AddTicks(5547), null, "Pasha.jpg", "Paşa", "Mütəxəssis", "Bayramov", null, false },
                    { 5, new DateTime(2024, 3, 9, 15, 53, 31, 980, DateTimeKind.Local).AddTicks(5564), null, "Maarif.jpg", "Maarif", "Mütəxəssis", "Aliyev", null, false },
                    { 6, new DateTime(2024, 3, 9, 15, 53, 31, 980, DateTimeKind.Local).AddTicks(5586), null, "Sahin.jpg", "Şahin", "Mütəxəssis", "Cəbrayılov", null, false }
                });

            migrationBuilder.InsertData(
                table: "Cars",
                columns: new[] { "Id", "BrandId", "ColorId", "CreatedAt", "DailyPrice", "DeletedAt", "FuelType", "ModelYear", "Name", "PassengerCount", "TransmissionType", "UpdatedAt", "isDeleted" },
                values: new object[,]
                {
                    { 1, 4, 2, new DateTime(2024, 3, 9, 15, 53, 31, 980, DateTimeKind.Local).AddTicks(3693), 250.0, null, 1, 2012, "M5", 5, 1, null, false },
                    { 2, 3, 2, new DateTime(2024, 3, 9, 15, 53, 31, 980, DateTimeKind.Local).AddTicks(3716), 120.0, null, 2, 2015, "Prado", 7, 1, null, false },
                    { 3, 8, 2, new DateTime(2024, 3, 9, 15, 53, 31, 980, DateTimeKind.Local).AddTicks(3731), 50.0, null, 3, 2013, "Aveo", 5, 2, null, false },
                    { 4, 2, 2, new DateTime(2024, 3, 9, 15, 53, 31, 980, DateTimeKind.Local).AddTicks(3746), 50.0, null, 2, 2014, "Accent", 5, 2, null, false },
                    { 5, 2, 2, new DateTime(2024, 3, 9, 15, 53, 31, 980, DateTimeKind.Local).AddTicks(3762), 85.0, null, 1, 2013, "Azera", 5, 1, null, false },
                    { 6, 2, 4, new DateTime(2024, 3, 9, 15, 53, 31, 980, DateTimeKind.Local).AddTicks(3780), 55.0, null, 1, 2015, "Elantra", 5, 1, null, false },
                    { 7, 2, 2, new DateTime(2024, 3, 9, 15, 53, 31, 980, DateTimeKind.Local).AddTicks(3796), 140.0, null, 2, 2014, "H1", 9, 1, null, false },
                    { 8, 2, 4, new DateTime(2024, 3, 9, 15, 53, 31, 980, DateTimeKind.Local).AddTicks(3812), 65.0, null, 1, 2014, "Ix35", 5, 1, null, false },
                    { 9, 2, 4, new DateTime(2024, 3, 9, 15, 53, 31, 980, DateTimeKind.Local).AddTicks(3828), 70.0, null, 2, 2013, "Sonata", 5, 1, null, false },
                    { 10, 2, 2, new DateTime(2024, 3, 9, 15, 53, 31, 980, DateTimeKind.Local).AddTicks(3933), 75.0, null, 1, 2017, "Sonata", 5, 1, null, false },
                    { 11, 7, 2, new DateTime(2024, 3, 9, 15, 53, 31, 980, DateTimeKind.Local).AddTicks(3951), 55.0, null, 1, 2014, "Cerato", 5, 1, null, false },
                    { 12, 7, 2, new DateTime(2024, 3, 9, 15, 53, 31, 980, DateTimeKind.Local).AddTicks(3966), 75.0, null, 1, 2014, "Optima", 5, 1, null, false },
                    { 13, 7, 4, new DateTime(2024, 3, 9, 15, 53, 31, 980, DateTimeKind.Local).AddTicks(3984), 50.0, null, 2, 2014, "Rio", 5, 2, null, false },
                    { 14, 7, 2, new DateTime(2024, 3, 9, 15, 53, 31, 980, DateTimeKind.Local).AddTicks(4296), 90.0, null, 2, 2013, "Sorento", 7, 1, null, false },
                    { 15, 7, 2, new DateTime(2024, 3, 9, 15, 53, 31, 980, DateTimeKind.Local).AddTicks(4312), 65.0, null, 2, 2014, "Sportage", 5, 1, null, false },
                    { 16, 9, 1, new DateTime(2024, 3, 9, 15, 53, 31, 980, DateTimeKind.Local).AddTicks(4334), 200.0, null, 1, 2012, "LX570", 7, 1, null, false },
                    { 17, 1, 1, new DateTime(2024, 3, 9, 15, 53, 31, 980, DateTimeKind.Local).AddTicks(4354), 350.0, null, 1, 2012, "G-class", 5, 1, null, false },
                    { 18, 1, 2, new DateTime(2024, 3, 9, 15, 53, 31, 980, DateTimeKind.Local).AddTicks(4378), 400.0, null, 1, 2015, "G-class", 5, 1, null, false },
                    { 19, 1, 1, new DateTime(2024, 3, 9, 15, 53, 31, 980, DateTimeKind.Local).AddTicks(4394), 350.0, null, 2, 2016, "S-class", 4, 1, null, false },
                    { 20, 1, 1, new DateTime(2024, 3, 9, 15, 53, 31, 980, DateTimeKind.Local).AddTicks(4419), 450.0, null, 2, 2016, "V-class", 6, 1, null, false },
                    { 21, 6, 1, new DateTime(2024, 3, 9, 15, 53, 31, 980, DateTimeKind.Local).AddTicks(4443), 80.0, null, 2, 2013, "L200", 5, 2, null, false },
                    { 22, 6, 2, new DateTime(2024, 3, 9, 15, 53, 31, 980, DateTimeKind.Local).AddTicks(4467), 85.0, null, 1, 2013, "Pajero", 7, 1, null, false },
                    { 23, 5, 2, new DateTime(2024, 3, 9, 15, 53, 31, 980, DateTimeKind.Local).AddTicks(4483), 90.0, null, 2, 2013, "Navara", 5, 2, null, false },
                    { 24, 5, 4, new DateTime(2024, 3, 9, 15, 53, 31, 980, DateTimeKind.Local).AddTicks(4498), 50.0, null, 1, 2014, "Sunny", 5, 2, null, false },
                    { 25, 3, 4, new DateTime(2024, 3, 9, 15, 53, 31, 980, DateTimeKind.Local).AddTicks(4514), 90.0, null, 1, 2013, "Camry", 5, 1, null, false },
                    { 26, 3, 2, new DateTime(2024, 3, 9, 15, 53, 31, 980, DateTimeKind.Local).AddTicks(4534), 160.0, null, 1, 2014, "Land Cruiser", 7, 1, null, false },
                    { 27, 3, 4, new DateTime(2024, 3, 9, 15, 53, 31, 980, DateTimeKind.Local).AddTicks(4551), 300.0, null, 1, 2018, "Land Cruiser", 7, 1, null, false },
                    { 28, 4, 3, new DateTime(2024, 3, 9, 15, 53, 31, 980, DateTimeKind.Local).AddTicks(4567), 200.0, null, 1, 2011, "X5", 5, 1, null, false },
                    { 29, 3, 2, new DateTime(2024, 3, 9, 15, 53, 31, 980, DateTimeKind.Local).AddTicks(4583), 200.0, null, 1, 2018, "Prado", 7, 1, null, false },
                    { 30, 10, 2, new DateTime(2024, 3, 9, 15, 53, 31, 980, DateTimeKind.Local).AddTicks(4599), 250.0, null, 4, 2016, "X", 5, 1, null, false },
                    { 31, 10, 5, new DateTime(2024, 3, 9, 15, 53, 31, 980, DateTimeKind.Local).AddTicks(4695), 270.0, null, 4, 2018, "S", 4, 1, null, false },
                    { 32, 10, 4, new DateTime(2024, 3, 9, 15, 53, 31, 980, DateTimeKind.Local).AddTicks(4712), 250.0, null, 4, 2017, "3", 4, 1, null, false },
                    { 33, 5, 6, new DateTime(2024, 3, 9, 15, 53, 31, 980, DateTimeKind.Local).AddTicks(4728), 70.0, null, 4, 2017, "Leaf", 5, 1, null, false }
                });

            migrationBuilder.InsertData(
                table: "CarImages",
                columns: new[] { "Id", "CarId", "CreatedAt", "DeletedAt", "ImageUrl", "IsMain", "UpdatedAt", "isDeleted" },
                values: new object[,]
                {
                    { 1, 1, null, null, "bmw-m5.jpg", true, null, false },
                    { 2, 2, null, null, "toyota-prado-2015.jpg", true, null, false },
                    { 3, 3, null, null, "chevrolet-aveo-2013.jpg", true, null, false },
                    { 4, 4, null, null, "hyundai-accent-2014.jpg", true, null, false },
                    { 5, 5, null, null, "hyundai-azera-2013.jpg", true, null, false },
                    { 6, 6, null, null, "hyundai-elantra-2015.jpg", true, null, false },
                    { 7, 7, null, null, "hyundai-h1-2014.jpg", true, null, false },
                    { 8, 8, null, null, "hyundai-ix35-2014.jpg", true, null, false },
                    { 9, 9, null, null, "hyundai-sonata-2013.jpg", true, null, false },
                    { 10, 10, null, null, "hyundai-sonata-2017.jpg", true, null, false },
                    { 11, 11, null, null, "kia-cerato-2014.jpg", true, null, false },
                    { 12, 12, null, null, "kia-optima-2014.jpg", true, null, false },
                    { 13, 13, null, null, "kia-rio-2014.jpg", true, null, false },
                    { 14, 14, null, null, "kia-sorento-2013.jpg", true, null, false },
                    { 15, 15, null, null, "kia-sportage-2014.jpg", true, null, false },
                    { 16, 16, null, null, "lexus-570-2012.jpg", true, null, false },
                    { 17, 17, null, null, "mercedes-g-class-2012.jpg", true, null, false },
                    { 18, 18, null, null, "mercedes-g-class-2015.jpg", true, null, false },
                    { 19, 19, null, null, "mercedes-s-class-2016.jpg", true, null, false },
                    { 20, 20, null, null, "mercedes-v-class-2016.jpg", true, null, false },
                    { 21, 21, null, null, "mitsubishi-L200-2013.jpg", true, null, false },
                    { 22, 22, null, null, "mitsubishi-pajero.jpg", true, null, false },
                    { 23, 23, null, null, "nissan-navara-2013.jpg", true, null, false },
                    { 24, 24, null, null, "nissan-sunny-2014.jpg", true, null, false },
                    { 25, 25, null, null, "toyota-camry-2013.jpg", true, null, false },
                    { 26, 26, null, null, "toyota-land-cruiser-2014.jpg", true, null, false },
                    { 27, 27, null, null, "toyota-land-cruiser-2018.jpg", true, null, false },
                    { 28, 28, null, null, "bmw-x5.png", true, null, false },
                    { 29, 29, null, null, "toyota-prado-2018.jpg", true, null, false },
                    { 30, 30, null, null, "tesla-x-2016.png", true, null, false },
                    { 31, 31, null, null, "tesla-s-2018.png", true, null, false },
                    { 32, 32, null, null, "tesla-3-2017.png", true, null, false },
                    { 33, 33, null, null, "nissan-leaf-2017.png", true, null, false }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_CarImages_CarId",
                table: "CarImages",
                column: "CarId");

            migrationBuilder.CreateIndex(
                name: "IX_Cars_BrandId",
                table: "Cars",
                column: "BrandId");

            migrationBuilder.CreateIndex(
                name: "IX_Cars_ColorId",
                table: "Cars",
                column: "ColorId");

            migrationBuilder.CreateIndex(
                name: "IX_Rentals_CarId",
                table: "Rentals",
                column: "CarId");

            migrationBuilder.CreateIndex(
                name: "IX_Rentals_UserId",
                table: "Rentals",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "About");

            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "Bios");

            migrationBuilder.DropTable(
                name: "CarImages");

            migrationBuilder.DropTable(
                name: "IdentityUser");

            migrationBuilder.DropTable(
                name: "Rentals");

            migrationBuilder.DropTable(
                name: "Teams");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Cars");

            migrationBuilder.DropTable(
                name: "Brands");

            migrationBuilder.DropTable(
                name: "Colors");
        }
    }
}
