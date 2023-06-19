using ShradhaGeneralBookStores.Models;
using ShradhaGeneralBookStores.Service.Interface;

namespace ShradhaGeneralBookStores.Service.Imp
{
    public class EventDetailCRUD : IServiceCRUD<EventDetail>
    {
        private readonly DatabaseContext _databaseContext;

        public EventDetailCRUD(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public bool Create(EventDetail entity)
        {
            try
            {
                _databaseContext.EventDetails.Add(entity);
                return _databaseContext.SaveChanges() > 0;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool Delete(int eventId, int productId)
        {
            try
            {
                var eventDetail = _databaseContext.EventDetails.FirstOrDefault(ed => ed.EventId == eventId && ed.ProductId == productId);
                if (eventDetail != null)
                {
                    _databaseContext.EventDetails.Remove(eventDetail);
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

        public dynamic Get(int eventId, int productId) => _databaseContext.EventDetails.FirstOrDefault(ed => ed.EventId == eventId && ed.ProductId == productId)!;

        public dynamic Get(int id)
        {
            throw new NotImplementedException();
        }

        public dynamic Read() => _databaseContext.EventDetails;

        public bool Update(EventDetail entity)
        {
            try
            {
                _databaseContext.EventDetails.Update(entity);
                return _databaseContext.SaveChanges() > 0;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
