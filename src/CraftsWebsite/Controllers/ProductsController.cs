using CraftsWebsite.Models;
using CraftsWebSite.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CraftsWebsite.Controllers
{
	[ApiController]
	[Route("/products")]
	public class ProductsController : ControllerBase
	{
		public ProductsController(JsonFileProductService productService) =>
		    ProductService = productService;
		public JsonFileProductService ProductService { get; }

		[HttpGet]
		public IEnumerable<Product> Get() => ProductService.GetProducts();

		//[HttpPatch]"[FromBody]"
		[Route("Rate")]
		[HttpGet]
		public ActionResult Get(
			[FromQuery]string ProductId,
			[FromQuery]int Rating
			)
		{
			ProductService.AddRating(ProductId,Rating);
			return Ok();
		}
	}
}
