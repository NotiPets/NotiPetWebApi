﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Notipet.Data;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Notipet.Data.Migrations
{
    [DbContext(typeof(NotiPetBdContext))]
    partial class NotiPetBdContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Notipet.Domain.Appointment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<bool>("Active")
                        .HasColumnType("boolean");

                    b.Property<int>("AppointmentStatus")
                        .HasColumnType("integer")
                        .HasColumnName("AppointmentStatusId");

                    b.Property<int>("AssetsServicesId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("Created")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("Date")
                        .HasColumnType("timestamp with time zone");

                    b.Property<bool>("IsEmergency")
                        .HasColumnType("boolean");

                    b.Property<Guid>("PetId")
                        .HasColumnType("uuid");

                    b.Property<DateTime?>("Updated")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.HasIndex("AppointmentStatus");

                    b.HasIndex("AssetsServicesId");

                    b.HasIndex("PetId");

                    b.ToTable("Appointments");
                });

            modelBuilder.Entity("Notipet.Domain.AppointmentStatus", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("character varying(10)");

                    b.HasKey("Id");

                    b.ToTable("AppointmentStatus");

                    b.HasData(
                        new
                        {
                            Id = 0,
                            Name = "Requested"
                        },
                        new
                        {
                            Id = 1,
                            Name = "Accepted"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Cancelled"
                        });
                });

            modelBuilder.Entity("Notipet.Domain.AssetsServices", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<bool>("Active")
                        .HasColumnType("boolean");

                    b.Property<int>("AssetsServiceType")
                        .HasColumnType("integer")
                        .HasColumnName("AssetsServiceTypeId");

                    b.Property<Guid>("BusinessId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("Created")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(300)
                        .HasColumnType("character varying(300)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<decimal>("Price")
                        .HasColumnType("numeric");

                    b.Property<int>("Quantity")
                        .HasColumnType("integer");

                    b.Property<DateTime?>("Updated")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("Vendor")
                        .HasColumnType("integer")
                        .HasColumnName("VendorId");

                    b.HasKey("Id");

                    b.HasIndex("AssetsServiceType");

                    b.HasIndex("BusinessId");

                    b.HasIndex("Vendor");

                    b.ToTable("AssetsServices");
                });

            modelBuilder.Entity("Notipet.Domain.AssetsServiceType", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("character varying(10)");

                    b.HasKey("Id");

                    b.ToTable("AssetsServiceType");

                    b.HasData(
                        new
                        {
                            Id = 0,
                            Name = "Product"
                        },
                        new
                        {
                            Id = 1,
                            Name = "Service"
                        });
                });

            modelBuilder.Entity("Notipet.Domain.Business", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Address1")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<string>("Address2")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<string>("BusinessName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasMaxLength(25)
                        .HasColumnType("character varying(25)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(320)
                        .HasColumnType("character varying(320)");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasMaxLength(15)
                        .HasColumnType("character varying(15)");

                    b.Property<string>("Province")
                        .IsRequired()
                        .HasMaxLength(25)
                        .HasColumnType("character varying(25)");

                    b.Property<string>("Rnc")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("character varying(20)");

                    b.HasKey("Id");

                    b.HasIndex("Rnc")
                        .IsUnique();

                    b.ToTable("Businesses");
                });

            modelBuilder.Entity("Notipet.Domain.DigitalVaccine", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("BusinessId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("Date")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("PetId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("UserRoleId")
                        .HasColumnType("uuid");

                    b.Property<string>("VaccineName")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("character varying(30)");

                    b.HasKey("Id");

                    b.HasIndex("BusinessId");

                    b.HasIndex("PetId");

                    b.HasIndex("UserRoleId");

                    b.ToTable("DigitalVaccines");
                });

            modelBuilder.Entity("Notipet.Domain.DocumentType", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("character varying(10)");

                    b.HasKey("Id");

                    b.ToTable("DocumentType");

                    b.HasData(
                        new
                        {
                            Id = 0,
                            Name = "Cedula"
                        },
                        new
                        {
                            Id = 1,
                            Name = "Passport"
                        });
                });

            modelBuilder.Entity("Notipet.Domain.Order", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<int>("AssetsAssetsServicesId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("Created")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("OrderStatus")
                        .HasColumnType("integer")
                        .HasColumnName("OrderStatusId");

                    b.Property<int>("Quantity")
                        .HasColumnType("integer");

                    b.Property<Guid>("SaleId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("UserRoleId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("AssetsAssetsServicesId");

                    b.HasIndex("OrderStatus");

                    b.HasIndex("SaleId");

                    b.HasIndex("UserRoleId");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("Notipet.Domain.OrderStatus", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("character varying(10)");

                    b.HasKey("Id");

                    b.ToTable("OrderStatus");

                    b.HasData(
                        new
                        {
                            Id = 0,
                            Name = "Created"
                        },
                        new
                        {
                            Id = 1,
                            Name = "Updated"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Completed"
                        },
                        new
                        {
                            Id = 3,
                            Name = "Cancelled"
                        });
                });

            modelBuilder.Entity("Notipet.Domain.Pet", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<bool>("Active")
                        .HasColumnType("boolean");

                    b.Property<DateOnly>("Birthdate")
                        .HasColumnType("date");

                    b.Property<DateTime>("Created")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("character varying(20)");

                    b.Property<int>("PetType")
                        .HasColumnType("integer")
                        .HasColumnName("PetTypeId");

                    b.Property<string>("PictureUrl")
                        .IsRequired()
                        .HasMaxLength(2048)
                        .HasColumnType("character varying(2048)");

                    b.Property<DateTime?>("Updated")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("UserRoleId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("PetType");

                    b.HasIndex("UserRoleId");

                    b.ToTable("Pets");
                });

            modelBuilder.Entity("Notipet.Domain.PetType", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("character varying(10)");

                    b.HasKey("Id");

                    b.ToTable("PetType");

                    b.HasData(
                        new
                        {
                            Id = 0,
                            Name = "Dog"
                        },
                        new
                        {
                            Id = 1,
                            Name = "Cat"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Bunny"
                        },
                        new
                        {
                            Id = 3,
                            Name = "Other"
                        });
                });

            modelBuilder.Entity("Notipet.Domain.Rating", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<int>("AssetsServicesId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("Created")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("RatingNumber")
                        .HasColumnType("integer");

                    b.Property<Guid>("UserRoleId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("AssetsServicesId");

                    b.HasIndex("UserRoleId");

                    b.ToTable("Ratings");
                });

            modelBuilder.Entity("Notipet.Domain.Role", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("character varying(10)");

                    b.HasKey("Id");

                    b.ToTable("Role");

                    b.HasData(
                        new
                        {
                            Id = 0,
                            Name = "Client"
                        },
                        new
                        {
                            Id = 1,
                            Name = "Seller"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Admin"
                        });
                });

            modelBuilder.Entity("Notipet.Domain.Sale", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("Created")
                        .HasColumnType("timestamp with time zone");

                    b.Property<decimal>("Total")
                        .HasColumnType("numeric");

                    b.Property<DateTime?>("Updated")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.ToTable("Sales");
                });

            modelBuilder.Entity("Notipet.Domain.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Address1")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<string>("Address2")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasMaxLength(25)
                        .HasColumnType("character varying(25)");

                    b.Property<DateTime>("Created")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Document")
                        .IsRequired()
                        .HasMaxLength(16)
                        .HasColumnType("character varying(16)");

                    b.Property<int>("DocumentType")
                        .HasColumnType("integer")
                        .HasColumnName("DocumentTypeId");

                    b.Property<string>("Lastnames")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("character varying(20)");

                    b.Property<string>("Names")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("character varying(20)");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasMaxLength(15)
                        .HasColumnType("character varying(15)");

                    b.Property<string>("Province")
                        .IsRequired()
                        .HasMaxLength(25)
                        .HasColumnType("character varying(25)");

                    b.Property<DateTime?>("Updated")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.HasIndex("Document")
                        .IsUnique();

                    b.HasIndex("DocumentType");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Notipet.Domain.UserRole", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<bool>("Active")
                        .HasColumnType("boolean");

                    b.Property<Guid>("BusinessId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("Created")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(320)
                        .HasColumnType("character varying(320)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("character varying(128)");

                    b.Property<int>("Role")
                        .HasColumnType("integer")
                        .HasColumnName("RoleId");

                    b.Property<DateTime?>("Updated")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("character varying(30)");

                    b.HasKey("Id");

                    b.HasIndex("BusinessId");

                    b.HasIndex("Role");

                    b.HasIndex("UserId");

                    b.ToTable("UserRoles");
                });

            modelBuilder.Entity("Notipet.Domain.Vendor", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("character varying(10)");

                    b.HasKey("Id");

                    b.ToTable("Vendor");

                    b.HasData(
                        new
                        {
                            Id = 0,
                            Name = "RoyalCanin"
                        },
                        new
                        {
                            Id = 1,
                            Name = "Purina"
                        });
                });

            modelBuilder.Entity("Notipet.Domain.Appointment", b =>
                {
                    b.HasOne("Notipet.Domain.AppointmentStatus", null)
                        .WithMany("Appointments")
                        .HasForeignKey("AppointmentStatus")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Notipet.Domain.AssetsServices", "AssetsServices")
                        .WithMany()
                        .HasForeignKey("AssetsServicesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Notipet.Domain.Pet", "Pet")
                        .WithMany()
                        .HasForeignKey("PetId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AssetsServices");

                    b.Navigation("Pet");
                });

            modelBuilder.Entity("Notipet.Domain.AssetsServices", b =>
                {
                    b.HasOne("Notipet.Domain.AssetsServiceType", null)
                        .WithMany("AssetsServices")
                        .HasForeignKey("AssetsServiceType")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Notipet.Domain.Business", "Business")
                        .WithMany()
                        .HasForeignKey("BusinessId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Notipet.Domain.Vendor", null)
                        .WithMany("AssetsServices")
                        .HasForeignKey("Vendor")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Business");
                });

            modelBuilder.Entity("Notipet.Domain.DigitalVaccine", b =>
                {
                    b.HasOne("Notipet.Domain.Business", "Business")
                        .WithMany()
                        .HasForeignKey("BusinessId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Notipet.Domain.Pet", "Pet")
                        .WithMany()
                        .HasForeignKey("PetId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Notipet.Domain.UserRole", "UserRole")
                        .WithMany()
                        .HasForeignKey("UserRoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Business");

                    b.Navigation("Pet");

                    b.Navigation("UserRole");
                });

            modelBuilder.Entity("Notipet.Domain.Order", b =>
                {
                    b.HasOne("Notipet.Domain.AssetsServices", "AssetsAssetsServices")
                        .WithMany()
                        .HasForeignKey("AssetsAssetsServicesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Notipet.Domain.OrderStatus", null)
                        .WithMany("Orders")
                        .HasForeignKey("OrderStatus")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Notipet.Domain.Sale", "Sale")
                        .WithMany()
                        .HasForeignKey("SaleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Notipet.Domain.UserRole", "UserRole")
                        .WithMany()
                        .HasForeignKey("UserRoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AssetsAssetsServices");

                    b.Navigation("Sale");

                    b.Navigation("UserRole");
                });

            modelBuilder.Entity("Notipet.Domain.Pet", b =>
                {
                    b.HasOne("Notipet.Domain.PetType", null)
                        .WithMany("Pets")
                        .HasForeignKey("PetType")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Notipet.Domain.UserRole", "UserRole")
                        .WithMany()
                        .HasForeignKey("UserRoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("UserRole");
                });

            modelBuilder.Entity("Notipet.Domain.Rating", b =>
                {
                    b.HasOne("Notipet.Domain.AssetsServices", "AssetsServices")
                        .WithMany()
                        .HasForeignKey("AssetsServicesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Notipet.Domain.UserRole", "UserRole")
                        .WithMany()
                        .HasForeignKey("UserRoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AssetsServices");

                    b.Navigation("UserRole");
                });

            modelBuilder.Entity("Notipet.Domain.User", b =>
                {
                    b.HasOne("Notipet.Domain.DocumentType", null)
                        .WithMany("Users")
                        .HasForeignKey("DocumentType")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Notipet.Domain.UserRole", b =>
                {
                    b.HasOne("Notipet.Domain.Business", "Business")
                        .WithMany()
                        .HasForeignKey("BusinessId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Notipet.Domain.Role", null)
                        .WithMany("UserRoles")
                        .HasForeignKey("Role")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Notipet.Domain.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Business");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Notipet.Domain.AppointmentStatus", b =>
                {
                    b.Navigation("Appointments");
                });

            modelBuilder.Entity("Notipet.Domain.AssetsServiceType", b =>
                {
                    b.Navigation("AssetsServices");
                });

            modelBuilder.Entity("Notipet.Domain.DocumentType", b =>
                {
                    b.Navigation("Users");
                });

            modelBuilder.Entity("Notipet.Domain.OrderStatus", b =>
                {
                    b.Navigation("Orders");
                });

            modelBuilder.Entity("Notipet.Domain.PetType", b =>
                {
                    b.Navigation("Pets");
                });

            modelBuilder.Entity("Notipet.Domain.Role", b =>
                {
                    b.Navigation("UserRoles");
                });

            modelBuilder.Entity("Notipet.Domain.Vendor", b =>
                {
                    b.Navigation("AssetsServices");
                });
#pragma warning restore 612, 618
        }
    }
}
