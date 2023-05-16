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
    public class DogLogicTester
    {
        DogLogic logic;
        Mock<IRepository<Dog>> moqDogRepo;

        [SetUp]
        public void Init()
        {
            moqDogRepo = new Mock<IRepository<Dog>>();
            moqDogRepo.Setup(t => t.ReadAll()).Returns(new List<Dog>()
            {
                new Dog(){ Id = 1, Name = "Shadow", BirthYear = 2022, Weight = 55, Color = 5, OwnerId = 1, BreedId = 1},
                new Dog(){ Id = 2, Name = "Bentley", BirthYear = 2014, Weight = 80, Color = 4, OwnerId = 1, BreedId = 2}
            }.AsQueryable());
            moqDogRepo.Setup(t => t.Read(1)).Returns(new Dog() { Id = 1, Name = "Shadow", BirthYear = 2022, Weight = 55, Color = 5, OwnerId = 1, BreedId = 1 });
            moqDogRepo.Setup(t => t.Read(2)).Returns(new Dog() { Id = 2, Name = "Bentley", BirthYear = 2014, Weight = 80, Color = 4, OwnerId = 1, BreedId = 2 });
            logic = new DogLogic(moqDogRepo.Object);
        }

        [Test]
        public void ReadTest_Correct()
        {
            int id = 1;
            logic.Read(id);
            moqDogRepo.Verify(t => t.Read(id), Times.AtLeast(2));
        }
        [Test]
        public void ReadTest_Incorrect()
        {
            int id = 3;
            Assert.Throws<ArgumentException>(() => logic.Read(id));

            moqDogRepo.Verify(t => t.Read(id), Times.Once);
        }
        [Test]
        public void Delete_Correct()
        {
            int id = 1;
            logic.Delete(id);
            moqDogRepo.Verify(t => t.Delete(id), Times.Once);
        }
    }
}
