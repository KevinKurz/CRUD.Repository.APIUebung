using CRUD.Contracts.QueryParams;
using CRUD.Core.DataModelService;
using CRUD.Core.DataService;

namespace CRUD.Core
{
    public class PathValidator
    {
        private readonly IDataService<IModel> _jsonService;
        public PathValidator(IDataService<IModel> jsonService) // Create a constructor, in which you define which JsonService you want to include
        {
            _jsonService = jsonService;
        }

        public void IsPathParameterValide(QueryParameter queryParameter)
        {
            int tableId = queryParameter.TableId;
            int reservationId = queryParameter.ReservationId;

            List<TableModel> tempList = (List<TableModel>)_jsonService.LoadList();

            if (tableId < 0 || reservationId < 0 || tableId > tempList.Count - 1 || reservationId > tempList[tableId].Availability.Count)
            {
                throw new ArgumentOutOfRangeException("You are looking out of your available range");
            }
        }
    }
}
