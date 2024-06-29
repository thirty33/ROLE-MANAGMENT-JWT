﻿// <auto-generated />
using System;
using ApiPeople.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace ApiPeople.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20240629184928_add_password_hash_to_user")]
    partial class add_password_hash_to_user
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("ApiPeople.Models.Category", b =>
                {
                    b.Property<Guid>("CategoryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.Property<int>("Weight")
                        .HasColumnType("int");

                    b.HasKey("CategoryId");

                    b.ToTable("Category", (string)null);

                    b.HasData(
                        new
                        {
                            CategoryId = new Guid("fe2de405-c38e-4c90-ac52-da0540dfb4ef"),
                            Description = "hola",
                            Name = "Actividades pendientes",
                            Weight = 20
                        },
                        new
                        {
                            CategoryId = new Guid("fe2de405-c38e-4c90-ac52-da0540dfb402"),
                            Description = "hola",
                            Name = "Actividades personales",
                            Weight = 50
                        });
                });

            modelBuilder.Entity("ApiPeople.Models.Person", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"));

                    b.Property<int>("Age")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("id");

                    b.ToTable("Persons");
                });

            modelBuilder.Entity("ApiPeople.Models.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.HasKey("Id");

                    b.ToTable("Roles");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Admin"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Customer"
                        });
                });

            modelBuilder.Entity("ApiPeople.Models.Task", b =>
                {
                    b.Property<Guid>("TaskId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("CategoryId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Priority")
                        .HasColumnType("int");

                    b.Property<string>("Tittle")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<DateTime>("created_at")
                        .HasColumnType("datetime2");

                    b.HasKey("TaskId");

                    b.HasIndex("CategoryId");

                    b.ToTable("task", (string)null);

                    b.HasData(
                        new
                        {
                            TaskId = new Guid("fe2de405-c38e-4c90-ac52-da0540dfb410"),
                            CategoryId = new Guid("fe2de405-c38e-4c90-ac52-da0540dfb4ef"),
                            Priority = 0,
                            Tittle = "Pago de servicios publicos",
                            created_at = new DateTime(2024, 6, 29, 14, 49, 27, 444, DateTimeKind.Local).AddTicks(5621)
                        },
                        new
                        {
                            TaskId = new Guid("fe2de405-c38e-4c90-ac52-da0540dfb411"),
                            CategoryId = new Guid("fe2de405-c38e-4c90-ac52-da0540dfb402"),
                            Priority = 0,
                            Tittle = "Terminar de ver pelicula en netflix",
                            created_at = new DateTime(2024, 6, 29, 14, 49, 27, 444, DateTimeKind.Local).AddTicks(5641)
                        });
                });

            modelBuilder.Entity("ApiPeople.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<byte[]>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<byte[]>("PasswordSalt")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(25)
                        .HasColumnType("nvarchar(25)");

                    b.HasKey("Id");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Email = "john.doe@example.com",
                            Name = "John Doe",
                            PasswordHash = new byte[] { 108, 57, 7, 34, 21, 127, 216, 7, 150, 237, 46, 66, 38, 0, 10, 110, 15, 234, 57, 180, 38, 237, 141, 222, 116, 229, 215, 193, 149, 232, 46, 155, 201, 167, 125, 78, 26, 136, 7, 133, 179, 251, 115, 45, 208, 218, 119, 9, 119, 246, 70, 83, 175, 213, 130, 89, 13, 8, 62, 159, 151, 107, 222, 46 },
                            PasswordSalt = new byte[] { 120, 146, 176, 28, 220, 31, 68, 108, 170, 170, 36, 52, 110, 155, 74, 167, 10, 218, 103, 117, 174, 62, 149, 241, 106, 107, 191, 3, 26, 155, 223, 155, 152, 195, 190, 24, 210, 226, 3, 102, 245, 234, 4, 117, 72, 166, 6, 85, 145, 132, 22, 83, 83, 237, 230, 150, 68, 68, 117, 33, 109, 220, 105, 46, 104, 71, 18, 58, 104, 253, 203, 81, 216, 94, 19, 63, 185, 196, 252, 183, 164, 86, 232, 99, 200, 193, 65, 242, 180, 110, 231, 190, 95, 179, 173, 14, 140, 131, 165, 105, 235, 127, 181, 129, 27, 54, 175, 129, 247, 225, 82, 222, 245, 109, 63, 2, 193, 203, 165, 37, 233, 146, 248, 149, 200, 183, 214, 94 },
                            Username = "John"
                        },
                        new
                        {
                            Id = 2,
                            Email = "jane.smith@example.com",
                            Name = "Jane Smith",
                            PasswordHash = new byte[] { 155, 35, 137, 117, 52, 87, 70, 103, 94, 22, 145, 62, 90, 133, 234, 160, 42, 219, 71, 150, 60, 83, 61, 244, 40, 65, 129, 45, 58, 18, 210, 53, 173, 55, 119, 93, 103, 182, 193, 144, 163, 74, 5, 248, 166, 93, 147, 18, 0, 95, 99, 108, 18, 102, 191, 46, 76, 230, 36, 100, 144, 64, 165, 120 },
                            PasswordSalt = new byte[] { 252, 32, 80, 231, 190, 66, 33, 157, 161, 31, 151, 100, 52, 208, 48, 200, 35, 203, 255, 99, 175, 111, 182, 123, 58, 248, 81, 116, 89, 60, 205, 214, 228, 92, 23, 162, 127, 125, 231, 6, 145, 173, 31, 217, 150, 65, 24, 152, 188, 252, 188, 170, 198, 10, 188, 94, 71, 159, 150, 42, 158, 73, 26, 164, 66, 94, 176, 45, 11, 241, 65, 183, 151, 2, 160, 160, 3, 156, 88, 181, 68, 207, 15, 30, 109, 143, 208, 155, 126, 32, 126, 116, 126, 70, 241, 98, 110, 177, 70, 218, 178, 143, 116, 161, 205, 135, 17, 154, 201, 177, 41, 64, 183, 53, 180, 187, 118, 147, 199, 32, 249, 86, 234, 126, 164, 60, 123, 45 },
                            Username = "Jane"
                        });
                });

            modelBuilder.Entity("ApiPeople.Models.UserRole", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("UserRoles");

                    b.HasData(
                        new
                        {
                            UserId = 1,
                            RoleId = 1
                        },
                        new
                        {
                            UserId = 2,
                            RoleId = 2
                        },
                        new
                        {
                            UserId = 1,
                            RoleId = 2
                        });
                });

            modelBuilder.Entity("ApiPeople.Models.Task", b =>
                {
                    b.HasOne("ApiPeople.Models.Category", "Category")
                        .WithMany("Tasks")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");
                });

            modelBuilder.Entity("ApiPeople.Models.UserRole", b =>
                {
                    b.HasOne("ApiPeople.Models.Role", "Role")
                        .WithMany("UserRoles")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ApiPeople.Models.User", "User")
                        .WithMany("UserRoles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");

                    b.Navigation("User");
                });

            modelBuilder.Entity("ApiPeople.Models.Category", b =>
                {
                    b.Navigation("Tasks");
                });

            modelBuilder.Entity("ApiPeople.Models.Role", b =>
                {
                    b.Navigation("UserRoles");
                });

            modelBuilder.Entity("ApiPeople.Models.User", b =>
                {
                    b.Navigation("UserRoles");
                });
#pragma warning restore 612, 618
        }
    }
}