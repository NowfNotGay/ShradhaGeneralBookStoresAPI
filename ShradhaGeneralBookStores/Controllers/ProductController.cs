using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShradhaGeneralBookStores.Models;
using ShradhaGeneralBookStores.Service.Interface;

namespace ShradhaGeneralBookStores.Controllers;
[Route("api/[controller]")]
[ApiController]
public class ProductController : ControllerBase
{
    private readonly IServiceCRUD<Product> _serviceCRUD;
    private readonly IProductService _productService;

    public ProductController(IServiceCRUD<Product> serviceCRUD, IProductService productService)
    {
        _serviceCRUD = serviceCRUD;
        _productService = productService;
    }

    [Produces("application/json")]
    [HttpGet("Read")]
    public IActionResult Read()
    {
        try
        {
            return Ok(_serviceCRUD.Read());
        }
        catch
        {
            return BadRequest();
        }
    }

    [Consumes("application/json")]
    [Produces("application/json")]
    [HttpGet("Get")]
    public IActionResult Read(int id)
    {
        try
        {
            return Ok(_serviceCRUD.Get(id));
        }
        catch
        {
            return BadRequest();
        }
    }

    [Consumes("application/json")]
    [Produces("application/json")]
    [HttpPost("Create")]
    public IActionResult Create([FromBody] ProductAPI product)
    {
        try
        {
            product.CreatedAt = DateTime.Now;
            product.UpdatedAt = DateTime.Now;

            return Ok();
        }
        catch
        {
            return BadRequest();
        }
    }


    [Consumes("application/json")]
    [Produces("application/json")]
    [HttpPost("Add")]
    public IActionResult Add([FromBody] ProductAPI product)
    {
        try
        {
            product.CreatedAt = DateTime.Now;
            product.UpdatedAt = DateTime.Now;
            return Ok(_productService.AddProduct(product));
        }
        catch
        {
            return BadRequest();
        }
    }
}
