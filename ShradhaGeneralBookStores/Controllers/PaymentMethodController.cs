using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShradhaGeneralBookStores.Models;
using ShradhaGeneralBookStores.Service.Interface;

namespace ShradhaGeneralBookStores.Controllers;
[Route("api/[controller]")]
[ApiController]
public class PaymentMethodController : ControllerBase
{
    private readonly IServiceCRUD<PaymentMethod> _serviceCRUD;

    public PaymentMethodController(IServiceCRUD<PaymentMethod> serviceCRUD)
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
    public IActionResult Create([FromBody] PaymentMethod paymentMethod)

    {
        try
        {
            paymentMethod.CreatedAt = DateTime.Now;
            paymentMethod.UpdatedAt = DateTime.Now;
            return Ok(_serviceCRUD.Create(paymentMethod));
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
    public IActionResult Update([FromBody] PaymentMethod paymentMethod)
    {
        try
        {
            paymentMethod.UpdatedAt = DateTime.Now;
            return Ok(_serviceCRUD.Update(paymentMethod));
        }
        catch
        {
            return BadRequest();
        }
    }
}   
