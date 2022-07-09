using Domain.Entities;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class ChessTournamentDbContext : DbContext 
    {
        public ChessTournamentDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<User>? Users { get; set; }
        public DbSet<Tournament>? Tournaments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.FirstName)
                      .IsRequired()
                      .HasMaxLength(20);
                entity.Property(e => e.LastName)
                      .IsRequired()
                      .HasMaxLength(20);
                entity.Property(e => e.Email)
                      .IsRequired()
                      .HasMaxLength(50);
                entity.HasIndex(e => e.Email)
                      .IsUnique()
                      .IncludeProperties(e => e.PasswordHash)
                      .IncludeProperties(e => e.RoleName)
                      .IncludeProperties(e => e.Id);
                entity.Property(e => e.RoleName)
                      .IsRequired();
                entity.HasCheckConstraint("ck_users_rolename", $"[{nameof(User.RoleName)}] IN ('Arbiter', 'Player')");
                entity.Property(e => e.Club)
                      .HasMaxLength(50);
                entity.Property(e => e.City)
                      .HasMaxLength(50);
                entity.HasCheckConstraint("ck_users_birthdate", $"[{nameof(User.Birthdate)}] < GETDATE()");
                entity.Property(e => e.DateOfAppending)
                      .IsRequired();
                entity.Property(e => e.PasswordHash)
                      .IsRequired();
                entity.HasMany(e => e.Tournaments)
                      .WithMany(e => e.Players);
            });

            modelBuilder.Entity<Tournament>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name)
                      .IsRequired()
                      .HasMaxLength(100);
                entity.Property(e => e.City)
                      .HasMaxLength(50);
                entity.Property(e => e.PostalCode)
                      .HasMaxLength(10);
                entity.Property(e => e.Street)
                      .HasMaxLength(50);
                entity.Property(e => e.BuildingNumber)
                      .HasMaxLength(10);
                entity.Property(e => e.LocalNumber)
                      .HasMaxLength(10);
                entity.Property(e => e.DateFrom)
                      .IsRequired();
                entity.Property(e => e.DateTo)
                      .IsRequired();
                entity.HasCheckConstraint("ck_tournaments_datefrom", $"[{nameof(Tournament.DateFrom)}] > GETDATE()");
                entity.HasCheckConstraint("ck_tournaments_dateto", $"[{nameof(Tournament.DateTo)}] > [{nameof(Tournament.DateFrom)}]");
                entity.HasCheckConstraint("ck_tournaments_maxplayers", $"[{nameof(Tournament.MaxPlayers)}] IS NULL OR [{nameof(Tournament.MaxPlayers)}] > 1");
                entity.Property(e => e.CostPerPlayer)
                      .HasColumnType("decimal(18,2)");
                entity.HasCheckConstraint("ck_tournaments_costperplayer", $"[{nameof(Tournament.CostPerPlayer)}] IS NULL OR [{nameof(Tournament.MaxPlayers)}] >= 0");
                entity.Property(e => e.Description)
                      .HasMaxLength(500);
                entity.Property(e => e.Status)
                      .IsRequired()
                      .HasDefaultValue(TournamentStatus.Planned);
                entity.Property(e => e.NumberOfRounds)
                      .IsRequired();
                entity.HasCheckConstraint("ck_tournaments_rounds", $"[{nameof(Tournament.NumberOfRounds)}] > 0");
                entity.HasOne(e => e.CreatedBy)
                      .WithMany(u => u.CreatedTournaments)
                      .HasForeignKey(e => e.CreatedById)
                      .OnDelete(DeleteBehavior.SetNull);
            });
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=LAPTOP-LJAH9CH6\\SQLEXPRESS;Database=ChessTournamentDb;Trusted_Connection=True;"); 
            }
        }
    }
}
