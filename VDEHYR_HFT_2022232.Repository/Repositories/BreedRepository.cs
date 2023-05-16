using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using VDEHYR_HFT_2022232.Models;
using VDEHYR_HFT_2022232.Repository.Database;
using VDEHYR_HFT_2022232.Repository.Interfaces;

namespace VDEHYR_HFT_2022232.Repository.Repositories
{
    public class BreedRepository : GenericRepository<Breed>, IRepository<Breed>
    {
        public BreedRepository(DogsDbContext ctx) : base(ctx) {}
        public override Breed Read(int id)
        {
            return ctx.Breeds.FirstOrDefault(b => b.Id == id);
        }

        public override void Update(Breed item)
        {
            var prev = Read(item.Id);
            foreach (var prop in prev.GetType().GetProperties())
            {
                if (prop.GetAccessors().FirstOrDefault(t => t.IsVirtual) == null)
                {
                    prop.SetValue(prev, prop.GetValue(item));
                }
            }
            ctx.SaveChanges();
        }
    }
}
