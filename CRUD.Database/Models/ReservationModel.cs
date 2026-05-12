namespace CRUD.Database.Models
{
    public class ReservationModel
    {
        private int _id;
        private int _tableId;
        private TableModel? table;
        private int kapacity;
        private string? lastName;
        private string? startTime;
        private string? endTime;
        private string? date;

        public int Id 
        { 
            get { return _id; }
        }

        public TableModel? Table
        {
            get { return table; }
            set { table = value; }
        }

        public int Kapacity
        {
            get { return kapacity; }
            set { kapacity = value; }
        }
        public string? LastName
        {
            get { return lastName; }
            set { lastName = value; }
        }
        public string? StartTime
        {
            get { return startTime; }
            set { startTime = value; }
        }
        
        public string? EndTime
        {
            get { return endTime; }
            set { endTime = value; }
        }
        
        public string? Date
        {
            get { return date; }
            set { date = value; }
        }
    }
}
