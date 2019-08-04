using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ThermoTracker.Service.Models
{
    public partial class ThermoTrackerDbContext : DbContext
    {
        public ThermoTrackerDbContext()
        {
        }

        public ThermoTrackerDbContext(DbContextOptions<ThermoTrackerDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<ClientIpAddresses> ClientIpAddresses { get; set; }
        public virtual DbSet<Devices> Devices { get; set; }
        public virtual DbSet<TemperatureRecords> TemperatureRecords { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Data Source = 127.0.0.1;Initial Catalog = ThermoTrackerDb;Persist Security Info=True;User ID=sa;Password=Pass@123");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.6-servicing-10079");

            modelBuilder.Entity<ClientIpAddresses>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Address)
                    .IsRequired()
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.FirstSeen)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.LastSeen)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");
            });

            modelBuilder.Entity<Devices>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.UserAgent)
                    .HasColumnType("text")
                    .HasDefaultValueSql("('')");

                entity.HasOne(d => d.Ip)
                    .WithMany(p => p.Devices)
                    .HasForeignKey(d => d.IpId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("IpFK");
            });

            modelBuilder.Entity<TemperatureRecords>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.DeviceId).HasDefaultValueSql("('-1')");

                entity.Property(e => e.Temperature).HasColumnType("decimal(5, 2)");

                entity.Property(e => e.Timestamp)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.Device)
                    .WithMany(p => p.TemperatureRecords)
                    .HasForeignKey(d => d.DeviceId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("DeviceFK");
            });
        }
    }
}
