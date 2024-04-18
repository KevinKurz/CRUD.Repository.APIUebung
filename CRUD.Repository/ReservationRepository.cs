using CRUD.DataBank;
using CRUD.Database.Data;
using CRUD.Database.DatabaseModel;
using CRUD.Database.Model;
using CRUD.DataBase.DataModel;
using CRUD.DataStructures.ReservationDTO;
using CRUD.DataStructures.TableDTO;
using CRUD.Interface;

namespace CRUD_Reservation_ClassLibrary
{
    public class ReservationRepository : IReservationRepository
    {
        JsonServiceWithMapperObjects jsonService = new JsonServiceWithMapperObjects();

        // ------------------------------------------------------------------
        // Post Reservation
        // ------------------------------------------------------------------
        public bool Create(CreateReservationDto reservation)
        {
            ReservationModel createModel = Mapper.Map(reservation);

            bool isTimeValid = true;

            List<TableModel> tempList = jsonService.LoadListFromJsonFile();

            int tableNumber = 0;

            while (createModel.Kapacity > tempList[tableNumber].Kapacity)
            {
                tableNumber++;
            }
            if (tempList[tableNumber].Availability.Count != 0)
            {
                foreach (ReservationModel resDto in tempList[tableNumber].Availability)
                {
                    if (resDto.Date.Equals(createModel.Date) == true)
                    {
                        if (TimeOnly.Parse(createModel.StartTime).IsBetween(TimeOnly.Parse(resDto.StartTime), TimeOnly.Parse(resDto.EndTime)) == true)
                        {
                            isTimeValid = false;
                        }
                        else if (TimeOnly.Parse(createModel.EndTime).IsBetween(TimeOnly.Parse(resDto.StartTime), TimeOnly.Parse(resDto.EndTime)) == true)
                        {
                            isTimeValid = false;
                        }
                    }
                }
            }
            tempList[tableNumber].Availability.Add(createModel);
            jsonService.SaveListAsJsonFile(tempList);

            return isTimeValid; 
        }

        // ------------------------------------------------------------------
        // Get All Reservations
        // ------------------------------------------------------------------
        public List<GetTableDto> GetAll()
        {
            List<TableModel> tempList = jsonService.LoadListFromJsonFile();

            List<GetTableDto> getAllList = new List<GetTableDto>();

            for (int i = 0; i < 4; i++)
            {
                GetTableDto getTableDto = new GetTableDto(tempList[i].Kapacity, tempList[i].Name);
                getAllList.Add(getTableDto);

                foreach (ReservationModel model in tempList[i].Availability)
                {
                    GetReservationDto reservationDto = new GetReservationDto(model.Kapacity, model.LastName, model.StartTime, model.EndTime, model.Date);
                    getAllList[i].Availability.Add(reservationDto);
                }
            }
            return getAllList;
        }

        // ------------------------------------------------------------------
        // Get Single Reservation
        // ------------------------------------------------------------------
        public GetReservationDto GetById(int tableId, int reservationId)
        {
            List<TableModel> tempList = jsonService.LoadListFromJsonFile();

            ReservationModel getModel = tempList[tableId].Availability[reservationId];

            GetReservationDto getReservationDto = new GetReservationDto(getModel.Kapacity, getModel.LastName, getModel.StartTime, getModel.EndTime, getModel.Date);

            return getReservationDto;
        }

        // ------------------------------------------------------------------
        // Delete All Reservations
        // ------------------------------------------------------------------
        public void DeleteAll()
        {
            List<TableModel> tempList = jsonService.LoadListFromJsonFile();

            foreach (TableModel tableDto in tempList)
            {
                if (tableDto.Availability.Count != 0 && tableDto.Availability != null)
                {
                    tableDto.Availability.Clear();
                    jsonService.SaveListAsJsonFile(tempList);
                }
            }
        }

        // ------------------------------------------------------------------
        // Delete Single Reservation
        // ------------------------------------------------------------------
        public void DeleteById(int tableId, int reservationId)
        {
            List<TableModel> tempList = jsonService.LoadListFromJsonFile();

            tempList[tableId].Availability.RemoveAt(reservationId);
            jsonService.SaveListAsJsonFile(tempList);
        }

        // ------------------------------------------------------------------
        // Update Reservation
        // ------------------------------------------------------------------
        public bool UpdateById(int tableId, int reservationId, UpdateReservationDto reservation)
        {
            ReservationModel updateModel = Mapper.Map(reservation);
            bool isTimeValid = true;

            List<TableModel> tempList = jsonService.LoadListFromJsonFile();

            //Delete Reservation through given IDs
            tempList[tableId].Availability.RemoveAt(reservationId);

            int tableNumber = 0;

            while (reservation.Kapacity > tempList[tableNumber].Kapacity)
            {
                tableNumber++;
            }

            if (tempList[tableNumber].Availability.Count != 0)
            {
                foreach (ReservationModel resDto in tempList[tableNumber].Availability)
                {
                    if (resDto.Date.Equals(reservation.Date) == true)
                    {
                        if (TimeOnly.Parse(reservation.StartTime).IsBetween(TimeOnly.Parse(resDto.StartTime), TimeOnly.Parse(resDto.EndTime)) == true)
                        {
                            isTimeValid = false;
                        }
                        else if (TimeOnly.Parse(reservation.EndTime).IsBetween(TimeOnly.Parse(resDto.StartTime), TimeOnly.Parse(resDto.EndTime)) == true)
                        {
                            isTimeValid = false;
                        }
                    }
                }
            }
            tempList[tableNumber].Availability.Add(updateModel);
            jsonService.SaveListAsJsonFile(tempList);
            return isTimeValid;
        }

        public void IsRequestQueryValide(int tableId, int reservationId)
        {
            List<TableModel> tempList = jsonService.LoadListFromJsonFile();

            if (tableId < 0 || reservationId < 0 || tableId > tempList.Count - 1 || reservationId > tempList[tableId].Availability.Count - 1)
            {
                throw new ArgumentOutOfRangeException();
            }
        }
    }
}
