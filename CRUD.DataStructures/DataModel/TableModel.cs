using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CRUD.DataStructures.DataModel
{
    /// <summary>
    /// Defines the SQL Main Table
    /// </summary>
    public class TableModel : IModel
    {
        // PrimaryKey for SQL Database
        private int _id;
        [Key]
        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        // ForeignKey, to reference to the PrimaryKey in the ReservationModelClass
        // Equals the List of Reservation in the DTO Class
        private int _reservationId;
        [ForeignKey("ReservationModel")]
        public int ReservationId
        {
            get { return _reservationId; }
            set { _reservationId = value; }
        }
        //public virtual ReservationModel ReservationModel { get; set; }


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

        public TableModel(int kapacityInput, string nameInput)
        {
            _capacity = kapacityInput;
            _name = nameInput;
        }
    }
}
