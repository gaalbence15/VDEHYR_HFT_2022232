using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace VDEHYR_HFT_2022232.Models
{
    public enum ColorBases
    {
        TriColor = 1,
        Blue = 2,
        Black = 3,
        Harlequin = 4,
        MelanisticMask = 5
    }
    public class Dog
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [StringLength(100)]
        public string Name { get; set; }
        public int BirthYear { get; set; }
        public int Weight { get; set; }
        public int Color { get; set; }
        public int OwnerId { get; set; }
        public int BreedId { get; set; }
        [JsonIgnore]
        [NotMapped]
        public virtual Breed Breed { get; set; }
        [JsonIgnore]
        [NotMapped]
        public virtual Owner Owner { get; set; }
        public Dog() { }
        public Dog(int id, string name, int birthYear, int weight, int color, int ownerId, int breedId)
        {
            if (!(color > 0 && color <= 5))
            {
                throw new ArgumentException("Wrong color code!");
            }
            Id = id;
            Name= name;
            BirthYear = birthYear;
            Weight = weight;
            Color = color;
            OwnerId = ownerId;
            BreedId = breedId;
        }
        public Dog(int id, string name, int birthYear, int weight/*, int color*/)
        {
            //if (!(color > 0 && color <= 5))
            //{
            //    throw new ArgumentException("Wrong color code!");
            //}
            Id = id;
            Name = name;
            BirthYear = birthYear;
            Weight = weight;
            //Color = color;
        }
        public override string ToString()
        {
            return $"{Id} - {Name}: {(ColorBases)Color} dog, born in {BirthYear}, weighing {Weight} kgs";
        }
    }
}
