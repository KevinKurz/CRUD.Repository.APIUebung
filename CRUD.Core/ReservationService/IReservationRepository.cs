using CRUD.DataStructures.DTOs.ReservationDTO;
using CRUD.DataStructures.DTOs.TableDTO;

namespace CRUD.Core.ReservationService
{
    public interface IReservationRepository
    {
        ReservationDto GetById(int id, int id2);
        List<ReservationDto> GetAll(int id);
        void Create(CreateReservationDto reservationDto);
        void UpdateById(int id1, int id2, UpdateReservationDto reservationDto);
        void DeleteById(int id, int id2);
        void DeleteAll();
        void IsRequestQueryValide(int id, int id2);
    }




    public interface IReposity<T>
    {
        T GetById(int id);
        List<T> GetAll();
    }


    public class Reservation2Repository: IReposity<ReservationDto>
    {
        public ReservationDto GetById(int z)
        {
            throw new NotImplementedException();
        }

        public List<ReservationDto> GetAll()
        {
            throw new NotImplementedException() { };
        }
    }
}
