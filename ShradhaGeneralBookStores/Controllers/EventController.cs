using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShradhaGeneralBookStores.Models;
using ShradhaGeneralBookStores.Service.Interface;

namespace ShradhaGeneralBookStores.Controllers;
[Route("api/[controller]")]
[ApiController]
public class EventController : ControllerBase
{
    private readonly IServiceCRUD<Event> _serviceCRUD;

    public EventController(IServiceCRUD<Event> serviceCRUD)
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
    public IActionResult Create([FromBody] Event eventi)
    {
        try
        {
            eventi.CreatedAt = DateTime.Now;
            eventi.UpdatedAt = DateTime.Now;
            return Ok(_serviceCRUD.Create(eventi));
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
    public IActionResult Update([FromBody] Event eventi)
    {
        try
        {
            eventi.UpdatedAt = DateTime.Now;
            return Ok(_serviceCRUD.Update(eventi));
        }
        catch
        {
            return BadRequest();
        }
    }
}
