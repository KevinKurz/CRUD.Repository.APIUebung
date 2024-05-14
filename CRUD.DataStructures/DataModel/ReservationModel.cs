using System.ComponentModel.DataAnnotations;

namespace CRUD.DataStructures.DataModel
{
    /// <summary>
    /// Defines the SQL Secondary Table, which is linked through an ForeignKey defined in the TableModelClass
    /// </summary>
    public class ReservationModel : IModel
    {
        // PrimaryKey for SQL Database
        private int id;
        [Key]
        public int Id
        {
            get { return id; }
            set { id = value; }
        }


        private int kapacity;
        public int Kapacity
        {
            get { return kapacity; }
            set { kapacity = value; }
        }
        private string lastName;
        public string LastName
        {
            get { return lastName; }
            set { lastName = value; }
        }
        private string startTime;
        public string StartTime
        {
            get { return startTime; }
            set { startTime = value; }
        }
        private string endTime;
        public string EndTime
        {
            get { return endTime; }
            set { endTime = value; }
        }
        private string date;
        public string Date
        {
            get { return date; }
            set { date = value; }
        }
        public ReservationModel(int kapacityInput, string lastnameInput, string startTimeInput, string endTimeInput, string dateInput)
        {
            kapacity = kapacityInput;
            lastName = lastnameInput;
            startTime = startTimeInput;
            endTime = endTimeInput;
            date = dateInput;
        }
    }
}
