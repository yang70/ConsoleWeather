using System.Text.Json;

namespace Helpers
{
	public static class Formator
	{
		public static string FormatWeatherResponse(JsonDocument weatherData)
		{
			string location = weatherData.RootElement.GetProperty("name").GetString() ?? "Unknown location";
			string weatherShort = weatherData.RootElement.GetProperty("weather")[0].GetProperty("main").GetString() ?? "Unknown weather";
			string weatherDescription = weatherData.RootElement.GetProperty("weather")[0].GetProperty("description").GetString() ?? "Unknown description";
			float temp = weatherData.RootElement.GetProperty("main").GetProperty("temp").GetSingle();
			float feelsLike = weatherData.RootElement.GetProperty("main").GetProperty("feels_like").GetSingle();
			float windSpeed = weatherData.RootElement.GetProperty("wind").GetProperty("speed").GetSingle();

			string formattedString = "";
			formattedString += $"{location} Weather\n";
			formattedString += "----------\n";
			formattedString += $"{weatherShort} - {weatherDescription}\n";
			formattedString += $"{temp}°F (feels like {feelsLike}°F)\n";
			formattedString += $"Wind: {windSpeed} mph\n";

			return formattedString;
		}
	}
}