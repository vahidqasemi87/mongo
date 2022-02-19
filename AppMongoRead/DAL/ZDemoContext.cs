using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace AppMongoRead.DAL
{
    public partial class ZDemoContext : DbContext
    {
        public ZDemoContext()
        {
        }

        public ZDemoContext(DbContextOptions<ZDemoContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Final> Finals { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("server=.;database=ZDemo;uid=sa;pwd=Ss1234!@#$");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Final>(entity =>
            {
                entity.ToTable("Final");

                entity.Property(e => e.FirstName)
                    .HasMaxLength(255)
                    .HasColumnName("FIRST_NAME");

                entity.Property(e => e.LastName)
                    .HasMaxLength(255)
                    .HasColumnName("LAST_NAME");

                entity.Property(e => e.Mobile)
                    .HasMaxLength(255)
                    .HasColumnName("MOBILE");

                entity.Property(e => e.NationalCode)
                    .HasMaxLength(255)
                    .HasColumnName("NATIONAL_CODE");
            });
            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
