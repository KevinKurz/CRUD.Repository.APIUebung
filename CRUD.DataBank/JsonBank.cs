using CRUD.DataStructures.DataModel;

namespace CRUD.DataBank
{
    public class JsonBank
    {
        public List<TableModel> AvailableTables = new List<TableModel>()
        {
            new TableModel(2, "Narrentisch"),
            new TableModel(5, "Prinzentisch"),
            new TableModel(8, "Königstisch"),
            new TableModel(10, "Göttertisch")
        };

        public readonly string _filepath = @"C:\Users\Kevin.Kurz\OneDrive - PlanB. GmbH\Desktop\TableList.json";
    }
}
