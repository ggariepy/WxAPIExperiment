# WxAPIExperiment
This gets the weather report from a JSON API and displays an English version on the screen.

It is a simple demonstration of using System.Net.Http and Newtonsoft.Json.Linq in a .NET 5 app to retrieve and display data obtained from a public API.

The API key is stored in an environment variable on the machine it runs on.  You can get your own for free at https://openweathermap.org/price.

Store the API key in your System-wide environment variables with the keyname "OpenWxMapAPIKey".  Once you have made this addition to the environment variables,
you MUST reboot your machine, else your app will never, ever be able to read it.

In the future I may add text to speech capabilities, if I decide on using Azure, or AWS or...maybe someone will port SAPI to .NET Core.

This project took about four hours to write.
