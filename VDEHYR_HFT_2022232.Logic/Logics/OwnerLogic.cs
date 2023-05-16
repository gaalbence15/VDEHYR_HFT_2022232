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
    public class OwnerLogic : IOwnerLogic
    {
        IRepository<Owner> Repo;
        public OwnerLogic(IRepository<Owner> repo)
        {
            this.Repo = repo;
        }

        public void Create(Owner item)
        {
            if (Repo.Read(item.Id) is not null)
            {
                throw new ArgumentException("ID exists");
            }
            Repo.Create(item);
        }

        public void Delete(int id)
        {
            if (Repo.Read(id) is null)
            {
                throw new ArgumentException("ID not exists");
            }
            Repo.Delete(id);
        }

        public Owner Read(int id)
        {
            if (Repo.Read(id) is null)
            {
                throw new ArgumentException("ID not exists");
            }
            return Repo.Read(id);
        }

        public IEnumerable<Owner> ReadAll()
        {
            return Repo.ReadAll();
        }

        public void Update(Owner item)
        {
            if (Repo.Read(item.Id) is null)
            {
                throw new ArgumentException("ID not exists");
            }
            Repo.Update(item);
        }
    }
}
