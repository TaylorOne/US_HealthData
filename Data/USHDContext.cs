using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using USHD.Models;

namespace USHD.Models
{
    public partial class USHDContext : DbContext
    {
        public USHDContext()
        {
        }

        public USHDContext(DbContextOptions<USHDContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Ahr> Ahr { get; set; }
        public virtual DbSet<Measures> Measures { get; set; }
        public virtual DbSet<Trends> Trends { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=USHD;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Ahr>(entity =>
            {
                entity.ToTable("AHR");

                entity.Property(e => e.AhrId).HasColumnName("ahrID");

                entity.Property(e => e.LowerCi)
                    .HasColumnName("Lower_ci")
                    .HasColumnType("decimal(18, 0)");

                entity.Property(e => e.Score).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.UpperCi)
                    .HasColumnName("Upper_ci")
                    .HasColumnType("decimal(18, 0)");

                entity.Property(e => e.Value).HasColumnType("decimal(18, 0)");
            });

            modelBuilder.Entity<Measures>(entity =>
            {
                entity.HasKey(e => e.MeasureId);

                entity.ToTable("measures");

                entity.Property(e => e.MeasureId).HasColumnName("MeasureID");

                entity.Property(e => e.Yr00)
                    .HasColumnName("yr00")
                    .HasColumnType("decimal(18, 0)");

                entity.Property(e => e.Yr01)
                    .HasColumnName("yr01")
                    .HasColumnType("decimal(18, 0)");

                entity.Property(e => e.Yr02)
                    .HasColumnName("yr02")
                    .HasColumnType("decimal(18, 0)");

                entity.Property(e => e.Yr03)
                    .HasColumnName("yr03")
                    .HasColumnType("decimal(18, 0)");

                entity.Property(e => e.Yr04)
                    .HasColumnName("yr04")
                    .HasColumnType("decimal(18, 0)");

                entity.Property(e => e.Yr05)
                    .HasColumnName("yr05")
                    .HasColumnType("decimal(18, 0)");

                entity.Property(e => e.Yr06)
                    .HasColumnName("yr06")
                    .HasColumnType("decimal(18, 0)");

                entity.Property(e => e.Yr07)
                    .HasColumnName("yr07")
                    .HasColumnType("decimal(18, 0)");

                entity.Property(e => e.Yr08)
                    .HasColumnName("yr08")
                    .HasColumnType("decimal(18, 0)");

                entity.Property(e => e.Yr09)
                    .HasColumnName("yr09")
                    .HasColumnType("decimal(18, 0)");

                entity.Property(e => e.Yr10)
                    .HasColumnName("yr10")
                    .HasColumnType("decimal(18, 0)");

                entity.Property(e => e.Yr11)
                    .HasColumnName("yr11")
                    .HasColumnType("decimal(18, 0)");

                entity.Property(e => e.Yr12)
                    .HasColumnName("yr12")
                    .HasColumnType("decimal(18, 0)");

                entity.Property(e => e.Yr13)
                    .HasColumnName("yr13")
                    .HasColumnType("decimal(18, 0)");

                entity.Property(e => e.Yr14)
                    .HasColumnName("yr14")
                    .HasColumnType("decimal(18, 0)");

                entity.Property(e => e.Yr15)
                    .HasColumnName("yr15")
                    .HasColumnType("decimal(18, 0)");

                entity.Property(e => e.Yr16)
                    .HasColumnName("yr16")
                    .HasColumnType("decimal(18, 0)");

                entity.Property(e => e.Yr17)
                    .HasColumnName("yr17")
                    .HasColumnType("decimal(18, 0)");
            });

            modelBuilder.Entity<Trends>(entity =>
            {
                entity.HasKey(e => e.TrendId);

                entity.ToTable("trends");

                entity.Property(e => e.TrendId).HasColumnName("trendID");

                entity.Property(e => e.Yr00).HasColumnName("yr00");

                entity.Property(e => e.Yr01).HasColumnName("yr01");

                entity.Property(e => e.Yr02).HasColumnName("yr02");

                entity.Property(e => e.Yr03).HasColumnName("yr03");

                entity.Property(e => e.Yr04).HasColumnName("yr04");

                entity.Property(e => e.Yr05).HasColumnName("yr05");

                entity.Property(e => e.Yr06).HasColumnName("yr06");

                entity.Property(e => e.Yr07).HasColumnName("yr07");

                entity.Property(e => e.Yr08).HasColumnName("yr08");

                entity.Property(e => e.Yr09).HasColumnName("yr09");

                entity.Property(e => e.Yr10).HasColumnName("yr10");

                entity.Property(e => e.Yr11).HasColumnName("yr11");

                entity.Property(e => e.Yr12).HasColumnName("yr12");

                entity.Property(e => e.Yr13).HasColumnName("yr13");

                entity.Property(e => e.Yr14).HasColumnName("yr14");

                entity.Property(e => e.Yr15).HasColumnName("yr15");

                entity.Property(e => e.Yr16).HasColumnName("yr16");

                entity.Property(e => e.Yr17).HasColumnName("yr17");
            });
        }
    }
}
