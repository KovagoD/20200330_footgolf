using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Linq;

namespace _20200330footgolf
{
    class Versenyzo
    {
        public string vnev;
        public string kateg;
        public string enev;
        public int[] pontok;

        public Versenyzo(string sor)
        {
            var sorok = sor.Split(';');

            this.vnev = sorok[0];
            this.kateg = sorok[1];
            this.enev = sorok[2];
            this.pontok = new int[8] { int.Parse(sorok[3]), int.Parse(sorok[4]), int.Parse(sorok[5]), int.Parse(sorok[6]), int.Parse(sorok[7]), int.Parse(sorok[8]), int.Parse(sorok[9]), int.Parse(sorok[10]) };
        }

        public int osszPontszam()
        {
            var plus_pontok = pontok.OrderBy(p => p).Take(2).Where(x => x > 0).Count();
         
            var sum= pontok.OrderByDescending(p => p).Take(6).Sum() + (10*plus_pontok);

            return sum;
        }
    }
    class Program
    {
        static List<Versenyzo> versenyzok = new List<Versenyzo>();

        static void Main(string[] args)
        {
            Fel02();
            Fel03();
            Fel04();
            Fel05();
            Fel06();
            Fel07();
            Fel08();

            Console.ReadKey();
        }
        struct Egyesulet
        {
            public string nev;
            public int fo;
        }

        private static void Fel08()
        {

            /*
        Egyesulet[] egyesuletek = versenyzok.GroupBy(x => x.enev).Select(x => new Egyesulet { nev = x.enev, Text = store.ID }).ToArray();
        
            var egyesuletek = versenyzok.Where(x => x.enev != "n.a.").
                Where(y => (versenyzok.GroupBy(x => x.enev).Count() > 2)).
                GroupBy(x => x.enev).Select(s => new Egyesulet { nev = s.Key, fo = versenyzok.GroupBy(x => x.enev).Count() })
                .ToArray();
        */
            Console.WriteLine("8. feladat:");

            var egyesuletek = versenyzok.Where(x => x.enev != "n.a.").GroupBy(n => n.enev).Select(group =>new Egyesulet{nev = group.Key, fo = group.Count()}).ToArray();

            for (int i = 0; i < egyesuletek.Length; i++)
            {
                if (egyesuletek[i].fo > 2)
                {
                Console.WriteLine(egyesuletek[i].nev+":"+ egyesuletek[i].fo);
                }
            }
            
        }

        private static void Fel07()
        {
            Versenyzo[] ferfi_versenyzok = versenyzok.Where(x => x.kateg is "Felnott ferfi").ToArray();

            StreamWriter sr = new StreamWriter(@"..\..\Res\osszpontFF.txt");
            foreach (var versenyzo in ferfi_versenyzok)
            {
                sr.WriteLine(versenyzo.vnev + ";" + versenyzo.osszPontszam());
            }
            sr.Close();
        }

        private static void Fel06()
        {
            Versenyzo legjobb_noi = versenyzok.Where(x => x.kateg is "Noi").OrderByDescending(x => x.osszPontszam()).First();
            Console.WriteLine($"6. feladat: \n\tNév: {legjobb_noi.vnev}\n\tEgyesület: {legjobb_noi.enev}\n\tÖsszpont: {legjobb_noi.osszPontszam()}");
        }

        private static void Fel05()
        {
            Console.WriteLine("5. feladat:");
            foreach (var item in versenyzok)
            {
                Console.WriteLine("\t" +
                    ""+item.vnev + " " + item.osszPontszam());
            }
        }

        private static void Fel04()
        {
            var count = (versenyzok.Where(x => x.kateg is "Noi").Count() / (double)versenyzok.Count()) *100;
            Console.WriteLine("4. feladat: A női versenyzők aránya: {0:00.00}", count);

        }

        private static void Fel03()
        {
            Console.WriteLine($"3. feladat: Versenyzők száma: {versenyzok.Count}");
        }

        private static void Fel02()
        {
            var sr = new StreamReader(@"..\..\Res\fob2016.txt", Encoding.UTF8);
            while (!sr.EndOfStream)
            {
                Versenyzo tmp = new Versenyzo(sr.ReadLine());
                versenyzok.Add(tmp);
            }
        }
    }
}
