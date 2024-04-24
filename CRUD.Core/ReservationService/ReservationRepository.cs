using CRUD.DataBank;
using CRUD.DataStructures.DTOs.ReservationDTO;
using CRUD.DataStructures.DTOs.TableDTO;
using CRUD.DataStructures.DataModel;

namespace CRUD.Core.ReservationService
{
    public class ReservationRepository : IReservationRepository<IReservationDto>
    {
        JsonService jsonService = new JsonService();

        // ------------------------------------------------------------------
        // Post Reservation
        // ------------------------------------------------------------------
        public void Create(IReservationDto reservation)
        {
            ReservationModel createModel = Mapper.Map((CreateReservationDto)reservation);

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
                            throw new NotImplementedException();
                        }
                        else if (TimeOnly.Parse(createModel.EndTime).IsBetween(TimeOnly.Parse(resDto.StartTime), TimeOnly.Parse(resDto.EndTime)) == true)
                        {
                            throw new NotImplementedException();
                        }
                    }
                }
            }
            tempList[tableNumber].Availability.Add(createModel);
            jsonService.SaveListAsJsonFile(tempList);
        }

        // ------------------------------------------------------------------
        // Get All Reservations
        // ------------------------------------------------------------------
        public IEnumerable<IReservationDto> GetAll(int tableID)
        {
            // get TableModel from JSON Table List per index
            TableModel tableModel = jsonService.LoadListFromJsonFile()[tableID];

            // Create temp List of reservations from the mapped table
            TableDto tableDto = Mapper.Map(tableModel);

            // Fill the tempList with mapped reservationModels
            foreach (ReservationModel model in tableModel.Availability)
            {
                ReservationDto reservationDto = Mapper.Map(model);
                tableDto.Availability.Add(reservationDto);
            }

            return tableDto.Availability;
        }

        // ------------------------------------------------------------------
        // Get Single Reservation
        // ------------------------------------------------------------------
        public IReservationDto GetById(int tableId, int reservationId)
        {
            ReservationModel tableModel = jsonService.LoadListFromJsonFile()[tableId].Availability[reservationId];
            
            ReservationDto tableDto = Mapper.Map(tableModel);

            return tableDto;
        }

        // ------------------------------------------------------------------
        // Delete All Reservations
        // ------------------------------------------------------------------
        public void DeleteAll()
        {
            List<TableModel> tempList = jsonService.LoadListFromJsonFile();

            foreach (TableModel tableDto in tempList)
            {
                if (tableDto.Availability!.Count != 0 && tableDto.Availability != null)
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
        public void UpdateById(int tableId, int reservationId, IReservationDto reservation)
        {
            ReservationModel updateModel = Mapper.Map((UpdateReservationDto)reservation);

            List<TableModel> tempList = jsonService.LoadListFromJsonFile();

            //Delete Reservation through given IDs
            tempList[tableId].Availability.RemoveAt(reservationId);

            int tableNumber = 0;

            while (updateModel.Kapacity > tempList[tableNumber].Kapacity)
            {
                tableNumber++;
            }

            if (tempList[tableNumber].Availability.Count != 0)
            {
                foreach (ReservationModel resDto in tempList[tableNumber].Availability)
                {
                    if (resDto.Date.Equals(updateModel.Date) == true)
                    {
                        if (TimeOnly.Parse(updateModel.StartTime).IsBetween(TimeOnly.Parse(resDto.StartTime), TimeOnly.Parse(resDto.EndTime)) == true)
                        {
                            throw new NotImplementedException();
                        }
                        else if (TimeOnly.Parse(updateModel.EndTime).IsBetween(TimeOnly.Parse(resDto.StartTime), TimeOnly.Parse(resDto.EndTime)) == true)
                        {
                            throw new NotImplementedException();
                        }
                    }
                }
            }
            tempList[tableNumber].Availability.Add(updateModel);
            jsonService.SaveListAsJsonFile(tempList);
        }


        // ------------------------------------------------------------------
        // Check, if query parametrs were given correctly
        // ------------------------------------------------------------------
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
