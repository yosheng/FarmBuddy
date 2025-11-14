namespace FarmBuddy.Service.ThirdApi.Cwa.Response;

public class GeneralWeatherResponse : CwaBaseResponse
{
    public Record Records { get; set; }

    public class Record
    {
        public string DatasetDescription { get; set; }
        public Location[] Location { get; set; }
    }

    public class Location
    {
        public string LocationName { get; set; }
        public WeatherElement[] WeatherElement { get; set; }
    }

    public class WeatherElement
    {
        public string ElementName { get; set; }
        public Time[] Time { get; set; }
    }

    public class Time
    {
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public Parameter Parameter { get; set; }
    }

    public class Parameter
    {
        public string ParameterName { get; set; }
        public string ParameterValue { get; set; }
        public string ParameterUnit { get; set; }
    }
}