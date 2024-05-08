using CRUD.Core.Interfaces;
using CRUD.DataBank;
using CRUD.DataStructures.DataModel;
using CRUD.DataStructures.DTOs.TableDTO;

namespace CRUD.Core.Repositories
{
    public class TableRepository2 : IRepository<ITableDto, IQueryParams>
    {
        private readonly IDataService<IModel> _jsonService;
        public TableRepository2(IDataService<IModel> jsonService)
        {
            _jsonService = jsonService;
        }

        /// <summary>
        /// Transforms a List of <see cref="TableModel"/> to a List of <see cref="TableDto"/><br/>
        /// Includes a <see cref="IQueryParams"/> for filtering
        /// </summary>
        /// <returns>List of <see cref="TableDto"/></returns>
        public IEnumerable<ITableDto> GetAll(int tableId, IQueryParams query)
        {
            List<TableModel> tempList = (List<TableModel>)_jsonService.LoadList();

            List<TableDto> dtoList = new List<TableDto>();

            foreach (TableModel model in tempList)
            {
                TableDto dto = Mapper.Map(model);
                dtoList.Add(dto);
            }
            return dtoList;
        }

        /// <summary>
        /// Gets the mapped <see cref="TableDto"/> from the List of <see cref="TableModel"/> by the index <paramref name="tableId"/><br/>
        /// Includes a <see cref="IQueryParams"/> for filtering
        /// </summary>
        /// <param name="tableId"></param>
        /// <returns><see cref="TableDto"/></returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public ITableDto GetById(int tableId, int reservationId,  IQueryParams query)
        {
            List<TableModel> tempList = (List<TableModel>)_jsonService.LoadList();

            if (tableId >= 0 && tableId <= tempList.Count - 1)
            {
                TableModel model = tempList[tableId];
                TableDto dto = Mapper.Map(model);
                return dto;
            }
            else
            {
                throw new ArgumentOutOfRangeException("You try to get a table which is not existing");
            }
        }

        /// <summary>
        /// <paramref name="tableDto"/> must be a type of <see cref="CreateTableDto"/>
        /// </summary>
        /// <param name="tableDto"></param>
        /// <exception cref="NotImplementedException"></exception>
        public void Create(ITableDto tableDto)
        {
            int currentCapacity = 0;
            TableModel model = Mapper.Map((CreateTableDto)tableDto);

            //Check if maximum capacity is not exceeded. Max capacity is 50 Places.
            foreach (TableModel capacityChecker in _jsonService.LoadList())
            {
                currentCapacity += capacityChecker.Kapacity;
            }

            if (currentCapacity + model.Kapacity <= 50)
            {
                List<TableModel> tempList = (List<TableModel>)_jsonService.LoadList();
                tempList.Add(model);
                _jsonService.SafeList(tempList);
            }
            else
            {
                throw new NotImplementedException("You exceeded your maximum capacity limit. Delete or edit some tables.");
            }
        }

        /// <summary>
        /// <paramref name="tableDto"/> must be a type of <see cref="UpdateTableDto"/><br/>
        /// <paramref name="tableId"/> must be grater then 3
        /// </summary>
        /// <param name="tableDto"></param>
        /// <param name="tableId"></param>
        /// <exception cref="NotImplementedException"></exception>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public void UpdateById(int tableId, int reservationId, ITableDto tableDto)
        {
            int currentCapacity = 0;

            //Check if maximum capacity is not exceeded. Max capacity is 50 Places.
            if (tableId > 3)
            {
                TableModel model = Mapper.Map((UpdateTableDto)tableDto);
                List<TableModel> tempList = (List<TableModel>)_jsonService.LoadList();

                //Check if maximum capacity is not exceeded. Max capacity is 50 Places.
                foreach (TableModel capacityChecker in tempList)
                {
                    currentCapacity += capacityChecker.Kapacity;
                }

                //Model to be updated is considered in the summary of the whole capacity
                if (currentCapacity + model.Kapacity - tempList[tableId].Kapacity <= 50)
                {
                    tempList[tableId] = model;
                    _jsonService.SafeList(tempList);
                }
                else
                {
                    throw new NotImplementedException("You exceeded your maximum capacity limit. Delete or edit some tables.");
                }
            }
            else
            {
                throw new ArgumentOutOfRangeException("You can't update the first four base tables");
            }
        }

        /// <summary>
        /// Delete all tables except the four base tables
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public void DeleteAll()
        {
            List<TableModel> tempList = (List<TableModel>)_jsonService.LoadList();

            //Do not delete basetables
            tempList.RemoveRange(4, tempList.Count - 4);
            _jsonService.SafeList(tempList);
        }

        /// <summary>
        /// Delete one table from the List of <see cref="TableModel"/> by the index <paramref name="tableId"/>
        /// </summary>
        /// <param name="tableId"></param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public void DeleteById(int tableId, int reservationId)
        {
            List<TableModel> tempList = (List<TableModel>)_jsonService.LoadList();

            //Check that tableNumber is bigger than the basetables
            if (tableId <= tempList.Count - 1 && tableId > 3)
            {
                tempList.RemoveAt(tableId);
                _jsonService.SafeList(tempList);
            }
            else
            {
                throw new ArgumentOutOfRangeException("You either try to delete one of your base tables or you try to delete out of your range");
            }
        }
    }
}
