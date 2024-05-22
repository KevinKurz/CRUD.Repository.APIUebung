namespace CRUD.Core.DataService
{
    public interface IDataService<T>
    {
        public void SafeList(IEnumerable<T> list);

        public IEnumerable<T> LoadList();

    }
}
