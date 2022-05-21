using AppMongoRead.Models;
using Microsoft.EntityFrameworkCore;
using System.IO;

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
		public virtual DbSet<UserExport> UserExports { get; set; }
		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string bin = Directory.GetCurrentDirectory();
            string fileName = "/dal.txt";
            string[] stringArray = new string[3];
            if (File.Exists(bin + fileName))
            {
                stringArray = File.ReadAllLines(bin + fileName);
                stringArray = File.ReadAllLines(bin + fileName);

                string connectionString = stringArray[0];
                //string databaseName = stringArray[1];
                //string collectionName = stringArray[2];
                System.Console.WriteLine("Read config sql successful");
                if (!optionsBuilder.IsConfigured)
                {
                    optionsBuilder.UseSqlServer(connectionString);
                }
            }
            else
            {
                System.Console.WriteLine("Read config sql not successful");
                if (!optionsBuilder.IsConfigured)
                {
                    optionsBuilder.UseSqlServer("server=.;database=ZDemo;uid=sa;pwd=Ss1234!@#$");
                }
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
