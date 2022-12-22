﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using RandomFacts.Models;

#nullable disable

namespace RandomFacts.Migrations
{
    [DbContext(typeof(MyContext))]
    [Migration("20221222050054_FourthMigration")]
    partial class FourthMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("RandomFacts.Models.Association", b =>
                {
                    b.Property<int>("AssociationId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("FactId")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("AssociationId");

                    b.HasIndex("FactId");

                    b.HasIndex("UserId");

                    b.ToTable("Associations");
                });

            modelBuilder.Entity("RandomFacts.Models.FactModel", b =>
                {
                    b.Property<int>("FactId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<int?>("UserId")
                        .HasColumnType("int");

                    b.Property<string>("fact")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("FactId");

                    b.HasIndex("UserId");

                    b.ToTable("Facts");
                });

            modelBuilder.Entity("RandomFacts.Models.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("UserId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("RandomFacts.Models.Association", b =>
                {
                    b.HasOne("RandomFacts.Models.FactModel", "Fact")
                        .WithMany("Associations")
                        .HasForeignKey("FactId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("RandomFacts.Models.User", "User")
                        .WithMany("Associations")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Fact");

                    b.Navigation("User");
                });

            modelBuilder.Entity("RandomFacts.Models.FactModel", b =>
                {
                    b.HasOne("RandomFacts.Models.User", "User")
                        .WithMany("Facts")
                        .HasForeignKey("UserId");

                    b.Navigation("User");
                });

            modelBuilder.Entity("RandomFacts.Models.FactModel", b =>
                {
                    b.Navigation("Associations");
                });

            modelBuilder.Entity("RandomFacts.Models.User", b =>
                {
                    b.Navigation("Associations");

                    b.Navigation("Facts");
                });
#pragma warning restore 612, 618
        }
    }
}
