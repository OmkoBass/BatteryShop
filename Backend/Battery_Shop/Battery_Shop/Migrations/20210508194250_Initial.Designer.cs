// <auto-generated />
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
    [Migration("20210508194250_Initial")]
    partial class Initial
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

                    b.Property<int>("Life")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(32)
                        .HasColumnType("nvarchar(32)");

                    b.Property<int>("Price")
                        .HasColumnType("int");

                    b.Property<int?>("StorageId")
                        .HasColumnType("int");

                    b.Property<DateTime>("Warrant")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("getdate()");

                    b.HasKey("Id");

                    b.HasIndex("StorageId");

                    b.ToTable("Batteries");
                });

            modelBuilder.Entity("Battery_Shop.Models.BatteryShop", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

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

            modelBuilder.Entity("Battery_Shop.Models.Employee", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("BatterShopId")
                        .HasColumnType("int");

                    b.Property<int?>("BatteryShopId")
                        .HasColumnType("int");

                    b.Property<int>("Job")
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

                    b.HasKey("Id");

                    b.HasIndex("BatteryShopId");

                    b.ToTable("Storages");
                });

            modelBuilder.Entity("Battery_Shop.Models.Battery", b =>
                {
                    b.HasOne("Battery_Shop.Models.Storage", null)
                        .WithMany("Batteries")
                        .HasForeignKey("StorageId");
                });

            modelBuilder.Entity("Battery_Shop.Models.Employee", b =>
                {
                    b.HasOne("Battery_Shop.Models.BatteryShop", "BatteryShop")
                        .WithMany("Employees")
                        .HasForeignKey("BatteryShopId");

                    b.Navigation("BatteryShop");
                });

            modelBuilder.Entity("Battery_Shop.Models.Storage", b =>
                {
                    b.HasOne("Battery_Shop.Models.BatteryShop", "BatteryShop")
                        .WithMany()
                        .HasForeignKey("BatteryShopId");

                    b.Navigation("BatteryShop");
                });

            modelBuilder.Entity("Battery_Shop.Models.BatteryShop", b =>
                {
                    b.Navigation("Employees");
                });

            modelBuilder.Entity("Battery_Shop.Models.Storage", b =>
                {
                    b.Navigation("Batteries");
                });
#pragma warning restore 612, 618
        }
    }
}
