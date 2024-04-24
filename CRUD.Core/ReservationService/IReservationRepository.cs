namespace CRUD.Core.ReservationService
{
    public interface IReservationRepository<T>
    {
        IEnumerable<T> GetAll(int id);
        T GetById(int id, int id2);
        void Create(T reservationDto);
        void UpdateById(int id1, int id2, T reservationDto);
        void DeleteById(int id, int id2);
        void DeleteAll();
        void IsRequestQueryValide(int id, int id2);
    }
}
