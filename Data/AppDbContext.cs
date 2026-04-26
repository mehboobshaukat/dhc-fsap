using DHC_FSAP.Models;
using DHC_FSAP.Models.Abouts;
using DHC_FSAP.Models.Blogs;
using DHC_FSAP.Models.Bookings;
using DHC_FSAP.Models.Contact;
using DHC_FSAP.Models.Portfolios;
using DHC_FSAP.Models.Services;
using DHC_FSAP.Models.Tags;
using DHC_FSAP.Models.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Options;

namespace DHC_FSAP.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<About> Abouts { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<BlogCategory> BlogCategories { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<ContactMessage> ContactMessages { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<ServiceCategories> ServiceCategories { get; set; }
        public DbSet<Portfolio> Portfolios { get; set; }
        public DbSet<PortfolioCategory> PortfolioCategories { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<EmailVerification> EmailVerifications { get; set; }
        public DbSet<User> Users { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //to string conversion for BlogLanguage(enum)
            modelBuilder.Entity<Blog>()
                .Property(b => b.BlogLanguage)
                .HasConversion<string>();

            //to string conversion for BookingCategory(enum)
            modelBuilder.Entity<Booking>()
                .Property(b => b.BookingCategory)
                .HasConversion<string>();

            //to string conversion for BookingStatus(enum)
            modelBuilder.Entity<Booking>()
                .Property(b => b.BookingStatus)
                .HasConversion<string>();

            //to string conversion for BookingType(enum)
            modelBuilder.Entity<Booking>()
                .Property(b => b.BookingType)
                .HasConversion<string>();

            //to string conversion for UserRole(enum)
            modelBuilder.Entity<User>()
                .Property(u => u.Role)
                .HasConversion<string>();

        }
    }
}
