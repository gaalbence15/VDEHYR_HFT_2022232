using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VDEHYR_HFT_2022232.Repository.Database;
using VDEHYR_HFT_2022232.Repository.Interfaces;

namespace VDEHYR_HFT_2022232.Repository.Repositories
{
    public abstract class GenericRepository<T> : IRepository<T> where T : class
    {
        protected DogsDbContext ctx;
        public GenericRepository(DogsDbContext ctx)
        {
            this.ctx = ctx;
        }

        public void Create(T item)
        {
            ctx.Set<T>().Add(item);
            ctx.SaveChanges();
        }

        public void Delete(int id)
        {
            ctx.Set<T>().Remove(Read(id));
            ctx.SaveChanges();
        }

        public abstract T Read(int id);

        public IQueryable<T> ReadAll()
        {
            return ctx.Set<T>();
        }

        public abstract void Update(T item);
    }
}
