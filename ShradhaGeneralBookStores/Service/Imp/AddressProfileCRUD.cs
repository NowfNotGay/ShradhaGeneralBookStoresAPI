using ShradhaGeneralBookStores.Models;
using ShradhaGeneralBookStores.Service.Interface;

namespace ShradhaGeneralBookStores.Service.Imp
{
    public class AddressProfileCRUD : IServiceCRUD<AddressProfile>
    {
        private readonly DatabaseContext _databaseContext;

        public AddressProfileCRUD(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public bool Create(AddressProfile entity)
        {
            try
            {
                _databaseContext.AddressProfiles.Add(entity);
                return _databaseContext.SaveChanges() > 0;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool Delete(int id)
        {
            try
            {
                var addressProfile = _databaseContext.AddressProfiles.FirstOrDefault(ap => ap.Id == id);
                if (addressProfile != null)
                {
                    _databaseContext.AddressProfiles.Remove(addressProfile);
                    return _databaseContext.SaveChanges() > 0;
                }
                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public dynamic Get(int id) => _databaseContext.AddressProfiles.Where(ap => ap.Id == id).Select(ap => new
        {
            ap.Id,
            ap.AccountId,
            ap.Street,
            ap.District,
            ap.City,
            ap.CreatedAt,
            ap.UpdatedAt
        }).FirstOrDefault()!;

        public dynamic Read() => _databaseContext.AddressProfiles.Select(ap => new
        {
            ap.Id,
            ap.AccountId,
            ap.Street,
            ap.District,
            ap.City,
            ap.CreatedAt,
            ap.UpdatedAt
        });

        public bool Update(AddressProfile entity)
        {
            try
            {
                _databaseContext.AddressProfiles.Update(entity);
                return _databaseContext.SaveChanges() > 0;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
