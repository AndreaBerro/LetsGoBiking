using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Routing.Proxy;
using System.Text.Json;
using System.Net.Http;
using System.Device.Location;
using System.Linq;

namespace Routing
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "RoutingService" in both code and config file together.
    public class RoutingService : IRoutingService
    {
        private ProxyServiceIClient proxyClient = new ProxyServiceIClient();
        private static HttpClient httpClient = new HttpClient();
        private List<Station> stations = new List<Station>();

        private const string API_PATH = "https://api.openrouteservice.org";
        private const string API_KEY = "5b3ce3597851110001cf6248c2e2a000ad224ebd96ea4655501ae40b";

        //renvoie les stations récupéré par le proxy
        public string GetStations()
        {
            string result = proxyClient.GetStations();
            stations = JsonSerializer.Deserialize<List<Station>>(result);
            return result;
        }

        public async Task<string> GetPathAsync(string location1, string location2)
        {
            //Test si on a déja récupéré les stations
            if (!(stations.Count>0))
            {
                string stationsString = proxyClient.GetStations();
                stations = JsonSerializer.Deserialize<List<Station>>(stationsString);
            }

            List<Coord> importantPoint = new List<Coord>();

            GeoCode geoCode1 = await getGeoCode(location1);
            GeoCode geoCode2 = await getGeoCode(location2);

            //Créé les coordonnée qui seront renvoyé
            Coord startingPosition = new Coord(geoCode1.features[0].geometry.coordinates[0], geoCode1.features[0].geometry.coordinates[1]);
            Coord endingPosition = new Coord(geoCode2.features[0].geometry.coordinates[0], geoCode2.features[0].geometry.coordinates[1]);

            //ajoute le départ au resultat
            importantPoint.Add(startingPosition);
            
            // Utilisation de GeoCoordinate pour le calcul de distance
            GeoCoordinate startingPoint = new GeoCoordinate(startingPosition.latitude, startingPosition.longitude);
            GeoCoordinate endingPoint = new GeoCoordinate(endingPosition.latitude, endingPosition.longitude);
            double distStartingEnding = startingPoint.GetDistanceTo(endingPoint);

            

            //On cherche la station de départ (s'il y en a besoin et si elle est dispo)
            Coord startingStation = null;
            //dict des stations par rapport a leur proximité avec le départ
            Dictionary<double, Station> stationDict = getStationsDict(startingPoint);

            foreach (KeyValuePair<double,Station> kvp in stationDict.OrderBy(el => el.Key))
            {
                //test s'il reste un vélo disponible et que la station n'est pas trop éloigné
                if (distStartingEnding > kvp.Key && kvp.Value.totalStands.availabilities.bikes>0 && kvp.Value.status == "OPEN")
                {
                    startingStation = new Coord(kvp.Value.position.longitude,kvp.Value.position.latitude);
                    break;
                }
            }

            //Test si on a réussi a trouvé un station de départ
            if (startingStation != null)
            {
                //On cherche une station d'arrivée
                Coord endingStation = null;
                //dict des stations par rapport a leur proximité avec le départ
                stationDict = getStationsDict(endingPoint);

                foreach (KeyValuePair<double,Station> kvp in stationDict.OrderBy(el => el.Key))
                {
                    //test s'il y a de la place pour un velo et que la station n'est pas trop éloigné
                    if (distStartingEnding > kvp.Key && kvp.Value.totalStands.availabilities.stands>0 && kvp.Value.status == "OPEN")
                    {
                        endingStation = new Coord(kvp.Value.position.longitude, kvp.Value.position.latitude);
                        break;
                    }
                }

                //Si on a trouvé une station de départ et d'arriver différente on les rajoute au resultat
                if (endingStation != null && 
                   !(startingStation.longitude == endingStation.longitude && startingStation.latitude == endingStation.latitude))
                {
                    importantPoint.Add(startingStation);
                    importantPoint.Add(endingStation);
                }
            }
            //ajoute la destination au resultat
            importantPoint.Add(endingPosition);

            List<List<Coord>> result = new List<List<Coord>>();
            // Ajoute chaque étape du chemin au resultat selon les disponibilité des stations
            if (importantPoint.Count == 4)
            {
                //Séparer le résultat en trois dans ce cas si nous permet de savoir si on a reussi a trouver des stations
                result.Add(getRealPath("foot-walking", importantPoint[0], importantPoint[1]).Result);
                result.Add(getRealPath("cycling-regular", importantPoint[1], importantPoint[2]).Result);
                result.Add(getRealPath("foot-walking", importantPoint[2], importantPoint[3]).Result);
            }
            else if (importantPoint.Count == 2)
            {
                result.Add(getRealPath("foot-walking", importantPoint[0], importantPoint[1]).Result);
            }

            string resultJson = JsonSerializer.Serialize(result);
            return resultJson;
        }

        // Method qui permet d'obtenir la longitude et la latitude d'une address
        private async Task<GeoCode> getGeoCode(string location)
        {
            string geoCodeUri = API_PATH + "/geocode/search?api_key=" + API_KEY + "&text='" + location + "'";
            var response = await httpClient.GetAsync(geoCodeUri);
            string jsonResponse = await response.EnsureSuccessStatusCode().Content.ReadAsStringAsync();
            GeoCode geoCode = JsonSerializer.Deserialize<GeoCode>(jsonResponse);
            return geoCode;
        }

        // Method qui permet d'obtenir le chemin qui suit la route entre 2 points
        private async Task<List<Coord>> getRealPath(string movingType, Coord coord1, Coord coord2)
        {
            List<Coord> result = new List<Coord>();

            // Création de certaine partie d'uri ou il faut remplacer les virgule des doubles par des point pour etre accepter par l'api
            string stringCoord1 = coord1.longitude.ToString() + "!" + coord1.latitude.ToString();
            stringCoord1 = stringCoord1.Replace(',', '.');
            stringCoord1 = stringCoord1.Replace('!', ',');

            string stringCoord2 = coord2.longitude.ToString() + "!" + coord2.latitude.ToString();
            stringCoord2 = stringCoord2.Replace(',', '.');
            stringCoord2 = stringCoord2.Replace('!', ',');

            string directionUri = API_PATH + "/v2/directions/" + movingType + "?api_key=" + API_KEY + "&start=" + stringCoord1 + "&end=" + stringCoord2;
            var response = await httpClient.GetAsync(directionUri);
            string jsonResponse = await response.EnsureSuccessStatusCode().Content.ReadAsStringAsync();

            Direction direction = JsonSerializer.Deserialize<Direction>(jsonResponse);
            
            foreach (List<double> coord in direction.features[0].geometry.coordinates)
            {
                result.Add(new Coord(coord[0], coord[1]));
            }

            return result;

        }

        // Metod qui retourne un dict de station avec en clef la distance avec le point préciser
        private Dictionary<double,Station> getStationsDict(GeoCoordinate point)
        {
            Dictionary<double,Station> result = new Dictionary<double,Station>();
            foreach (Station station in stations)
            {
                GeoCoordinate geoCoordinateStation = new GeoCoordinate(station.position.latitude, station.position.longitude);
                double dist = point.GetDistanceTo(geoCoordinateStation);
                while (result.Keys.Contains(dist)) { dist++; }
                result.Add(dist, station);
            }
            return result;
        }

    }
}
