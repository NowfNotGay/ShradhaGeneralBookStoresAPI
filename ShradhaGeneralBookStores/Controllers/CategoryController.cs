using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShradhaGeneralBookStores.Models;
using ShradhaGeneralBookStores.Service.Interface;
using System.Diagnostics;

namespace ShradhaGeneralBookStores.Controllers;
[Route("api/[controller]")]
[ApiController]
public class CategoryController : ControllerBase
{
    private readonly IServiceCRUD<Category> _serviceCRUD;
    private readonly ICategoryService _categoryService;

    public CategoryController(IServiceCRUD<Category> serviceCRUD, ICategoryService categoryService)
    {
        _serviceCRUD = serviceCRUD;
        _categoryService = categoryService;
    }

    [Consumes("application/json")]
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
    public IActionResult Create([FromBody] Category category)
    {
        try
        {
            return Ok(_serviceCRUD.Create(category));
        }
        catch
        {
            return BadRequest();
        }
    }

    [Consumes("application/json")]
    [Produces("application/json")]
    [HttpGet("GetListParent")]
    public IActionResult GetListParent(int categoryId)
    {
        try
        {
            return Ok(_categoryService.GetSubParent(categoryId));
        }
        catch
        {
            return BadRequest();
        }
    }
}
