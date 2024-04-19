using CRUD.DataStructures.DTOs.ReservationDTO;
using CRUD.DataStructures.DTOs.TableDTO;

namespace CRUD.Core.ReservationService
{
    public interface IReservationRepository
    {
        GetReservationDto GetById(int id, int id2);
        List<GetTableDto> GetAll();
        bool Create(CreateReservationDto reservationDto);
        bool UpdateById(int id1, int id2, UpdateReservationDto reservationDto);
        void DeleteById(int id, int id2);
        void DeleteAll();
        void IsRequestQueryValide(int id, int id2);
    }
}
