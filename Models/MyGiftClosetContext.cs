namespace MGC.Models
{
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using System;
    using System.Linq;

    public class MyGiftClosetContext : IdentityDbContext<GiftUser>
    {
        private IConfigurationRoot _config;

        // Your context has been configured to use a 'MyGiftClosetEntities' connection string from your application's 
        // configuration file (App.config or Web.config). By default, this connection string targets the 
        // 'MyGiftCloset.Models.MyGiftClosetEntities' database on your LocalDb instance. 
        // 
        // If you wish to target a different database and/or database provider, modify the 'MyGiftClosetEntities' 
        // connection string in the application configuration file.
        public MyGiftClosetContext(IConfigurationRoot config, DbContextOptions options) : base(options)
        {
            _config = config;
            //Database.SetInitializer(new DatabaseInitializer());
        }

        // Add a DbSet for each entity type that you want to include in your model. For more information 
        // on configuring and using a Code First model, see http://go.microsoft.com/fwlink/?LinkId=390109.

        
        public DbSet<Gift> Gifts { get; set; }
        public DbSet<Holiday> Holidays { get; set; }
        public DbSet<Recipient> Recipients { get; set; }
        public DbSet<GiftUser> GiftUsers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Holiday>()
                .HasIndex(h => h.Name)
                .IsUnique();

            modelBuilder.Entity<Recipient>()
                .HasIndex(r => r.Name)
                .IsUnique();

            modelBuilder.Entity<Gift>()
                .Property(g => g.Price)
                .HasColumnType("decimal(7,2)");
       //         .IsRequired(false);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer(_config["ConnectionStrings:MGCContextConnection"]);
        }

    }


}