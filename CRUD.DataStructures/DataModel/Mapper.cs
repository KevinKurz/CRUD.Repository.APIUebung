using CRUD.DataStructures.DTOs.ReservationDTO;
using CRUD.DataStructures.DTOs.TableDTO;

namespace CRUD.DataStructures.DataModel
{
    public static class Mapper
    {
        //Map from CreateReservationDto to Model
        public static ReservationModel Map(CreateReservationDto reservationDto)
        {
            ReservationModel model = new ReservationModel(reservationDto.Kapacity, reservationDto.LastName, reservationDto.StartTime, reservationDto.EndTime, reservationDto.Date);
            return model;
        }

        //Map from UpdateReservationDto to Model
        public static ReservationModel Map(UpdateReservationDto reservationDto)
        {
            ReservationModel model = new ReservationModel(reservationDto.Kapacity, reservationDto.LastName, reservationDto.StartTime, reservationDto.EndTime, reservationDto.Date);
            return model;
        }

        //Map from Model to ReservationDto
        public static ReservationDto Map(ReservationModel reservationDto)
        {
            ReservationDto dto = new ReservationDto(reservationDto.Kapacity, reservationDto.LastName, reservationDto.StartTime, reservationDto.EndTime, reservationDto.Date);
            return dto;
        }

        //Map from TableModel to TableDto
        public static TableDto Map(TableModel tableModel)
        {
            TableDto dto = new TableDto(tableModel.Kapacity, tableModel.Name);
            return dto;
        }
    }
}
