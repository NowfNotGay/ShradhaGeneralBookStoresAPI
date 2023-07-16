using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShradhaGeneralBookStores.Models;
using ShradhaGeneralBookStores.Service.Interface;

namespace ShradhaGeneralBookStores.Controllers;
[Route("api/[controller]")]
[ApiController]
public class AddressProfileController : ControllerBase
{
    private readonly IServiceCRUD<AddressProfile> _serviceCRUD;

    public AddressProfileController(IServiceCRUD<AddressProfile> serviceCRUD)
    {
        _serviceCRUD = serviceCRUD;
    }

    [Produces("application/json")]
    [HttpPut("Read")]
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
    public IActionResult Create([FromBody] AddressProfile addressProfile)

    {
        try
        {
            addressProfile.CreatedAt = DateTime.Now;
            addressProfile.UpdatedAt = DateTime.Now;
            return Ok(_serviceCRUD.Create(addressProfile));
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
    public IActionResult Update([FromBody] AddressProfile addressProfile)
    {
        try
        {
            addressProfile.UpdatedAt = DateTime.Now;
            return Ok(_serviceCRUD.Update(addressProfile));
        }
        catch
        {
            return BadRequest();
        }
    }
}
