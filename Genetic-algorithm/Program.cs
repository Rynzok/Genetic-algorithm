using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http.Headers;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;


namespace Genetic_algorithm
{

    internal class Program
    {

        class Indidvid
        {
            public Indidvid() // Конструктор единичной осыби популяции
            {
                array = new int[32];
                
            }

            public int[] array;
            public Random random = new Random();
            public double value;

            public void Create_Individ() // Заносим в массив занчения
            {
                //Random random = new Random();
                for (int i = 0; i< array.Length; i++)
                {
                    array[i] = random.Next(2);
                }
                value = Value_Finding(array);
            }

            public void Reproduction(int[] perent1, int[] perent2, int s1, int s2, bool shit)
            {
                if (shit)
                {
                    for (int i = 0; i < s1; i++)
                    {
                        array[i] = perent1[i];
                    }
                    for (int i = s1; i < s2; i++)
                    {
                        array[i] = perent2[i];
                    }
                    for (int i = s2; i < perent1.Length; i++)
                    {
                        array[i] = perent1[i];
                    }
                }
                else
                {
                    for (int i = 0; i < s1; i++)
                    {
                        array[i] = perent2[i];
                    }
                    for (int i = s1; i < s2; i++)
                    {
                        array[i] = perent1[i];
                    }
                    for (int i = s2; i < perent1.Length; i++)
                    {
                        array[i] = perent2[i];
                    }
                }
                value = Value_Finding(array);
            }
        }

        class Population
        {
            public Population(int N, int x) // Конструктор создания популяции
            {
                this.N = N;
                Indidvid[] ind = Create_Population(N);
                this.x = x;
                Console.WriteLine("lol! А это только " + x + "-ая популяция, смотри что будет дальше:");
                Copulation(ind);
            }

            public Indidvid[] ind;
            public int x; // Номер популяции
            public int N; // Количесвто осыбей

            public Indidvid[] Create_Population(int N) // Создаём популяция
            {
                Indidvid[] ind = new Indidvid[N]; // Массив из объектов типа особь, по факту наша популяция
                for (int i = 0; i < N; i++)
                {
                    ind[i] = new Indidvid();
                    ind[i].Create_Individ();
                    Print(ind[i].array, ind[i].value);
                }
                return ind;
            }

            public void Copulation(Indidvid[] ind) // Метод совокупления
            {
                Random random = new Random();
                Indidvid[] children = new Indidvid[ind.Length]; // Массив из объектов типа ребёнок
                for (int i = 0; i < ind.Length;i += 2)
                {
                    int s1 = random.Next(1, ind.Length / 2); // 1 точка для кроссинговера
                    int s2 = random.Next(ind.Length / 2, ind.Length); // 2 точка для кроссинговера
                    //int s1 = 5;
                    //int s2 = 23;


                    children[i] = new Indidvid(); // Создаём детей
                    children[i+1] = new Indidvid();
                    children[i].Reproduction(ind[i].array, ind[i + 1].array, s1, s2, true);
                    children[i+1].Reproduction(ind[i].array, ind[i + 1].array, s1, s2, false);
                }
                for(int i = 0; i < children.Length; i++)
                {
                    Print(children[i].array, children[i].value); // Печатем детей
                }

            }

        }

        static void Print(int[] array, double value) // Метод вывода новых генов в консоль
        {
            for (byte i = 0; i < array.Length; i++)
            {
                Console.Write(array[i]);
            }
            Console.Write(" Ценнсть гена: " + value + "\n");
        }

        static double Value_Finding(int[] array)
        {
            double X = 0;
            for (int i = array.Length / 2; i > 0; i--)
            {
                X += array[i] * i;
            }
            double Y = 0;
            for (int i = array.Length - 1; i > array.Length / 2; i--)
            {
                Y += array[i] * -(i - array.Length);
            }
            double value = Math.Pow(1.5 - X + X * Y, 2) + Math.Pow(2.25 - X + X * Y * Y, 2) + Math.Pow(2.625 - X + X * Y * Y * Y, 2);
            return value;
        }


        static void Main(string[] args)
        {
            int N = Convert.ToInt32(Console.ReadLine());
            Population population = new Population(N,1);
            
        }
    }
}
