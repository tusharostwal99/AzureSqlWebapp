using AzureSqlWebapp.Models;
using AzureSqlWebapp.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AzureSqlWebapp.Pages;

public class IndexModel : PageModel
{
    public List<Product> Products;

    public IndexModel(IProductService productService)
    {
        ProductService = productService;
    }

    public IProductService ProductService { get; }

    public void OnGet()
    {
        // ProductService productService = new ProductService();
        Products = ProductService.GetProducts();

    }
}
