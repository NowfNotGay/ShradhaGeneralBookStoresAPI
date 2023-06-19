namespace ShradhaGeneralBookStores.Service.Interface;

public interface IServiceCRUD<T>
{
    public dynamic Read();
    public bool Create(T entity);
    public bool Update(T entity);
    public bool Delete(int id);
    public dynamic Get(int id);
}
