using CRUD.Core.Interfaces;
using CRUD.Core.QueryParams;
using CRUD.DataBank;
using CRUD.DataStructures.DataModel;
using CRUD.DataStructures.DTOs.TableDTO;
using CRUD.Core.FilterService;

namespace CRUD.Core.Repositories
{
    public class TableRepository : IRepository<ITableDto, QueryParameter, OptionsParameter>
    {
        private readonly IDataService<IModel> _jsonService;
        private readonly PathValidator _pathValidator;
        private readonly TableFilterService _tableFilterService;
        public TableRepository(IDataService<IModel> jsonService, PathValidator pathValidator, TableFilterService tableFilterService)
        {
            _jsonService = jsonService;
            _pathValidator = pathValidator;
            _tableFilterService = tableFilterService;
        }

        /// <summary>
        /// Transforms a List of <see cref="TableModel"/> to a List of <see cref="TableDto"/><br/>
        /// </summary>
        /// <returns>List of <see cref="TableDto"/></returns>
        public IEnumerable<ITableDto> GetAll(QueryParameter queryParameter, OptionsParameter optionsParameter)
        {
            List<TableModel> tempList = (List<TableModel>)_jsonService.LoadList();

            List<TableDto> dtoList = new List<TableDto>();

            foreach (TableModel model in tempList)
            {
                TableDto dto = Mapper.Map(model);
                dtoList.Add(dto);
            }

            dtoList = _tableFilterService.Filter(optionsParameter, dtoList);

            return dtoList;
        }

        /// <summary>
        /// Gets the mapped <see cref="TableDto"/> from the List of <see cref="TableModel"/> by the index <paramref name="tableId"/><br/>
        /// </summary>
        /// <param name="tableId"></param>
        /// <returns><see cref="TableDto"/></returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public ITableDto GetById(QueryParameter queryParameter, OptionsParameter optionsParameter)
        {
            _pathValidator.IsPathParameterValide(queryParameter);
            int tableId = queryParameter.TableId;
            List<TableModel> tempList = (List<TableModel>)_jsonService.LoadList();

            TableModel model = tempList[tableId];
            TableDto dto = Mapper.Map(model);
            return dto;
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
                currentCapacity += capacityChecker.Capacity;
            }

            if (currentCapacity + model.Capacity <= 50)
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
        public void UpdateById(QueryParameter queryParameter, ITableDto tableDto)
        {
            _pathValidator.IsPathParameterValide(queryParameter);
            int currentCapacity = 0;
            int tableId = queryParameter.TableId;

            //Check that tableNumber is bigger than the basetables
            if (tableId > 3)
            {
                TableModel model = Mapper.Map((UpdateTableDto)tableDto);
                List<TableModel> tempList = (List<TableModel>)_jsonService.LoadList();

                //Check if maximum capacity is not exceeded. Max capacity is 50 Places.
                foreach (TableModel capacityChecker in tempList)
                {
                    currentCapacity += capacityChecker.Capacity;
                }

                //Model to be updated is considered in the summary of the whole capacity
                if (currentCapacity + model.Capacity - tempList[tableId].Capacity <= 50)
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
        public void DeleteById(QueryParameter queryParameter)
        {
            _pathValidator.IsPathParameterValide(queryParameter);
            int tableId = queryParameter.TableId;
            List<TableModel> tempList = (List<TableModel>)_jsonService.LoadList();

            //Check that tableNumber is bigger than the basetables
            if (tableId > 3)
            {
                tempList.RemoveAt(tableId);
                _jsonService.SafeList(tempList);
            }
            else
            {
                throw new ArgumentOutOfRangeException("You try to delete one of your base tables");
            }
        }
    }
}
