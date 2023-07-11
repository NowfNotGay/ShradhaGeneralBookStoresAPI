using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using ShradhaGeneralBookStores.Models;
using ShradhaGeneralBookStores.Service.Interface;

namespace ShradhaGeneralBookStores.Service.ServiceClassImpl;

public class AccountAdmin : IAccountAdmin
{
    private readonly DatabaseContext _databaseContext;

    public AccountAdmin(DatabaseContext databaseContext)
    {
        _databaseContext = databaseContext;
    }

    public bool AddAccount(AccountAPI account)
    {
        try
        {
            using (var transaction = _databaseContext.Database.BeginTransaction() )
            {
                #region phan loai account 
                var accountadd = new Account();
                accountadd.FirstName = account.FirstName;
                accountadd.LastName = account.LastName;
                accountadd.Phone = account.Phone;
                accountadd.Email = account.Email;
                accountadd.Password = account.Password;
                accountadd.Status = true;
                accountadd.Avatar = account.Avatar;
                accountadd.CreatedAt = account.CreatedAt;
                accountadd.UpdatedAt = account.UpdatedAt;
                accountadd.SecurityCode = account.SecurityCode;
                #endregion
                _databaseContext.Accounts.Add(accountadd);
                _databaseContext.SaveChanges();
                //account roll
                #region them role
                account.roleId.ForEach(r =>
                {
                    var accountrole = new AccountRole()
                    {
                        AccountId = accountadd.Id,
                        RoleId = r,
                        CreatedAt = DateTime.Now,
                        UpdatedAt = DateTime.Now
                    };
                    _databaseContext.AccountRoles.Add(accountrole);
                });
                #endregion
                int result = _databaseContext.SaveChanges();
                transaction.Commit();
                return result > 0;
            }            
        }
        catch(Exception ex)
        {
            using (var transaction = _databaseContext.Database.BeginTransaction())
            {
                transaction.Rollback();
            }
            return false;
        }
    }

    public dynamic GetRoleByAccountId(int accountId)
    {
        var a = new List<string>();
        foreach (var role in _databaseContext.Accounts.FirstOrDefault(b => b.Id == accountId)!.AccountRoles)
        {
            a.Add(_databaseContext.Roles.Find(role.RoleId)!.Name);
        }
        return a;
    }

    public bool UpdateRoleAccount(AccountAPI account)
    {
        try
        {
            using (var transaction = _databaseContext.Database.BeginTransaction())
            {
                #region phan loai account 
                var accountadd = new Account() ;
                accountadd.Id = account.Id;
                accountadd.FirstName = account.FirstName;
                accountadd.LastName = account.LastName;
                accountadd.Phone = account.Phone;
                accountadd.Email = account.Email;
                accountadd.Password = account.Password;
                accountadd.Status = true;
                accountadd.Avatar = account.Avatar;
                accountadd.CreatedAt = account.CreatedAt;
                accountadd.UpdatedAt = account.UpdatedAt;
                accountadd.SecurityCode = account.SecurityCode;
                #endregion
                _databaseContext.Accounts.Update(accountadd);
                _databaseContext.SaveChanges();
                //account role
                #region sua role
                //var roles = _databaseContext.AccountRoles.Where(r => r.AccountId == account.Id)
                //   .Select(r => r.RoleId)
                //   .ToList();
                //if (account.roleId.Count() == roles.Count() && account.roleId.All(roles.Contains))
                //{

                //}



                //remove all role of account
                foreach(var roleid in _databaseContext.AccountRoles.Where(a=>a.AccountId == account.Id))
                {
                    _databaseContext.AccountRoles.Remove(roleid);
                }
                _databaseContext.SaveChanges();
                //add role of account
                account.roleId.ForEach(r =>
                {
                    var accountrole = new AccountRole()
                    {
                        AccountId = account.Id,
                        RoleId = r,
                        CreatedAt = DateTime.Now,
                        UpdatedAt = DateTime.Now
                    };
                    _databaseContext.AccountRoles.Add(accountrole);
                });
                int result = _databaseContext.SaveChanges();
                transaction.Commit();
                return result > 0;
                #endregion
            }
        }
        catch (Exception ex)
        {
            using (var transaction = _databaseContext.Database.BeginTransaction())
            {
                transaction.Rollback();
            }
            return false;
        }
    }

}
