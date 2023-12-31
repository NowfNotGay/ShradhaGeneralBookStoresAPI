﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ShradhaGeneralBookStores.Helpers;
using ShradhaGeneralBookStores.Models;
using ShradhaGeneralBookStores.Models.ModelTemp;
using ShradhaGeneralBookStores.Service.Interface;
using System.Diagnostics;
using System.Text.Json.Serialization;

namespace ShradhaGeneralBookStores.Controllers;
[Route("api/[controller]")]
[ApiController]
public class AccountController : ControllerBase
{
    private readonly IServiceCRUD<Account> _serviceCRUD;
    private readonly IAccountService _accountService;
    private readonly IAccountRoleService _accountRoleService;
    private readonly IConfiguration _configuration;
    private readonly IAccountAdmin _accountAdmin;

    public AccountController(IServiceCRUD<Account> serviceCRUD, IAccountService accountService, IAccountRoleService accountRoleService, IConfiguration configuration, IAccountAdmin accountAdmin)
    {
        _serviceCRUD = serviceCRUD;
        _accountService = accountService;
        _accountRoleService = accountRoleService;
        _configuration = configuration;
        _accountAdmin = accountAdmin;
    }


    //Register start
    [Produces("application/json")]
    [HttpPost("Register")]
    public IActionResult Register([FromBody] AccountAPI account)
    {
        try
        {   //Date cập nhật và date tạo
            account.CreatedAt = DateTime.Now;
            account.UpdatedAt = DateTime.Now;
            account.Password = BCrypt.Net.BCrypt.HashPassword(account.Password);
            account.Status = false;
            account.SecurityCode = RandomHelper.RandomString(6);
            var mailhelper = new MailHelper(_configuration);
            var a = _accountService.Register(account);
            if (a > 0)
            {
                string contentmail = "<a href=\"http://localhost:4400/active;id=" + a+ "&securitycode=" + account.SecurityCode + "\">Nhấn để kích hoạt</a>";
                mailhelper.Send(_configuration["Gmail:Username"]!, account.Email, "Verify Mail", contentmail);
                return Ok(true);
            } 
            
            return Ok(false);
        }
        catch
        {
            return BadRequest();
        }
    }

    [Produces("application/json")]
    [HttpGet("Active")]
    public IActionResult Active(int id, string securitycode)
    {
        try
        {
            return Ok(_accountService.ActiveAccount(id, securitycode));
        }
        catch
        {
            return BadRequest();
        }
    }
    //register end


    //login start
    [Produces("application/json")]
    [HttpPost("Login")]
    public IActionResult Login([FromBody] Account account)
    {
        try
        {
            return Ok(_accountService.Login(account.Email, account.Password));
        }
        catch
        {
            return BadRequest();
        }
    }
    //login end

    //forget password start
    [Produces("application/json")]
    [HttpPut("ForgetPassword/{email}")]
    public IActionResult ForgetPassword(string email, object a)
    {
        try
        {
            var account = _accountService.GetAccountOfEmailForgetPassword(email);
            account.Password = RandomHelper.RandomString(8);
            var mailhelper = new MailHelper(_configuration);
            string contentmail = $"Your new password:{account.Password}";
            mailhelper.Send(_configuration["Gmail:Username"]!, account.Email, "Verify Mail", contentmail);
            account.UpdatedAt = DateTime.Now;
            account.Password= BCrypt.Net.BCrypt.HashPassword(account.Password);
            return Ok(_serviceCRUD.Update(account));
        }
        catch
        {
            return BadRequest();
        }
    }


    // zô tri
    [Produces("application/json")]
    [HttpPut("CheckCodeForget")]
    public IActionResult CheckCodeForget(string email, string securityCode)
    {
        try
        {
            var account = _accountService.GetAccountForForgetPassword(email, securityCode);
            if (account == null)
            {
                return Ok(false);
            }
            return Ok(true);
        }
        catch
        {
            return BadRequest();
        }
    }

    [Produces("application/json")]
    [HttpPut("ChangePassword")]
    public IActionResult ChangePassword(Account account)
    {
        try
        {
            var accountChange = _accountService.GetAccountOfEmailForgetPassword(account.Email);
            accountChange.Password = BCrypt.Net.BCrypt.HashPassword(account.Password);
            accountChange.UpdatedAt = DateTime.Now;
            return Ok(_serviceCRUD.Update(accountChange));
        }
        catch
        {
            return BadRequest();
        }
    }
    //forget password end 
    // end zô tri

