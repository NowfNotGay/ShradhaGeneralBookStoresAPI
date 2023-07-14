using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShradhaGeneralBookStores.Models;
using ShradhaGeneralBookStores.Service.Interface;

namespace ShradhaGeneralBookStores.Controllers;
[Route("api/[controller]")]
[ApiController]
public class ProductController : ControllerBase
{
    private readonly IServiceCRUD<Product> _serviceCRUD;
    private readonly IProductService _productService;

    public ProductController(IServiceCRUD<Product> serviceCRUD, IProductService productService)
    {
        _serviceCRUD = serviceCRUD;
        _productService = productService;
    }
    //http get
    //product manager get
    [Produces("application/json")]
    [HttpGet("Read")]
    public IActionResult Read()
    {
        try
        {
            return Ok(_productService.Read());
        }
        catch
        {
            return BadRequest();
        }
    }

    [Produces("application/json")]
    [HttpGet("ReadDisable")]
    public IActionResult Read()
    {
        try
        {
            return Ok(_productService.Read());
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
            return Ok(_productService.GetById(id));
        }
        catch
        {
            return BadRequest();
        }
    }


    [Produces("application/json")]
    [HttpGet("GetUser")]
    public IActionResult GetUser(int id)
    {
        try
        {
            return Ok(_productService.GetByIdUser(id));
        }
        catch
        {
            return BadRequest();
        }
    }

    // end product manager get

    [Produces("application/json")]
    [HttpGet("ReadForAuthor")]
    public IActionResult ReadForAuthor(int idAuthor)
    {
        try
        {
            return Ok(_productService.ReadForAuthor(idAuthor));
        }
        catch
        {
            return BadRequest();
        }
    }

    [Produces("application/json")]
    [HttpGet("ReadForAuthorUser")]
    public IActionResult ReadForAuthorUser(int idAuthor)
    {
        try
        {
            return Ok(_productService.ReadForAuthorUser(idAuthor));
        }
        catch
        {
            return BadRequest();
        }
    }

    [Produces("application/json")]
    [HttpGet("ReadForCategory")]
    public IActionResult ReadForCategory(int idCategory)
    {
        try
        {
            return Ok(_productService.ReadForCategory(idCategory));
        }
        catch
        {
            return BadRequest();
        }
    }

    [Produces("application/json")]
    [HttpGet("ReadForCategoryUser")]
    public IActionResult ReadForCategoryUser(int idCategory)
    {
        try
        {
            return Ok(_productService.ReadForCategoryUser(idCategory));
        }
        catch
        {
            return BadRequest();
        }
    }

    [Produces("application/json")]
    [HttpGet("ReadForUser")]
    public IActionResult ReadForUser()
    {
        try
        {
            return Ok(_productService.ReadForUser());
        }
        catch
        {
            return BadRequest();
        }
    }


    [Produces("application/json")]
    [HttpGet("ReadByPrice")]
    public IActionResult ReadByPrice(int min,int max)
    {
        try
        {
            return Ok(_productService.ReadByPrice(min,max));
        }
        catch
        {
            return BadRequest();
        }
    }

    [Produces("application/json")]
    [HttpGet("ReadForPublisher")]
    public IActionResult ReadForPublisher(int idPublisher)
    {
        try
        {
            return Ok(_productService.ReadForPublisher(idPublisher));
        }
        catch
        {
            return BadRequest();
        }
    }

    [Produces("application/json")]
    [HttpGet("ReadForPublisherUser")]
    public IActionResult ReadForPublisherUser(int idPublisher)
    {
        try
        {
            return Ok(_productService.ReadForPublisherUser(idPublisher));
        }
        catch
        {
            return BadRequest();
        }
    }
    //end httpget


    //httppost
    [Consumes("application/json")]
    [Produces("application/json")]
    [HttpPost("Create")]
    public IActionResult Create([FromBody] ProductAPI product)
    {
        try
        {
            product.CreatedAt = DateTime.Now;
            product.UpdatedAt = DateTime.Now;

            return Ok();
        }
        catch
        {
            return BadRequest();
        }
    }


    [Consumes("application/json")]
    [Produces("application/json")]
    [HttpPost("Add")]
    public IActionResult Add([FromBody] ProductAPI product)
    {
        try
        {
            product.CreatedAt = DateTime.Now;
            product.UpdatedAt = DateTime.Now;
            return Ok(_productService.AddProduct(product));
        }
        catch
        {
            return BadRequest();
        }
    }
    //end httppost


    //httpput
    [Consumes("application/json")]
    [Produces("application/json")]
    [HttpPut("Update")]
    public IActionResult Update([FromBody] ProductAPI product)
    {
        try
        {
            product.UpdatedAt = DateTime.Now;
            return Ok(_productService.UpdateProduct(product));
        }
        catch
        {
            return BadRequest();
        }
    }
    //end httpput


    //delete
    [Produces("application/json")]
    [HttpPut("Deleted/{id}")]
    public IActionResult DeleteProduct(int id, Object a)
    {
        try
        {
            return Ok(_productService.DeleteProduct(id));
        }
        catch
        {
            return BadRequest();
        }
    }
    //delete
}
