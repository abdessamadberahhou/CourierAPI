using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace CourierApi.Models.courier
{
    public partial class CouriersContext : DbContext
    {
        public CouriersContext()
        {
        }

        public CouriersContext(DbContextOptions<CouriersContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Courrier> Courriers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("your_string_connection");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Courrier>(entity =>
            {
                entity.ToTable("courrier");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("ID");

                entity.Property(e => e.CourrierArchiver).HasColumnName("COURRIER_ARCHIVER");

                entity.Property(e => e.CourrierFavoriser).HasColumnName("COURRIER_FAVORISER");

                entity.Property(e => e.CourrierUrgent).HasColumnName("COURRIER_URGENT");

                entity.Property(e => e.DateCourrier)
                    .HasColumnType("date")
                    .HasColumnName("DATE_COURRIER");

                entity.Property(e => e.DestinataireCourrier)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("DESTINATAIRE_COURRIER");

                entity.Property(e => e.ExpiditeurCourrier)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("EXPIDITEUR_COURRIER");

                entity.Property(e => e.IdUser).HasColumnName("ID_USER");

                entity.Property(e => e.ObjetCourrier)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("OBJET_COURRIER");

                entity.Property(e => e.TagsCourrier)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("TAGS_COURRIER");

                entity.Property(e => e.TypeCourrier)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("TYPE_COURRIER");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
