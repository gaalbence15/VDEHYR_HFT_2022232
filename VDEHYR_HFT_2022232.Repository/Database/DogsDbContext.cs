using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VDEHYR_HFT_2022232.Models;

namespace VDEHYR_HFT_2022232.Repository.Database
{
    public class DogsDbContext : DbContext
    {
        public DbSet<Dog> Dogs { get; set; }
        public DbSet<Owner> Owners { get; set; }
        public DbSet<Breed> Breeds { get; set; }
        public DogsDbContext()
        {
            this.Database.EnsureCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseLazyLoadingProxies()
                    .UseInMemoryDatabase("Dogs");
            }
            base.OnConfiguring(optionsBuilder);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Dog>(d => d
                .HasOne(d => d.Owner)
                .WithMany(o => o.Dogs)
                .HasForeignKey(d => d.OwnerId)
                .OnDelete(DeleteBehavior.Cascade));

            modelBuilder.Entity<Breed>(b => b
                .HasMany(b => b.Dogs)
                .WithOne(d => d.Breed)
                .HasForeignKey(b => b.BreedId)
                .OnDelete(DeleteBehavior.Cascade));

            var dogs = new Dog[]
            {
                new Dog(){ Id = 1, Name = "Shadow", BirthYear = 2022, Weight = 55, Color = 5, OwnerId = 1, BreedId = 1},
                new Dog(){ Id = 2, Name = "Bentley", BirthYear = 2014, Weight = 80, Color = 4, OwnerId = 1, BreedId = 2},
                new Dog(){ Id = 3, Name = "Atticus", BirthYear = 2007, Weight = 72, Color = 3, OwnerId = 1, BreedId = 3},
                new Dog(){ Id = 4, Name = "Brutus", BirthYear = 2023, Weight = 21, Color = 2, OwnerId = 2, BreedId = 4},
                new Dog(){ Id = 5, Name = "Pluto", BirthYear = 2016, Weight = 45, Color = 1, OwnerId = 3, BreedId = 5},
                new Dog(){ Id = 6, Name = "Bruno", BirthYear = 2023, Weight = 15, Color = 1, OwnerId = 3, BreedId = 5},
            };

            var owners = new Owner[]
            {
                new Owner{ Id = 1, Name = "John", Age = 32 },
                new Owner{ Id = 2, Name = "Jane", Age = 25 },
                new Owner{ Id = 3, Name = "Jessica", Age = 63 }
            };

            var breeds = new Breed[]
            {
                new Breed{ Id = 1, Name = "Bullmastiff", Origin = "England", Lifespan = 10},
                new Breed{ Id = 2, Name = "Great Dane", Origin = "Germany", Lifespan = 9},
                new Breed{ Id = 3, Name = "Newfundland", Origin = "Canada", Lifespan = 8},
                new Breed{ Id = 4, Name = "Blue Lacy", Origin = "United States", Lifespan = 15},
                new Breed{ Id = 5, Name = "Bernese Mountain", Origin = "Switzerland", Lifespan = 6}
            };

            modelBuilder.Entity<Dog>().HasData(dogs);
            modelBuilder.Entity<Owner>().HasData(owners);
            modelBuilder.Entity<Breed>().HasData(breeds);

            base.OnModelCreating(modelBuilder);
        }
    }
}
