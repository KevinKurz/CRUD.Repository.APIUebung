using CRUD.DataStructures.DTOs.ReservationDTO;

namespace CRUD.Core.ReservationService

{
    public interface IReservationRepository<T>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="tableId"></param>
        /// <returns>List of <see cref="ReservationDto"/></returns>
        IEnumerable<T> GetAll(int tableId);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="tableId"></param>
        /// <param name="reservationId"></param>
        /// <returns><see cref="ReservationDto"/></returns>
        T GetById(int tableId, int reservationId);
        void Create(T reservationDto);
        void UpdateById(int tableId, int reservationId, T reservationDto);
        void DeleteById(int tableId, int reservationId);
        void DeleteAll();
        void IsRequestQueryValide(int tableId, int reservationId);
    }
}
