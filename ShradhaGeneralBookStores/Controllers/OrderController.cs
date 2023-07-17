using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShradhaGeneralBookStores.Models;
using ShradhaGeneralBookStores.Models.ModelTemp;
using ShradhaGeneralBookStores.Service.Interface;

namespace ShradhaGeneralBookStores.Controllers;
[Route("api/[controller]")]
[ApiController]
public class OrderController : ControllerBase
{
    private readonly IServiceCRUD<Order> _serviceCRUD;
    private readonly IOrderService _orderService;
    public OrderController(IServiceCRUD<Order> serviceCRUD, IOrderService orderService)
    {
        _serviceCRUD = serviceCRUD;
        _orderService = orderService;
    }
    [Produces("application/json")]
    [HttpGet("Read")]
    public IActionResult Read()
    {
        try
        {
            return Ok(_orderService.Read());
        }
        catch
        {
            return BadRequest();
        }
    }

    [Produces("application/json")]
    [HttpGet("Get")]
    public IActionResult Get(int id)
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

    [Produces("application/json")]
    [HttpGet("Paid")]
    public IActionResult Paid(int id)
    {
        try
        {
            return Ok(_orderService.Paid(id));
        }
        catch
        {
            return BadRequest();
        }
    }

    [Produces("application/json")]
    [HttpGet("GetByAccountId")]
    public IActionResult GetByAccountId(int id)
    {
        try
        {
            return Ok(_orderService.GetByAccountId(id));
        }
        catch
        {
            return BadRequest();
        }
    }

    [Produces("application/json")]
    [HttpGet("GetById")]
    public IActionResult GetById(int id)
    {
        try
        {
            return Ok(_orderService.GetById(id));
        }
        catch
        {
            return BadRequest();
        }
    }


    [Consumes("application/json")]
    [Produces("application/json")]
    [HttpPost("Create")]
    public IActionResult Create([FromBody]OrderAPI orderAPI)

    {
        try
        {
            return Ok(_orderService.Create(orderAPI));
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
    public IActionResult Update([FromBody] Order order)
    {
        try
        {
            order.UpdatedAt = DateTime.Now;
            return Ok(_serviceCRUD.Update(order));
        }
        catch
        {
            return BadRequest();
        }
    }
}
