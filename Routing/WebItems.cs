using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Net.Http;

namespace Routing
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

    public class Lang
    {
        public string name { get; set; }
        public string iso6391 { get; set; }
        public string iso6393 { get; set; }
        public string via { get; set; }
        public bool defaulted { get; set; }
    }

    public class ParsedText
    {
        public string housenumber { get; set; }
        public string street { get; set; }
        public string city { get; set; }
    }

    public class Query
    {
        public string text { get; set; }
        public int size { get; set; }
        public bool @private { get; set; }
        public Lang lang { get; set; }
        public int querySize { get; set; }
        public string parser { get; set; }
        public ParsedText parsed_text { get; set; }
    }

    public class Engine
    {
        public string name { get; set; }
        public string author { get; set; }
        public string version { get; set; }
    }

    public class Geocoding
    {
        public string version { get; set; }
        public string attribution { get; set; }
        public Query query { get; set; }
        public Engine engine { get; set; }
        public long timestamp { get; set; }
    }

    public class Geometry
    {
        public string type { get; set; }
        public List<double> coordinates { get; set; }
    }

    public class Properties
    {
        public string id { get; set; }
        public string gid { get; set; }
        public string layer { get; set; }
        public string source { get; set; }
        public string source_id { get; set; }
        public string name { get; set; }
        public string street { get; set; }
        public double confidence { get; set; }
        public string match_type { get; set; }
        public string accuracy { get; set; }
        public string country { get; set; }
        public string country_gid { get; set; }
        public string country_a { get; set; }
        public string macroregion { get; set; }
        public string macroregion_gid { get; set; }
        public string macroregion_a { get; set; }
        public string region { get; set; }
        public string region_gid { get; set; }
        public string region_a { get; set; }
        public string macrocounty { get; set; }
        public string macrocounty_gid { get; set; }
        public string county { get; set; }
        public string county_gid { get; set; }
        public string localadmin { get; set; }
        public string localadmin_gid { get; set; }
        public string locality { get; set; }
        public string locality_gid { get; set; }
        public string continent { get; set; }
        public string continent_gid { get; set; }
        public string label { get; set; }
    }

    public class Feature
    {
        public string type { get; set; }
        public Geometry geometry { get; set; }
        public Properties properties { get; set; }
        public List<double> bbox { get; set; }
    }

    public class GeoCode
    {
        public Geocoding geocoding { get; set; }
        public string type { get; set; }
        public List<Feature> features { get; set; }
        public List<double> bbox { get; set; }
    }

    public class Coord
    {
        public double latitude { get; set; }
        public double longitude { get; set; }

        public Coord(double lon, double lat) 
        {
            longitude = lon;
            latitude = lat;
        }
    }

    public class Step
    {
        public double distance { get; set; }
        public double duration { get; set; }
        public int type { get; set; }
        public string instruction { get; set; }
        public string name { get; set; }
        public List<int> way_points { get; set; }
        public int? exit_number { get; set; }
    }

    public class Segment
    {
        public double distance { get; set; }
        public double duration { get; set; }
        public List<Step> steps { get; set; }
    }

    public class Summary
    {
        public double distance { get; set; }
        public double duration { get; set; }
    }

    public class Properties2
    {
        public List<Segment> segments { get; set; }
        public Summary summary { get; set; }
        public List<int> way_points { get; set; }
    }

    public class Geometry2
    {
        public List<List<double>> coordinates { get; set; }
        public string type { get; set; }
    }

    public class Feature2
    {
        public List<double> bbox { get; set; }
        public string type { get; set; }
        public Properties2 properties { get; set; }
        public Geometry2 geometry { get; set; }
    }

    public class Query2
    {
        public List<List<double>> coordinates { get; set; }
        public string profile { get; set; }
        public string format { get; set; }
    }

    public class Engine2
    {
        public string version { get; set; }
        public DateTime build_date { get; set; }
        public DateTime graph_date { get; set; }
    }

    public class Metadata
    {
        public string attribution { get; set; }
        public string service { get; set; }
        public long timestamp { get; set; }
        public Query2 query { get; set; }
        public Engine2 engine { get; set; }
    }

    public class Direction
    {
        public string type { get; set; }
        public List<Feature2> features { get; set; }
        public List<double> bbox { get; set; }
        public Metadata metadata { get; set; }
    }

}
