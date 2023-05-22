using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using VDEHYR_HFT_2022232.Logic.Interfaces;
using VDEHYR_HFT_2022232.Models;

namespace VDEHYR_HFT_2022232.Endpoint.Controllers
{
    //public class ExtendMethodLogicController
    [Route("[controller]/[action]")]
    [ApiController]
    public class ExtendMethodLogicController : ControllerBase
    {
        IExtendMethods logic;
        public ExtendMethodLogicController(IExtendMethods logic)
        {
            this.logic = logic;
        }

        [HttpGet("{year}/{breedId}")]
        public IEnumerable<Dog> DogsBornBeforeIsBreed(int year, int breedId)
        {
            return this.logic.DogsBornBeforeIsBreed(year, breedId);
        }

        [HttpGet("{year}/{breedId}")]
        public IEnumerable<Dog> DogsBornAfterIsBreed(int year, int breedId)
        {
            return this.logic.DogsBornAfterIsBreed(year, breedId);
        }

        [HttpGet("{count}")]
        public IEnumerable<Breed> BreedWithDogsMoreThan(int count)
        {
            return this.logic.BreedWithDogsMoreThan(count);
        }

        [HttpGet("{count}")]
        public IEnumerable<Owner> OwnerWithMoreDogsThan(int count)
        {
            return this.logic.OwnerWithMoreDogsThan(count);
        }

        [HttpGet("{count}/{age}")]
        public IEnumerable<Owner> OwnerWithMoreDogsThanAndOlderThan(int count, int age)
        {
            return this.logic.OwnerWithMoreDogsThanAndOlderThan(count, age);
        }

        [HttpGet]
        public IEnumerable<DogStats> DogStats()
        {
            return this.logic.DogStats();
        }
    }
}