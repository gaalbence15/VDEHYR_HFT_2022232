using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VDEHYR_HFT_2022232.Models;

namespace VDEHYR_HFT_2022232.Logic.Interfaces
{
    public interface IOwnerLogic
    {
        void Create(Owner item);
        void Update(Owner item);
        Owner Read(int id);
        IEnumerable<Owner> ReadAll();
        void Delete(int id);
    }
}
