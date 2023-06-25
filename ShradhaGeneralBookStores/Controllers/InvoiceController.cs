using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShradhaGeneralBookStores.Models;
using ShradhaGeneralBookStores.Service.Interface;

namespace ShradhaGeneralBookStores.Controllers;
[Route("api/[controller]")]
[ApiController]
public class InvoiceController : ControllerBase
{
    private readonly IServiceCRUD<Invoice> _serviceCRUD;

    public InvoiceController(IServiceCRUD<Invoice> serviceCRUD)
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
    public IActionResult Create([FromBody] Invoice invoice)

    {
        try
        {
            invoice.CreatedAt = DateTime.Now;
            invoice.UpdatedAt = DateTime.Now;
            return Ok(_serviceCRUD.Create(invoice));
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
    public IActionResult Update([FromBody] Invoice invoice)
    {
        try
        {
            invoice.UpdatedAt = DateTime.Now;
            return Ok(_serviceCRUD.Update(invoice));
        }
        catch
        {
            return BadRequest();
        }
    }
}
