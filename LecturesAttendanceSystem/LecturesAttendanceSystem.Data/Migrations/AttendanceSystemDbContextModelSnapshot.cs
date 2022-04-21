﻿// <auto-generated />
using System;
using LecturesAttendanceSystem.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace LecturesAttendanceSystem.Data.Migrations
{
    [DbContext(typeof(AttendanceSystemDbContext))]
    partial class AttendanceSystemDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.15")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("LecturesAttendanceSystem.Data.Entities.Lesson", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("LessonType")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<DateTime>("ScheduledOn")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("Name");

                    b.ToTable("Lessons");
                });

            modelBuilder.Entity("LecturesAttendanceSystem.Data.Entities.LessonParticipant", b =>
                {
                    b.Property<long>("LessonId")
                        .HasColumnType("bigint");

                    b.Property<long>("ParticipantId")
                        .HasColumnType("bigint");

                    b.Property<bool>("Present")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false);

                    b.HasKey("LessonId", "ParticipantId");

                    b.HasIndex("ParticipantId");

                    b.ToTable("LessonParticipants");
                });

            modelBuilder.Entity("LecturesAttendanceSystem.Data.Entities.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Roles");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "administrator"
                        },
                        new
                        {
                            Id = 2,
                            Name = "student"
                        },
                        new
                        {
                            Id = 3,
                            Name = "teacher"
                        });
                });

            modelBuilder.Entity("LecturesAttendanceSystem.Data.Entities.User", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("HashingSalt")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("RegisteredOn")
                        .HasColumnType("datetime2");

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.HasIndex("RoleId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("LecturesAttendanceSystem.Data.Entities.LessonParticipant", b =>
                {
                    b.HasOne("LecturesAttendanceSystem.Data.Entities.Lesson", "Lesson")
                        .WithMany("LessonParticipants")
                        .HasForeignKey("LessonId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("LecturesAttendanceSystem.Data.Entities.User", "Participant")
                        .WithMany("LessonParticipants")
                        .HasForeignKey("ParticipantId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Lesson");

                    b.Navigation("Participant");
                });

            modelBuilder.Entity("LecturesAttendanceSystem.Data.Entities.User", b =>
                {
                    b.HasOne("LecturesAttendanceSystem.Data.Entities.Role", "Role")
                        .WithMany("Users")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");
                });

            modelBuilder.Entity("LecturesAttendanceSystem.Data.Entities.Lesson", b =>
                {
                    b.Navigation("LessonParticipants");
                });

            modelBuilder.Entity("LecturesAttendanceSystem.Data.Entities.Role", b =>
                {
                    b.Navigation("Users");
                });

            modelBuilder.Entity("LecturesAttendanceSystem.Data.Entities.User", b =>
                {
                    b.Navigation("LessonParticipants");
                });
#pragma warning restore 612, 618
        }
    }
}
