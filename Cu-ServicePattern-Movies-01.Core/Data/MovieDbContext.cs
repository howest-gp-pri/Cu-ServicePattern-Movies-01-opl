using Cu_ServicePattern_Movies_01.Core.Data.Seeding;
using Microsoft.EntityFrameworkCore;


namespace Cu_ServicePattern_Movies_01.Core.Data
{
    public class MovieDbContext : DbContext
    {
        public DbSet<Movie> Movies { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Actor> Actors { get; set; }
        public DbSet<Director> Directors { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Rating> Ratings { get; set; }

        public MovieDbContext(DbContextOptions<MovieDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //fluent api configurations
            modelBuilder.Entity<Movie>().Property
                (m => m.Title)
                .IsRequired()
                .HasMaxLength(100);
            modelBuilder.Entity<User>().Property
                (u => u.Username)
                .IsRequired()
                .HasMaxLength(100);
            modelBuilder.Entity<User>().Property
                (u => u.Firstname)
                .HasMaxLength(100);
            modelBuilder.Entity<User>().Property
                (u => u.Lastname)
                .HasMaxLength(100);
            modelBuilder.Entity<Movie>().Property
                (m => m.Price)
                .HasColumnType("money");
                
            //Actor
            modelBuilder.Entity<Actor>().Property
                (u => u.Firstname)
                .IsRequired()
                .HasMaxLength(100);
            modelBuilder.Entity<Actor>().Property
                (u => u.Lastname)
                .IsRequired()
                .HasMaxLength(100);
            //Director
            modelBuilder.Entity<Director>().Property
                (u => u.Firstname)
                .IsRequired()
                .HasMaxLength(100);
            modelBuilder.Entity<Director>().Property
                (u => u.Lastname)
                .IsRequired()
                .HasMaxLength(100);
            //Company
            modelBuilder.Entity<Company>().Property
                (u => u.Name)
                .IsRequired()
                .HasMaxLength(200);
            //Rating
            modelBuilder.Entity<Rating>().Property
                (u => u.Score)
                .IsRequired();
            //call the seeder
            Seeder.Seed(modelBuilder);
            base.OnModelCreating(modelBuilder);
        }
    }
}
