using System.ComponentModel.DataAnnotations;
using CRUD.DataStructures.AttributeService;

namespace CRUD.DataStructures.DTOs.ReservationDTO
{
    public class CreateReservationDto : IReservationDto
    {
        private int _capacity;
        private string _lastName;
        private string _startTime;
        private string _endTime;
        private string _date;

        [Range(1, 10)]
        public int Capacity
        {
            get { return _capacity; }
            set { _capacity = value; }
        }

        [Required]
        [MaxLength(20)]
        public string LastName
        {
            get { return _lastName; }
            set { _lastName = value; }
        }

        [Required]
        [DataType(DataType.Time)]
        [EndtimeEarlierThanStarttime("EndTime")]
        public string StartTime
        {
            get { return _startTime; }
            set { _startTime = value; }
        }

        [Required]
        [DataType(DataType.Time)]
        public string EndTime
        {
            get { return _endTime; }
            set { _endTime = value; }
        }

        [Required]
        [DataType(DataType.Date)]
        [DateValidation()]
        public string Date
        {
            get { return _date; }
            set { _date = value; }
        }

        public CreateReservationDto(int kapacityInput, string lastnameInput, string startTimeInput, string endTimeinput, string dateInput)
        {
            _capacity = kapacityInput;
            _lastName = lastnameInput;
            _startTime = startTimeInput;
            _endTime = endTimeinput;
            _date = dateInput;
        }
    }
}
