﻿// <auto-generated />
using System;
using CourierApi.Models.courier;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace CourierApi.Migrations.Couriers
{
    [DbContext(typeof(CouriersContext))]
    partial class CouriersContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.16")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("CourierApi.Models.courier.Courrier", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("ID");

                    b.Property<int?>("CourrierArchiver")
                        .HasColumnType("int")
                        .HasColumnName("COURRIER_ARCHIVER");

                    b.Property<int?>("CourrierFavoriser")
                        .HasColumnType("int")
                        .HasColumnName("COURRIER_FAVORISER");

                    b.Property<int?>("CourrierUrgent")
                        .HasColumnType("int")
                        .HasColumnName("COURRIER_URGENT");

                    b.Property<DateTime?>("DateCourrier")
                        .HasColumnType("date")
                        .HasColumnName("DATE_COURRIER");

                    b.Property<string>("DestinataireCourrier")
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("varchar(100)")
                        .HasColumnName("DESTINATAIRE_COURRIER");

                    b.Property<string>("ExpiditeurCourrier")
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("varchar(100)")
                        .HasColumnName("EXPIDITEUR_COURRIER");

                    b.Property<Guid?>("IdUser")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("ID_USER");

                    b.Property<string>("ObjetCourrier")
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("varchar(100)")
                        .HasColumnName("OBJET_COURRIER");

                    b.Property<string>("TagsCourrier")
                        .HasMaxLength(200)
                        .IsUnicode(false)
                        .HasColumnType("varchar(200)")
                        .HasColumnName("TAGS_COURRIER");

                    b.Property<string>("TypeCourrier")
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("varchar(100)")
                        .HasColumnName("TYPE_COURRIER");

                    b.HasKey("Id");

                    b.ToTable("courrier");
                });
#pragma warning restore 612, 618
        }
    }
}
