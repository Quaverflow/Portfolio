using Microsoft.VisualBasic.FileIO;
using Quaverflow.Data.CovidModels;
using System;
using System.Collections.Generic;

namespace Covid.Core
{
    public static class CovidReader
    {

        public static List<Country> ReadCountries()
        {
            var continents = new List<Continent>()
            {
                new Continent()
                {
                    Name = "Asia",
                    ContinentCode = "AS"
                },
                new Continent()
                {
                    Name = "Europe",
                    ContinentCode = "EU"
                },
                new Continent()
                {
                    Name = "Antartica",
                    ContinentCode = "AN"
                },
                new Continent()
                {
                    Name = "Africa",
                    ContinentCode = "AF"
                },
                new Continent()
                {
                    Name = "Oceania",
                    ContinentCode = "OC"
                },
                new Continent()
                {
                    Name = "North America",
                    ContinentCode = "NA"
                },
                new Continent()
                {
                    Name = "South America",
                    ContinentCode = "SA"
                }
            };


            TextFieldParser parser = new TextFieldParser
                ("D:/Coding studies/MirkosWebsite/Portfolio/Datasets/Covid/CovidLondon.csv");
            parser.SetDelimiters(",");

            var countries = new List<Country>();
            string[] fields;

            var counter = 0;
            while (!parser.EndOfData)
            {
                fields = parser.ReadFields();

                if (counter != 0 && !string.IsNullOrEmpty(fields[1]))
                {

                    //double? Population;
                    //double? LifeExpectancy;
                    //double? HospitalBedsPerThousand;
                    //double? PopulationDensity;


                    //if (string.IsNullOrEmpty(fields[44])) { Population = null; }
                    //else { Population = double.Parse(fields[44]); }

                    //if (string.IsNullOrEmpty(fields[57])) { LifeExpectancy = null; }
                    //else { LifeExpectancy = double.Parse(fields[57]); }

                    //if (string.IsNullOrEmpty(fields[56])) { HospitalBedsPerThousand = null; }
                    //else { HospitalBedsPerThousand = double.Parse(fields[56]); }

                    //if (string.IsNullOrEmpty(fields[45])) { PopulationDensity = null; }
                    //else { PopulationDensity = double.Parse(fields[45]); }

                    //var CountryCode = fields[0];
                    //var Continent = continents.Where(c => c.Name == fields[1]).FirstOrDefault();
                    //var Name = fields[2];


                    //Population = Convert.ToInt32(Population);
                    //LifeExpectancy = LifeExpectancy != null ? Math.Round(double.Parse(fields[57]), 2) : null;
                    //HospitalBedsPerThousand = HospitalBedsPerThousand != null ? Math.Round((double)HospitalBedsPerThousand, 2) : null;
                    //PopulationDensity = PopulationDensity != null ? Math.Round((double)PopulationDensity, 2) : null;
                  
                    countries.Add(new Country()
                    {
                        CountryCode = fields[0],
                        HumanDevelopmentIndex = Math.Round(double.Parse(string.IsNullOrEmpty(fields[58]) ? "0.0" : fields[58]), 2)
                        //Continent = continents.Where(c => c.Name == fields[1]).FirstOrDefault(),
                        //Name = fields[2],
                        //Population = Convert.ToInt32(Population),
                        //LifeExpectancy = LifeExpectancy != null ? Math.Round(double.Parse(fields[57]), 2) : null,
                        //HospitalBedsPerThousand = HospitalBedsPerThousand != null ? Math.Round((double)HospitalBedsPerThousand, 2) : null,
                        //PopulationDensity = PopulationDensity != null ? Math.Round((double)PopulationDensity, 2) : null
                    }) ;
                }
                counter++;
            }
            var countriesX = new List<Country>();

            for (int i = 0; i < countries.Count; i++)
            {
                if (i + 1 != countries.Count)
                {
                    if (countries[i].CountryCode != countries[i + 1].CountryCode)
                    {
                        countriesX.Add(countries[i]);
                    }
                }
            }

            return countriesX;
        }

        public static List<Continent> ReadContinents()
        {
            return new List<Continent>()
            {
                new Continent()
                {
                    Name = "Asia",
                    ContinentCode = "AS"
                },
                new Continent()
                {
                    Name = "Europe",
                    ContinentCode = "EU"
                },
                new Continent()
                {
                    Name = "Antartica",
                    ContinentCode = "AN"
                },
                new Continent()
                {
                    Name = "Africa",
                    ContinentCode = "AF"
                },
                new Continent()
                {
                    Name = "Oceania",
                    ContinentCode = "OC"
                },
                new Continent()
                {
                    Name = "North America",
                    ContinentCode = "NA"
                },
                new Continent()
                {
                    Name = "South America",
                    ContinentCode = "SA"
                }
            };
        }

        public static List<CovidDailySummary> ReadSummaries()
        {
            TextFieldParser parser = new TextFieldParser
                ("D:/Coding studies/MirkosWebsite/Portfolio/Datasets/Covid/CovidLondon.csv");
            parser.SetDelimiters(",");

            var summaries = new List<CovidDailySummary>();
            string[] fields;

            var counter = 0;
            while (!parser.EndOfData)
            {
                fields = parser.ReadFields();
                if (counter != 0 && !string.IsNullOrEmpty(fields[1]))
                {
                    summaries.Add(new CovidDailySummary()
                    {
                        Country = new Country() { Name = fields[2] },
                        Date = DateTime.Parse(fields[3]),
                        TotalCases = Convert.ToInt32(double.Parse(string.IsNullOrEmpty(fields[4]) ? "0.0" : fields[4])),
                        NewCases = Convert.ToInt32(double.Parse(string.IsNullOrEmpty(fields[5]) ? "0" : fields[5])),
                        TotalDeaths = Convert.ToInt32(double.Parse(string.IsNullOrEmpty(fields[7]) ? "0" : fields[7])),
                        NewDeaths = Convert.ToInt32(double.Parse(string.IsNullOrEmpty(fields[8]) ? "0" : fields[8])),
                        RRate = double.Parse(string.IsNullOrEmpty(fields[16]) ? "0.0" : fields[16]),
                        StringencyIndex = double.Parse(string.IsNullOrEmpty(fields[43]) ? "0.0" : fields[43]),
                        GDPPerCapita = double.Parse(string.IsNullOrEmpty(fields[49]) ? "0.0" : fields[49])
                    });

                }
                counter++;
            }
            return summaries;
        }
    }
}
