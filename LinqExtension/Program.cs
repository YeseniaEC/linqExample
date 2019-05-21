using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace LinqExtension
{

    public class Program
    {
        delegate double doubleIt ( double val );

        public static void Main ( string[ ] args )
        {
            #region Lambda example
            doubleIt dblIt = x => x*2;
            Console.WriteLine ( $"5 * 2 = {dblIt ( 5 )}" );

            List<int> numList = new List<int> { 1, 9, 2, 4, 6, 8, 2, 3 };

            var evenList = numList.Where ( a => a%2==0 ).ToList ( );

            var rangeList = numList.Where ( x => ( x>2 )&&( x<9 ) ).ToList ( );

            foreach ( var r in rangeList )
            {
                Console.WriteLine ( r );
            }

            #endregion

            #region while/where lambda example
            List<int> flipList = new List<int> ( );

            int i = 0;
            Random random = new Random ( ); while(i < 100)
            {
                flipList.Add ( random.Next ( 1, 3 ) );
                i++;
            }

            Console.WriteLine ("Heads: {0}", flipList.Where(a => a ==1).ToList().Count());
            Console.WriteLine ("Tails: {0}", flipList.Where ( a => a==2 ).ToList ( ).Count ( ) );

            var nameList = new List<string>
            {
                "Doug", "Sara","Sue"
            };

            var sNameList = nameList.Where ( x => x.StartsWith ( "S" ) );

            foreach(var s in sNameList)
            {
                Console.WriteLine (s);
            }
            #endregion

            #region select example

            var oneToTen = new List<int> ( );
            oneToTen.AddRange ( Enumerable.Range ( 1, 10 ) );

            var squares = oneToTen.Select ( x => x*x );

            foreach(var s in squares )
            {
                Console.WriteLine (s);
            }

            #endregion

            #region zip example

            var listOne = new List<int> ( new int[ ] { 1, 2, 3, 4, } );

            var listTwo = new List<int> ( new int[ ] { 5, 6, 7, 8 } );

            var sumList = listOne.Zip ( listTwo, ( x, y ) => x+y ).ToList ( );

            foreach( var s in sumList)
            {
                Console.WriteLine (s);
            }

            #endregion

            #region aggregate example

            var numList2 = new List<int> ( ) { 1, 2, 3, 4, 5 };
            Console.WriteLine ("Sum : {0} ", numList2.Aggregate((a, b) => a+b ));

            #endregion

            #region average example

            var numList3 =new List<int> ( ) { 1, 3, 5, 7, 9 };

            Console.WriteLine ("Average : {0}", numList3.AsQueryable().Average());

            #endregion 

            #region if all/any example

            var numList4 = new List<int> ( ) { 1, 3, 5, 7, 9 };

            Console.WriteLine ("All > 3: {0}", numList4.All(x => x > 3));
            Console.WriteLine ( "Any > 3: {0}", numList4.Any( x => x>3 ) );


            #endregion

            #region distinct example

            var numList5 = new List<int> ( ) { 1, 2, 3, 3, 2, 1 };

            Console.WriteLine ("Distinct: {0} ", string.Join(" , ", numList5.Distinct()));

            #endregion

            #region except example

            var numList6 = new List<int> ( ) { 1, 3, 5, 7 };
            var numList7 = new List<int> ( ) { 3, 7 };

            Console.WriteLine ( " Except : {0}", string.Join ( " , ",
                numList6.Except ( numList7 ) ) );

            #endregion

            #region intersect example
            var numList8 = new List<int> ( ) { 1, 3, 5, 7 };
            var numList9 = new List<int> ( ) { 3, 7 };

            Console.WriteLine ( " Intersect : {0}", string.Join ( " , ",
                numList6.Intersect ( numList7 ) ) );

            #endregion

            Console.ReadLine ();
            CreateWebHostBuilder ( args ).Build ( ).Run ( );
        }

        public static IWebHostBuilder CreateWebHostBuilder ( string[ ] args ) =>
            WebHost.CreateDefaultBuilder ( args )
                .UseStartup<Startup> ( );
    }
}
