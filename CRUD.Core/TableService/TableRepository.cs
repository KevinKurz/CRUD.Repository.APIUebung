using CRUD.DataBank;
using CRUD.DataStructures.DataModel;
using CRUD.DataStructures.DTOs.TableDTO;

namespace CRUD.Core.TableService
{
    public class TableRepository : ITableRepository
    {
        JsonService jsonService = new JsonService();
        public void CreateTable(CreateTableDto tableDto)
        {

        }
        public void UpdateTable(UpdateTableDto tableDto, int tableNumber)
        {

        }
        public void DeleteById(int tableNumber)
        {

        }
        public void DeleteAll()
        {

        }
        public TableDto GetById(int tableNumber)
        {
            return null;
        }
        public List<TableDto> GetAll()
        {
            return null;
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
