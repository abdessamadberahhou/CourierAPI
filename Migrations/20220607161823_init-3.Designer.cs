﻿// <auto-generated />
using System;
using CourierApi.Models.RefreshToken;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace CourierApi.Migrations.RefreshToken
{
    [DbContext(typeof(RefreshTokenContext))]
    [Migration("20220607161823_init-2")]
    partial class init2
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.16")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("CourierApi.Models.RefreshToken.RefreshToken", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("id");

                    b.Property<string>("RefreshToken1")
                        .HasMaxLength(1500)
                        .IsUnicode(false)
                        .HasColumnType("varchar(1500)")
                        .HasColumnName("RefreshToken");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("User_Id");

                    b.HasKey("Id");

                    b.ToTable("refresh_tokens");
                });
#pragma warning restore 612, 618
        }
    }
}
