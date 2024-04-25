using CRUD.DataStructures.DTOs.TableDTO;

namespace CRUD.Core.TableService
{
    public interface ITableRepository<T>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns>List of <see cref="TableDto"/></returns>
        IEnumerable<T> GetAll();
        /// <summary>
        /// 
        /// </summary>
        /// <param name="tableId"></param>
        /// <returns><see cref="TableDto"/></returns>
        T GetById(int tableId);
        void Create(T tableDto);
        void UpdateById(T tableDto, int tableId);
        void DeleteById(int tableId);
        void DeleteAll();
        void IsRequestQueryValide(int tableId);

    }
}
