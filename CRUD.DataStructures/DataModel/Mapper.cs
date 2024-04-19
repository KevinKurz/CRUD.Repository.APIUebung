using CRUD.DataStructures.DTOs.ReservationDTO;

namespace CRUD.DataStructures.DataModel
{
    public static class Mapper
    {
        //Map from CreateReservation to Model
        public static ReservationModel Map(CreateReservationDto reservationDto)
        {
            ReservationModel model = new ReservationModel(reservationDto.Kapacity, reservationDto.LastName, reservationDto.StartTime, reservationDto.EndTime, reservationDto.Date);
            return model;
        }

        //Map from UpdateReservation to Model
        public static ReservationModel Map(UpdateReservationDto reservationDto)
        {
            ReservationModel model = new ReservationModel(reservationDto.Kapacity, reservationDto.LastName, reservationDto.StartTime, reservationDto.EndTime, reservationDto.Date);
            return model;
        }

        //Map from Model to GetReservation
        public static GetReservationDto Map(ReservationModel reservationDto)
        {
            GetReservationDto model = new GetReservationDto(reservationDto.Kapacity, reservationDto.LastName, reservationDto.StartTime, reservationDto.EndTime, reservationDto.Date);
            return model;
        }
    }
}
