namespace Harmony.DAL
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using Harmony.Models;
    public partial class HarmonyContext : DbContext
    {
        public HarmonyContext()
         : base("name=HarmonyContext")
        // : base("name=HarmonyContext_Azure")

        {
        }

        public virtual DbSet<Genre> Genres { get; set; }
        public virtual DbSet<Show> Shows { get; set; }
        public virtual DbSet<User_Show> User_Show { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Venue> Venues { get; set; }
        public virtual DbSet<VenueType> VenueTypes { get; set; }
        public virtual DbSet<Rating> Ratings { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Genre>()
                .HasMany(e => e.Users)
                .WithMany(e => e.Genres)
                .Map(m => m.ToTable("Musician_Genre").MapLeftKey("GenreID").MapRightKey("UserID"));

            modelBuilder.Entity<Show>()
                .HasMany(e => e.User_Show)
                .WithRequired(e => e.Show)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .HasMany(e => e.User_Show)
                .WithRequired(e => e.User)
                .HasForeignKey(e => e.MusicianID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .HasMany(e => e.User_Show1)
                .WithRequired(e => e.User1)
                .HasForeignKey(e => e.VenueOwnerID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .HasMany(e => e.Venues)
                .WithRequired(e => e.User)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .HasMany(e => e.Ratings)
                .WithRequired(e => e.User)
                .HasForeignKey(e => e.UserID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<VenueType>()
                .HasMany(e => e.Venues)
                .WithRequired(e => e.VenueType)
                .WillCascadeOnDelete(false);
        }
    }
}