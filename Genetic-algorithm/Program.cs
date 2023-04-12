using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Genetic_algorithm
{



    internal class Program
    {
        class Indidvid
        {
            public Indidvid()
            {
                Create_Individ();
            }
            byte[] array = new byte[32];

            public void Create_Individ()
            {
                for(byte i = 0; i< array.Length; i++)
                {
                    array[i] = i;
                }
            }

            public void Print()
            {
                for (byte i = 0; i < array.Length; i++)
                {
                    Console.Write(array[i]);
                }
                Console.Write("\n");
            }
        }

        class Population
        {
            public Population(int N)
            { 
                Create_Population(N);
            }

            public int x = 0; //Номер популяции

            public void Create_Population(int N)
            {
                Indidvid[] ind = new Indidvid[N];
                for (int i = 0; i < N; i++)
                {
                    ind[i] = new Indidvid();
                    ind[i].Print();
                }
            }
        }

        static void Main(string[] args)
        {
            int N = 20;
            Population population = new Population(N);
            Console.WriteLine("lol");

        }
    }
}
