using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VDEHYR_HFT_2022232.Models;

namespace VDEHYR_HFT_2022232.Logic.Interfaces
{
    public interface IBreedLogic
    {
        void Create(Breed item);
        void Update(Breed item);
        Breed Read(int id);
        IEnumerable<Breed> ReadAll();
        void Delete(int id);
    }
}
