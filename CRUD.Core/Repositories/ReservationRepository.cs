using CRUD.DataBank;
using CRUD.DataStructures.DTOs.ReservationDTO;
using CRUD.DataStructures.DTOs.TableDTO;
using CRUD.DataStructures.DataModel;
using CRUD.Core.Interfaces;

namespace CRUD.Core.Repositories
{
    public class ReservationRepository : IReservationRepository<IReservationDto>
    {
        private IDataService<IModel> _jsonService;
        public ReservationRepository(IDataService<IModel> jsonService) // Create a constructor, in which you define which JsonService you want to include
        {
            _jsonService = jsonService;
        }

        /// <summary>
        /// Get all reservations <br/>
        /// Maps a List of objects from <see cref="ReservationModel"/> to a List of <see cref="ReservationDto"/>
        /// </summary>
        /// <param name="tableID"></param>
        /// <returns>List of <see cref="ReservationDto"/></returns>
        public IEnumerable<IReservationDto> GetAll(int tableID)
        {
            // get TableModel from JSON Table List per index
            TableModel tableModel = (TableModel)_jsonService.LoadList().ElementAt(tableID);

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

        /// <summary>
        /// Get reservation by Id <br/>
        /// Gets the specific object of <see cref="ReservationDto"/> by the index <paramref name="tableId"/> and <paramref name="reservationId"/>
        /// </summary>
        /// <param name="tableId"></param>
        /// <param name="reservationId"></param>
        /// <returns><see cref="ReservationDto"/></returns>
        public IReservationDto GetById(int tableId, int reservationId)
        {
            ReservationModel tableModel = ((TableModel)_jsonService.LoadList().ElementAt(tableId)).Availability.ElementAt(reservationId); //"double"cast nessecary

            ReservationDto tableDto = Mapper.Map(tableModel);

            return tableDto;
        }


        /// <summary>
        /// Create a reservation <br/>
        /// <paramref name="reservation"/> must be a type of <see cref="CreateReservationDto"/>
        /// </summary>
        /// <param name="reservation"></param>
        /// <exception cref="NotImplementedException"></exception>
        public void Create(IReservationDto reservation)
        {
            ReservationModel createModel = Mapper.Map((CreateReservationDto)reservation);

            List<TableModel> tempList = (List<TableModel>)_jsonService.LoadList();

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
                        if (TimeOnly.Parse(createModel.StartTime).IsBetween(TimeOnly.Parse(resDto.StartTime), TimeOnly.Parse(resDto.EndTime)))
                        {
                            throw new NotImplementedException("Your starttime is in range with an already existing reservation");
                        }
                        else if (TimeOnly.Parse(createModel.EndTime).IsBetween(TimeOnly.Parse(resDto.StartTime), TimeOnly.Parse(resDto.EndTime)))
                        {
                            throw new NotImplementedException("Your endtime is in range with an already existing reservation");
                        }
                        else if (TimeOnly.Parse(resDto.StartTime).IsBetween(TimeOnly.Parse(createModel.StartTime), TimeOnly.Parse(createModel.EndTime)) && TimeOnly.Parse(resDto.EndTime).IsBetween(TimeOnly.Parse(createModel.StartTime), TimeOnly.Parse(createModel.EndTime)))
                        {
                            throw new NotImplementedException("Your reservation surrounds an already existing reservation");
                        }
                    }
                }
            }
            tempList[tableNumber].Availability.Add(createModel);
            _jsonService.SafeList(tempList);
        }

        /// <summary>
        /// Update reservation by ID <br/>
        /// <paramref name="reservation"/> must be a type of <see cref="UpdateReservationDto"/>
        /// </summary>
        /// <param name="tableId"></param>
        /// <param name="reservationId"></param>
        /// <param name="reservation"></param>
        /// <exception cref="NotImplementedException"></exception>
        public void UpdateById(int tableId, int reservationId, IReservationDto reservation)
        {
            ReservationModel updateModel = Mapper.Map((UpdateReservationDto)reservation);

            List<TableModel> tempList = (List<TableModel>)_jsonService.LoadList();

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
                            throw new NotImplementedException("Your starttime collides with already existing reservationtimes");
                        }
                        else if (TimeOnly.Parse(updateModel.EndTime).IsBetween(TimeOnly.Parse(resDto.StartTime), TimeOnly.Parse(resDto.EndTime)) == true)
                        {
                            throw new NotImplementedException("Your endtime collides with already existing reservationtimes");
                        }
                        else if (TimeOnly.Parse(resDto.StartTime).IsBetween(TimeOnly.Parse(updateModel.StartTime), TimeOnly.Parse(updateModel.EndTime)) && TimeOnly.Parse(resDto.EndTime).IsBetween(TimeOnly.Parse(updateModel.StartTime), TimeOnly.Parse(updateModel.EndTime)))
                        {
                            throw new NotImplementedException("Your reservation surrounds an already existing reservation");
                        }
                    }
                }
            }
            tempList[tableNumber].Availability.Add(updateModel);
            _jsonService.SafeList(tempList);
        }

        /// <summary>
        /// Delete reservation by ID <br/>
        /// Delete a specific <see cref="ReservationModel"/> from a table of the List of <see cref="TableModel"/>
        /// </summary>
        /// <param name="tableId"></param>
        /// <param name="reservationId"></param>
        public void DeleteById(int tableId, int reservationId)
        {
            List<TableModel> tempList = (List<TableModel>)_jsonService.LoadList();

            tempList[tableId].Availability.RemoveAt(reservationId);
            _jsonService.SafeList(tempList);
        }

        /// <summary>
        /// Delete all Reservations from all tables in the List of <see cref="TableModel"/>
        /// </summary>
        public void DeleteAll()
        {
            List<TableModel> tempList = (List<TableModel>)_jsonService.LoadList();

            foreach (TableModel tableDto in tempList)
            {
                if (tableDto.Availability!.Count != 0 && tableDto.Availability != null)
                {
                    tableDto.Availability.Clear();
                    _jsonService.SafeList(tempList);
                }
            }
        }
    }
}
