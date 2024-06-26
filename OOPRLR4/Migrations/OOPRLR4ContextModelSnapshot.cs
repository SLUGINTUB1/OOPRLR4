﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using OOPRLR4.Data;

#nullable disable

namespace OOPRLR4.Migrations
{
    [DbContext(typeof(OOPRLR4Context))]
    partial class OOPRLR4ContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("ApplicantFaculty", b =>
                {
                    b.Property<int>("ApplicantsApplicantId")
                        .HasColumnType("int");

                    b.Property<int>("FacultiesFacultyId")
                        .HasColumnType("int");

                    b.HasKey("ApplicantsApplicantId", "FacultiesFacultyId");

                    b.HasIndex("FacultiesFacultyId");

                    b.ToTable("ApplicantFaculty");
                });

            modelBuilder.Entity("FacultyTeacher", b =>
                {
                    b.Property<int>("FacultiesFacultyId")
                        .HasColumnType("int");

                    b.Property<int>("TeachersTeacherId")
                        .HasColumnType("int");

                    b.HasKey("FacultiesFacultyId", "TeachersTeacherId");

                    b.HasIndex("TeachersTeacherId");

                    b.ToTable("FacultyTeacher");
                });

            modelBuilder.Entity("OOPRLR4.Models.Applicant", b =>
                {
                    b.Property<int>("ApplicantId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ApplicantId"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ExamId")
                        .HasColumnType("int");

                    b.Property<string>("Gender")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ApplicantId");

                    b.HasIndex("ExamId");

                    b.ToTable("Applicant");
                });

            modelBuilder.Entity("OOPRLR4.Models.Exam", b =>
                {
                    b.Property<int>("ExamId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ExamId"));

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.HasKey("ExamId");

                    b.ToTable("Exam");
                });

            modelBuilder.Entity("OOPRLR4.Models.Faculty", b =>
                {
                    b.Property<int>("FacultyId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("FacultyId"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("NumberOfStudents")
                        .HasColumnType("int");

                    b.HasKey("FacultyId");

                    b.ToTable("Faculty");
                });

            modelBuilder.Entity("OOPRLR4.Models.Mark", b =>
                {
                    b.Property<int>("MarkId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MarkId"));

                    b.Property<bool>("Evaluated")
                        .HasColumnType("bit");

                    b.Property<int>("ExamId")
                        .HasColumnType("int");

                    b.Property<int>("SubjectIdent")
                        .HasColumnType("int");

                    b.Property<int>("Value")
                        .HasColumnType("int");

                    b.HasKey("MarkId");

                    b.HasIndex("ExamId");

                    b.ToTable("Mark");
                });

            modelBuilder.Entity("OOPRLR4.Models.Teacher", b =>
                {
                    b.Property<int>("TeacherId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("TeacherId"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Gender")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("SubjectIdent")
                        .HasColumnType("int");

                    b.HasKey("TeacherId");

                    b.ToTable("Teacher");
                });

            modelBuilder.Entity("ApplicantFaculty", b =>
                {
                    b.HasOne("OOPRLR4.Models.Applicant", null)
                        .WithMany()
                        .HasForeignKey("ApplicantsApplicantId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("OOPRLR4.Models.Faculty", null)
                        .WithMany()
                        .HasForeignKey("FacultiesFacultyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("FacultyTeacher", b =>
                {
                    b.HasOne("OOPRLR4.Models.Faculty", null)
                        .WithMany()
                        .HasForeignKey("FacultiesFacultyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("OOPRLR4.Models.Teacher", null)
                        .WithMany()
                        .HasForeignKey("TeachersTeacherId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("OOPRLR4.Models.Applicant", b =>
                {
                    b.HasOne("OOPRLR4.Models.Exam", "Exam")
                        .WithMany()
                        .HasForeignKey("ExamId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Exam");
                });

            modelBuilder.Entity("OOPRLR4.Models.Mark", b =>
                {
                    b.HasOne("OOPRLR4.Models.Exam", "Exam")
                        .WithMany("Subjects")
                        .HasForeignKey("ExamId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Exam");
                });

            modelBuilder.Entity("OOPRLR4.Models.Exam", b =>
                {
                    b.Navigation("Subjects");
                });
#pragma warning restore 612, 618
        }
    }
}
