using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Runtime.Serialization;


using System.Net.Http;
using System.Runtime.Serialization.Json;
using System.IO;

namespace WeatherApp
{
    public class Coord
    {
        public double lon { get; set; }
        public double lat { get; set; }
    }

    public class City
    {
        public int id { get; set; }
        public string name { get; set; }
        public Coord coord { get; set; } 
        public string country { get; set; }
        public int population { get; set; }

        public string CityCountry
        {
            get
            {
                return name + ", " + country;
            }
        }
    }

    public class Temp
    {
        public double day { get; set; }
        public double min { get; set; }
        public double max { get; set; }
        public double night { get; set; }
        public double eve { get; set; }
        public double morn { get; set; }
    }

    public class Weather
    {
        public int id { get; set; }
        public string main { get; set; }
        public string description { get; set; }
        public string icon { get; set; }
    }

    public class DayElem
    {
        private int _dt;
        public int dt
        {
            get { return _dt; }
            set
            {
                _dt = value;
                Date = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).AddSeconds(value);
            }
        }

        private DateTime _date;
        public DateTime Date
        {
            get { return _date; }
            set { _date = value; }
        }

        public Temp temp { get; set; }
        public double pressure { get; set; }
        public int humidity { get; set; }
        public List<Weather> weather { get; set; }
        public double speed { get; set; }
        public int deg { get; set; }
        public int clouds { get; set; }
        public double rain { get; set; }
        public double? snow { get; set; }

        public string ImgPath
        {
            get
            {
                return "/Icons/" + weather[0].icon + ".png";
            }
        }

        public string DateFormated
        {
            get
            {
                return Date.DayOfWeek.ToString().Substring(0, 3) + " " + Date.Day.ToString();
            }
        }
    }

    [DataContract]
    public class RootObject
    {
        [DataMember]
        public City city { get; set; }
        [DataMember]
        public string cod { get; set; }
        [DataMember]
        public double message { get; set; }
        [DataMember]
        public int cnt { get; set; }
        [DataMember]
        public List<DayElem> list { get; set; }
    }



    public static class WeatherApiClient
    {
        public static async Task<RootObject> GetWeatherForecast(string url)
        {
            try
            {
                var httpClient = new HttpClient();
                var content = await httpClient.GetStringAsync(url);
                DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(RootObject));
                using (var ms = new MemoryStream(Encoding.Unicode.GetBytes(content)))
                {
                    var weatherData = (RootObject)serializer.ReadObject(ms);
                    return weatherData;
                }
            }
            catch (Exception e)
            {
                LogAPIException(e);
                return null;
            }

        }

        public static string GetAPIKey()
        {
            //hardcoded for demonstration
            return "92950373bdc96ef2546a5f529d47ffbb";
        }

        public static void LogAPIException(Exception e)
        {
            //placeholder
        }
    }

    public sealed class StringFormatter : Windows.UI.Xaml.Data.IValueConverter
    {
        public string StringFormat { get; set; }

        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (!String.IsNullOrEmpty(StringFormat))
                return String.Format(StringFormat, value);

            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }

    public class StringFormatConverter : Windows.UI.Xaml.Data.IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value == null)
                return null;

            if (parameter == null)
                return value;

            return string.Format((string)parameter, value);
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}