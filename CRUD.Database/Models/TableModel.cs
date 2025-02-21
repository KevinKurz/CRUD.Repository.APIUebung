using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUD.Database.Models
{
    public class TableModel
    {
        private int _capacity;
        private string? _name;
        private ICollection<ReservationModel>? _reservations; // Ausbilder fragen
        public int Capacity
        {
            get { return _capacity; }
            set { _capacity = value; }
        }
        public string? Name
        {
            get { return _name; }
            set { _name = value; }
        }
        public ICollection<ReservationModel>? Reservations
        {
            get { return _reservations; }
            set { _reservations = value; }
        }
    }
}
