using CRUD.DataStructures.DTOs.ReservationDTO;
using CRUD.DataStructures.DTOs.TableDTO;
using System.Net.NetworkInformation;

namespace CRUD.DataStructures.DataModel
{
    public static class Mapper
    {

        // Reservation Mapping--------------------------------------------------------------------------------------

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

        // Table Mapping--------------------------------------------------------------------------------------------

        //Map from Model to TableDto
        public static TableDto Map(TableModel tableModel)
        {
            TableDto dto = new TableDto(tableModel.Kapacity, tableModel.Name);
            return dto;
        }

        //Map from CreateTableDto to Model
        public static TableModel Map(CreateTableDto tableDto) 
        {
            TableModel model = new TableModel(tableDto.Kapacity, tableDto.Name);
            return model;
        }

        //Map from UpdateTableDto to Model
        public static TableModel Map(UpdateTableDto tableDto)
        {
            TableModel model = new TableModel(tableDto.Kapacity, tableDto.Name );
            return model;
        }
    }
}