    //list account start
    [Produces("application/json")]
    [HttpGet("Read")]
    public IActionResult Read()
    {
        try
        {
            return Ok(_accountService.Read());
        }
        catch
        {
            return BadRequest();
        }
    }

    //forget password end 
    // end zô tri

    //list account start
    [Produces("application/json")]
    [HttpGet("ReadDisable")]
    public IActionResult ReadDisable()
    {
        try
        {
            return Ok(_accountService.ReadDisable());
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
            return Ok(_accountService.Get(id));
        }
        catch
        {
            return BadRequest();
        }
    }

    [Produces("application/json")]
    [HttpGet("GetByEmail")]
    public IActionResult GetByEmail(string email)
    {
        try
        {
            return Ok(_accountService.GetByEmail(email));
        }
        catch
        {
            return BadRequest();
        }
    }

    [Produces("application/json")]
    [HttpPut("Delete/{id}")]
    public IActionResult Delete(int id,object a)
    {
        try
        {
            return Ok(_accountService.DisableAccount(id));
        }
        catch
        {
            return BadRequest();
        }
    }
    //list account end


    //add account admin
    [Consumes("application/json")]
    [Produces("application/json")]
    [HttpPost("Create")]
    public IActionResult Create([FromBody] AccountAPI account)
    {
        try
        {   //Date cập nhật và date tạo
            account.CreatedAt = DateTime.Now;
            account.UpdatedAt = DateTime.Now;
            account.Status = true;
            account.SecurityCode = RandomHelper.RandomString(6);
            account.Password = BCrypt.Net.BCrypt.HashPassword(account.Password);
            return Ok(_accountAdmin.AddAccount(account));
        }
        catch
        {
            return BadRequest();
        }
    }

    //edit account admin
    [Consumes("multipart/form-data")]
    [Produces("application/json")]
    [HttpPut("UpdateProfile")]
    public IActionResult UpdateProfile(IFormFile avatar, IFormCollection formData)
    {
        try
        {
            var profile = JsonConvert.DeserializeObject<Profile>(formData["profile"]);
            return Ok(_accountService.UpdateProfile(profile, avatar));
        }
        catch
        {
            return BadRequest();
        }
    }

    [Consumes("multipart/form-data")]
    [Produces("application/json")]
    [HttpPut("UpdateProfileNoAvatar")]
    public IActionResult UpdateProfileNoAvatar(IFormCollection formData)
    {
        try
        {
            var profile = JsonConvert.DeserializeObject<Profile>(formData["profile"]);

            return Ok(_accountService.UpdateProfile(profile));
        }
        catch
        {
            return BadRequest();
        }
    }

    //edit profile account admin
    [Consumes("application/json")]
    [Produces("application/json")]
    [HttpPut("Update")]
    public IActionResult Update([FromBody] AccountAPI account)
    {
        try
        {
            var a = _serviceCRUD.Get(account.Id);
            //Date cập nhật và date tạo
            account.CreatedAt = a.CreatedAt;
            account.UpdatedAt = DateTime.Now;
            account.Status = true;
            account.SecurityCode = a.SecurityCode;
            account.Password = (account.Password != "") ? BCrypt.Net.BCrypt.HashPassword(account.Password) : a.Password;
            return Ok(_accountAdmin.UpdateRoleAccount(account));
        }
        catch
        {
            return BadRequest();
        }
    }

    //list account start
    [Produces("application/json")]
    [HttpGet("CheckExist")]
    public IActionResult CheckExist(string email)
    {
        try
        {
            return Ok(_accountService.CheckExistsForEmail(email));
        }
        catch
        {
            return BadRequest();
        }
    }


    //list account start
    [Produces("application/json")]
    [HttpGet("GetRoleByAccountId")]
    public IActionResult GetRoleByAccountId(int id)
    {
        try
        {
            return Ok(_accountAdmin.GetRoleByAccountId(id));
        }
        catch
        {
            return BadRequest();
        }
    }



    [Produces("application/json")]
    [HttpPut("EnableAccount/{id}")]
    public IActionResult EnableAccount(int id, object a)
    {
        try
        {
            return Ok(_accountService.EnableAccount(id));
        }
        catch
        {
            return BadRequest();
        }
    }

    [Consumes("application/json")]
    [Produces("application/json")]
    [HttpPut("ChangePassword")]
    public IActionResult ChangePassword([FromBody]ChangePassword changePassword)
    {
        try
        {
            return Ok(_accountService.ChangePassword(changePassword));
        }
        catch
        {
            return BadRequest();
        }
    }
}