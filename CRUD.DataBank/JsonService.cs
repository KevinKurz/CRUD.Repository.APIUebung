using CRUD.DataStructures.DataModel;
using Newtonsoft.Json;

namespace CRUD.DataBank
{
    public class JsonService
    {
        private JsonBank _jsonBank;
        public JsonService(JsonBank jsonBank) 
        {
            _jsonBank = jsonBank;
        }

        public virtual void SaveListAsJsonFile(List<TableModel> availableTables)
        {
            string jsonData = JsonConvert.SerializeObject(availableTables, Formatting.Indented);
            File.WriteAllText(_jsonBank._filepath, jsonData);
        }

        public virtual List<TableModel> LoadListFromJsonFile()
        {
            if (File.Exists(_jsonBank._filepath) == false || string.IsNullOrEmpty(File.ReadAllText(_jsonBank._filepath)) == true)
            {
                SaveListAsJsonFile(_jsonBank.AvailableTables);
            }
            using (StreamReader reader = new StreamReader(_jsonBank._filepath))
            {
                string jsonData = reader.ReadToEnd();
                List<TableModel> tables = JsonConvert.DeserializeObject<List<TableModel>>(jsonData)!;
                return tables;
            }
        }
    }
}
