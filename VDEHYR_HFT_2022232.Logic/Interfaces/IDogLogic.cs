using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VDEHYR_HFT_2022232.Models;
using VDEHYR_HFT_2022232.Repository.Interfaces;

namespace VDEHYR_HFT_2022232.Logic.Interfaces
{
    public interface IDogLogic
    {
        void Create(Dog item);
        void Update(Dog item);
        Dog Read(int id);
        IEnumerable<Dog> ReadAll();
        void Delete(int id);
    }
}
