using ShradhaGeneralBookStores.Models;
using ShradhaGeneralBookStores.Service.Interface;

namespace ShradhaGeneralBookStores.Service.Imp
{
    public class VoucherCRUD : IServiceCRUD<Voucher>
    {
        private readonly DatabaseContext _databaseContext;

        public VoucherCRUD(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public bool Create(Voucher entity)
        {
            try
            {
                _databaseContext.Vouchers.Add(entity);
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
                var voucher = _databaseContext.Vouchers.Find(id);
                if (voucher != null)
                {
                    _databaseContext.Vouchers.Remove(voucher);
                    return _databaseContext.SaveChanges() > 0;
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public dynamic Get(int id) => _databaseContext.Vouchers.Where(v => v.Id == id).Select(v => new
        {
            v.Id,
            v.Name,
            v.VarityCode,
            v.Discount,
            v.Condition,
            v.Quantity,
            v.TimeStart,
            v.TimeEnd,
            v.Status,
            v.CreatedAt,
            v.UpdatedAt
        });

        public dynamic Read() => _databaseContext.Vouchers.Select(v => new
        {
            v.Id,
            v.Name,
            v.VarityCode,
            v.Discount,
            v.Condition,
            v.Quantity,
            v.TimeStart,
            v.TimeEnd,
            v.Status,
            v.CreatedAt,
            v.UpdatedAt
        });

        public bool Update(Voucher entity)
        {
            try
            {
                _databaseContext.Vouchers.Update(entity);
                return _databaseContext.SaveChanges() > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
