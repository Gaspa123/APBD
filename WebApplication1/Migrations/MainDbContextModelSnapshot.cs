﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WebApplication1.Models;

namespace WebApplication1.Migrations
{
    [DbContext(typeof(MainDbContext))]
    partial class MainDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.5")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("WebApplication1.Models.Doctor", b =>
                {
                    b.Property<int>("IdDoctor")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("IdDoctor");

                    b.ToTable("Doctor");

                    b.HasData(
                        new
                        {
                            IdDoctor = 1,
                            Email = "gmail1@gmail.com",
                            FirstName = "Piotr",
                            LastName = "A"
                        },
                        new
                        {
                            IdDoctor = 2,
                            Email = "gmail2@gmail.com",
                            FirstName = "Adam",
                            LastName = "B"
                        },
                        new
                        {
                            IdDoctor = 3,
                            Email = "gmail3@gmail.com",
                            FirstName = "Jan",
                            LastName = "C"
                        });
                });

            modelBuilder.Entity("WebApplication1.Models.Medicament", b =>
                {
                    b.Property<int>("IdMedicament")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("IdMedicament");

                    b.ToTable("Medicament");

                    b.HasData(
                        new
                        {
                            IdMedicament = 1,
                            Description = "Ból",
                            Name = "Apap",
                            Type = "paracetamol"
                        },
                        new
                        {
                            IdMedicament = 2,
                            Description = "Gorączka",
                            Name = "Ibuprom",
                            Type = "ibuprofen"
                        },
                        new
                        {
                            IdMedicament = 3,
                            Description = "Wysoka gorączka",
                            Name = "Ibuprom Max",
                            Type = "ibuprofen"
                        });
                });

            modelBuilder.Entity("WebApplication1.Models.Patient", b =>
                {
                    b.Property<int>("IdPatient")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("BirthDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("IdPatient");

                    b.ToTable("Patient");

                    b.HasData(
                        new
                        {
                            IdPatient = 1,
                            BirthDate = new DateTime(2021, 5, 15, 0, 0, 0, 0, DateTimeKind.Local),
                            FirstName = "Piotr",
                            LastName = "D"
                        },
                        new
                        {
                            IdPatient = 2,
                            BirthDate = new DateTime(2021, 5, 15, 0, 0, 0, 0, DateTimeKind.Local),
                            FirstName = "Kasia",
                            LastName = "E"
                        },
                        new
                        {
                            IdPatient = 3,
                            BirthDate = new DateTime(2021, 5, 15, 0, 0, 0, 0, DateTimeKind.Local),
                            FirstName = "Kacper",
                            LastName = "F"
                        });
                });

            modelBuilder.Entity("WebApplication1.Models.Prescription", b =>
                {
                    b.Property<int>("IdPrescription")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DueDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("IdDoctor")
                        .HasColumnType("int");

                    b.Property<int>("IdPatient")
                        .HasColumnType("int");

                    b.HasKey("IdPrescription");

                    b.HasIndex("IdDoctor");

                    b.HasIndex("IdPatient");

                    b.ToTable("Prescription");

                    b.HasData(
                        new
                        {
                            IdPrescription = 1,
                            Date = new DateTime(2021, 5, 15, 0, 0, 0, 0, DateTimeKind.Local),
                            DueDate = new DateTime(2021, 5, 25, 0, 0, 0, 0, DateTimeKind.Local),
                            IdDoctor = 1,
                            IdPatient = 1
                        },
                        new
                        {
                            IdPrescription = 2,
                            Date = new DateTime(2021, 5, 15, 0, 0, 0, 0, DateTimeKind.Local),
                            DueDate = new DateTime(2021, 6, 4, 0, 0, 0, 0, DateTimeKind.Local),
                            IdDoctor = 2,
                            IdPatient = 2
                        },
                        new
                        {
                            IdPrescription = 3,
                            Date = new DateTime(2021, 5, 15, 0, 0, 0, 0, DateTimeKind.Local),
                            DueDate = new DateTime(2021, 6, 14, 0, 0, 0, 0, DateTimeKind.Local),
                            IdDoctor = 3,
                            IdPatient = 3
                        });
                });

            modelBuilder.Entity("WebApplication1.Models.Prescription_Medicament", b =>
                {
                    b.Property<int>("IdPrescription")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Details")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int?>("Dose")
                        .HasColumnType("int");

                    b.Property<int>("IdMedicament")
                        .HasColumnType("int");

                    b.Property<int?>("MedicamentIdMedicament")
                        .HasColumnType("int");

                    b.Property<int?>("PrescriptionIdPrescription")
                        .HasColumnType("int");

                    b.HasKey("IdPrescription");

                    b.HasIndex("MedicamentIdMedicament");

                    b.HasIndex("PrescriptionIdPrescription");

                    b.ToTable("Prescription_Medicament");

                    b.HasData(
                        new
                        {
                            IdPrescription = 1,
                            Details = "details1",
                            Dose = 2,
                            IdMedicament = 1
                        },
                        new
                        {
                            IdPrescription = 2,
                            Details = "details2",
                            IdMedicament = 2
                        },
                        new
                        {
                            IdPrescription = 3,
                            Details = "details3",
                            Dose = 100,
                            IdMedicament = 3
                        });
                });

            modelBuilder.Entity("WebApplication1.Models.User", b =>
                {
                    b.Property<string>("Login")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("RefreshToken")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<DateTime?>("RefreshTokenExp")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("datetime2");

                    b.Property<string>("Salt")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Login");

                    b.ToTable("User");

                    b.HasData(
                        new
                        {
                            Login = "Piotr",
                            Password = "6sYiYlWogqjuYyLpBJDnIxFXZcFyJZT5+a0wNDPo9pI=",
                            RefreshToken = "didiidieieicinflcfikmejckjeklalkjmikjfnkohnjuijohnujdasdsa",
                            RefreshTokenExp = new DateTime(2021, 5, 15, 1, 0, 0, 0, DateTimeKind.Local),
                            Salt = "t/B16TuEHBonmTJythUc9w=="
                        },
                        new
                        {
                            Login = "C#",
                            Password = "+k9RpdH0GbfC2DC3kfKl/SS2JRhMlbqZkvRXKOWzY0I=",
                            RefreshToken = "aaaaaaaaaaaaaaaaaaaaaaaaabbbbbbbbbbbbbbbbbbbbbbccccccccccccccde",
                            RefreshTokenExp = new DateTime(2021, 5, 15, 1, 0, 0, 0, DateTimeKind.Local),
                            Salt = "qiWKIJPJ0JAzagrgJ6WSag=="
                        });
                });

            modelBuilder.Entity("WebApplication1.Models.Prescription", b =>
                {
                    b.HasOne("WebApplication1.Models.Doctor", "Doctor")
                        .WithMany("Prescriptions")
                        .HasForeignKey("IdDoctor")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WebApplication1.Models.Patient", "Patient")
                        .WithMany("Prescriptions")
                        .HasForeignKey("IdPatient")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Doctor");

                    b.Navigation("Patient");
                });

            modelBuilder.Entity("WebApplication1.Models.Prescription_Medicament", b =>
                {
                    b.HasOne("WebApplication1.Models.Medicament", "Medicament")
                        .WithMany("Prescription_Medicaments")
                        .HasForeignKey("MedicamentIdMedicament");

                    b.HasOne("WebApplication1.Models.Prescription", "Prescription")
                        .WithMany("Prescription_Medicaments")
                        .HasForeignKey("PrescriptionIdPrescription");

                    b.Navigation("Medicament");

                    b.Navigation("Prescription");
                });

            modelBuilder.Entity("WebApplication1.Models.Doctor", b =>
                {
                    b.Navigation("Prescriptions");
                });

            modelBuilder.Entity("WebApplication1.Models.Medicament", b =>
                {
                    b.Navigation("Prescription_Medicaments");
                });

            modelBuilder.Entity("WebApplication1.Models.Patient", b =>
                {
                    b.Navigation("Prescriptions");
                });

            modelBuilder.Entity("WebApplication1.Models.Prescription", b =>
                {
                    b.Navigation("Prescription_Medicaments");
                });
#pragma warning restore 612, 618
        }
    }
}
