using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using CraftsWebsite.Models;
using Microsoft.AspNetCore.Hosting;

namespace CraftsWebSite.Services
{
	public class JsonFileProductService
	{
		public JsonFileProductService(IWebHostEnvironment webHostEnvironment)
		{
			WebHostEnvironment = webHostEnvironment;
		}

		public IWebHostEnvironment WebHostEnvironment { get; }

		private string JsonFileName => Path.Combine(WebHostEnvironment.WebRootPath, "data", "products.json");

		public IEnumerable<Product> GetProducts()
		{
			using var jsonFileReader = File.OpenText(JsonFileName);
			return JsonSerializer.Deserialize<Product[]>(jsonFileReader.ReadToEnd(),
			    new JsonSerializerOptions
			    {
				    PropertyNameCaseInsensitive = true
			    });
		}

		public void AddRating(string productId,int rating)
		{
			var products = GetProducts();

			Product product = products.First(x => x.Id == productId);
			if (product.Ratings == null)
			{
				product.Ratings = new int[] { rating };
			}
			else
			{
				var ratings = product.Ratings.ToList();
				ratings.Add(rating);
				product.Ratings = ratings.ToArray();
			}
			product.Rating = (float?)Enumerable.Average(product.Ratings);
			using var outputStream = File.OpenWrite(JsonFileName);

			JsonSerializer.Serialize<IEnumerable<Product>>(
			    new Utf8JsonWriter(outputStream, new JsonWriterOptions
			    {
				    SkipValidation = true,
				    Indented = true
			    }),
			    products
			);
		}
	}
}