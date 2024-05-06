using CRUD.DataStructures.DTOs.ReservationDTO;

namespace CRUD.Core.ReservationService

{
    public interface IReservationRepository<T>
    {
        /// <summary>
        /// </summary>
        /// <param name="tableId"></param>
        /// <returns>List of <see cref="ReservationDto"/></returns>
        IEnumerable<T> GetAll(int tableId);

        /// <summary>
        /// </summary>
        /// <param name="tableId"></param>
        /// <param name="reservationId"></param>
        /// <returns><see cref="ReservationDto"/></returns>
        T GetById(int tableId, int reservationId);

        /// <summary>
        /// <see cref="T"/> must be a Type of <see cref="CreateReservationDto"/>
        /// </summary>
        /// <param name="reservationDto"></param>
        void Create(T reservationDto);

        /// <summary>
        /// <paramref name="reservationDto"/> must be a type of <see cref="UpdateReservationDto"/>
        /// </summary>
        /// <param name="tableId"></param>
        /// <param name="reservationId"></param>
        /// <param name="reservationDto"></param>
        void UpdateById(int tableId, int reservationId, T reservationDto);

        /// <summary>
        /// Delete reservation of the Type <see cref="ReservationDto"/>by ID <br/>
        /// </summary>
        void DeleteById(int tableId, int reservationId);

        /// <summary>
        /// Deletre all reservations of the Type  <see cref="ReservationDto"/>
        /// </summary>
        void DeleteAll();
    }
}
