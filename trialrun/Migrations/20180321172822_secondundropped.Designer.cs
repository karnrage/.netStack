﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;
using trialrun.Models;

namespace trialrun.Migrations
{
    [DbContext(typeof(TrialrunContext))]
    [Migration("20180321172822_secondundropped")]
    partial class secondundropped
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn)
                .HasAnnotation("ProductVersion", "2.0.0-rtm-26452");

            modelBuilder.Entity("trialrun.Models.Product", b =>
                {
                    b.Property<int>("ProductID")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("UserID");

                    b.Property<int>("bid");

                    b.Property<DateTime>("createdAt");

                    b.Property<string>("description")
                        .IsRequired();

                    b.Property<DateTime>("endDate");

                    b.Property<string>("name");

                    b.Property<DateTime>("updatedAt");

                    b.HasKey("ProductID");

                    b.HasIndex("UserID");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("trialrun.Models.User", b =>
                {
                    b.Property<int>("UserID")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("createdAt");

                    b.Property<string>("firstName");

                    b.Property<string>("lastName");

                    b.Property<string>("password");

                    b.Property<DateTime>("updatedAt");

                    b.Property<string>("username");

                    b.HasKey("UserID");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("trialrun.Models.Product", b =>
                {
                    b.HasOne("trialrun.Models.User", "user")
                        .WithMany("products")
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
