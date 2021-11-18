using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Region.Data.Models;

namespace Region.Data.Contexts
{
    public partial class PhoneNumbersDbContext : DbContext
    {
        public PhoneNumbersDbContext()
        {
        }

        public PhoneNumbersDbContext(DbContextOptions<PhoneNumbersDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<PhoneNumbers> PhoneNumbers { get; set; }
        public virtual DbSet<Regions> Regions { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                IConfigurationRoot configuration = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json")
                    .Build();
                var connectionString = configuration.GetConnectionString("phoneDb");
                optionsBuilder.UseSqlServer(connectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PhoneNumbers>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("Phone_Numbers");

                entity.Property(e => e.LineNumber)
                    .IsRequired()
                    .HasColumnName("Line_Number");

                entity.Property(e => e.PhoneNumber)
                    .IsRequired()
                    .HasColumnName("Phone_Number");

                entity.Property(e => e.RegionId).HasColumnName("Region_ID");
            });

            modelBuilder.Entity<Regions>(entity =>
            {
                entity.HasNoKey();

                entity.HasIndex(e => e.Id)
                    .HasName("UQ__Regions__3214EC2662C3B529")
                    .IsUnique();

                entity.HasIndex(e => e.RegionCode)
                    .HasName("UQ__Regions__63C1AD9DC2081977")
                    .IsUnique();

                entity.HasIndex(e => e.RegionName)
                    .HasName("UQ__Regions__2C94D3D80AF2C28F")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.RegionCode)
                    .IsRequired()
                    .HasColumnName("Region_Code")
                    .HasMaxLength(300);

                entity.Property(e => e.RegionName)
                    .IsRequired()
                    .HasColumnName("Region_Name")
                    .HasMaxLength(10)
                    .IsFixedLength();
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
