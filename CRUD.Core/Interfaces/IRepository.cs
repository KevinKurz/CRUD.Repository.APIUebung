using CRUD.DataStructures.DTOs;

namespace CRUD.Core.Interfaces
{
    public interface IRepository<T, QueryParams> 
    {
        /// <summary>
        /// </summary>
        /// <param name="tableId"></param>
        /// <returns>List of <see cref="IDto"/></returns>
        IEnumerable<T> GetAll(int tableId, QueryParams param);

        /// <summary>
        /// </summary>
        /// <param name="tableId"></param>
        /// <param name="reservationId"></param>
        /// <returns>Type of <see cref="IDto"/></returns>
        T GetById(int tableId, int reservationId, QueryParams param);

        /// <summary>
        /// <see cref="T"/> must be a CreateType of <see cref="IDto"/>
        /// </summary>
        /// <param name="dto"></param>
        void Create(T dto);

        /// <summary>
        /// <paramref name="reservationDto"/> must be a type of <see cref="UpdateReservationDto"/>
        /// </summary>
        /// <param name="tableId"></param>
        /// <param name="reservationId"></param>
        /// <param name="reservationDto"></param>
        void UpdateById(int tableId, int reservationId, T dto);

        /// <summary>
        /// Delete reservation of the Type <see cref="ReservationDto"/>by ID <br/>
        /// </summary>
        void DeleteById(int tableId, int reservationId);

        /// <summary>
        /// Deletre all reservations of the Type  <see cref="IDto"/>
        /// </summary>
        void DeleteAll();
    }
}
