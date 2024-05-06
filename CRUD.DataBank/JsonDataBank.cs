using CRUD.DataStructures.DataModel;

namespace CRUD.DataBank
{
    public class JsonDataBank
    {
        public List<TableModel> AvailableTables = new List<TableModel>()
        {
            new TableModel(2, "Narrentisch"),
            new TableModel(5, "Prinzentisch"),
            new TableModel(8, "Königstisch"),
            new TableModel(10, "Göttertisch")
        };

        public readonly string filepath = Environment.GetEnvironmentVariable("FilePathForJsonFile")!;
    }
}
