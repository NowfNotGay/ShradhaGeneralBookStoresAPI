using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShradhaGeneralBookStores.Models;
using ShradhaGeneralBookStores.Service.Interface;

namespace ShradhaGeneralBookStores.Controllers;
[Route("api/[controller]")]
[ApiController]
public class OrderStatusController : ControllerBase
{
    private readonly IServiceCRUD<OrderStatus> _serviceCRUD;

    public OrderStatusController(IServiceCRUD<OrderStatus> serviceCRUD)
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
    public IActionResult Create([FromBody] OrderStatus orderStatus)

    {
        try
        {
            orderStatus.CreatedAt = DateTime.Now;
            orderStatus.UpdatedAt = DateTime.Now;
            return Ok(_serviceCRUD.Create(orderStatus));
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
    public IActionResult Update([FromBody] OrderStatus orderStatus)
    {
        try
        {
            orderStatus.UpdatedAt = DateTime.Now;
            return Ok(_serviceCRUD.Update(orderStatus));
        }
        catch
        {
            return BadRequest();
        }
    }
}
