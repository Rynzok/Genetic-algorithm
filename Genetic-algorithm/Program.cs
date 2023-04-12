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
                Create_Individ();
            }
            public int[] array;

            public void Create_Individ() // Заносим в массив занчения
            {
                Random random = new Random();
                for (int i = 0; i< array.Length; i++)
                {
                    array[i] = random.Next(10);
                }
            }

            public void Print() // Выводим занчения в консоль
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
            public Population(int N, int x) // Конструктор создания популяции
            {

                Indidvid[] ind = Create_Population(N);
                this.x = x;
                Console.WriteLine("lol! А это только " + x + "-ая популяция, смотри что будет дальше:");
                Copulation(ind);
            }

            public int x; // Номер популяции

            public Indidvid[] Create_Population(int N) // Создаём популяция
            {
                Indidvid[] ind = new Indidvid[N]; // Массив из объектов типа особь, по факту наша популяция
                for (int i = 0; i < N; i++)
                {
                    ind[i] = new Indidvid();
                    ind[i].Print();
                }
                return ind;
            }

            public void Copulation(Indidvid[] ind) // Метод совокупления
            {
                Random random = new Random();
                Child[] children = new Child[ind.Length]; // Массив из объектов типа ребёнок
                for (int i = 0; i < ind.Length;i += 2)
                {
                    int s1 = random.Next(1, ind.Length / 2); // 1 точка для кроссинговера
                    int s2 = random.Next(ind.Length / 2, ind.Length); // 2 точка для кроссинговера
                    //int s1 = 5;
                    //int s2 = 23;


                    children[i] = new Child(ind[i], ind[i+1], s1 , s2, true); // Создаём детей
                    children[i+1] = new Child(ind[i], ind[i + 1], s1, s2, false);
                }
                for(int i = 0; i < children.Length; i++)
                {
                    children[i].Print(); // Печатем детей
                }
            }

            class Child
            {
                public Child(Indidvid perent1, Indidvid perent2, int s1, int s2, bool shit) 
                {
                    this.perent1 = perent1.array;
                    this.perent2 = perent2.array;
                    if (shit)
                    {
                        Create_Child1(s1,s2); // Это 1 ребёнок или второй?
                    }
                    else
                    {
                        Create_Child2(s1,s2);
                    }

                }

                int[] array = new int[32];
                public int[] perent1;
                public int[] perent2;

                public void Create_Child1(int s1, int s2) // Заполняем массивчик
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
                public void Create_Child2(int s1, int s2) // Заполняем массивчик
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
                public void Print() // Метод вывода новых генов в консоль
                {
                    for (byte i = 0; i < array.Length; i++)
                    {
                        Console.Write(array[i]);
                    }
                    Console.WriteLine();
                }
            }

        }



        static void Main(string[] args)
        {
            int N = Convert.ToInt32(Console.ReadLine());
            Population population = new Population(N,1);
            
        }
    }
}
