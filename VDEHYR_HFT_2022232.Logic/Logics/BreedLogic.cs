using Microsoft.Extensions.Caching.Memory;
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
    public class BreedLogic : IBreedLogic
    {
        IRepository<Breed> Repo;
        public BreedLogic(IRepository<Breed> repo)
        {
            this.Repo = repo;
        }

        public void Create(Breed item)
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

        public Breed Read(int id)
        {
            if (Repo.Read(id) is null)
            {
                throw new ArgumentException("ID not exists");
            }
            return Repo.Read(id);
        }

        public IEnumerable<Breed> ReadAll()
        {
            return Repo.ReadAll();
        }

        public void Update(Breed item)
        {
            if (Repo.Read(item.Id) is null)
            {
                throw new ArgumentException("ID not exists");
            }
            Repo.Update(item);
        }
    }
}
