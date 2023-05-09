﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VDEHYR_HFT_2022232.Repository.Interfaces
{
    public interface IRepository<T> where T : class
    {
        void Create(T item);
        IQueryable<T> ReadAll();
        T Read(int id);
        void Update(T item);
        void Delete(int id);
    }
}
