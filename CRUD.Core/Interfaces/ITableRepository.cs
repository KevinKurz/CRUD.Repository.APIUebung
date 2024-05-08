using CRUD.DataStructures.DTOs.TableDTO;

namespace CRUD.Core.Interfaces
{
    public interface ITableRepository<T, QueryParams>
    {
        /// <summary>
        /// With a <see cref="QueryParams"/> as a filtering object
        /// </summary>
        /// <returns>List of <see cref="TableDto"/></returns>
        IEnumerable<T> GetAll(QueryParams queryParams);

        /// <summary>
        /// With a <see cref="QueryParams"/> as a filtering object
        /// </summary>
        /// <param name="tableId"></param>
        /// <returns><see cref="TableDto"/></returns>
        T GetById(int tableId, QueryParams query);

        /// <summary>
        /// </summary>
        /// <returns>List of <see cref="TableDto"/></returns>
        IEnumerable<T> GetAll();

        /// <summary>
        /// </summary>
        /// <param name="tableId"></param>
        /// <returns><see cref="TableDto"/></returns>
        T GetById(int tableId);

        /// <summary>
        /// <see cref="T"/> must be a type of <see cref="CreateTableDto"/>
        /// </summary>
        /// <param name="tableDto"></param>
        void Create(T tableDto);

        /// <summary>
        /// <see cref="T"/> must be a type of <see cref="UpdateTableDto"/>
        /// </summary>
        /// <param name="tableDto"></param>
        /// <param name="tableId"></param>
        void UpdateById(T tableDto, int tableId);

        /// <summary>
        /// Delete table of the tyoe <see cref="TableDto"/>
        /// </summary>
        /// <param name="tableId"></param>
        void DeleteById(int tableId);

        /// <summary>
        /// Delete all tables of the type <see cref="TableDto"/>
        /// </summary>
        void DeleteAll();
    }
}
