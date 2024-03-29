﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Workshop.API.Data;

#nullable disable

namespace Workshop.API.Migrations
{
    [DbContext(typeof(WorkshopDbContext))]
    [Migration("20230102195919_cars_workshop_service_repair_localization")]
    partial class cars_workshop_service_repair_localization
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("Workshop.API.Models.Car", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Brand")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LicensePlate")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Model")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PersonalUserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("ProductionYear")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PersonalUserId");

                    b.ToTable("Cars");
                });

            modelBuilder.Entity("Workshop.API.Models.Localization", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<double>("Latitude")
                        .HasColumnType("float");

                    b.Property<double>("Longitude")
                        .HasColumnType("float");

                    b.Property<string>("WorkshopId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("WorkshopId")
                        .IsUnique()
                        .HasFilter("[WorkshopId] IS NOT NULL");

                    b.ToTable("Localizations");
                });

            modelBuilder.Entity("Workshop.API.Models.RefreshToken", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("CreationTime")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("Token")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("RefreshTokens");
                });

            modelBuilder.Entity("Workshop.API.Models.Repair", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("CarId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Message")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PersonalUserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("WorkshopId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("CarId");

                    b.HasIndex("PersonalUserId");

                    b.HasIndex("WorkshopId");

                    b.ToTable("Repairs");
                });

            modelBuilder.Entity("Workshop.API.Models.RepairService", b =>
                {
                    b.Property<string>("ServiceId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RepairId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("ServiceId", "RepairId");

                    b.HasIndex("RepairId");

                    b.ToTable("RepairServices");
                });

            modelBuilder.Entity("Workshop.API.Models.Service", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Category")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Price")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.ToTable("Services");

                    b.HasData(
                        new
                        {
                            Id = "6182c48f-4e41-4609-808e-f6df95e5d85f",
                            Category = "Maintenance",
                            Name = "Oil change",
                            Price = 201.5
                        },
                        new
                        {
                            Id = "9bcc9fd5-0a5a-439d-b76a-4acc39714d60",
                            Category = "Maintenance",
                            Name = "Tire rotation",
                            Price = 50.0
                        },
                        new
                        {
                            Id = "1e256131-463d-4412-8759-37d1c640420b",
                            Category = "Maintenance",
                            Name = "Brake maintenance",
                            Price = 100.0
                        },
                        new
                        {
                            Id = "1d16eea6-efc0-4cf9-bdba-b69326495071",
                            Category = "Repair",
                            Name = "Engine repair",
                            Price = 2000.0
                        },
                        new
                        {
                            Id = "33625433-e450-4100-946c-1a8ccd8305ca",
                            Category = "Repair",
                            Name = "Transmission repair",
                            Price = 1000.0
                        },
                        new
                        {
                            Id = "ba654a63-7f40-4890-82f6-bed5037392fc",
                            Category = "Repair",
                            Name = "Suspension repair",
                            Price = 500.0
                        },
                        new
                        {
                            Id = "7f785c6b-99d2-41d3-aeea-1d54dbc7c964",
                            Category = "Electrical system",
                            Name = "Repair lectrical issues",
                            Price = 200.0
                        },
                        new
                        {
                            Id = "8f30b948-9c82-4448-90f0-e0b93dae57e8",
                            Category = "Air conditioning",
                            Name = "Compressor repair",
                            Price = 100.0
                        },
                        new
                        {
                            Id = "2e264c35-29ce-40f2-9377-cb7a6ac9fb88",
                            Category = "Air conditioning",
                            Name = "Evaporator repair",
                            Price = 100.0
                        },
                        new
                        {
                            Id = "32b98306-a9b0-4672-8203-bd8ad14fcdf7",
                            Category = "Detailing",
                            Name = "Exterior wash",
                            Price = 200.0
                        },
                        new
                        {
                            Id = "53c816fa-792f-4e2f-b35d-33a2e43f5df0",
                            Category = "Safety inspections",
                            Name = "Safety inspection",
                            Price = 300.0
                        },
                        new
                        {
                            Id = "bb8281ea-0583-4705-b8c7-9da2692d9542",
                            Category = "Other",
                            Name = "Other",
                            Price = 50.0
                        });
                });

            modelBuilder.Entity("Workshop.API.Models.Workshop", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LocalizationId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("OwnerId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("OwnerId");

                    b.ToTable("Workshops");
                });

            modelBuilder.Entity("Workshop.API.Models.WorkshopService", b =>
                {
                    b.Property<string>("ServiceId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("WorkshopId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("ServiceId", "WorkshopId");

                    b.HasIndex("WorkshopId");

                    b.ToTable("WorkshopServices");
                });

            modelBuilder.Entity("Workshop.API.Models.WorkshopUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("Workshop.API.Models.BusinessUser", b =>
                {
                    b.HasBaseType("Workshop.API.Models.WorkshopUser");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.ToTable("BusinessUsers");
                });

            modelBuilder.Entity("Workshop.API.Models.PersonalUser", b =>
                {
                    b.HasBaseType("Workshop.API.Models.WorkshopUser");

                    b.Property<string>("FristName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.ToTable("PersonalUsers");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Workshop.API.Models.WorkshopUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Workshop.API.Models.WorkshopUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Workshop.API.Models.WorkshopUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Workshop.API.Models.WorkshopUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Workshop.API.Models.Car", b =>
                {
                    b.HasOne("Workshop.API.Models.PersonalUser", "PersonalUser")
                        .WithMany("Cars")
                        .HasForeignKey("PersonalUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("PersonalUser");
                });

            modelBuilder.Entity("Workshop.API.Models.Localization", b =>
                {
                    b.HasOne("Workshop.API.Models.Workshop", "Workshop")
                        .WithOne("Localization")
                        .HasForeignKey("Workshop.API.Models.Localization", "WorkshopId");

                    b.Navigation("Workshop");
                });

            modelBuilder.Entity("Workshop.API.Models.RefreshToken", b =>
                {
                    b.HasOne("Workshop.API.Models.WorkshopUser", "User")
                        .WithMany("RefreshTokens")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Workshop.API.Models.Repair", b =>
                {
                    b.HasOne("Workshop.API.Models.Car", "Car")
                        .WithMany()
                        .HasForeignKey("CarId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Workshop.API.Models.PersonalUser", "PersonalUser")
                        .WithMany("Repairs")
                        .HasForeignKey("PersonalUserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Workshop.API.Models.Workshop", "Workshop")
                        .WithMany("Repairs")
                        .HasForeignKey("WorkshopId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Car");

                    b.Navigation("PersonalUser");

                    b.Navigation("Workshop");
                });

            modelBuilder.Entity("Workshop.API.Models.RepairService", b =>
                {
                    b.HasOne("Workshop.API.Models.Repair", "Repair")
                        .WithMany("RepairServices")
                        .HasForeignKey("RepairId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Workshop.API.Models.Service", "Service")
                        .WithMany("RepairServices")
                        .HasForeignKey("ServiceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Repair");

                    b.Navigation("Service");
                });

            modelBuilder.Entity("Workshop.API.Models.Workshop", b =>
                {
                    b.HasOne("Workshop.API.Models.BusinessUser", "Owner")
                        .WithMany("Workshops")
                        .HasForeignKey("OwnerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Owner");
                });

            modelBuilder.Entity("Workshop.API.Models.WorkshopService", b =>
                {
                    b.HasOne("Workshop.API.Models.Service", "Service")
                        .WithMany("WorkshopServices")
                        .HasForeignKey("ServiceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Workshop.API.Models.Workshop", "Workshop")
                        .WithMany("WorkshopServices")
                        .HasForeignKey("WorkshopId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Service");

                    b.Navigation("Workshop");
                });

            modelBuilder.Entity("Workshop.API.Models.BusinessUser", b =>
                {
                    b.HasOne("Workshop.API.Models.WorkshopUser", null)
                        .WithOne()
                        .HasForeignKey("Workshop.API.Models.BusinessUser", "Id")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Workshop.API.Models.PersonalUser", b =>
                {
                    b.HasOne("Workshop.API.Models.WorkshopUser", null)
                        .WithOne()
                        .HasForeignKey("Workshop.API.Models.PersonalUser", "Id")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Workshop.API.Models.Repair", b =>
                {
                    b.Navigation("RepairServices");
                });

            modelBuilder.Entity("Workshop.API.Models.Service", b =>
                {
                    b.Navigation("RepairServices");

                    b.Navigation("WorkshopServices");
                });

            modelBuilder.Entity("Workshop.API.Models.Workshop", b =>
                {
                    b.Navigation("Localization")
                        .IsRequired();

                    b.Navigation("Repairs");

                    b.Navigation("WorkshopServices");
                });

            modelBuilder.Entity("Workshop.API.Models.WorkshopUser", b =>
                {
                    b.Navigation("RefreshTokens");
                });

            modelBuilder.Entity("Workshop.API.Models.BusinessUser", b =>
                {
                    b.Navigation("Workshops");
                });

            modelBuilder.Entity("Workshop.API.Models.PersonalUser", b =>
                {
                    b.Navigation("Cars");

                    b.Navigation("Repairs");
                });
#pragma warning restore 612, 618
        }
    }
}
