using CRUD.DataBank;
using CRUD.DataStructures.DataModel;
using CRUD.DataStructures.DTOs.TableDTO;

namespace CRUD.Core.TableService
{
    public class TableRepository : ITableRepository<ITableDto>
    {
        private JsonService _jsonService = new JsonService();
        public TableRepository(JsonService jsonService)
        {
            _jsonService = jsonService;
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
            foreach (TableModel capacityChecker in _jsonService.LoadListFromJsonFile())
            {
                currentCapacity += capacityChecker.Kapacity;
            }

            if (currentCapacity + model.Kapacity <= 50)
            {
                List<TableModel> tempList = _jsonService.LoadListFromJsonFile();
                tempList.Add(model);
                _jsonService.SaveListAsJsonFile(tempList);
            }
            else
            {
                throw new NotImplementedException("You exceeded your maximum capacity limit. Delete or edit some tables.");
            }
        }
        /// <summary>
        /// <paramref name="tableDto"/> must be a type of <see cref="UpdateTableDto"/><br/>
        /// <paramref name="tableNumber"/> must be grater then 3
        /// </summary>
        /// <param name="tableDto"></param>
        /// <param name="tableNumber"></param>
        /// <exception cref="NotImplementedException"></exception>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public void UpdateById(ITableDto tableDto, int tableNumber)
        {
            int currentCapacity = 0;

            //Check if maximum capacity is not exceeded. Max capacity is 50 Places.
            if (tableNumber > 3)
            {
                TableModel model = Mapper.Map((UpdateTableDto)tableDto);
                List<TableModel> tempList = _jsonService.LoadListFromJsonFile();

                //Check if maximum capacity is not exceeded. Max capacity is 50 Places.
                foreach (TableModel capacityChecker in tempList)
                {
                    currentCapacity += capacityChecker.Kapacity;
                }

                //Model to be updated is considered in the summary of the whole capacity
                if (currentCapacity + model.Kapacity - tempList[tableNumber].Kapacity <= 50)
                {
                    tempList[tableNumber] = model; 
                    _jsonService.SaveListAsJsonFile(tempList);
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
        /// Delete one table from the List of <see cref="TableModel"/> by the index <paramref name="tableNumber"/>
        /// </summary>
        /// <param name="tableNumber"></param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public void DeleteById(int tableNumber)
        {
            List<TableModel> tempList = _jsonService.LoadListFromJsonFile();

            //Check that tableNumber is bigger than the basetables
            if (tableNumber <= tempList.Count - 1 && tableNumber > 3)
            {
                tempList.RemoveAt(tableNumber);
                _jsonService.SaveListAsJsonFile(tempList);
            }
            else
            {
                throw new ArgumentOutOfRangeException("You either try to delete one of your base tables or you try to delete out of your range");
            }
        }
        /// <summary>
        /// Delete all tables except the four base tables
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public void DeleteAll()
        {
            List<TableModel> tempList = _jsonService.LoadListFromJsonFile();

            //Do not delete basetables
            tempList.RemoveRange(4, tempList.Count - 4);
            _jsonService.SaveListAsJsonFile(tempList);

        }
        /// <summary>
        /// Gets the mapped <see cref="TableDto"/> from the List of <see cref="TableModel"/> by the index <paramref name="tableNumber"/>
        /// </summary>
        /// <param name="tableNumber"></param>
        /// <returns><see cref="TableDto"/></returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public ITableDto GetById(int tableNumber)
        {
            List<TableModel> tempList = _jsonService.LoadListFromJsonFile();

            if (tableNumber >= 0 && tableNumber <= tempList.Count - 1)
            {
                TableModel model = tempList[tableNumber];
                TableDto dto = Mapper.Map(model);
                return dto;
            }
            else
            {
                throw new ArgumentOutOfRangeException("You try to get a table which is not existing");
            }
        }
        /// <summary>
        /// Transforms a List of <see cref="TableModel"/> to a List of <see cref="TableDto"/>
        /// </summary>
        /// <returns>List of <see cref="TableDto"/></returns>
        public IEnumerable<ITableDto> GetAll()
        {
            List<TableModel> tempList = _jsonService.LoadListFromJsonFile();

            List<TableDto> dtoList = new List<TableDto>();

            foreach (TableModel model in tempList)
            {
                TableDto dto = Mapper.Map(model);
                dtoList.Add(dto);
            }
            return dtoList;
        }
    }
}
