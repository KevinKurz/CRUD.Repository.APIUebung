using CRUD.Core.Interfaces;
using CRUD.DataStructures.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUD.Core.QueryParams
{
    public class TableQueryParams : IQueryParams
    {
        public TableQueryParams(string capacity, string name, string availability) 
        {
            if (int.TryParse(capacity, out int result))
            {
                _capacity = result;
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
