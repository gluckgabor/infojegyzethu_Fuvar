using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Fuvar
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] lines = File.ReadAllLines("C:/Users/gluck/source/repos/Fuvar/fuvar.csv");

            List<Fuvar> fuvarList = new List<Fuvar>();
            
            for (int i = 1; i < lines.Length; i++)
            {
                Fuvar fuvarinstance = new Fuvar(lines[i]);

                fuvarList.Add(fuvarinstance);
            }

            Console.WriteLine("3. feladat");
            numberOfJourneys(fuvarList);

            Console.WriteLine("4. feladat");
            incomeOfGivenEntrepreneurAndNumberOfJourneys(fuvarList);

            Console.WriteLine("5. feladat");
            paymentMethodStatistics(fuvarList);

            Console.WriteLine("6. feladat");
            totalKm(fuvarList);

            Console.WriteLine("7. feladat");
            longestJourney(fuvarList);

            Console.WriteLine("8. feladat");
            errataGenerator(fuvarList);

            Console.ReadKey();
        }

        private static void errataGenerator(List<Fuvar> fuvarlist)
        {
            List<string> errata = new List<string>();

            fuvarlist = fuvarlist.OrderBy(i => i.indulas).ToList();

            foreach (var fuvar in fuvarlist)
            {
                if (fuvar.viteldij > 0 && fuvar.idotartam > 0 && fuvar.tavolsag == 0)
                {
                    errata.Add(fuvar.taxi_id + ";" + fuvar.indulas + ";" + fuvar.idotartam + ";" + fuvar.tavolsag + ";" + fuvar.viteldij + ";" + fuvar.borravalo + ";" + fuvar.fizetes_modja);
                }
            }

            //FileStream fileStream = new FileStream("C:/Users/gluck/source/repos/Fuvar/hibak.txt", FileMode.Create);
            StreamWriter sw = new StreamWriter("C:/Users/gluck/source/repos/Fuvar/hibak.txt", false);

            sw.WriteLine("taxi_id;indulas;idotartam;tavolsag;viteldij;borravalo;fizetes_modja");

            for (int i = 0; i < errata.Count; i++)
            {
                sw.WriteLine("{0};", errata[i].ToString());
            }

            sw.Close();
        }


        private static void longestJourney(List<Fuvar> fuvarList)
        {
            double longestJourneyDistance = 0;
            int longestJourneyDuration = 0;
            int longestJourneyTaxiId = 0;
            double longestJourneyFare = 0;

            foreach (var fuvar in fuvarList)
            {
                if (fuvar.idotartam >= longestJourneyDuration)
                {
                    longestJourneyDistance = fuvar.tavolsag;
                    longestJourneyDuration = fuvar.idotartam;
                    longestJourneyTaxiId = fuvar.taxi_id;
                    longestJourneyFare = fuvar.viteldij;
                }
            }
            Console.WriteLine("A leghosszabb fuvar:");
            Console.WriteLine("Fuvar hossza: " + longestJourneyDuration);
            Console.WriteLine("Taxi azonosító: " + longestJourneyTaxiId);
            Console.WriteLine("Megtett távolság: " + longestJourneyDistance + " mérföld, " + string.Format("{0:0.00}", longestJourneyDistance *1.6) + " km");
            Console.WriteLine("Viteldíj: " + longestJourneyFare);
        }

        private static void totalKm(List<Fuvar> fuvarList)
        {
            double totalKm = 0;

            foreach (var fuvar in fuvarList)
            {
                totalKm += fuvar.tavolsag * 1.6;
            }
            Console.WriteLine(String.Format("{0:0.00}", totalKm));
        }

        private static void paymentMethodStatistics(List<Fuvar> fuvarList)
        {
            List<string> paymentmethods = new List<string>();
            List<string> paymentmethodsDistinct = new List<string>();

            foreach (var fuvar in fuvarList)
            {
                paymentmethods.Add(Convert.ToString(fuvar.fizetes_modja));  
            }

            paymentmethodsDistinct = paymentmethods.Distinct().ToList();
            int z = 0;
            
            for (int i = 0; i < paymentmethodsDistinct.Count; i++)
            {
                z = 0;

                for (int j = 0; j < fuvarList.Count; j++)
                {
                    if (paymentmethodsDistinct[i] == fuvarList[j].fizetes_modja)
                    {
                        z++;
                    }
                }
                Console.WriteLine(paymentmethodsDistinct[i] + ": " + z + " fuvar");
                ;
            }
        }

        private static void incomeOfGivenEntrepreneurAndNumberOfJourneys(List<Fuvar> fuvarList)
        {
            double bevetel = 0;
            int journeyCounter = 0;

            foreach (var fuvar in fuvarList)
            {
                if (fuvar.taxi_id == 6185)
                {
                    //bevetel += fuvar.viteldij + fuvar.borravalo;
                    bevetel += fuvar.viteldij;
                    journeyCounter++;
                }                
            }
            Console.WriteLine(journeyCounter + " fuvar alatt: " + bevetel + "$");
        }

        private static void numberOfJourneys(List<Fuvar> fuvarList)
        {
            Console.WriteLine(fuvarList.Count);
        }
        
    }
}
