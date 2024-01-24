using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;

public static class Weather
{
  private static readonly HttpClient client = new HttpClient();

  public static string ByZip(string[] args)
  {
    int zip = Helpers.Validator.GetZip(args);
    JsonDocument locationData = GetLocation(zip).Result;
    float lat = locationData.RootElement.GetProperty("lat").GetSingle();
    float lon = locationData.RootElement.GetProperty("lon").GetSingle();

    JsonDocument weatherData = GetWeather(lat, lon).Result;
    return Helpers.Formator.FormatWeatherResponse(weatherData);
  }

  private static async Task<JsonDocument> GetLocation(int zip)
  {
    HttpResponseMessage response = await client.GetAsync(GetLocationURL(zip));

    if (response.IsSuccessStatusCode)
    {
      return await JsonDocument.ParseAsync(await response.Content.ReadAsStreamAsync());
    }

    Console.WriteLine("Error getting location from US zip code.");
    Environment.Exit(1);

    return null!;
  }

  private static async Task<JsonDocument> GetWeather(float lat, float lon)
  {
    HttpResponseMessage response = await client.GetAsync(GetWeatherURL(lat, lon));

    if (response.IsSuccessStatusCode)
    {
      return await JsonDocument.ParseAsync(await response.Content.ReadAsStreamAsync());
    }

    Console.WriteLine("Error getting weather from lat and lon.");
    Environment.Exit(1);

    return null!;
  }

  private static string AppId()
  {
    string? key = Environment.GetEnvironmentVariable("OPEN_WEATHER_API_KEY");

    if (key is null)
    {
      Console.WriteLine("Please set the OPEN_WEATHER_API_KEY environment variable.");
      Environment.Exit(1);
    }

    return key;
  }

  private static string GetLocationURL(int zip)
  {
    return $"https://api.openweathermap.org/geo/1.0/zip?zip={zip},us&appid={AppId()}";
  }

  private static string GetWeatherURL(float lat, float lon)
  {
    return $"https://api.openweathermap.org/data/2.5/weather?lat={lat}&lon={lon}&units=imperial&appid={AppId()}";
  }
}
