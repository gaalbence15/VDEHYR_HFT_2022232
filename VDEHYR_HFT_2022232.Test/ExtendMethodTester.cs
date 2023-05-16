using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VDEHYR_HFT_2022232.Logic.Logics;
using VDEHYR_HFT_2022232.Models;
using VDEHYR_HFT_2022232.Repository.Interfaces;

namespace VDEHYR_HFT_2022232.Test
{
    [TestFixture]
    public class ExtendMethodTester
    {
        DogLogic doglogic;
        Mock<IRepository<Dog>> moqDogRepo;
        BreedLogic breedlogic;
        Mock<IRepository<Breed>> moqBreedRepo;
        OwnerLogic ownerlogic;
        Mock<IRepository<Owner>> moqOwnerRepo;
        ExtendMethodLogic logic;
        [SetUp]
        public void Init()
        {
            moqDogRepo = new Mock<IRepository<Dog>>();
            moqDogRepo.Setup(t => t.ReadAll()).Returns(new List<Dog>()
            {
                new Dog(){ Id = 1, Name = "Shadow", BirthYear = 2022, Weight = 55, Color = 5, OwnerId = 1, BreedId = 1},
                new Dog(){ Id = 2, Name = "Bentley", BirthYear = 2014, Weight = 80, Color = 4, OwnerId = 1, BreedId = 2}
            }.AsQueryable());
            doglogic = new DogLogic(moqDogRepo.Object);

            moqBreedRepo = new Mock<IRepository<Breed>>();
            moqBreedRepo.Setup(t => t.ReadAll()).Returns(new List<Breed>()
            {
                new Breed { Id = 1, Name = "Bullmastiff", Origin = "England", Lifespan = 10 },
                new Breed { Id = 2, Name = "Great Dane", Origin = "Germany", Lifespan = 9 },
                new Breed { Id = 3, Name = "Newfundland", Origin = "Canada", Lifespan = 8 }
            }.AsQueryable());
            breedlogic = new BreedLogic(moqBreedRepo.Object);

            moqOwnerRepo = new Mock<IRepository<Owner>>();
            moqOwnerRepo.Setup(t => t.ReadAll()).Returns(new List<Owner>()
            {
                new Owner{ Id = 1, Name = "John", Age = 32 },
                new Owner{ Id = 2, Name = "Jane", Age = 25 }
            }.AsQueryable());
            ownerlogic = new OwnerLogic(moqOwnerRepo.Object);

            logic = new ExtendMethodLogic(moqDogRepo.Object, moqBreedRepo.Object, moqOwnerRepo.Object);
        }

        [Test]
        public void BreedWithDogsMoreThanTest()
        {
            int count = 2;
            logic.BreedWithDogsMoreThan(count);
            moqBreedRepo.Verify(t => t.ReadAll(), Times.Once);
        }

        [Test]
        public void OwnerWithMoreDogsThanTest()
        {
            int count = 2;
            logic.OwnerWithMoreDogsThan(count);
            moqOwnerRepo.Verify(t => t.ReadAll(), Times.Once);
        }
    }
}
