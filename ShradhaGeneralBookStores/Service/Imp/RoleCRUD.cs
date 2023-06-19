﻿using ShradhaGeneralBookStores.Models;
using ShradhaGeneralBookStores.Service.Interface;

namespace ShradhaGeneralBookStores.Service.Imp
{
    public class RoleCRUD : IServiceCRUD<Role>
    {
        private readonly DatabaseContext _databaseContext;

        public RoleCRUD(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public bool Create(Role entity)
        {
            try
            {
                _databaseContext.Roles.Add(entity);
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
                var role = _databaseContext.Roles.Find(id);
                if (role != null)
                {
                    _databaseContext.Roles.Remove(role);
                    return _databaseContext.SaveChanges() > 0;
                }
                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public dynamic Get(int id) => _databaseContext.Roles.Find(id)!;

        public dynamic Read() => _databaseContext.Roles;

        public bool Update(Role entity)
        {
            try
            {
                _databaseContext.Roles.Update(entity);
                return _databaseContext.SaveChanges() > 0;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}