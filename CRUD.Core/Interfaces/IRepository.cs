using CRUD.DataStructures.DTOs;

namespace CRUD.Core.Interfaces
{
    public interface IRepository<T, QueryParameter, OptionsParameter> 
    {
        /// <summary>
        /// </summary>
        /// <returns>List of <see cref="IDto"/></returns>
        IEnumerable<T> GetAll(QueryParameter queryParameter, OptionsParameter optionsParameter);

        /// <summary>
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Type of <see cref="IDto"/></returns>
        T GetById(QueryParameter queryParameter, OptionsParameter optionsParameter);

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
        /// <param name="dto"></param>
        void UpdateById(QueryParameter queryParameter, T dto);

        /// <summary>
        /// Delete reservation of the Type <see cref="ReservationDto"/>by ID <br/>
        /// </summary>
        void DeleteById(QueryParameter queryParameter);

        /// <summary>
        /// Deletre all reservations of the Type  <see cref="IDto"/>
        /// </summary>
        void DeleteAll();
    }
}
