using CRUD.DataStructures.DataModel;
using Newtonsoft.Json;

namespace CRUD.DataBank
{
    public class JsonService
    {
        private static List<TableModel> AvailableTables = new List<TableModel>()
        {
            new TableModel(2, "Narrentisch"),
            new TableModel(5, "Prinzentisch"),
            new TableModel(8, "Königstisch"),
            new TableModel(10, "Göttertisch"),
        };

        const string filepath = @"C:\Users\Kevin.Kurz\OneDrive - PlanB. GmbH\Desktop\TableList.json";

        public void SaveListAsJsonFile(List<TableModel> availableTables)
        {
            string jsonData = JsonConvert.SerializeObject(availableTables, Formatting.Indented);
            File.WriteAllText(filepath, jsonData);
        }

        public List<TableModel> LoadListFromJsonFile()
        {
            if (File.Exists(filepath) == false || string.IsNullOrEmpty(File.ReadAllText(filepath)) == true)
            {
                SaveListAsJsonFile(AvailableTables);
            }
            using (StreamReader reader = new StreamReader(filepath))
            {
                string jsonData = reader.ReadToEnd();
                List<TableModel> tables = JsonConvert.DeserializeObject<List<TableModel>>(jsonData);
                return tables;
            }
        }
    }
}
