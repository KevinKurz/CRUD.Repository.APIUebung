namespace CRUD.Core.DataModelService
{
    public class TableModel : IModel
    {
        private int _capacity;
        public int Capacity
        {
            get { return _capacity; }
            set { _capacity = value; }
        }

        private string _name;
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        private List<ReservationModel> availability = new List<ReservationModel>();
        public List<ReservationModel> Availability
        {
            get { return availability; }
            set { availability = value; }
        }

        public TableModel(int capacityInput, string nameInput)
        {
            _capacity = capacityInput;
            _name = nameInput;
        }
    }
}
