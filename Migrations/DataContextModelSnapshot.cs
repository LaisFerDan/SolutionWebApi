﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WebApi.Data;

#nullable disable

namespace WebApi.Migrations
{
    [DbContext(typeof(DataContext))]
    partial class DataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("ChessPlayer", b =>
            {
                b.Property<int>("id")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("int");

                SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"), 1L, 1);

                b.Property<int>("best_rating")
                    .HasColumnType("int");

                b.Property<string>("country")
                    .HasColumnType("varchar(3)");

                b.Property<int>("followers")
                    .HasColumnType("int");

                b.Property<DateTime>("joined")
                    .HasColumnType("date");

                b.Property<DateTime>("last_online")
                    .HasColumnType("date");

                b.Property<int>("loses")
                   .HasColumnType("int");

                b.Property<string>("name")
                    .HasColumnType("varchar(100)");

                b.Property<int>("number_of_games")
                    .HasColumnType("int");

                b.Property<string>("url")
                    .HasColumnType("varchar(70)");

                b.Property<string>("username")
                    .HasColumnType("varchar(40)");

                b.Property<int>("wins")
                    .HasColumnType("int");

                b.HasKey("id");

                b.ToTable("ChessPlayers");
            });
#pragma warning restore 612, 618
        }
    }
}
