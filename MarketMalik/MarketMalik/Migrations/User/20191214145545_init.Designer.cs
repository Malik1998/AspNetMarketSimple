﻿// <auto-generated />
using MarketMalik.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace MarketMalik.Migrations.User
{
    [DbContext(typeof(UserContext))]
    [Migration("20191214145545_init")]
    partial class init
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("MarketMalik.Models.User", b =>
                {
                    b.Property<string>("login")
                        .HasColumnType("nvarchar(450)");

                    b.Property<bool>("is_admin")
                        .HasColumnType("bit");

                    b.Property<string>("password")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("login");

                    b.ToTable("Users");
                });
#pragma warning restore 612, 618
        }
    }
}
