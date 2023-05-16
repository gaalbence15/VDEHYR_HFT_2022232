using Moq;
using NUnit.Framework;
using System;
using VDEHYR_HFT_2022232.Logic.Logics;
using VDEHYR_HFT_2022232.Models;
using VDEHYR_HFT_2022232.Repository.Interfaces;
using System.Linq;
using System.Collections.Generic;

namespace VDEHYR_HFT_2022232.Test
{
    [TestFixture]
    public class BreedLogicTester
    {
        BreedLogic logic;
        Mock<IRepository<Breed>> moqBreedRepo;

        [SetUp]
        public void Init()
        {
            moqBreedRepo= new Mock<IRepository<Breed>>();
            moqBreedRepo.Setup(t => t.ReadAll()).Returns(new List<Breed>()
            {
                new Breed { Id = 1, Name = "Bullmastiff", Origin = "England", Lifespan = 10 },
                new Breed { Id = 2, Name = "Great Dane", Origin = "Germany", Lifespan = 9 },
                new Breed { Id = 3, Name = "Newfundland", Origin = "Canada", Lifespan = 8 }
            }.AsQueryable());
            moqBreedRepo.Setup(t => t.Read(1)).Returns(new Breed { Id = 1, Name = "Bullmastiff", Origin = "England", Lifespan = 10 });
            moqBreedRepo.Setup(t => t.Read(2)).Returns(new Breed { Id = 2, Name = "Great Dane", Origin = "Germany", Lifespan = 9 });
            moqBreedRepo.Setup(t => t.Read(3)).Returns(new Breed { Id = 3, Name = "Newfundland", Origin = "Canada", Lifespan = 8 });
            logic = new BreedLogic(moqBreedRepo.Object);
        }

        [Test]
        public void CreateTest_Correct()
        {
            var breed = new Breed { Id = 4, Name = "Blue Lacy", Origin = "United States", Lifespan = 15 };

            logic.Create(breed);

            moqBreedRepo.Verify(t => t.Create(breed), Times.Once);
        }

        [Test]
        public void CreateTest_IDExists()
        {
            var breed = new Breed { Id = 3, Name = "Blue Lacy", Origin = "United States", Lifespan = 15 };

            Assert.Throws<ArgumentException>(() => logic.Create(breed));

            moqBreedRepo.Verify(t => t.Create(breed), Times.Never);
        }

        [Test]
        public void ReadTest_Correct()
        {
            int id = 1;
            logic.Read(id);
            moqBreedRepo.Verify(t => t.Read(id), Times.AtLeast(2));
        }

    }
}
