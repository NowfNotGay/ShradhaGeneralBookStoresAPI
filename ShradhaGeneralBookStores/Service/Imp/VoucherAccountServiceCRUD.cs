using ShradhaGeneralBookStores.Models;
using ShradhaGeneralBookStores.Service.Interface;

namespace ShradhaGeneralBookStores.Service.Imp
{
    public class VoucherAccountServiceCRUD : IServiceCRUD<VoucherAccount>
    {
        private readonly DatabaseContext _databaseContext;

        public VoucherAccountServiceCRUD(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public bool Create(VoucherAccount entity)
        {
            try
            {
                _databaseContext.VoucherAccounts.Add(entity);
                return _databaseContext.SaveChanges() > 0;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool Delete(int accountId, int voucherId)
        {
            try
            {
                var voucherAccount = _databaseContext.VoucherAccounts.FirstOrDefault(va => va.AccountId == accountId && va.VoucherId == voucherId);
                if (voucherAccount != null)
                {
                    _databaseContext.VoucherAccounts.Remove(voucherAccount);
                    return _databaseContext.SaveChanges() > 0;
                }
                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }

        public dynamic Get(int accountId, int voucherId) => _databaseContext.VoucherAccounts.FirstOrDefault(va => va.AccountId == accountId && va.VoucherId == voucherId)!;

        public dynamic Get(int id)
        {
            throw new NotImplementedException();
        }

        public dynamic Read() => _databaseContext.VoucherAccounts;

        public bool Update(VoucherAccount entity)
        {
            try
            {
                _databaseContext.VoucherAccounts.Update(entity);
                return _databaseContext.SaveChanges() > 0;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
