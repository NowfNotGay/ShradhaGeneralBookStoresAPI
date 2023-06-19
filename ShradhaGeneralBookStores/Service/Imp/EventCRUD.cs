using ShradhaGeneralBookStores.Models;
using ShradhaGeneralBookStores.Service.Interface;

namespace ShradhaGeneralBookStores.Service.Imp
{
    public class EventCRUD : IServiceCRUD<Event>
    {
        private readonly DatabaseContext _databaseContext;

        public EventCRUD(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public bool Create(Event entity)
        {
            try
            {
                _databaseContext.Events.Add(entity);
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
                var eventEntity = _databaseContext.Events.FirstOrDefault(e => e.Id == id);
                if (eventEntity != null)
                {
                    _databaseContext.Events.Remove(eventEntity);
                    return _databaseContext.SaveChanges() > 0;
                }
                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public dynamic Get(int id) => _databaseContext.Events.FirstOrDefault(e => e.Id == id)!;

        public dynamic Read() => _databaseContext.Events;

        public bool Update(Event entity)
        {
            try
            {
                _databaseContext.Events.Update(entity);
                return _databaseContext.SaveChanges() > 0;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
