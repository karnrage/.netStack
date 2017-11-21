using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using weddingPlanner.Models;

namespace weddingPlanner.Migrations
{
    [DbContext(typeof(weddingPlannerContext))]
    [Migration("20171121013827_secondMigration")]
    partial class secondMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn)
                .HasAnnotation("ProductVersion", "1.1.2");

            modelBuilder.Entity("weddingPlanner.Models.guest", b =>
                {
                    b.Property<int>("guestId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("userID");

                    b.Property<int>("wedID");

                    b.HasKey("guestId");

                    b.HasIndex("userID");

                    b.HasIndex("wedID");

                    b.ToTable("guests");
                });

            modelBuilder.Entity("weddingPlanner.Models.user", b =>
                {
                    b.Property<int>("userID")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("createAt");

                    b.Property<string>("email");

                    b.Property<string>("firstName");

                    b.Property<string>("lastName");

                    b.Property<string>("password");

                    b.HasKey("userID");

                    b.ToTable("users");
                });

            modelBuilder.Entity("weddingPlanner.Models.wedding", b =>
                {
                    b.Property<int>("wedID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("address")
                        .IsRequired();

                    b.Property<DateTime>("createAt");

                    b.Property<string>("husband")
                        .IsRequired();

                    b.Property<int>("userId");

                    b.Property<DateTime>("wedDate");

                    b.Property<string>("wife")
                        .IsRequired();

                    b.HasKey("wedID");

                    b.HasIndex("userId");

                    b.ToTable("weddings");
                });

            modelBuilder.Entity("weddingPlanner.Models.guest", b =>
                {
                    b.HasOne("weddingPlanner.Models.user", "user")
                        .WithMany("guests")
                        .HasForeignKey("userID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("weddingPlanner.Models.wedding", "wed")
                        .WithMany("guests")
                        .HasForeignKey("wedID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("weddingPlanner.Models.wedding", b =>
                {
                    b.HasOne("weddingPlanner.Models.user", "user")
                        .WithMany("weddings")
                        .HasForeignKey("userId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
