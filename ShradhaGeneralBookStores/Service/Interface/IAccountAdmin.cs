using ShradhaGeneralBookStores.Models;

namespace ShradhaGeneralBookStores.Service.Interface;

public interface IAccountAdmin
{
    public bool AddAccount(AccountAPI account);
    public bool UpdateRoleAccount(AccountAPI account);
    public dynamic GetRoleByAccountId(int accountId);
}
