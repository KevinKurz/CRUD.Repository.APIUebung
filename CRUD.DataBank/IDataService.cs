namespace CRUD.DataBank
{
    public interface IDataService<T>
    {
        public void SafeList(IEnumerable<T> list);

        public IEnumerable<T> LoadList();

    }
}
