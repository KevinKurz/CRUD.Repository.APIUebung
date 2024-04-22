using CRUD.DataStructures.DTOs.TableDTO;

namespace CRUD.Core.TableService
{
    public interface ITableRepository
    {
        List<TableDto> GetAll();
        TableDto GetById(int id);
        void CreateTable(CreateTableDto table);
        void UpdateTable(UpdateTableDto table, int id);
        void DeleteById(int id);
        void DeleteAll();
        void IsRequestQueryValide(int id);

    }
}
