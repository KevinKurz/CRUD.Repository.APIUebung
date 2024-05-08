using CRUD.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUD.Core.QueryParams
{
    public class ReservationQueryParams : IQueryParams
    {
        public ReservationQueryParams(string capacity, string lastName, string startTime, string endTime, string date) 
        {
            if(int.TryParse(capacity, out int result))
            {
                _capacity = result;
            }
            _lastName = lastName;
            _startTime = startTime;
            _endTime = endTime;
            _date = date;
        }

        private int _capacity;
        private string _lastName;
        private string _startTime;
        private string _endTime;
        private string _date;
        public int Capacity
        {
            get { return _capacity; }
            set { _capacity = value; }
        }
        public string LastName
        {
            get { return _lastName; }
            set { _lastName = value; }
        }
        public string StartTime
        {
            get { return _startTime; }
            set { _startTime = value; }
        }
        public string EndTime
        {
            get { return _endTime; }
            set { _endTime = value; }
        }
        public string Date
        {
            get { return _date; }
            set { _date = value; }
        }
    }
}
