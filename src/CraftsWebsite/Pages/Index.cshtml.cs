using CraftsWebsite.Models;
using CraftsWebsite.Services;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CraftsWebsite.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger,
            JsonFileProductService productService)
        {
            _logger = logger;
            ProductService = productService;
        }

        public JsonFileProductService ProductService { get; }
        public IEnumerable<Product>? Products { get; private set; }

        public void OnGet() => Products = ProductService.GetProducts();
        public float Average(int[] ints)
                {
                        float sum = 0;
                        for(int i = 0; i < ints.Length; i++)
                        {
                                sum += ints[i];
                        }
                        return (float)Math.Round(sum/ints.Length,1);
                }
    }
}