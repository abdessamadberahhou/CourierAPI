using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace CourierApi.Models.file
{
    public partial class FilesContext : DbContext
    {
        public FilesContext()
        {
        }

        public FilesContext(DbContextOptions<FilesContext> options)
            : base(options)
        {
        }

        public virtual DbSet<File> Files { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=tcp:couriermanagerdbserver.database.windows.net,1433;Initial Catalog=couriermanager;User Id=abdessamad@couriermanagerdbserver;Password=Berahhou@2001");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<File>(entity =>
            {
                entity.HasKey(e => e.IdFile)
                    .HasName("PK__image__2C7C2D9405D45317");

                entity.ToTable("files");

                entity.Property(e => e.IdFile)
                    .ValueGeneratedNever()
                    .HasColumnName("ID_FILE");

                entity.Property(e => e.File1)
                    .IsRequired()
                    .HasColumnName("FILE");

                entity.Property(e => e.FileExtention)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.FileName)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.IdCourrier).HasColumnName("ID_COURRIER");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
