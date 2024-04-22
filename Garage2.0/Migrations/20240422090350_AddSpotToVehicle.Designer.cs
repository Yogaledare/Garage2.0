﻿// <auto-generated />
using System;
using Garage2._0.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Garage2._0.Migrations
{
    [DbContext(typeof(Garage2_0Context))]
    [Migration("20240422090350_AddSpotToVehicle")]
    partial class AddSpotToVehicle
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Garage2._0.Models.ParkingSpot", b =>
                {
                    b.Property<int>("ParkingSpotId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ParkingSpotId"));

                    b.Property<bool>("IsAvailable")
                        .HasColumnType("bit");

                    b.Property<int>("Spot")
                        .HasColumnType("int");

                    b.HasKey("ParkingSpotId");

                    b.ToTable("ParkingSpot");
                });

            modelBuilder.Entity("Garage2._0.Models.Vehicle", b =>
                {
                    b.Property<int>("VehicleId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("VehicleId"));

                    b.Property<DateTime>("ArrivalTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("Brand")
                        .IsRequired()
                        .HasMaxLength(40)
                        .HasColumnType("nvarchar(40)");

                    b.Property<string>("Color")
                        .IsRequired()
                        .HasMaxLength(40)
                        .HasColumnType("nvarchar(40)");

                    b.Property<DateTime?>("DepartureTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("LicensePlate")
                        .IsRequired()
                        .HasMaxLength(40)
                        .HasColumnType("nvarchar(40)");

                    b.Property<string>("Model")
                        .IsRequired()
                        .HasMaxLength(40)
                        .HasColumnType("nvarchar(40)");

                    b.Property<int>("NumberOfWheels")
                        .HasColumnType("int");

                    b.Property<int?>("ParkingSpotId")
                        .HasColumnType("int");

                    b.Property<int>("VehicleType")
                        .HasColumnType("int");

                    b.HasKey("VehicleId");

                    b.HasIndex("ParkingSpotId");

                    b.ToTable("Vehicles");
                });

            modelBuilder.Entity("Garage2._0.Models.Vehicle", b =>
                {
                    b.HasOne("Garage2._0.Models.ParkingSpot", "ParkingSpot")
                        .WithMany()
                        .HasForeignKey("ParkingSpotId");

                    b.Navigation("ParkingSpot");
                });
#pragma warning restore 612, 618
        }
    }
}
