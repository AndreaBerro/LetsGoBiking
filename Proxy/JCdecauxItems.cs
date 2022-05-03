using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Net.Http;

namespace Proxy
{
    public class Contract
    {
        public string name { get; set; }
        public string commercial_name { get; set; }
        public List<string> cities { get; set; }
        public string country_code { get; set; }

    }

    public class Position
    {
        public double latitude { get; set; }
        public double longitude { get; set; }
    }

    public class Availabilities
    {
        public int bikes { get; set; }
        public int stands { get; set; }
        public int mechanicalBikes { get; set; }
        public int electricalBikes { get; set; }
        public int electricalInternalBatteryBikes { get; set; }
        public int electricalRemovableBatteryBikes { get; set; }
    }

    public class TotalStands
    {
        public Availabilities availabilities { get; set; }
        public int capacity { get; set; }
    }

    public class MainStands
    {
        public Availabilities availabilities { get; set; }
        public int capacity { get; set; }
    }

    public class OverflowStands
    {
        public Availabilities availabilities { get; set; }
        public int capacity { get; set; }
    }

    public class Station
    {
        public int number { get; set; }
        public string contractName { get; set; }
        public string name { get; set; }
        public string address { get; set; }
        public Position position { get; set; }
        public bool banking { get; set; }
        public bool bonus { get; set; }
        public string status { get; set; }
        public string lastUpdate { get; set; }
        public bool connected { get; set; }
        public bool overflow { get; set; }
        public object shape { get; set; }
        public TotalStands totalStands { get; set; }
        public MainStands mainStands { get; set; }
        public OverflowStands overflowStands { get; set; }
    }

    public class Stations
    {
        public List<Station> stations = new List<Station>();
        string API_KEY = "2c73b14e4aec1c1fc64a0a5c7b3c8f149ba3f550";
        HttpClient httpClient = new HttpClient();

        public Stations()
        {
            try
            {
                List<Station> stations = JsonSerializer.Deserialize<List<Station>>(retrieveStations().Result);
                this.stations = stations;
            }
            catch (JsonException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (Exception ex) 
            {
                Console.WriteLine(ex.Message);
            }
        }

        private async Task<string> retrieveStations()
        {
            HttpResponseMessage response = await httpClient.GetAsync("https://api.jcdecaux.com/vls/v3/stations?apiKey=" + API_KEY);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }
    }
}
