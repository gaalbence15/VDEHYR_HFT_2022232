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
    public class Breed
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [StringLength(50)]
        public string Name { get; set; }
        [StringLength(50)]
        public string Origin { get; set; }
        public int Lifespan { get; set; }
        public int DogId { get; set; }
        [JsonIgnore]
        [NotMapped]
        public virtual Dog Dog { get; set; }
        public Breed() {}
        public Breed(int id, string name, string origin, int lifespan, int dogId)
        {
            Id = id;
            Name = name;
            Origin = origin;
            Lifespan = lifespan;
            DogId = dogId;
        }
        public override string ToString()
        {
            return $"{Id} - {Name}: from {Origin} with an estimated lifespan of {Lifespan} years";
        }
    }
}
