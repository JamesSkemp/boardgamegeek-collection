using System;
using System.IO;
using System.Net.Http;
using Microsoft.Extensions.Configuration;

namespace boardgamegeek_collection
{
	class Program
	{
		private static IConfigurationRoot configuration;

		static void Main(string[] args)
		{
			var builder = new ConfigurationBuilder()
				.SetBasePath(Directory.GetCurrentDirectory())
				.AddJsonFile("appsettings.json");

			configuration = builder.Build();

			Console.WriteLine(configuration["apiBaseUrl"]);

			GetData();

			Console.WriteLine("Hello World!");

			Console.ReadLine();
			Console.ReadKey();
		}

		static async void GetData()
		{
			string baseUrl = $"{configuration["apiBaseUrl"]}collection/{configuration["defaultUser"]}";
			Console.WriteLine(baseUrl);
			using (HttpClient client = new HttpClient())
			{
				using (HttpResponseMessage res = await client.GetAsync(baseUrl))
				{
					if (res.StatusCode == System.Net.HttpStatusCode.OK)
					{
						using (HttpContent content = res.Content)
						{
							string data = await content.ReadAsStringAsync();
							if (data != null)
							{
								Console.WriteLine(data);
							}
						}
					}
					else
					{
						Console.WriteLine(res.StatusCode.ToString());
					}
				}
			}
		}
	}
}
