using Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Data.Data
{
    public class CarsDbContext : DbContext
    {
        public DbSet<Cars> Cars { get; set; }
        public DbSet<Category> Categories { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Car_interior;Integrated Security=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Category>().HasData(new[]
            {
                new Category() { Id = 1, Name = "Sedan" },
                new Category() { Id = 2, Name = "Coupe" },
                new Category() { Id = 3, Name = "Roadster" },
                new Category() { Id = 4, Name = "Convertible" },
                new Category() { Id = 5, Name = "Gran Coupe" },
                new Category() { Id = 6, Name = "Station wagon" },
                new Category() { Id = 7, Name = "Hatchback" },
                new Category() { Id = 8, Name = "Crossover" },
                new Category() { Id = 9, Name = "SUV" },
                new Category() { Id = 10, Name = "Roadster" }
            });

            modelBuilder.Entity<Cars>().HasData(new List<Cars>()
            {
                new Cars() { Id = 1, Name = "BMW M3", CategoryId = 1, Price = 100000, Quantity = 3, ImageUrl = "https://www.bmw.ua/content/dam/bmw/common/all-models/m-series/m3-sedan/2023/highlights/bmw-3-series-cs-m-automobiles-gallery-impressions-m3-competition-02_890.jpg" },

                new Cars() { Id = 2, Name = "BMW M5", CategoryId = 1, Price = 90000, Quantity = 5, ImageUrl = "https://www.bmw.tj/content/dam/bmw/common/all-models/m-series/m5-sedan/2021/Overview/bmw-m5-cs-onepager-gallery-m5-core-02-wallpaper.jpg" },

                new Cars() { Id = 3, Name = "BMW M4", CategoryId = 2, Price = 105000, Quantity = 2, ImageUrl = "https://carnetwork.s3.ap-southeast-1.amazonaws.com/file/8647cc8284b349178fd78c46e65daa36.jpg" },

                new Cars() { Id = 4, Name = "BMW M8", CategoryId = 5, Price = 120000, Quantity = 2, ImageUrl = "https://www.bmw.ua/content/dam/bmw/common/all-models/m-series/m8-gran-coupe/2022/onepager/bmw-m8-gran-coupe-onepager-gallery-m8-gc-thumbnail-01.jpg" },

                new Cars() { Id = 5, Name = "BMW X5", CategoryId = 8, Price = 70000, Quantity = 3, ImageUrl = "https://static.tcimg.net/vehicles/primary/b98f3827e42dc106/2024-BMW-X5_M-white-full_color-driver_side_front_quarter.png" },

                new Cars() { Id = 6, Name = "BMW M760e", CategoryId = 1, Price = 1670000, Quantity = 1, ImageUrl = "https://jaegersentrum.no/wp-content/uploads/2022/06/bildekort.png" },

                new Cars() { Id = 7, Name = "BMW XM", CategoryId = 8, Price = 240000, Quantity = 1, ImageUrl = "https://www.motortrend.com/uploads/2022/09/2023-BMW-XM-1.jpg?w=768&width=768&q=75&format=webp" },

                new Cars() { Id = 8, Name = "BMW X7", CategoryId = 9, Price = 150000, Quantity = 2, ImageUrl = "https://img.tipcars.com/fotky_velke/26141816_1/0/E/bmw-x7-xdrive40i.jpg" },
            });
        }
    }
}
