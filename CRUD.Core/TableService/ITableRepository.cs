namespace CRUD.Core.TableService
{
    public interface ITableRepository<T>
    {
        IEnumerable<T> GetAll();
        T GetById(int id);
        void Create(T tableDto);
        void UpdateById(T tableDto, int id);
        void DeleteById(int id);
        void DeleteAll();
        void IsRequestQueryValide(int id);

    }
}
