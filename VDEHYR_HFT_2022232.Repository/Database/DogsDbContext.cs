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
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Breed>(breed => breed
                .HasOne(breed => breed.Dog)
                .WithOne(dog => dog.Breed)
                .OnDelete(DeleteBehavior.Cascade));

            modelBuilder.Entity<Owner>(owner => owner
                .HasMany(owner => owner.Dogs)
                .WithOne(dog => dog.Owner)
                .OnDelete(DeleteBehavior.Cascade));

            //modelBuilder.Entity<Dog>(dog => dog
            //    .HasOne(dog => dog.Owner)
            //    .WithOne(owner => owner.Dogs))
        }
    }
}
