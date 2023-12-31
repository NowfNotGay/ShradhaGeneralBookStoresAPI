﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShradhaGeneralBookStores.Models;
using ShradhaGeneralBookStores.Service.Interface;
using ShradhaGeneralBookStores.Service.ServiceClassImpl;

namespace ShradhaGeneralBookStores.Controllers;
[Route("api/[controller]")]
[ApiController]
public class PublisherController : ControllerBase
{
    private readonly IServiceCRUD<Publisher> _serviceCRUD;
    private readonly IPublisherService _publisherService;

    public PublisherController(IServiceCRUD<Publisher> serviceCRUD, IPublisherService publisherService)
    {
        _serviceCRUD = serviceCRUD;
        _publisherService = publisherService;
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

    [Produces("application/json")]
    [HttpGet("ReadForMenu")]
    public IActionResult ReadForMenu()
    {
        try
        {
            return Ok(_publisherService.GetForMenu());
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
    public IActionResult Create([FromBody] Publisher publisher)
    {
        try
        {
            publisher.CreatedAt = DateTime.Now;
            publisher.UpdatedAt = DateTime.Now;
            return Ok(_serviceCRUD.Create(publisher));
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
    public IActionResult Update([FromBody] Publisher publisher)
    {
        try
        {
            publisher.UpdatedAt = DateTime.Now;
            return Ok(_serviceCRUD.Update(publisher));
        }
        catch
        {
            return BadRequest();
        }
    }
}
