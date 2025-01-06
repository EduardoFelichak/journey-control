﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using journey_control.Infra.Context;

#nullable disable

namespace journey_control.Migrations
{
    [DbContext(typeof(ApplicationDBContext))]
    [Migration("20250102133254_UpdateEntriesTable")]
    partial class UpdateEntriesTable
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("journey_control.Models.Entrie", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("DateEntrie")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("Duration")
                        .HasColumnType("integer");

                    b.Property<bool>("IsInProgress")
                        .HasColumnType("boolean");

                    b.Property<string>("TaskId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("TaskUserId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("TaskId", "TaskUserId");

                    b.ToTable("entries");
                });

            modelBuilder.Entity("journey_control.Models.Project", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("projects");
                });

            modelBuilder.Entity("journey_control.Models.Task", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("DueDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("Project")
                        .HasColumnType("integer");

                    b.Property<int>("Size")
                        .HasColumnType("integer");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("VersionId")
                        .HasColumnType("integer");

                    b.Property<int>("VersionProjectId")
                        .HasColumnType("integer");

                    b.HasKey("Id", "UserId");

                    b.HasIndex("VersionId", "VersionProjectId");

                    b.ToTable("tasks");
                });

            modelBuilder.Entity("journey_control.Models.Version", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("integer");

                    b.Property<int>("ProjectId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("DueDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id", "ProjectId");

                    b.HasIndex("ProjectId");

                    b.ToTable("versions");
                });

            modelBuilder.Entity("journey_control.Models.Entrie", b =>
                {
                    b.HasOne("journey_control.Models.Task", "Task")
                        .WithMany("Entries")
                        .HasForeignKey("TaskId", "TaskUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Task");
                });

            modelBuilder.Entity("journey_control.Models.Task", b =>
                {
                    b.HasOne("journey_control.Models.Version", "Version")
                        .WithMany("Tasks")
                        .HasForeignKey("VersionId", "VersionProjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Version");
                });

            modelBuilder.Entity("journey_control.Models.Version", b =>
                {
                    b.HasOne("journey_control.Models.Project", "Project")
                        .WithMany("Versions")
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Project");
                });

            modelBuilder.Entity("journey_control.Models.Project", b =>
                {
                    b.Navigation("Versions");
                });

            modelBuilder.Entity("journey_control.Models.Task", b =>
                {
                    b.Navigation("Entries");
                });

            modelBuilder.Entity("journey_control.Models.Version", b =>
                {
                    b.Navigation("Tasks");
                });
#pragma warning restore 612, 618
        }
    }
}
