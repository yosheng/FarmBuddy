namespace FarmBuddy.Service.ThirdApi.Cwa.Response;

public class ObservationResponse : CwaBaseResponse
{
    public Record Records { get; set; }

    public class Record
    {
        public Locations[] Locations { get; set; }
    }

    public class Locations
    {
        public string DatasetDescription { get; set; }
        public string LocationsName { get; set; }
        public string Dataid { get; set; }
        public Location[] Location { get; set; }
    }

    public class Location
    {
        public string LocationName { get; set; }
        public string Geocode { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public WeatherElement[] WeatherElement { get; set; }
    }

    public class WeatherElement
    {
        public string ElementName { get; set; }
        public Time[] Time { get; set; }
    }

    public class Time
    {
        public string DataTime { get; set; }
        public ElementValue[] ElementValue { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
    }

    public class ElementValue
    {
        public string Temperature { get; set; }
        public string DewPoint { get; set; }
        public string RelativeHumidity { get; set; }
        public string ApparentTemperature { get; set; }
        public string ComfortIndex { get; set; }
        public string ComfortIndexDescription { get; set; }
        public string WindSpeed { get; set; }
        public string BeaufortScale { get; set; }
        public string WindDirection { get; set; }
        public string ProbabilityOfPrecipitation { get; set; }
        public string Weather { get; set; }
        public string WeatherCode { get; set; }
        public string WeatherDescription { get; set; }
    }
}