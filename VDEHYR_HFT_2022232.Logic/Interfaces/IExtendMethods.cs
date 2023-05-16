using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VDEHYR_HFT_2022232.Models;

namespace VDEHYR_HFT_2022232.Logic.Interfaces
{
    public interface IExtendMethods
    {
        IEnumerable<Dog> DogsBornBeforeIsBreed(int year, int breedId);
        IEnumerable<Dog> DogsBornAfterIsBreed(int year, int breedId);
        IEnumerable<Breed> BreedWithDogsMoreThan(int count);
        IEnumerable<Owner> OwnerWithMoreDogsThan(int count);
        IEnumerable<Owner> OwnerWithMoreDogsThanAndOlderThan(int count, int age);
        IEnumerable<DogStats> DogStats();

    }
}
