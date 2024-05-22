using CRUD.Contracts.DTOs.ReservationDto;
using CRUD.Contracts.DTOs.TableDto;
using CRUD.Contracts.Queries.ReservationQuery;
using CRUD.Contracts.Queries.TableQuery;

namespace CRUD.Core.DataModelService
{
    public static class Mapper
    {

        // Reservation Mapping--------------------------------------------------------------------------------------

        //Map from CreateReservationQuery to Model
        public static ReservationModel Map(CreateReservationQuery reservationDto)
        {
            ReservationModel model = new ReservationModel(reservationDto.Capacity, reservationDto.LastName, reservationDto.StartTime, reservationDto.EndTime, reservationDto.Date);
            return model;
        }

        //Map from UpdateReservationQuery to Model
        public static ReservationModel Map(UpdateReservationQuery reservationDto)
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
            TableDto dto = new TableDto(tableModel.Capacity, tableModel.Name);
            return dto;
        }

        //Map from CreateTableDto to Model
        public static TableModel Map(CreateTableQuery tableDto)
        {
            TableModel model = new TableModel(tableDto.Capacity, tableDto.Name);
            return model;
        }

        //Map from UpdateTableDto to Model
        public static TableModel Map(UpdateTableQuery tableDto)
        {
            TableModel model = new TableModel(tableDto.Capacity, tableDto.Name);
            return model;
        }
    }
}
