using System.Collections.Generic;

namespace getCurrentWx
{
    public static class WxDescriptors
    {
        public static Dictionary<int, string> WeatherConditions = new Dictionary<int, string>()
        {
            // Stormy conditions
            { 200 , "a thunderstorm with light rain" },
            { 201 , "a thunderstorm with rain" },
            { 202 , "a thunderstorm with heavy rain" },
            { 210 , "a light thunderstorm" },
            { 211 , "a thunderstorm" },
            { 212 , "a heavy thunderstorm" },
            { 221 , "a ragged thunderstorm" },
            { 230 , "a thunderstorm with light drizzle" },
            { 231 , "a thunderstorm with drizzle" },
            { 232 , "a thunderstorm with heavy drizzle" },
    
        
            // Drizzle conditions
            { 300 , "a light intensity drizzle" },
            { 301 , "drizzle" },
            { 302 , "heavy intensity rain drizzle" },
            { 310 , "light intensity rain drizzle " },
            { 311 , "a rain drizzle" },
            { 312 , "a heavy rain drizzle" },
            { 313 , "a rain and drizzle shower " },
            { 314 , "a heavy rain and drizzle shower " },
            { 321 , "a drizzle shower" },

            // Rainy conditions
            { 500 , "light rain" },  
            { 501 , "moderate rain" },
            { 502 , "heavy rain" },
            { 503 , "very heavy rain" },
            { 504 , "extremely heavy rain" },
            { 511 , "freezing rain" },
            { 520 , "a light rain shower" }, 
            { 521 , "a moderate rain shower" },
            { 522 , "a heavy rain shower" }, 
            { 531 , "a rain shower of varying intensity" },

            // Snowy conditions
            { 600, "light snow" },
            { 601,  "snow" },
            { 602,  "heavy snow" },
            { 611,  "sleet" },
            { 612,  "a light sleet shower " },
            { 613,  "a moderate sleet shower" },
            { 615,  "light rain and snow" },
            { 616,  "moderate rain and snow" },
            { 620,  "a light snow shower" },
            { 621,  "a moderate snow shower" },
            { 622,  "a heavy snow shower" },

            // Atmospheric conditions
            { 701, "Misty" },
            { 711, "Smoky" },
            { 721, "Hazy" },
            { 731, "Dusty" },
            { 741, "Foggy" },
            { 751, "Sandy" },
            { 761, "Dust" },
            { 762, "volcanic ash" },
            { 771, "Squall" },
            { 781, "Tornado" },

            // Cloud conditions
            { 800, "Clear" },
            { 801, "Mostly Clear" },
            { 802, "Scattered Clouds" },
            { 803, "Mostly Cloudy" },
            { 804, "Overcast" }
        };
    }
}
