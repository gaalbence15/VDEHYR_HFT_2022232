using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VDEHYR_HFT_2022232.Logic.Interfaces;
using VDEHYR_HFT_2022232.Models;
using VDEHYR_HFT_2022232.Repository.Interfaces;

namespace VDEHYR_HFT_2022232.Logic.Logics
{
    public class ExtendMethodLogic : IExtendMethods
    {
        IRepository<Dog> DogRepo;
        IRepository<Breed> BreedRepo;
        IRepository<Owner> OwnerRepo;

        public ExtendMethodLogic(IRepository<Dog> dogRepo, IRepository<Breed> breedRepo, IRepository<Owner> ownerRepo)
        {
            DogRepo = dogRepo;
            BreedRepo = breedRepo;
            OwnerRepo = ownerRepo;
        }

        public IEnumerable<Breed> BreedWithDogsMoreThan(int count)
        {
            return BreedRepo.ReadAll().Where(t => t.Dogs.Count > count);
        }

        public IEnumerable<Dog> DogsBornAfterIsBreed(int year, int breedId)
        {
            return DogRepo.ReadAll().Where(t => t.BreedId == breedId && t.BirthYear > year);
        }

        public IEnumerable<Dog> DogsBornBeforeIsBreed(int year, int breedId)
        {
            return DogRepo.ReadAll().Where(t => t.BreedId == breedId && t.BirthYear < year);
        }

        public IEnumerable<DogStats> DogStats()
        {
            List<DogStats> dogstat = new();
            Owner owner;
            IEnumerable<Dog> dogs;
            foreach (var item in OwnerRepo.ReadAll())
            {
                owner = item;
                dogs = item.Dogs;
                dogstat.Add(new DogStats(owner, dogs));
            }
            return dogstat;
        }

        public IEnumerable<Owner> OwnerWithMoreDogsThan(int count)
        {
            return OwnerRepo.ReadAll().Where(t => t.Dogs.Count > count);
        }

        public IEnumerable<Owner> OwnerWithMoreDogsThanAndOlderThan(int count, int age)
        {
            return OwnerRepo.ReadAll().Where(t => t.Dogs.Count > count && t.Age > age);
        }
    }
}
