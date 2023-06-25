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

    public ProductImageController(IServiceCRUD<ProductImage> serviceCRUD)
    {
        _serviceCRUD = serviceCRUD;
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
    [HttpPost("Create")]
    public IActionResult Create([FromBody] ProductImage productImage)

    {
        try
        {
            productImage.CreatedAt = DateTime.Now;
            productImage.UpdatedAt = DateTime.Now;
            return Ok(_serviceCRUD.Create(productImage));
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

    [Consumes("application/json")]
    [Produces("application/json")]
    [HttpPut("Update")]
    public IActionResult Update([FromBody] ProductImage productImage)
    {
        try
        {
            productImage.UpdatedAt = DateTime.Now;
            return Ok(_serviceCRUD.Update(productImage));
        }
        catch
        {
            return BadRequest();
        }
    }
}
