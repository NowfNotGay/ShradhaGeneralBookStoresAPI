﻿using ShradhaGeneralBookStores.Models;
using ShradhaGeneralBookStores.Service.Interface;

namespace ShradhaGeneralBookStores.Service.ServiceClassImpl;

public class AccountService : IAccountService
{
    private readonly DatabaseContext _databaseContext;

    public AccountService(DatabaseContext databaseContext)
    {
        _databaseContext = databaseContext;
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
}