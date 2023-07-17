using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShradhaGeneralBookStores.Service.Interface;

namespace ShradhaGeneralBookStores.Controllers;
[Route("api/[controller]")]
[ApiController]
public class OrderDetailController : ControllerBase
{
    private readonly IOrderDetailService _orderDetailService;

    public OrderDetailController(IOrderDetailService orderDetailService)
    {
        _orderDetailService = orderDetailService;
    }

    [Produces("application/json")]
    [HttpGet]
    [Route("GetByOrderId")]
    public IActionResult GetByOrderId(int orderid)
    {
        try
        {
            return Ok(_orderDetailService.GetById(orderid));
        }
        catch
        {
            return BadRequest();
        }
    }
}
