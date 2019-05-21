using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using LinqExample.Web.Models.Animals;
using LinqExample.Web.Models.Owner;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace LinqExample.Web
{
    public class Program
    {
        public static void Main ( string[ ] args )
        {
            CreateWebHostBuilder ( args ).Build ( ).Run ( );
            QueryStringArray ( );
            QueryIntArray ( );
            QueryArrayList ( );
            QueryCollection ( );
            QueryAnimalData ( );
            Console.ReadLine ( );
        }

        public static IWebHostBuilder CreateWebHostBuilder ( string[ ] args ) =>
            WebHost.CreateDefaultBuilder ( args )
                .UseStartup<Startup> ( );

        static void QueryStringArray()
        {
            string[ ] dogs = {"K 9", "Brian Griffin",
            "Scooby Doo", "Old Yeller", "Rin Tin Tin",
            "Benji", "Charlie B. Barkin", "Lassie",
            "Snoopy"};

            var dogSpaces = from dog in dogs
                            where dog.Contains ( " " )
                            orderby dog descending
                            select dog;
            foreach( var i in dogSpaces)
            {
                Console.WriteLine ( i );
            }
            Console.WriteLine ( );


        }

        static int[ ] QueryIntArray ( )
        {
            int[ ] nums = { 5, 10, 15, 20, 25, 30, 35, 40 };

            var gt20 = from num in nums
                       where num>20
                       orderby num
                       select num;
            foreach(var i in gt20 )
            {
                Console.WriteLine ( i );
            }
            Console.WriteLine ( );

            Console.WriteLine ($"Get Type: {gt20.GetType()}");

            var listGT20 = gt20.ToList<int> ( );
            var arrayGT20 = gt20.ToArray ( );

            nums[0]=45;

            foreach(var i in gt20 )
            {
                Console.WriteLine (i);
            }
            Console.WriteLine ();
            return arrayGT20;
        }

        static void QueryArrayList ( )
        {
            ArrayList famAnimals = new ArrayList ( )
            {
                new Animal {Name = "Heidi",
                Height = .8,
                Weight = 18},

                new Animal
                {
                    Name = "Shrek",
                    Height = 4,
                    Weight = 130
                },

                new Animal
                {
                    Name = "Congo",
                    Height = 3.8,
                    Weight = 90
                }
            };

            var famAnimalEnumerable = famAnimals.OfType<Animal> ( );
            var smAnimals = from animal in famAnimalEnumerable
                            where animal.Weight<=90
                            orderby animal.Name
                            select animal;
            foreach(var animal in smAnimals )
            {
                Console.WriteLine ("{0} weighs {1} lbs",
                    animal.Name, animal.Weight);
            }
        }

        static void QueryCollection ( )
        {
            var animalList = new List<Animal> ( )
            {
                new Animal{Name = "German Shepherd",
                Height = 25,
                Weight = 77},
                new Animal{Name = "Chihuahua",
                Height = 7,
                Weight = 4.4},
                new Animal{Name = "Saint Bernard",
                Height = 30,
                Weight = 200}
            };

            var bigDogs = from dog in animalList
                          where ( dog.Weight>70 )&&( dog.Height>25 )
                          orderby dog.Name
                          select dog;

            foreach ( var dog in bigDogs )
            {
                Console.WriteLine ( "A {0} weighs {1}lbs",
                    dog.Name, dog.Weight );
            }

            Console.WriteLine ( );

        }

        static void QueryAnimalData ( )
        {
            Animal[ ] animals = new[ ]
            {
                new Animal{Name = "German Shepherd",
                Height = 25,
                Weight = 77,
                AnimalID = 1},
                new Animal{Name = "Chihuahua",
                Height = 7,
                Weight = 4.4,
                AnimalID = 2},
                new Animal{Name = "Saint Bernard",
                Height = 30,
                Weight = 200,
                AnimalID = 3},
                new Animal{Name = "Pug",
                Height = 12,
                Weight = 16,
                AnimalID = 1},
                new Animal{Name = "Beagle",
                Height = 15,
                Weight = 23,
                AnimalID = 2}
            };

            Owner[ ] owners = new[ ]
            {
                new Owner{Name = "Doug Parks",
                OwnerID = 1},
                new Owner{Name = "Sally Smith",
                OwnerID = 2},
                new Owner{Name = "Paul Brooks",
                OwnerID = 3}
            };

            // Remove name and height
            var nameHeight = from a in animals
                             select new
                             {
                                 a.Name,
                                 a.Height
                             };

            // Convert to an object array
            Array arrNameHeight = nameHeight.ToArray ( );

            foreach ( var i in arrNameHeight )
            {
                Console.WriteLine ( i.ToString ( ) );
            }
            Console.WriteLine ( );

            // Create an inner join
            // Join info in owners and animals using
            // equal values for IDs
            // Store values for animal and owner
            var innerJoin =
               from animal in animals
               join owner in owners on animal.AnimalID
               equals owner.OwnerID
               select new { OwnerName = owner.Name, AnimalName = animal.Name };

            foreach ( var i in innerJoin )
            {
                Console.WriteLine ( "{0} owns a {1}",
                    i.OwnerName, i.AnimalName );
            }

            Console.WriteLine ( );

            // Create a group inner join
            // Get all animals and put them in a
            // newly created owner group if their
            // IDs match the owners ID 
            var groupJoin =
                from owner in owners
                orderby owner.OwnerID
                join animal in animals on owner.OwnerID
                equals animal.AnimalID into ownerGroup
                select new
                {
                    Owner = owner.Name,
                    Animals = from owner2 in ownerGroup
                              orderby owner2.Name
                              select owner2
                };

            //int totalAnimals = 0;

            foreach ( var ownerGroup in groupJoin )
            {
                Console.WriteLine ( ownerGroup.Owner );
                foreach ( var animal in ownerGroup.Animals )
                {
                    //      totalAnimals++;
                    Console.WriteLine ( "* {0}", animal.Name );
                }
            }
        }



        }   
    }
