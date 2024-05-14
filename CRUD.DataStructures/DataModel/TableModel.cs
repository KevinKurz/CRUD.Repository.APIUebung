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
        private int id;
        [Key]
        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        // ForeignKey, to reference to the PrimaryKey in the ReservationModelClass
        // Equals the List of Reservation in the DTO Class
        private int reservationId;
        [ForeignKey("ReservationModel")]
        public int ReservationId
        {
            get { return reservationId; }
            set { reservationId = value; }
        }
        //public virtual ReservationModel ReservationModel { get; set; }


        private int kapacity;
        public int Kapacity
        {
            get { return kapacity; }
            set { kapacity = value; }
        }

        private string name;
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        private List<ReservationModel> availability = new List<ReservationModel>();
        public List<ReservationModel> Availability
        {
            get { return availability; }
            set { availability = value; }
        }

        public TableModel(int kapacityInput, string nameInput)
        {
            Kapacity = kapacityInput;
            Name = nameInput;
        }
    }
}
