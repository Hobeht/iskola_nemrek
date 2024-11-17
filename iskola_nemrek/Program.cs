using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iskola_nemrek
{
    internal class Program
    {
        private static (bool lehet_e, int bejelolt_iskola) Oszlopban_keres(int jelenleg_vizsgalt_tanulo, int[] result, List<int>[] tanulok, int[] kapacitasok)
        {
            int tanulo_valasztott_iskolaja = result[jelenleg_vizsgalt_tanulo] + 1;
            while (tanulo_valasztott_iskolaja < kapacitasok.Length && !Elhelyezheto(jelenleg_vizsgalt_tanulo, tanulo_valasztott_iskolaja, tanulok, kapacitasok, result))
            {
                tanulo_valasztott_iskolaja++;
            }
            return (tanulo_valasztott_iskolaja < kapacitasok.Length, tanulo_valasztott_iskolaja < kapacitasok.Length ? tanulo_valasztott_iskolaja : result[jelenleg_vizsgalt_tanulo] + 1);
        }
        private static bool Elhelyezheto(int jelenleg_vizsgalt_tanulo, int tanulo_valasztott_iskolaja, List<int>[] tanulok, int[] kapacitasok, int[] result)
        {
            if (tanulok[jelenleg_vizsgalt_tanulo].Contains(tanulo_valasztott_iskolaja + 1) && result.Where(v => v == tanulo_valasztott_iskolaja).Count() < kapacitasok[tanulo_valasztott_iskolaja])
            {
                return true;
            }

            return false;
        }
        static int[] Feldolgozas(int N, int M, List<int>[] tanulok, int[] kapacitasok)
        {
            int[] result = new int[N];
            for (int ix = 0; ix < N; ix++)
            {
                result[ix] = -1;
            }
            int jelenleg_vizsgalt_tanulo = 0;

            while (-1 < jelenleg_vizsgalt_tanulo && jelenleg_vizsgalt_tanulo < N)
            {
                (bool lehet_e, int tanulo_valasztott_iskolaja) = Oszlopban_keres(jelenleg_vizsgalt_tanulo, result, tanulok, kapacitasok);
                if (lehet_e)
                {
                    result[jelenleg_vizsgalt_tanulo] = tanulo_valasztott_iskolaja;
                    jelenleg_vizsgalt_tanulo++;
                }
                else
                {
                    result[jelenleg_vizsgalt_tanulo] = -1;
                    jelenleg_vizsgalt_tanulo--;
                }
            }
            return result;
        }

        static (int, int, List<int>[], int[]) Beolvas()
        {
            string sor = Console.ReadLine();
            string[] sortomb = sor.Split(' ');
            int N = int.Parse(sortomb[0]);
            int M = int.Parse(sortomb[1]);

            List<int>[] tanulok = new List<int>[N];
            for (int i = 0; i < N; i++)
            {
                string[] s = Console.ReadLine().Split(' ');
                tanulok[i] = new List<int>();
                tanulok[i].Add(int.Parse(s[0]));
                if (s[1] != "0")
                {
                    tanulok[i].Add(int.Parse(s[1]));
                }
            }

            int[] kapacitasok = new int[M];

            for (int i = 0; i < M; i++)
            {
                kapacitasok[i] = int.Parse(Console.ReadLine());
            }
            return (N, M, tanulok, kapacitasok);
        }
        static void Diagnosztika(int[] result, List<int>[] tanulok, int[] kapacitas)
        {
            Console.Error.Write(string.Join(" ", kapacitas));
            Console.Error.Write("    |    ");
            for (int i = 0; i < tanulok.Length; i++)
            {
                if (0 <= result[i])
                {
                    Console.Error.Write(tanulok[i][result[i]] + " ");
                }
            }
            Console.Error.WriteLine();
        }

        static void Kiir(int[] result)
        {
            if (0 <= result[0])
            {
                for (int i = 0; i < result.Length; i++)
                {
                    result[i] = result[i] + 1;
                }
                Console.WriteLine(string.Join(" ", result));
            }
            else
            {
                Console.WriteLine(-1);
            }
        }
        static void Main(string[] args)
        {
            (int N, int M, List<int>[] tanulok, int[] kapacitas) = Beolvas();
            int[] result = Feldolgozas(N, M, tanulok, kapacitas);
            Console.Error.WriteLine(string.Join(", ", result));
            Kiir(result);
            Console.ReadKey();

        }
    }
}
