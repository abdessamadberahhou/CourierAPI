﻿// <auto-generated />
using System;
using CourierApi.Models.file;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace CourierApi.Migrations.Files
{
    [DbContext(typeof(FilesContext))]
    partial class FilesContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.16")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("CourierApi.Models.file.File", b =>
                {
                    b.Property<Guid>("IdFile")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("ID_FILE");

                    b.Property<byte[]>("File1")
                        .IsRequired()
                        .HasColumnType("varbinary(max)")
                        .HasColumnName("FILE");

                    b.Property<string>("FileExtention")
                        .HasMaxLength(10)
                        .IsUnicode(false)
                        .HasColumnType("varchar(10)");

                    b.Property<string>("FileName")
                        .HasMaxLength(200)
                        .IsUnicode(false)
                        .HasColumnType("varchar(200)");

                    b.Property<Guid>("IdCourrier")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("ID_COURRIER");

                    b.HasKey("IdFile")
                        .HasName("PK__image__2C7C2D9405D45317");

                    b.ToTable("files");
                });
#pragma warning restore 612, 618
        }
    }
}
