﻿// <auto-generated />
using System;
using Battery_Shop.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Battery_Shop.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    [Migration("20210513075939_BatteriesBatteryShopid")]
    partial class BatteriesBatteryShopid
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.5")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Battery_Shop.Models.Battery", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("BatteryShopId")
                        .HasColumnType("int");

                    b.Property<int?>("CustomerId")
                        .HasColumnType("int");

                    b.Property<int>("Life")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(32)
                        .HasColumnType("nvarchar(32)");

                    b.Property<int>("Price")
                        .HasColumnType("int");

                    b.Property<bool>("Sold")
                        .HasColumnType("bit");

                    b.Property<int>("StorageId")
                        .HasColumnType("int");

                    b.Property<DateTime>("Warrant")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("BatteryShopId");

                    b.HasIndex("CustomerId");

                    b.HasIndex("StorageId");

                    b.ToTable("Batteries");
                });

            modelBuilder.Entity("Battery_Shop.Models.BatteryShop", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Address")
                        .HasMaxLength(64)
                        .HasColumnType("nvarchar(64)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(16)
                        .HasColumnType("nvarchar(16)");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasMaxLength(11)
                        .HasColumnType("nvarchar(11)");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("BatteryShops");
                });

            modelBuilder.Entity("Battery_Shop.Models.Customer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("nvarchar(64)");

                    b.Property<int>("BatteryShopId")
                        .HasColumnType("int");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(32)
                        .HasColumnType("nvarchar(32)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(16)
                        .HasColumnType("nvarchar(16)");

                    b.HasKey("Id");

                    b.HasIndex("BatteryShopId");

                    b.ToTable("Customers");
                });

            modelBuilder.Entity("Battery_Shop.Models.Employee", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("BatteryShopId")
                        .HasColumnType("int");

                    b.Property<int>("Job")
                        .HasColumnType("int");

                    b.Property<string>("LastName")
                        .HasMaxLength(32)
                        .HasColumnType("nvarchar(32)");

                    b.Property<string>("Name")
                        .HasMaxLength(16)
                        .HasColumnType("nvarchar(16)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("nvarchar(64)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(16)
                        .HasColumnType("nvarchar(16)");

                    b.HasKey("Id");

                    b.HasIndex("BatteryShopId");

                    b.ToTable("Employees");
                });

            modelBuilder.Entity("Battery_Shop.Models.Storage", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("BatterShopId")
                        .HasColumnType("int");

                    b.Property<int?>("BatteryShopId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(32)
                        .HasColumnType("nvarchar(32)");

                    b.HasKey("Id");

                    b.HasIndex("BatteryShopId");

                    b.ToTable("Storages");
                });

            modelBuilder.Entity("Battery_Shop.Models.Battery", b =>
                {
                    b.HasOne("Battery_Shop.Models.BatteryShop", "BatteryShop")
                        .WithMany()
                        .HasForeignKey("BatteryShopId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Battery_Shop.Models.Customer", "Customer")
                        .WithMany("Batteries")
                        .HasForeignKey("CustomerId");

                    b.HasOne("Battery_Shop.Models.Storage", "Storage")
                        .WithMany("Batteries")
                        .HasForeignKey("StorageId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("BatteryShop");

                    b.Navigation("Customer");

                    b.Navigation("Storage");
                });

            modelBuilder.Entity("Battery_Shop.Models.Customer", b =>
                {
                    b.HasOne("Battery_Shop.Models.BatteryShop", "BatteryShop")
                        .WithMany("Customers")
                        .HasForeignKey("BatteryShopId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("BatteryShop");
                });

            modelBuilder.Entity("Battery_Shop.Models.Employee", b =>
                {
                    b.HasOne("Battery_Shop.Models.BatteryShop", "BatteryShop")
                        .WithMany("Employees")
                        .HasForeignKey("BatteryShopId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("BatteryShop");
                });

            modelBuilder.Entity("Battery_Shop.Models.Storage", b =>
                {
                    b.HasOne("Battery_Shop.Models.BatteryShop", "BatteryShop")
                        .WithMany("Storages")
                        .HasForeignKey("BatteryShopId");

                    b.Navigation("BatteryShop");
                });

            modelBuilder.Entity("Battery_Shop.Models.BatteryShop", b =>
                {
                    b.Navigation("Customers");

                    b.Navigation("Employees");

                    b.Navigation("Storages");
                });

            modelBuilder.Entity("Battery_Shop.Models.Customer", b =>
                {
                    b.Navigation("Batteries");
                });

            modelBuilder.Entity("Battery_Shop.Models.Storage", b =>
                {
                    b.Navigation("Batteries");
                });
#pragma warning restore 612, 618
        }
    }
}