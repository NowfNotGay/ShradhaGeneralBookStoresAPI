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
    public IActionResult Create([FromBody] CategoryAPI categoryapi)
    {
        try
        {
            var category = new Category();
            category.Name = categoryapi.Name;

            category.ParentId = categoryapi.ParentId == 0 ? null : categoryapi.ParentId;
            category.CreatedAt = DateTime.Now;
            category.UpdatedAt = DateTime.Now;
            return Ok(_serviceCRUD.Create(category));
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
    public IActionResult Update([FromBody] Category category)
    {
        try
        {
            category.UpdatedAt= DateTime.Now;
            return Ok(_serviceCRUD.Update(category));
        }
        catch
        {
            return BadRequest();
        }
    }

    [Produces("application/json")]
    [HttpGet("GetListParent")]
    public IActionResult GetListParent()
    {
        try
        {
            return Ok(_categoryService.GetAllCategoryByLevel());
        }
        catch
        {
            return BadRequest();
        }
    }
}
