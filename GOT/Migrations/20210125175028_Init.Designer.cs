﻿// <auto-generated />
using System;
using GOT.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace GOT.Migrations
{
    [DbContext(typeof(GotDbContext))]
    [Migration("20210125175028_Init")]
    partial class Init
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.2");

            modelBuilder.Entity("GOT.Models.Area", b =>
                {
                    b.Property<int>("AreaId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("AreaName")
                        .IsRequired()
                        .HasMaxLength(40)
                        .HasColumnType("nvarchar(40)");

                    b.HasKey("AreaId");

                    b.ToTable("Areas");

                    b.HasData(
                        new
                        {
                            AreaId = 1,
                            AreaName = "Beskidy Zachodnie"
                        },
                        new
                        {
                            AreaId = 2,
                            AreaName = "Beskidy Wschodnie"
                        },
                        new
                        {
                            AreaId = 3,
                            AreaName = "Tatry"
                        },
                        new
                        {
                            AreaId = 4,
                            AreaName = "Sudety"
                        },
                        new
                        {
                            AreaId = 5,
                            AreaName = "Gory Swietokrzyskie"
                        });
                });

            modelBuilder.Entity("GOT.Models.Checkpoint", b =>
                {
                    b.Property<int>("CheckpointId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int>("AreaId")
                        .HasColumnType("int");

                    b.Property<string>("CheckpointDescription")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CheckpointName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("XCoords")
                        .HasColumnType("float");

                    b.Property<double>("YCoords")
                        .HasColumnType("float");

                    b.HasKey("CheckpointId");

                    b.HasIndex("AreaId");

                    b.ToTable("Checkpoints");
                });

            modelBuilder.Entity("GOT.Models.Path", b =>
                {
                    b.Property<int>("PathId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int>("CheckpointAId")
                        .HasColumnType("int");

                    b.Property<int>("CheckpointBId")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("DistanceAB")
                        .HasColumnType("int");

                    b.Property<int>("DistanceBA")
                        .HasColumnType("int");

                    b.Property<int>("ElevationAB")
                        .HasColumnType("int");

                    b.Property<int>("ElevationBA")
                        .HasColumnType("int");

                    b.Property<string>("PathName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("PathId");

                    b.HasIndex("CheckpointAId");

                    b.HasIndex("CheckpointBId");

                    b.ToTable("Paths");
                });

            modelBuilder.Entity("GOT.Models.Checkpoint", b =>
                {
                    b.HasOne("GOT.Models.Area", "Area")
                        .WithMany("Checkpoints")
                        .HasForeignKey("AreaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Area");
                });

            modelBuilder.Entity("GOT.Models.Path", b =>
                {
                    b.HasOne("GOT.Models.Checkpoint", "CheckpointA")
                        .WithMany("PathsA")
                        .HasForeignKey("CheckpointAId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("GOT.Models.Checkpoint", "CheckpointB")
                        .WithMany("PathsB")
                        .HasForeignKey("CheckpointBId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("CheckpointA");

                    b.Navigation("CheckpointB");
                });

            modelBuilder.Entity("GOT.Models.Area", b =>
                {
                    b.Navigation("Checkpoints");
                });

            modelBuilder.Entity("GOT.Models.Checkpoint", b =>
                {
                    b.Navigation("PathsA");

                    b.Navigation("PathsB");
                });
#pragma warning restore 612, 618
        }
    }
}
