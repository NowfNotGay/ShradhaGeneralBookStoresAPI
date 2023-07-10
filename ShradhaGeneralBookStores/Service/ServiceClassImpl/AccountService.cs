using Microsoft.EntityFrameworkCore;
using ShradhaGeneralBookStores.Models;
using ShradhaGeneralBookStores.Service.Interface;

namespace ShradhaGeneralBookStores.Service.ServiceClassImpl;

public class AccountService : IAccountService
{
    private readonly DatabaseContext _databaseContext;
    private readonly IConfiguration _configuration;

    public AccountService(DatabaseContext databaseContext, IConfiguration configuration)
    {
        _databaseContext = databaseContext;
        _configuration = configuration;
    }

    public bool ActiveAccount(string email, string security)
    {
        try
        {
            var account = _databaseContext.Accounts.FirstOrDefault(a => a.Email == email && a.SecurityCode == security);
            if (account == null)
            {
                return false;
            }
            account.Status = true;
            account.UpdatedAt = DateTime.Now;
            _databaseContext.Accounts.Update(account);
            return _databaseContext.SaveChanges() > 0;
        }
        catch
        {
            return false;
        }
    }

    public bool CheckExistsForEmail(string email)
    {
        try 
        {
            return _databaseContext.Accounts.FirstOrDefault(a => a.Email == email) != null ? true: false;
        }
        catch 
        { 
            return false;
        }
    }

    public bool DisableAccount(int id)
    {
        try
        {
            var account = _databaseContext.Accounts.FirstOrDefault(a => a.Id == id);
            if(account == null)
            {
                return false;
            }
            account.UpdatedAt = DateTime.Now;
            account.SecurityCode = "-1";
            _databaseContext.Accounts.Update(account);
            return  _databaseContext.SaveChanges() > 0;
        }
        catch
        {
            return false;
        }
    }

    public Account GetAccountForForgetPassword(string email, string securityCode) => _databaseContext.Accounts.FirstOrDefault(a => a.Email == email && a.SecurityCode == securityCode)!;

    public Account GetAccountOfEmailForgetPassword(string email) => _databaseContext.Accounts.FirstOrDefault(a=>a.Email == email && a.Status == true)!;

    public bool Login(string email, string password)
    {
        try
        {
            var account = _databaseContext.Accounts.FirstOrDefault(a => a.Email == email && a.Status == true && a.SecurityCode != "-1");
            if (account == null)
            {
                return false;
            }
            if (!BCrypt.Net.BCrypt.Verify(password, account.Password)) {
                return false;
            }
            return true;
        }
        catch
        {
            return false;
        }
    }
    public dynamic Read() => _databaseContext.Accounts
            .Include(a => a.AccountRoles)
            .ThenInclude(ar => ar.Role)
            .Select(a => new
            {
                a.Id,
                a.Email,
                a.Password,
                a.FirstName,
                a.LastName,
                a.Phone,
                a.Avatar,
                a.Status,
                a.SecurityCode,
                a.CreatedAt,
                a.UpdatedAt,
                Roles = a.AccountRoles.Select(ar => ar.Role.Name).ToList()
            });
    public dynamic Get(int id)
    {
        return _databaseContext.Accounts
            .Include(a => a.AccountRoles)
            .ThenInclude(ar => ar.Role)
            .Where(a => a.Id == id)
            .Select(a => new
            {
                a.Id,
                a.Email,
                a.Password,
                a.FirstName,
                a.LastName,
                a.Phone,
                a.Avatar,
                a.Status,
                a.SecurityCode,
                a.CreatedAt,
                a.UpdatedAt,
                RoleId = a.AccountRoles.Select(ar => ar.Role.Id).ToList()
            });
    }

    public dynamic GetByEmail(string email)
    {
        return _databaseContext.Accounts
            .Include(a => a.AccountRoles)
            .ThenInclude(ar => ar.Role)
            .Where(a => a.Email == email)
            .Select(a => new
            {
                a.Id,
                a.Email,
                a.Password,
                a.FirstName,
                a.LastName,
                a.Phone,
                Avatar = _configuration["BaseURL"] + "Images/Avatars/" + a.Avatar,
                a.Status,
                a.SecurityCode,
                a.CreatedAt,
                a.UpdatedAt,
                Roles = a.AccountRoles.Select(ar => ar.Role.Name).ToList()
            });
    }
}
