using CRUD.DataStructures.DataModel;
using Newtonsoft.Json;

namespace CRUD.DataBank
{
    public class DataService : IDataService<IModel>
    {
        private JsonDataBank _jsonDataBank;
        public DataService()
        {
            _jsonDataBank = new JsonDataBank();
        }

        public virtual void SafeList(IEnumerable<IModel> availableTables)
        {
            string jsonData = JsonConvert.SerializeObject(availableTables, Formatting.Indented);
            File.WriteAllText(_jsonDataBank.filepath, jsonData);
        }

        public virtual IEnumerable<IModel> LoadList()
        {
            if (File.Exists(_jsonDataBank.filepath) == false || string.IsNullOrEmpty(File.ReadAllText(_jsonDataBank.filepath)) == true)
            {
                SafeList(_jsonDataBank.AvailableTables);
            }
            using (StreamReader reader = new StreamReader(_jsonDataBank.filepath))
            {
                string jsonData = reader.ReadToEnd();
                List<TableModel> tables = JsonConvert.DeserializeObject<List<TableModel>>(jsonData)!;
                return tables;
            }
        }
    }
}
