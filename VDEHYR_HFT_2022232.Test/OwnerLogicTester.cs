using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VDEHYR_HFT_2022232.Logic.Logics;
using VDEHYR_HFT_2022232.Models;
using VDEHYR_HFT_2022232.Repository.Interfaces;

namespace VDEHYR_HFT_2022232.Test
{
    [TestFixture]
    public class OwnerLogicTester
    {
        OwnerLogic logic;
        Mock<IRepository<Owner>> moqOwnerRepo;

        [SetUp]
        public void Init()
        {
            moqOwnerRepo = new Mock<IRepository<Owner>>();
            moqOwnerRepo.Setup(t => t.ReadAll()).Returns(new List<Owner>()
            {
                new Owner{ Id = 1, Name = "John", Age = 32 },
                new Owner{ Id = 2, Name = "Jane", Age = 25 }
            }.AsQueryable());
            moqOwnerRepo.Setup(t => t.Read(1)).Returns(new Owner { Id = 1, Name = "John", Age = 32 });
            moqOwnerRepo.Setup(t => t.Read(2)).Returns(new Owner { Id = 2, Name = "Jane", Age = 25 });
            logic = new OwnerLogic(moqOwnerRepo.Object);
        }

        [Test]
        public void DeleteTest_Correct()
        {
            int id = 1;
            logic.Delete(id);
            moqOwnerRepo.Verify(t => t.Delete(id), Times.Once);
        }
        [Test]
        public void DeleteTest_Inorrect()
        {
            int id = 3;
            Assert.Throws<ArgumentException>(() => logic.Delete(id));
            moqOwnerRepo.Verify(t => t.Delete(id), Times.Never);
        }
        [Test]
        public void UpdateTest_Correct()
        {
            var owner = new Owner { Id = 1, Name = "James", Age = 35 };
            logic.Update(owner);
            moqOwnerRepo.Verify(t => t.Update(owner), Times.Once);
        }
        [Test]
        public void UpdateTest_Incorrect()
        {
            var owner = new Owner { Id = 3, Name = "James", Age = 35 };
            Assert.Throws<ArgumentException>(() => logic.Update(owner));
            moqOwnerRepo.Verify(t => t.Update(owner), Times.Never);
        }
    }
}
