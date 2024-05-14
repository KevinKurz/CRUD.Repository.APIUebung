using CRUD.Core.Interfaces;

namespace CRUD.Core.QueryParams
{
    public class TableOptionsParameter : IOptionsParameter
    {
        public TableOptionsParameter(string capacity, string name, string availability)
        {
            if (int.TryParse(capacity, out int resultCapacity))
            {
                _capacity = resultCapacity;
            }
            _name = name;
            _availability = availability;
        }

        private int _capacity;
        private string _name;
        private string _availability;
        public int Capacity
        {
            get { return _capacity; }
            set { _capacity = value; }
        }
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }
        public string Availability
        {
            get { return _availability; }
            set { _availability = value; }
        }
    }
}
