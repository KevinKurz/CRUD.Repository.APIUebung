using CRUD.Contracts.DTOs;
using CRUD.Contracts.Queries;

namespace CRUD.Repository.Interfaces
{
    public interface IRepository<DTO, Query, QueryParameter, OptionsParameter>
        where DTO : IDto
        where Query : IQuery
    {
        /// <summary>
        /// </summary>
        /// <returns>List of <see cref="IQuery"/></returns>
        IEnumerable<DTO> GetAll(QueryParameter queryParameter, OptionsParameter optionsParameter);

        /// <summary>
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Type of <see cref="IQuery"/></returns>
        DTO GetById(QueryParameter queryParameter, OptionsParameter optionsParameter);

        /// <summary>
        /// <see cref="T"/> must be a CreateType of <see cref="IQuery"/>
        /// </summary>
        /// <param name="query"></param>
        void Create(Query query);

        /// <summary>
        /// <paramref name="reservationDto"/> must be a type of <see cref="UpdateReservationDto"/>
        /// </summary>
        /// <param name="tableId"></param>
        /// <param name="reservationId"></param>
        /// <param name="query"></param>
        void UpdateById(QueryParameter queryParameter, Query query);

        /// <summary>
        /// Delete reservation of the Type <see cref="ReservationDto"/>by ID <br/>
        /// </summary>
        void DeleteById(QueryParameter queryParameter);

        /// <summary>
        /// Delete all reservations of the Type  <see cref="IQuery"/>
        /// </summary>
        void DeleteAll();
    }
}
