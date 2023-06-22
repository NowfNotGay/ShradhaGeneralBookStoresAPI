using ShradhaGeneralBookStores.Models;
using ShradhaGeneralBookStores.Service.Interface;

namespace ShradhaGeneralBookStores.Service.Imp;

public class AccoutServiceCRUD: IServiceCRUD<Account>
{
    private readonly DatabaseContext _databaseContext;

    public AccoutServiceCRUD(DatabaseContext databaseContext)
    {
        _databaseContext = databaseContext;
    }

    public bool Create(Account entity)
    {
        try
        {
            _databaseContext.Accounts.Add(entity);
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
            var account = _databaseContext.Accounts.FirstOrDefault(a => a.Id == id);
            if (account != null)
            {
                _databaseContext.Accounts.Remove(account);
                return _databaseContext.SaveChanges() > 0;
            }
            return false;
        }
        catch (Exception)
        {
            return false;
        }
    }

    public dynamic Get(int id) => _databaseContext.Accounts.Where(a => a.Id == id).Select(a => new
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
        a.UpdatedAt
    }).FirstOrDefault()!;

    public dynamic Read() => _databaseContext.Accounts.Select(a => new
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
        a.UpdatedAt
    });

    public bool Update(Account entity)
    {
        try
        {
            _databaseContext.Accounts.Update(entity);
            return _databaseContext.SaveChanges() > 0;
        }
        catch (Exception)
        {
            return false;
        }
    }
}
