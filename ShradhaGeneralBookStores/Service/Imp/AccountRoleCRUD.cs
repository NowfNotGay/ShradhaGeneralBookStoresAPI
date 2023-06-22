using ShradhaGeneralBookStores.Models;
using ShradhaGeneralBookStores.Service.Interface;

namespace ShradhaGeneralBookStores.Service.Imp
{
    public class AccountRoleCRUD : IServiceCRUD<AccountRole>
    {
        private readonly DatabaseContext _databaseContext;

        public AccountRoleCRUD(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public bool Create(AccountRole entity)
        {
            try
            {
                _databaseContext.AccountRoles.Add(entity);
                return _databaseContext.SaveChanges() > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Delete(int id)
        {
            try
            {
                var accountRole = _databaseContext.AccountRoles.FirstOrDefault(ar => ar.AccountId == id);
                if (accountRole != null)
                {
                    _databaseContext.AccountRoles.Remove(accountRole);
                    return _databaseContext.SaveChanges() > 0;
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public dynamic Get(int id) => _databaseContext.AccountRoles.Where(ar => ar.AccountId == id).Select(ar => new
        {
            ar.AccountId,
            ar.RoleId,
            ar.CreatedAt,
            ar.UpdatedAt
        }).FirstOrDefault()!;

        public dynamic Read() => _databaseContext.AccountRoles.Select(ar => new
        {
            ar.AccountId,
            ar.RoleId,
            ar.CreatedAt,
            ar.UpdatedAt
        });

        public bool Update(AccountRole entity)
        {
            try
            {
                _databaseContext.AccountRoles.Update(entity);
                return _databaseContext.SaveChanges() > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
