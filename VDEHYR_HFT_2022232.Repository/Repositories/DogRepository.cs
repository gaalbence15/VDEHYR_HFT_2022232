using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VDEHYR_HFT_2022232.Models;
using VDEHYR_HFT_2022232.Repository.Database;
using VDEHYR_HFT_2022232.Repository.Interfaces;

namespace VDEHYR_HFT_2022232.Repository.Repositories
{
    public class DogRepository : GenericRepository<Dog>, IRepository<Dog>
    {
        public DogRepository(DogsDbContext ctx) : base(ctx) { }
        public override Dog Read(int id)
        {
            return ctx.Dogs.FirstOrDefault(b => b.Id == id);
        }

        public override void Update(Dog item)
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
