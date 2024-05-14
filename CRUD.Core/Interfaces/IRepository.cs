using CRUD.DataStructures.DTOs;

namespace CRUD.Core.Interfaces
{
    public interface IRepository<T, PathParameter, OptionsParameter> 
    {
        /// <summary>
        /// </summary>
        /// <returns>List of <see cref="IDto"/></returns>
        IEnumerable<T> GetAll(PathParameter pathParameter, OptionsParameter optionsParameter);

        /// <summary>
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Type of <see cref="IDto"/></returns>
        T GetById(PathParameter pathParameter, OptionsParameter optionsParameter);

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
        void UpdateById(PathParameter pathParameter, T dto);

        /// <summary>
        /// Delete reservation of the Type <see cref="ReservationDto"/>by ID <br/>
        /// </summary>
        void DeleteById(PathParameter pathParameter);

        /// <summary>
        /// Deletre all reservations of the Type  <see cref="IDto"/>
        /// </summary>
        void DeleteAll();
    }
}
