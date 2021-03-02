using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;
using System.Text;

namespace getCurrentWx
{
    public class CallWXApi
    {
        public bool ProcessComplete = false;
        public JObject WeatherReport;
        public string City;
        public int WindSpeed;
        public int? WindGust;
        public int WindDirectionDegrees;
        public int Temperature;
        public int FeelsLike;
        public int Humiditypct;
        public decimal BarometricPressure;
        public string CurrentConditions;
        public string CurrentCity;
        public string ReportTime;
        public int Visibility;
        public DateTime Sunrise;
        public DateTime Sunset;

        public CallWXApi(string city="Sterling Heights")
        {
            City = city;
            RetrieveProcessWxReport();
        }

        public async void RetrieveProcessWxReport()
        {
            var WeatherJSON = await GetCurrentLocalWeather();
            WeatherReport = JObject.Parse(WeatherJSON);
            DecodeWeatherConditions();
            Console.WriteLine(IssueReport());
            ProcessComplete = true;
        }

        public string IssueReport()
        {
            StringBuilder mainreport = new StringBuilder();
            mainreport.Append($"In {CurrentCity} at {DateTime.Now.ToShortTimeString()}, {CurrentConditions}.\r\n");
            mainreport.Append($"The wind is coming in from the {CompassDegreesToText(WindDirectionDegrees)} at {WindDirectionDegrees} degrees.\r\n");  
            if ((WindGust ?? 0) != 0)
            {
                mainreport.Append($"The wind speed is {WindSpeed} miles per hour, gusting at {WindGust} miles per hour\r\n");
            }
            else
            { 
                mainreport.Append($"The wind speed is { WindSpeed } miles per hour.\r\n");
            }

            mainreport.Append($"The current temperature is {Temperature} degrees fahrenheit, and it feels like {FeelsLike} degrees fahrenheit.\r\n");
            mainreport.Append($"The relative humidity is {Humiditypct} percent.\r\n");
            mainreport.Append($"The barometric pressure is {BarometricPressure:F} inches of mercury.\r\n");
            mainreport.Append($"Visibility is {Visibility} feet.\r\n");

            string sunrise = (Sunrise < DateTime.Now) ? $"The sunrise was at {Sunrise.ToShortTimeString()}" :
                                                        $"The sunrise will be at {Sunrise.ToShortTimeString()}";

            string sunset = (Sunset > DateTime.Now) ? $"The sunset will be at {Sunset.ToShortTimeString()}" :
                                                      $"The sunset was at {Sunset.ToShortTimeString()}";

            mainreport.Append("\r\n");
            mainreport.Append(sunrise);
            mainreport.Append("\r\n");
            mainreport.Append(sunset);

            return mainreport.ToString();
        }

        public string CompassDegreesToText(int degrees)
        {
            string retval = string.Empty;

            string[] dirs = new string[]
            { 
                "north", 
                "north north east", 
                "north east", 
                "east north east", 
                "east", 
                "east south east", 
                "south east", 
                "south south east", 
                "south", 
                "south south west", 
                "south west", 
                "west south west", 
                "west", 
                "west north west", 
                "north west", 
                "north north west"
             };
            int index = Convert.ToInt32((degrees + 11.25) / 22.5);
            return dirs[index % 16];
        }

        public void DecodeWeatherConditions()
        {
            int conditionID = (int)WeatherReport["weather"][0]["id"];
            CurrentCity = (string)WeatherReport["name"];
            CurrentConditions = "Conditions are " + WxDescriptors.WeatherConditions[conditionID];
            WindSpeed = (int)WeatherReport["wind"]["speed"];
            if (WeatherReport["wind"]["gust"] != null)
            {
                WindGust = (int?)WeatherReport["wind"]["gust"];
            }
            WindDirectionDegrees = (int)WeatherReport["wind"]["deg"];
            Temperature = (int)WeatherReport["main"]["temp"];
            FeelsLike = (int)WeatherReport["main"]["feels_like"];
            Humiditypct = (int)WeatherReport["main"]["humidity"];
            BarometricPressure = (decimal)((int)(WeatherReport["main"]["pressure"]) / 33.86);
            Visibility = (int)WeatherReport["visibility"];
            long sunrisesecs = (long)WeatherReport["sys"]["sunrise"];
            long sunsetsecs = (long)WeatherReport["sys"]["sunset"];
            long reporttime = (long)WeatherReport["dt"];
            DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            ReportTime = $"At {dtDateTime.AddSeconds(sunrisesecs).ToLocalTime().ToShortTimeString()}";
            Sunrise = dtDateTime.AddSeconds(sunrisesecs).ToLocalTime();
            Sunset = dtDateTime.AddSeconds(sunsetsecs).ToLocalTime();
        }

        public async Task<string> GetCurrentLocalWeather()
        {
            var APIKey = Environment.GetEnvironmentVariable("OpenWxMapAPIKey");

            var HttpClient = new HttpClient();
            var APIRequestURI = $"https://api.openweathermap.org/data/2.5/weather?"
                                + $"q={City}&appid={APIKey}&units=imperial";
            var content = await HttpClient.GetAsync(APIRequestURI, HttpCompletionOption.ResponseContentRead);
            var json = await content.Content.ReadAsStringAsync();
            return json;
        }
    }
}