using CRUD.DataBank;
using CRUD.DataStructures.DataModel;

namespace CRUD.Core
{
    public class QueryValidator
    {
        private JsonService _jsonService = new JsonService();
        public QueryValidator(JsonService jsonService) // Create a constructor, in which you define which JsonService you want to include
        {
            _jsonService = jsonService;
        }

        public void IsReservationRequestQueryValide(int tableId, int reservationId)
        {
            List<TableModel> tempList = _jsonService.LoadListFromJsonFile();

            if (tableId < 0 || reservationId < 0 || tableId > tempList.Count - 1 || reservationId > tempList[tableId].Availability.Count - 1)
            {
                throw new ArgumentOutOfRangeException("You are looking out of your available range");
            }
        }

        public void IsTableRequestQueryValide(int tableId)
        {
            List<TableModel> tempList = _jsonService.LoadListFromJsonFile();

            if (tableId < 0 || tableId > tempList.Count - 1)
            {
                throw new ArgumentOutOfRangeException("You are looking out of your available range");
            }
        }
    }
}
