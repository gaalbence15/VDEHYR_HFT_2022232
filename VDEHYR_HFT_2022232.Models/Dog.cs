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
        [StringLength(100)]
        public int BirthYear { get; set; }
        [StringLength(10)]
        public int Weight { get; set; }
        public int Color { get; set; }
        [JsonIgnore]
        public virtual Breed Breed { get; set; }
        [JsonIgnore]
        public virtual Owner Owner { get; set; }
        public Dog() { }
        public Dog(int id, string name, int birthYear, int weight, int color, int breedId)
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
            BreedId = breedId;
        }
        public override string ToString()
        {
            return $"{Id} - {Name}: {(ColorBases)Color} dog, born in {BirthYear}, weighing {Weight} kgs";
        }
    }
}
