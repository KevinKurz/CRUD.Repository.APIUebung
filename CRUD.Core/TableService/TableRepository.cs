using CRUD.DataBank;
using CRUD.DataStructures.DataModel;
using CRUD.DataStructures.DTOs.TableDTO;

namespace CRUD.Core.TableService
{
    public class TableRepository : ITableRepository<ITableDto>
    {
        JsonService jsonService = new JsonService();

        /// <summary>
        /// <paramref name="tableDto"/> must be a type of <see cref="CreateTableDto"/>
        /// </summary>
        /// <param name="tableDto"></param>
        /// <exception cref="NotImplementedException"></exception>
        public void Create(ITableDto tableDto)
        {
            int maxCapacity = 0;
            
            //Check if maximum capacity is not exceeded. Max capacity is 50 Places.
            foreach (TableModel capacityChecker in jsonService.LoadListFromJsonFile())
            {
                maxCapacity += capacityChecker.Kapacity;
            }

            if (maxCapacity <= 50)
            {
                TableModel model = Mapper.Map((CreateTableDto)tableDto);
                List<TableModel> tempList = jsonService.LoadListFromJsonFile();
                tempList.Add(model);
                jsonService.SaveListAsJsonFile(tempList);
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
            int maxCapacity = 0;

            //Check if maximum capacity is not exceeded. Max capacity is 50 Places.
            if (tableNumber < 3)
            {
                foreach (TableModel capacityChecker in jsonService.LoadListFromJsonFile())
                {
                    maxCapacity += capacityChecker.Kapacity;
                }
                if (maxCapacity <= 50)
                {
                    TableModel model = Mapper.Map((UpdateTableDto)tableDto);
                    List<TableModel> tempList = jsonService.LoadListFromJsonFile();
                    model = tempList[tableNumber];
                    jsonService.SaveListAsJsonFile(tempList);
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
            List<TableModel> tempList = jsonService.LoadListFromJsonFile();

            //Check that tableNumber is bigger than the basetables
            if(tableNumber <= tempList.Count - 1 && tableNumber > 3 )
            {
                tempList.RemoveAt(tableNumber);
                jsonService.SaveListAsJsonFile(tempList);
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
            List<TableModel> tempList = jsonService.LoadListFromJsonFile();

            //Do not delete basetables
            if(tempList.Count >= 4 )
            {
                tempList.RemoveRange(4, tempList.Count - 4);
                jsonService.SaveListAsJsonFile(tempList);
            }
            else
            { 
                throw new ArgumentOutOfRangeException("All possible tables are already deletet"); 
            }
        }
        /// <summary>
        /// Gets the mapped <see cref="TableDto"/> from the List of <see cref="TableModel"/> by the index <paramref name="tableNumber"/>
        /// </summary>
        /// <param name="tableNumber"></param>
        /// <returns><see cref="TableDto"/></returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public ITableDto GetById(int tableNumber)
        {
            List<TableModel> tempList = jsonService.LoadListFromJsonFile();

            if (tableNumber > 0 && tableNumber <= tempList.Count - 1)
            {
                TableModel model = tempList[tableNumber];
                TableDto dto = Mapper.Map(model);
                return dto;
            }
            else
            {
                throw new ArgumentOutOfRangeException();
            }
        }
        /// <summary>
        /// Transforms a List of <see cref="TableModel"/> to a List of <see cref="TableDto"/>
        /// </summary>
        /// <returns>List of <see cref="TableDto"/></returns>
        public IEnumerable<ITableDto> GetAll()
        {
            List<TableModel> tempList = jsonService.LoadListFromJsonFile();

            List<TableDto> dtoList = new List<TableDto>();

            foreach ( TableModel model in tempList )
            {
                TableDto dto = Mapper.Map(model);
                dtoList.Add(dto);
            }

            return dtoList;
        }
        public void IsRequestQueryValide(int tableId)
        {
            List<TableModel> tempList = jsonService.LoadListFromJsonFile();

            if (tableId < 0 || tableId > tempList.Count - 1)
            {
                throw new ArgumentOutOfRangeException();
            }
        }
    }
}
