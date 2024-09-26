﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace CustomerManagementApi.Migrations
{
    [DbContext(typeof(CustomerDbContext))]
    [Migration("20240926230332_Initial")]
    partial class Initial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.8");

            modelBuilder.Entity("CustomerManagementApi.Models.Customer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("Created")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("TEXT");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("Updated")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP");

                    b.HasKey("Id");

                    b.ToTable("Customers");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Created = new DateTime(2024, 9, 27, 0, 3, 31, 794, DateTimeKind.Local).AddTicks(8272),
                            Email = "john.doe@example.com",
                            FirstName = "John",
                            PhoneNumber = "1234567890",
                            Surname = "Doe",
                            Updated = new DateTime(2024, 9, 27, 0, 3, 31, 794, DateTimeKind.Local).AddTicks(8311)
                        },
                        new
                        {
                            Id = 2,
                            Created = new DateTime(2024, 9, 27, 0, 3, 31, 794, DateTimeKind.Local).AddTicks(8315),
                            Email = "jane.smith@example.com",
                            FirstName = "Jane",
                            PhoneNumber = "0987654321",
                            Surname = "Smith",
                            Updated = new DateTime(2024, 9, 27, 0, 3, 31, 794, DateTimeKind.Local).AddTicks(8317)
                        },
                        new
                        {
                            Id = 3,
                            Created = new DateTime(2024, 9, 27, 0, 3, 31, 794, DateTimeKind.Local).AddTicks(8319),
                            Email = "bob.brown@example.com",
                            FirstName = "Bob",
                            PhoneNumber = "1122334455",
                            Surname = "Brown",
                            Updated = new DateTime(2024, 9, 27, 0, 3, 31, 794, DateTimeKind.Local).AddTicks(8321)
                        },
                        new
                        {
                            Id = 4,
                            Created = new DateTime(2024, 9, 27, 0, 3, 31, 794, DateTimeKind.Local).AddTicks(8323),
                            Email = "alice.johnson@example.com",
                            FirstName = "Alice",
                            PhoneNumber = "2233445566",
                            Surname = "Johnson",
                            Updated = new DateTime(2024, 9, 27, 0, 3, 31, 794, DateTimeKind.Local).AddTicks(8324)
                        },
                        new
                        {
                            Id = 5,
                            Created = new DateTime(2024, 9, 27, 0, 3, 31, 794, DateTimeKind.Local).AddTicks(8327),
                            Email = "charlie.white@example.com",
                            FirstName = "Charlie",
                            PhoneNumber = "3344556677",
                            Surname = "White",
                            Updated = new DateTime(2024, 9, 27, 0, 3, 31, 794, DateTimeKind.Local).AddTicks(8328)
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
