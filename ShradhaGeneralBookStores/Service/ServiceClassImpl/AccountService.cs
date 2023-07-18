using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using ShradhaGeneralBookStores.Models;
using ShradhaGeneralBookStores.Models.ModelTemp;
using ShradhaGeneralBookStores.Service.Interface;

namespace ShradhaGeneralBookStores.Service.ServiceClassImpl;

public class AccountService : IAccountService
{
    private readonly DatabaseContext _databaseContext;
    private readonly IConfiguration _configuration;
    private readonly IWebHostEnvironment _webHostEnvironmentl;

    public AccountService(DatabaseContext databaseContext, IConfiguration configuration, IWebHostEnvironment webHostEnvironmentl)
    {
        _databaseContext = databaseContext;
        _configuration = configuration;
        _webHostEnvironmentl = webHostEnvironmentl;
    }

    public bool ActiveAccount(int id, string security)
    {
        try
        {
            Account account = _databaseContext.Accounts.FirstOrDefault(a=>a.Id == id);
             
            if (account.SecurityCode.Equals(security))
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
            return _databaseContext.Accounts.FirstOrDefault(a => a.Email == email) != null ? true : false;
        }
        catch
        {
            return false;
        }
    }

    public bool CheckDisableAccount(string email)
    {
        try
        {
            return _databaseContext.Accounts.FirstOrDefault(a => a.Email == email && a.Status == false && a.SecurityCode == "-1") != null ? true : false;
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
            if (account == null)
            {
                return false;
            }
            account.UpdatedAt = DateTime.Now;
            account.SecurityCode = "-1";
            account.Status = false;
            _databaseContext.Accounts.Update(account);
            return _databaseContext.SaveChanges() > 0;
        }
        catch
        {
            return false;
        }
    }

    public Account GetAccountForForgetPassword(string email, string securityCode) => _databaseContext.Accounts.FirstOrDefault(a => a.Email == email && a.SecurityCode == securityCode)!;

    public Account GetAccountOfEmailForgetPassword(string email) => _databaseContext.Accounts.FirstOrDefault(a => a.Email == email && a.Status == true)!;

    public bool Login(string email, string password)
    {
        try
        {
            var account = _databaseContext.Accounts.FirstOrDefault(a => a.Email == email && a.Status == true);
            if (account == null)
            {
                return false;
            }
            if (!BCrypt.Net.BCrypt.Verify(password, account.Password))
            {
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
            .Where(a => a.Status == true && a.Id != 1)
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

    public dynamic UpdateProfile(Profile profile, IFormFile? avatar = null)
    {
        try
        {
            var account = _databaseContext.Accounts.FirstOrDefault(a => a.Email == profile.Email);
            account.FirstName = profile.FirstName;
            account.LastName = profile.LastName;
            account.Phone = profile.Phone;
            if (avatar != null)
            {
                var filename = account.Id + "avatar" + avatar.FileName.Substring(avatar.FileName.LastIndexOf('.'));
                var path = Path.Combine(_webHostEnvironmentl.WebRootPath, "Images/Avatars", filename);
                if (account.Avatar != "no-avatar.jpg")
                {
                    var pathold = Path.Combine(_webHostEnvironmentl.WebRootPath, "Images/Avatars", account.Avatar!);
                    if (System.IO.File.Exists(pathold))
                    {
                        System.IO.File.Delete(pathold);
                    }
                }
                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    avatar.CopyTo(fileStream);
                }
                account.Avatar = filename;
            };
            account.UpdatedAt = DateTime.Now;
            _databaseContext.Accounts.Update(account);
            if (_databaseContext.SaveChanges() > 0)
            {
                return _databaseContext.Accounts
            .Include(a => a.AccountRoles)
            .ThenInclude(ar => ar.Role)
            .Where(a => a.Email == profile.Email)
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
            }); ;

            }
            else
                return null;
        }
        catch
        {
            return null;
        }

    }

    public dynamic ReadDisable() => _databaseContext.Accounts
            .Include(a => a.AccountRoles)
            .ThenInclude(ar => ar.Role)
            .Where(a => a.Status == false && a.Id != 1)
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

    public bool EnableAccount(int id)
    {
        try
        {
            var account = _databaseContext.Accounts.FirstOrDefault(a => a.Id == id);
            if (account == null)
            {
                return false;
            }
            account.UpdatedAt = DateTime.Now;
            account.SecurityCode = "0";
            account.Status = true;
            _databaseContext.Accounts.Update(account);
            return _databaseContext.SaveChanges() > 0;
        }
        catch
        {
            return false;
        }
    }

    public bool ChangePassword(ChangePassword changePassword)
    {
        try
        {
            var account = _databaseContext.Accounts.FirstOrDefault(a => a.Id == changePassword.Id);
            if (account == null || !BCrypt.Net.BCrypt.Verify(changePassword.OldPassword, account.Password))
            {
                return false;
            }
            account.Password = BCrypt.Net.BCrypt.HashPassword(changePassword.NewPassword);
            _databaseContext.Accounts.Update(account);
            return _databaseContext.SaveChanges() > 0;
        }
        catch
        {
            return false;
        }
    }

    public int Register(AccountAPI accountapi)
    {
        try
        {
            using (var transaction = _databaseContext.Database.BeginTransaction())
            {
                var account = new Account()
                {
                    FirstName = accountapi.FirstName,
                    LastName = accountapi.LastName,
                    Phone = accountapi.Phone,
                    Email = accountapi.Email,
                    Password = accountapi.Password,
                    Status = false,
                    Avatar = accountapi.Avatar,
                    CreatedAt = accountapi.CreatedAt,
                    UpdatedAt = accountapi.UpdatedAt,
                    SecurityCode = accountapi.SecurityCode,
                };
                _databaseContext.Accounts.Add(account);
                _databaseContext.SaveChanges();

                _databaseContext.AccountRoles.Add(new AccountRole()
                {
                    AccountId = account.Id,
                    RoleId = 3,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                });
                var a = _databaseContext.SaveChanges();
                transaction.Commit();
                return account.Id;
            }
        }
        catch
        {
            using (var transaction = _databaseContext.Database.BeginTransaction())
            {
                transaction.Rollback();
                return -1;
            }
        }
    }
}
