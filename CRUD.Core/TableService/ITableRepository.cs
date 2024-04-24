namespace CRUD.Core.TableService
{
    public interface ITableRepository<T>
    {
        IEnumerable<T> GetAll();
        T GetById(int id);
        void Create(T table);
        void UpdateById(T table, int id);
        void DeleteById(int id);
        void DeleteAll();
        void IsRequestQueryValide(int id);

    }
}
