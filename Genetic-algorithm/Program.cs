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
            public Indidvid(Random random) // Конструктор единичной осыби популяции
            {
                array = new int[32];
                this.random = random;
                
            }

            public int[] array;
            public double value;
            public Random random;

            public void Create_Individ() // Заносим в массив занчения
            {
                
                for (int i = 0; i< array.Length; i++)
                {
                    array[i] = random.Next(2);
                }
                value = Value_Finding(array);
            }

            public void Reproduction(int[] perent1, int[] perent2, int s1, int s2, bool shit) // Осуществяем кроссигновер от родителей
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
                Mutation(array);
                value = Value_Finding(array);
            }

            public void Mutation(int[] array) // Осуществление мутации
            {
                int Pm = random.Next(100);
                if (Pm < 8)
                {
                    int Gen1 = random.Next(0, 17);
                    int Gen2 = random.Next(17, 32);
                    int t = array[Gen1];
                    array[Gen1] = array[Gen2];
                    array[Gen2] = t;
                }
            }
        }

        class Population
        {
            public Population(int N, int x) // Конструктор создания популяции
            {
                this.N = N;
                Console.WriteLine("Исходная популяция");
                ind = Create_Population(N);
                this.x = x;
                min_value = 1000;
                
                //Copulation(ind);
            }

            public Indidvid[] ind;
            public int x; // Номер популяции
            public int N; // Количесвто осыбей
            public Random random = new Random(); // Генератор случайных чисел
            public double min_value;

            public Indidvid[] Create_Population(int N) // Создаём популяция
            {
                Indidvid[] ind = new Indidvid[N]; // Массив из объектов типа особь, по факту наша популяция
                for (int i = 0; i < N; i++)
                {
                    ind[i] = new Indidvid(random);
                    ind[i].Create_Individ();
                    Print(ind[i].array, ind[i].value);
                }
                return ind;
            }

            public void Copulation(Indidvid[] ind) // Метод совокупления
            {
                Array.Sort(ind, (x, y) => x.value.CompareTo(y.value)); // Сортируем массив по значения фитнесс-функции
                double sum_value = Sum(ind);
                double[] roulette_fields = new double[((int)(ind.Length * 0.6 + 1))]; // Создаём массив для нашей рулетки
                roulette_fields[0] = 0;

                for (int i = 0; i < roulette_fields.Length - 1; i++) // Заполняем массив
                {
                    roulette_fields[i + 1] = roulette_fields[i] + (1 / ind[i].value )/ sum_value;
                }
                int x1;
                int x2;

                Indidvid[] children = new Indidvid[N]; // Массив потомства
                for (int i = 0; i < ind.Length;i += 2)
                {
                    int s1 = random.Next(1, 32 / 2); // 1 точка для кроссинговера
                    int s2 = random.Next(32 / 2, 32); // 2 точка для кроссинговера

                    //int s1 = 5;
                    //int s2 = 23;


                    children[i] = new Indidvid(random); // Создаём детей
                    children[i+1] = new Indidvid(random);

                    do
                    {
                        x1 = Selection(roulette_fields);
                        x2 = Selection(roulette_fields);
                    } while (x1 == x2); // Выбираем им родителей

                    children[i].Reproduction(ind[x1].array, ind[x2].array, s1, s2, true);
                    children[i+1].Reproduction(ind[x1].array, ind[x2].array, s1, s2, false);

                }
                this.x++;
                Overwriting(children);
                Console.WriteLine("lol! А это только " + x + "-ое потомство, смотри что будет дальше:");
                for (int i = 0; i < N; i++)
                {
                    Print(ind[i].array, ind[i].value); // Печатем детей
                }
                this.min_value = GetValue(ind);
            }

            public int Selection(double[] roulette_fields) // Выбераем родителей
            {
                
                int x = 0;
                double m = random.NextDouble(); // Крутим колесо рулетки)
                for (int i = 0; i < roulette_fields.Length - 1; i++) // проходим по всем полям значений рулетки
                {
                    if (roulette_fields[i] < m && m < roulette_fields[i+1]) // Ищем
                    {
                        x = i;
                        break;
                    }
                }
                return x;
            }

            public void Overwriting(Indidvid[] children)
            {
                for (int i = 0; i < N; i++)
                {
                    ind[i] = children[i];
                }
            }

            public double GetValue(Indidvid[] ind)
            {

                for (int i = 0; i < N; i++)
                {
                    if(min_value < ind[i].value)
                    {
                        min_value = ind[i].value;
                    }
                }
                return min_value;
            }

        }

        static double Sum(Indidvid[] ind) 
        {
            double sum = 0;
            for (int i = 0; i < ind.Length * 0.6; i++) // Сумма фитнесс-функций 60% наиболееприспособленных осыбей
            {
                sum += 1 / ind[i].value;
            }
            return sum;
        }  // Обратная сумма

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
            for (int i = array.Length / 16; i > 0; i--)
            {
                X += array[i] * Math.Pow(2,-(i - array.Length / 16));
            }
            double X_point = 0;
            for (int i = array.Length / 2; i > array.Length / 16; i--)
            {
                X_point += array[i] * Math.Pow(2, -(i - array.Length / 2));
            }
            X += X_point/Math.Pow(10, X_point.ToString().Length);
            double Y = 0;
            for (int i = array.Length / 16 * 9; i > array.Length / 2; i--)
            {
                Y += array[i] * Math.Pow(2, -(i - array.Length / 16 * 9));
            }
            double Y_point = 0;
            for (int i = array.Length - 1; i > array.Length / 16 * 9; i--)
            {
                Y_point += array[i] * Math.Pow(2, -(i - array.Length + 1));
            }
            Y += Y_point / Math.Pow(10, Y_point.ToString().Length);
            double value = Math.Pow(1.5 - X + X * Y, 2) + Math.Pow(2.25 - X + X * Y * Y, 2) + Math.Pow(2.625 - X + X * Y * Y * Y, 2);
            return value;
        } //Расчёт ценности гена


        static void Main(string[] args)
        {
            int N = Convert.ToInt32(Console.ReadLine());
            Population population = new Population(N,0);
            while( population.min_value != 0)
            {
                population.Copulation(population.ind);
            }

        }
    }
}
