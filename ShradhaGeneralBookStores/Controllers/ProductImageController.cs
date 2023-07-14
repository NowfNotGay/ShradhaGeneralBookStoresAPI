using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShradhaGeneralBookStores.Models;
using ShradhaGeneralBookStores.Service.Interface;

namespace ShradhaGeneralBookStores.Controllers;
[Route("api/[controller]")]
[ApiController]
public class ProductImageController : ControllerBase
{
    private readonly IServiceCRUD<ProductImage> _serviceCRUD;
    private readonly IProductImageService _productImageService;

    public ProductImageController(IServiceCRUD<ProductImage> serviceCRUD, IProductImageService productImageService)
    {
        _serviceCRUD = serviceCRUD;
        _productImageService = productImageService;
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
    [Produces("application/json")]
    [HttpPost("Create/{productId}")]
    public IActionResult Create( IFormFile[] photo, int productId)

    {
        try
        {
            return Ok(_productImageService.Add(productId,photo));
        }
        catch
        {
            return BadRequest();
        }
    }

    [Produces("application/json")]
    [HttpDelete("Delete")]
    public IActionResult Delete(int id)
    {
        try
        {
            return Ok(_serviceCRUD.Delete(id));
        }
        catch
        {
            return BadRequest();
        }
    }

    [Produces("application/json")]
    [HttpPut("Update/{productId}")]
    public IActionResult Update(IFormFile[] photo, int productId)
    {
        try
        {
            return Ok(_productImageService.Update(productId, photo));
        }
        catch
        {
            return BadRequest();
        }
    }


}
