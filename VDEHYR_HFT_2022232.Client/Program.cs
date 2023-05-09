using System;
using System.Linq;
using VDEHYR_HFT_2022232.Repository.Database;

namespace VDEHYR_HFT_2022232.Client
{
    internal class Program
    {
        static void Main(string[] args)
        {
            DogsDbContext ctx = new DogsDbContext();
            var dogs = ctx.Dogs.ToArray();
            var owners = ctx.Owners.ToArray();
            var breeds = ctx.Breeds.ToArray();
            ;
        }
    }
}
