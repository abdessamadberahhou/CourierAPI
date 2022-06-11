using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace CourierApi.Models.Users
{
    public partial class UsersContext : DbContext
    {
        public UsersContext()
        {
        }

        public UsersContext(DbContextOptions<UsersContext> options)
            : base(options)
        {
        }

        public virtual DbSet<User> Users { get; set; }

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

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("users");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.Avatar).HasColumnName("AVATAR");

                entity.Property(e => e.IsAdmin).HasColumnName("IS_ADMIN");
                entity.Property(e => e.is_accepted).HasColumnName("is_accepted");

                entity.Property(e => e.BirthDay)
                    .HasMaxLength(12)
                    .IsUnicode(false)
                    .HasColumnName("birth_day");

                entity.Property(e => e.Cin)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("cin");

                entity.Property(e => e.Email)
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasColumnName("email");

                entity.Property(e => e.FirstName)
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasColumnName("first_name");

                entity.Property(e => e.LastName)
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasColumnName("last_name");

                entity.Property(e => e.NumTele)
                    .HasMaxLength(14)
                    .IsUnicode(false)
                    .HasColumnName("NUM_TELE");

                entity.Property(e => e.Password)
                    .HasMaxLength(1500)
                    .IsUnicode(false)
                    .HasColumnName("password");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
