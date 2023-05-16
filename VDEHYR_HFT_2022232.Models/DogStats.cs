using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VDEHYR_HFT_2022232.Models
{
    public class DogStats
    {
        public Owner Owner { get; }
        public IEnumerable<Dog> Dogs { get; }
        public DogStats(Owner owner, IEnumerable<Dog> dogs)
        {
            Owner = owner;
            Dogs = dogs;
        }
        public override string ToString()
        {
            string output = "";
            output+= Owner.ToString() + "\n";
            foreach (var dog in Dogs)
            {
                output += dog.ToString() + "\n";
            }
            return output;
        }
    }
}
