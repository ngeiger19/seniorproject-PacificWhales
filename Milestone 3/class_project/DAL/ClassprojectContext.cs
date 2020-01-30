namespace class_project.DAL
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using Models;
    public partial class ClassprojectContext : DbContext
    {
        public ClassprojectContext()
            /*: base("name=ClassprojectContext")*/
            : base("name=ClassprojectContext_Azure")
        {
        }

        public virtual DbSet<Athlete> Athletes { get; set; }
        public virtual DbSet<Coach> Coaches { get; set; }
        public virtual DbSet<Event> Events { get; set; }
        public virtual DbSet<Meet> Meets { get; set; }
        public virtual DbSet<Team> Teams { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Athlete>()
                .HasMany(e => e.Meets)
                .WithRequired(e => e.Athlete)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Coach>()
                .HasMany(e => e.Athletes)
                .WithRequired(e => e.Coach)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Event>()
                .HasMany(e => e.Meets)
                .WithRequired(e => e.Event)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Team>()
                .HasMany(e => e.Athletes)
                .WithRequired(e => e.Team)
                .WillCascadeOnDelete(false);
        }
    }
}
