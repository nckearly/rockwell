using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Rockwell.Models
{
    public partial class RWStudiosContext : DbContext
    {
        public RWStudiosContext()
        {
        }

        public RWStudiosContext(DbContextOptions<RWStudiosContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Actor> Actor { get; set; }
        public virtual DbSet<Contact> Contact { get; set; }
        public virtual DbSet<Film> Film { get; set; }
        public virtual DbSet<FilmActor> FilmActor { get; set; }
        public virtual DbSet<FilmRating> FilmRating { get; set; }
        public virtual DbSet<FilmReview> FilmReview { get; set; }
        public virtual DbSet<Merchandise> Merchandise { get; set; }
        public virtual DbSet<UserRole> UserRole { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {

            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.2-servicing-10034");

            modelBuilder.Entity<Actor>(entity =>
            {
                entity.HasKey(e => e.ActorPk);

                entity.Property(e => e.ActorPk).HasColumnName("ActorPK");

                entity.Property(e => e.ActorAgent).HasMaxLength(50);

                entity.Property(e => e.Gender)
                    .IsRequired()
                    .HasMaxLength(1);

                entity.Property(e => e.NameFirst)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.NameFirstReal).HasMaxLength(50);

                entity.Property(e => e.NameLast)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.NameLastReal).HasMaxLength(50);
            });

            modelBuilder.Entity<Contact>(entity =>
            {
                entity.HasKey(e => e.ContactPk);

                entity.Property(e => e.ContactPk).HasColumnName("ContactPK");

                entity.Property(e => e.Address).HasMaxLength(100);

                entity.Property(e => e.City).HasMaxLength(50);

                entity.Property(e => e.Country).HasMaxLength(50);

                entity.Property(e => e.Email).HasMaxLength(100);

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Phone).HasMaxLength(50);

                entity.Property(e => e.State).HasMaxLength(5);

                entity.Property(e => e.UserLogin).HasMaxLength(50);

                entity.Property(e => e.UserPassword).HasMaxLength(50);

                entity.Property(e => e.UserRoleFk).HasColumnName("UserRoleFK");

                entity.Property(e => e.Zip).HasMaxLength(10);

                entity.HasOne(d => d.UserRoleFkNavigation)
                    .WithMany(p => p.Contact)
                    .HasForeignKey(d => d.UserRoleFk)
                    .HasConstraintName("FK_Contact_UserRole");
            });

            modelBuilder.Entity<Film>(entity =>
            {
                entity.HasKey(e => e.FilmPk);

                entity.Property(e => e.FilmPk).HasColumnName("FilmPK");

                entity.Property(e => e.DateInTheaters).HasColumnType("date");

                entity.Property(e => e.ImageName).HasMaxLength(50);

                entity.Property(e => e.MovieTitle)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.PitchText)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.RatingFk).HasColumnName("RatingFK");

                entity.HasOne(d => d.RatingFkNavigation)
                    .WithMany(p => p.Film)
                    .HasForeignKey(d => d.RatingFk)
                    .HasConstraintName("FK_Film_FilmRating");
            });

            modelBuilder.Entity<FilmActor>(entity =>
            {
                entity.HasKey(e => e.FilmActorPk);

                entity.Property(e => e.FilmActorPk)
                    .HasColumnName("FilmActorPK")
                    .ValueGeneratedNever();

                entity.Property(e => e.ActorFk).HasColumnName("ActorFK");

                entity.Property(e => e.FilmFk).HasColumnName("FilmFK");

                entity.Property(e => e.Salary).HasColumnType("money");

                entity.HasOne(d => d.ActorFkNavigation)
                    .WithMany(p => p.FilmActor)
                    .HasForeignKey(d => d.ActorFk)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FilmActor_Actor");

                entity.HasOne(d => d.FilmFkNavigation)
                    .WithMany(p => p.FilmActor)
                    .HasForeignKey(d => d.FilmFk)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FilmActor_Film");
            });

            modelBuilder.Entity<FilmRating>(entity =>
            {
                entity.HasKey(e => e.RatingPk);

                entity.Property(e => e.RatingPk)
                    .HasColumnName("RatingPK")
                    .ValueGeneratedNever();

                entity.Property(e => e.Rating)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<FilmReview>(entity =>
            {
                entity.HasKey(e => e.ReviewPk);

                entity.Property(e => e.ReviewPk).HasColumnName("ReviewPK");

                entity.Property(e => e.ContactFk).HasColumnName("ContactFK");

                entity.Property(e => e.FilmFk).HasColumnName("FilmFK");

                entity.Property(e => e.ReviewDate).HasColumnType("datetime");

                entity.Property(e => e.ReviewSummary)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.HasOne(d => d.ContactFkNavigation)
                    .WithMany(p => p.FilmReview)
                    .HasForeignKey(d => d.ContactFk)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FilmReview_Contact");

                entity.HasOne(d => d.FilmFkNavigation)
                    .WithMany(p => p.FilmReview)
                    .HasForeignKey(d => d.FilmFk)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FilmReview_Film");
            });

            modelBuilder.Entity<Merchandise>(entity =>
            {
                entity.HasKey(e => e.MerchandisePk);

                entity.Property(e => e.MerchandisePk).HasColumnName("MerchandisePK");

                entity.Property(e => e.FilmFk).HasColumnName("FilmFK");

                entity.Property(e => e.ImageNameLarge).HasMaxLength(50);

                entity.Property(e => e.ImageNameSmall).HasMaxLength(50);

                entity.Property(e => e.MerchandiseDescription).HasMaxLength(100);

                entity.Property(e => e.MerchandiseName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.MerchandisePrice).HasColumnType("money");

                entity.HasOne(d => d.FilmFkNavigation)
                    .WithMany(p => p.Merchandise)
                    .HasForeignKey(d => d.FilmFk)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Merchandise_Film");
            });

            modelBuilder.Entity<UserRole>(entity =>
            {
                entity.HasKey(e => e.UserRolePk);

                entity.Property(e => e.UserRolePk)
                    .HasColumnName("UserRolePK")
                    .ValueGeneratedNever();

                entity.Property(e => e.UserRoleFunction)
                    .IsRequired()
                    .HasMaxLength(75);

                entity.Property(e => e.UserRoleName)
                    .IsRequired()
                    .HasMaxLength(20);
            });
        }
    }
}
