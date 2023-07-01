using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShradhaGeneralBookStores.Models;
using ShradhaGeneralBookStores.Service.Interface;

namespace ShradhaGeneralBookStores.Controllers;
[Route("api/[controller]")]
[ApiController]
public class VoucherController : ControllerBase
{
    private readonly IServiceCRUD<Voucher> _serviceCRUD;

    public VoucherController(IServiceCRUD<Voucher> serviceCRUD)
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
    public IActionResult Create([FromBody] Voucher voucher)

    {
        try
        {
            voucher.CreatedAt = DateTime.Now;
            voucher.UpdatedAt = DateTime.Now;
            return Ok(_serviceCRUD.Create(voucher));
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
    public IActionResult Update([FromBody] Voucher voucher)
    {
        try
        {
            voucher.UpdatedAt = DateTime.Now;
            return Ok(_serviceCRUD.Update(voucher));
        }
        catch
        {
            return BadRequest();
        }
    }
}
