using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HeavyClient.Routing;
using System.Text.Json;

namespace HeavyClient
{
    internal class Program
    {
        static void Main(string[] args)
        {
            RoutingServiceClient routing = new RoutingServiceClient();

            Console.WriteLine("appuyer sur une touche pour lancer le test");
            Console.ReadKey();

            //Test sans cache
            Console.WriteLine("\nLancement du test sans cache . . .");
            //Recupération des stations
            var starting_time = DateTime.Now;
            List<Station> stations = JsonSerializer.Deserialize<List<Station>>(routing.GetStations());
            var ending_time = DateTime.Now;
            var TotalTimeWithoutCache = ending_time - starting_time;
            Console.WriteLine("\nRécupération des " + stations.Count + " stations en " + (ending_time - starting_time) + ".");
            
            //Recupération d'un chemin
            starting_time = DateTime.Now;
            string posistionsJson = routing.GetPathAsync("Nice France", "Paris France").Result;
            List<List<Position>> positions = JsonSerializer.Deserialize<List<List<Position>>>(posistionsJson);
            ending_time = DateTime.Now;
             TotalTimeWithoutCache += ending_time - starting_time;
            Console.WriteLine("Récupération du chemin (" + (positions.Count+1) + " étapes) en " + (ending_time - starting_time) + ".");
            Console.WriteLine("\nTemps total du test sans cache : " + TotalTimeWithoutCache+"\n\n");


            Console.WriteLine("appuyer sur une touche pour continuer");
            Console.ReadKey();


            //Test avec cache
            Console.WriteLine("\nLancement du 2eme test (avec le cache si fait assez rapidement) . . .");
            //Recupération des stations
            starting_time = DateTime.Now;
            stations = JsonSerializer.Deserialize<List<Station>>(routing.GetStations());
            ending_time = DateTime.Now;
            var TotalTimeWithCache = ending_time - starting_time;
            Console.WriteLine("\nRécupération des " + stations.Count + " stations en " + (ending_time - starting_time) + ".");

            //Recupération d'un chemin
            starting_time = DateTime.Now;
            posistionsJson = routing.GetPathAsync("Nice France", "Paris France").Result;
            positions = JsonSerializer.Deserialize<List<List<Position>>>(posistionsJson);
            ending_time = DateTime.Now;
            TotalTimeWithCache += ending_time - starting_time;
            Console.WriteLine("Récupération du chemin (" + (positions.Count+1) + " étapes) en " + (ending_time - starting_time) + ".");
            Console.WriteLine("\nTemps total du 2eme test : " + TotalTimeWithCache + "\n\n");


            Console.WriteLine("appuyer sur une touche pour quitter");
            Console.ReadKey();


        }
    }
}
