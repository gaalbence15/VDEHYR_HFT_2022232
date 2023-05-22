using ConsoleTools;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using VDEHYR_HFT_2022232.Models;

namespace VDEHYR_HFT_2022232.Client
{
    class Program
    {
        public static RestService rest;
        static void Main(string[] args)
        {
            rest = new("http://localhost:21058/");
            //Statistics Menu
            var extendMethodsSubMenu = new ConsoleMenu(args, level: 1)
                .Add("DogsBornBeforeIsBreed", () => DogsBornBeforeIsBreed())
                .Add("DogsBornAfterIsBreed", () => DogsBornAfterIsBreed())
                .Add("BreedWithDogsMoreThan", () => BreedWithDogsMoreThan())
                .Add("OwnerWithMoreDogsThan", () => OwnerWithMoreDogsThan())
                .Add("OwnerWithMoreDogsThanAndOlderThan", () => OwnerWithMoreDogsThanAndOlderThan())
                .Add("DogStats", () => DogStats())
                .Add("Return", ConsoleMenu.Close);

            //Hangars Menu
            var breedSubMenu = new ConsoleMenu(args, level: 1)
                .Add("Create", () => Create("Breed"))
                .Add("Read", () => Read("Breed"))
                .Add("Update", () => Update("Breed"))
                .Add("Delete", () => Delete("Breed"))
                .Add("List", () => ReadAll("Breed"))
                .Add("Return", ConsoleMenu.Close);

            //Starships Menu
            var dogSubMenu = new ConsoleMenu(args, level: 1)
                .Add("Create", () => Create("Dog"))
                .Add("Read", () => Read("Dog"))
                .Add("Update", () => Update("Dog"))
                .Add("Delete", () => Delete("Dog"))
                .Add("List", () => ReadAll("Dog"))
                .Add("Return", ConsoleMenu.Close);

            //Owners Menu
            var ownerSubMenu = new ConsoleMenu(args, level: 1)
                .Add("Create", () => Create("Owner"))
                .Add("Read", () => Read("Owner"))
                .Add("Update", () => Update("Owner"))
                .Add("Delete", () => Delete("Owner"))
                .Add("List", () => ReadAll("Owner"))
                .Add("Return", ConsoleMenu.Close);

            //Main menu
            var menu = new ConsoleMenu(args, level: 0)
                .Add("Owners", () => ownerSubMenu.Show())
                .Add("Dogs", () => dogSubMenu.Show())
                .Add("Breeds", () => breedSubMenu.Show())
                .Add("Extend Methods", () => extendMethodsSubMenu.Show())
                .Add("Exit", ConsoleMenu.Close);
            menu.Show();
        }
        public static void Create(string model)
        {
            try
            {
                if (model == "Owner")
                {
                    string Name;
                    int Age;
                    int Id;

                    Console.Write("Owner Id:\t");
                    Id = int.Parse(Console.ReadLine());
                    Console.WriteLine();

                    Console.Write("Owner Name:\t");
                    Name = Console.ReadLine();

                    Console.WriteLine();
                    Console.Write("Owner Age:\t");
                    Age = int.Parse(Console.ReadLine());

                    var owner = new Owner(Id, Name, Age);
                    rest.Post(owner, "OwnerLogic");
                }
                else if (model == "Breed")
                {
                    string Name;
                    string Origin;
                    int Id;
                    int LifeSpan;

                    Console.Write("Breed Id:\t");
                    Id = int.Parse(Console.ReadLine());
                    Console.WriteLine();

                    Console.Write("Breed Name:\t");
                    Name = Console.ReadLine();
                    Console.WriteLine();

                    Console.Write("Breed Origin:\t");
                    Origin = Console.ReadLine();
                    Console.WriteLine();

                    Console.Write("Breed LifeSpan:\t");
                    LifeSpan = int.Parse(Console.ReadLine());

                    var breed = new Breed(Id, Name, Origin, LifeSpan);
                    rest.Post(breed, "BreedLogic");
                }
                else if (model == "Dog")
                {
                    int Id;
                    string Name;
                    int BirthYear;
                    int Weight;
                    int Color;
                    int OwnerId;
                    int BreedId;

                    Console.Write("Dog Id:\t");
                    Id = int.Parse(Console.ReadLine());
                    Console.WriteLine();

                    Console.Write("Dog Name:\t");
                    Name = Console.ReadLine();
                    Console.WriteLine();

                    Console.Write("Dog BirthYear:\t");
                    BirthYear = int.Parse(Console.ReadLine());
                    Console.WriteLine();

                    Console.Write("Dog Weight:\t");
                    Weight = int.Parse(Console.ReadLine());
                    Console.WriteLine();

                    Console.Write("Dog Color:\t");
                    Color = int.Parse(Console.ReadLine());
                    Console.WriteLine();

                    Console.Write("Dog OwnerId:\t");
                    OwnerId = int.Parse(Console.ReadLine());
                    Console.WriteLine();

                    Console.Write("Dog BreedId:\t");
                    BreedId = int.Parse(Console.ReadLine());

                    var dog = new Dog(Id, Name, BirthYear, Weight, Color, OwnerId, BreedId);
                    rest.Post(dog, "DogLogic");
                }
                Console.WriteLine(model + " Created");
                Console.ReadLine();
            }
            catch (ArgumentException e)
            {
                Console.Clear();
                Console.WriteLine(e.Message);
                Console.ReadLine();
            }
        }
        public static void Read(string model)
        {
            try
            {
                Console.Write("Id:\t");
                int Id = int.Parse(Console.ReadLine());
                object o = null;
                if (model == "Owner")
                {
                    o = rest.Get<Owner>(Id, "OwnerLogic");
                }
                else if (model == "Dog")
                {
                    o = rest.Get<Dog>(Id, "DogLogic");
                }
                else if (model == "Breed")
                {
                    o = rest.Get<Breed>(Id, "BreedLogic");
                }
                Console.Clear();
                Console.WriteLine(o.ToString());
                Console.ReadLine();
            }
            catch (ArgumentException e)
            {
                Console.Clear();
                Console.WriteLine(e.Message);
                Console.ReadLine();
            }
        }
        public static void ReadAll(string model)
        {
            try
            {
                if (model == "Owner")
                {
                    List<Owner> Out = rest.Get<Owner>("OwnerLogic");
                    foreach (var item in Out)
                    {
                        Console.WriteLine(item.ToString());
                    }
                }
                else if (model == "Breed")
                {
                    List<Breed> Out = rest.Get<Breed>("BreedLogic");
                    foreach (var item in Out)
                    {
                        Console.WriteLine(item.ToString());
                    }
                }
                else if (model == "Dog")
                {
                    List<Dog> Out = rest.Get<Dog>("DogLogic");
                    foreach (var item in Out)
                    {
                        Console.WriteLine(item.ToString());
                    }
                }
                Console.ReadLine();
            }
            catch (ArgumentException e)
            {
                Console.Clear();
                Console.WriteLine(e.Message);
                Console.ReadLine();
            }
        }
        public static void Update(string model)
        {
            try
            {
                int? ID = null;
                if (model == "Owner")
                {
                    string Name;
                    int Age;
                    int Id;

                    Console.Write("Owner Id:\t");
                    Id = int.Parse(Console.ReadLine());
                    ID = Id;
                    Console.WriteLine();

                    Console.Write("Owner Name:\t");
                    Name = Console.ReadLine();

                    Console.WriteLine();
                    Console.Write("Owner Age:\t");
                    Age = int.Parse(Console.ReadLine());

                    var owner = new Owner(Id, Name, Age);
                    rest.Put(owner, "OwnerLogic");
                }
                else if (model == "Breed")
                {
                    string Name;
                    string Origin;
                    int Id;
                    int LifeSpan;

                    Console.Write("Breed Id:\t");
                    Id = int.Parse(Console.ReadLine());
                    ID = Id;
                    Console.WriteLine();

                    Console.Write("Breed Name:\t");
                    Name = Console.ReadLine();
                    Console.WriteLine();

                    Console.Write("Breed Origin:\t");
                    Origin = Console.ReadLine();
                    Console.WriteLine();

                    Console.Write("Breed LifeSpan:\t");
                    LifeSpan = int.Parse(Console.ReadLine());

                    var breed = new Breed(Id, Name, Origin, LifeSpan);
                    rest.Put(breed, "BreedLogic");
                }
                else if (model == "Dog")
                {
                    int Id;
                    string Name;
                    int BirthYear;
                    int Weight;
                    int Color;
                    int OwnerId;
                    int BreedId;

                    Console.Write("Dog Id:\t");
                    Id = int.Parse(Console.ReadLine());
                    ID = Id;
                    Console.WriteLine();

                    Console.Write("Dog Name:\t");
                    Name = Console.ReadLine();
                    Console.WriteLine();

                    Console.Write("Dog BirthYear:\t");
                    BirthYear = int.Parse(Console.ReadLine());
                    Console.WriteLine();

                    Console.Write("Dog Weight:\t");
                    Weight = int.Parse(Console.ReadLine());
                    Console.WriteLine();

                    Console.Write("Dog Color:\t");
                    Color = int.Parse(Console.ReadLine());
                    Console.WriteLine();

                    Console.Write("Dog OwnerId:\t");
                    OwnerId = int.Parse(Console.ReadLine());
                    Console.WriteLine();

                    Console.Write("Dog BreedId:\t");
                    BreedId = int.Parse(Console.ReadLine());

                    var dog = new Dog(Id, Name, BirthYear, Weight, Color, OwnerId, BreedId);
                    rest.Put(dog, "DogLogic");
                }
                Console.WriteLine("Updated " + ID + ". in " + model);
                Console.ReadLine();
            }
            catch (ArgumentException e)
            {
                Console.Clear();
                Console.WriteLine(e.Message + " Ez a büdös kurva rendetlenkedik: " + e.ParamName);
                Console.ReadLine();
            }
        }
        public static void Delete(string model)
        {
            try
            {
                int Id = -1;
                if (model == "Owner")
                {
                    Console.Write("Owner Id:\t");
                    Id = int.Parse(Console.ReadLine());
                    Console.WriteLine();

                    rest.Delete(Id, "OwnerLogic");
                }
                else if (model == "Dog")
                {
                    Console.Write("Dog Id:\t");
                    Id = int.Parse(Console.ReadLine());
                    Console.WriteLine();

                    rest.Delete(Id, "DogLogic");
                }
                else if (model == "Breed")
                {
                    Console.Write("Breed Id:\t");
                    Id = int.Parse(Console.ReadLine());
                    Console.WriteLine();

                    rest.Delete(Id, "BreedLogic");
                }
                Console.WriteLine("Deleted " + Id + ". in " + model);
                Console.ReadLine();
            }
            catch (ArgumentException e)
            {
                Console.Clear();
                Console.WriteLine(e.Message);
                Console.ReadLine();
            }
        }
        public static void DogsBornBeforeIsBreed()
        {
            int year;
            int breedId;

            Console.Write("Year:\t");
            year = int.Parse(Console.ReadLine());

            Console.Write("BreedId:\t");
            breedId = int.Parse(Console.ReadLine());

            List<Dog> Out = rest.Get<Dog>("ExtendMethodLogic/DogsBornBeforeIsBreed/" + year + "/" + breedId);
            foreach (var item in Out)
            {
                Console.WriteLine(item.ToString());
            }
            Console.ReadLine();
        }
        public static void DogsBornAfterIsBreed()
        {
            int year;
            int breedId;

            Console.Write("Year:\t");
            year = int.Parse(Console.ReadLine());

            Console.Write("BreedId:\t");
            breedId = int.Parse(Console.ReadLine());

            List<Dog> Out = rest.Get<Dog>("ExtendMethodLogic/DogsBornAfterIsBreed/" + year + "/" + breedId);
            foreach (var item in Out)
            {
                Console.WriteLine(item.ToString());
            }
            Console.ReadLine();
        }
        public static void BreedWithDogsMoreThan()
        {
            int count;

            Console.Write("Count:\t");
            count = int.Parse(Console.ReadLine());

            List<Breed> Out = rest.Get<Breed>("ExtendMethodLogic/BreedWithDogsMoreThan/" + count);
            foreach (var item in Out)
            {
                Console.WriteLine(item.ToString());
            }
            Console.ReadLine();
        }
        public static void OwnerWithMoreDogsThan()
        {
            int count;

            Console.Write("Count:\t");
            count = int.Parse(Console.ReadLine());

            List<Owner> Out = rest.Get<Owner>("ExtendMethodLogic/OwnerWithMoreDogsThan/" + count);
            foreach (var item in Out)
            {
                Console.WriteLine(item.ToString());
            }
            Console.ReadLine();
        }
        public static void OwnerWithMoreDogsThanAndOlderThan()
        {
            int count;
            int age;

            Console.Write("Count:\t");
            count = int.Parse(Console.ReadLine());
            Console.WriteLine();

            Console.Write("Age:\t");
            age = int.Parse(Console.ReadLine());

            List<Owner> Out = rest.Get<Owner>("ExtendMethodLogic/OwnerWithMoreDogsThanAndOlderThan/" + count + "/" + age);
            foreach (var item in Out)
            {
                Console.WriteLine(item.ToString());
            }
            Console.ReadLine();
        }
        public static void DogStats()
        {
            List<DogStats> Out = rest.Get<DogStats>("ExtendMethodLogic/DogStats");
            foreach (var item in Out)
            {
                Console.WriteLine(item.ToString());
            }
            Console.ReadLine();
        }
    }
}