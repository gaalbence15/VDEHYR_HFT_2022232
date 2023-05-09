using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace VDEHYR_HFT_2022232.Models
{
    public class Owner
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [StringLength(100)]
        public string Name { get; set; }
        public int Age { get; set; }
        public int DogId { get; set; }
        [JsonIgnore]
        public virtual ICollection<Dog> Dogs { get; set; }
        public Owner() { }
        public Owner(int id, string name, int age)
        {
            Id = id;
            Name = name;
            Age = age;
        }
        public override string ToString()
        {
            return $"{Id} - {Name}, {Age} yo";
        }
    }
}
