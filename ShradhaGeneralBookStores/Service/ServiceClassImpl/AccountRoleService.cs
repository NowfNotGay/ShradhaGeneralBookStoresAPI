using Microsoft.EntityFrameworkCore.Storage;
using ShradhaGeneralBookStores.Models;
using ShradhaGeneralBookStores.Service.Interface;

namespace ShradhaGeneralBookStores.Service.ServiceClassImpl;

public class AccountRoleService : IAccountRoleService
{
    private readonly DatabaseContext _databaseContext;

    public AccountRoleService(DatabaseContext databaseContext)
    {
        _databaseContext = databaseContext;
    }

    public bool Create(int customerId, int roleId)
    {
        try
        {
            var accountRole = new AccountRole() { 
                AccountId = customerId, 
                RoleId = roleId, 
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            };
            _databaseContext.AccountRoles.Add(accountRole);
            return _databaseContext.SaveChanges()>0;
        }
        catch(Exception)
        {
            return false;
        }
    }
}
