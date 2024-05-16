using System.ComponentModel.DataAnnotations;
using CRUD.DataStructures.AttributeService;

namespace CRUD.DataStructures.DTOs.ReservationDTO
{
    public class ReservationDto : IReservationDto
    {
        private int _capacity;
        private string? _lastName;
        private string? _startTime;
        private string? _endTime;
        private string? _date;

        [Range(1, 10)]
        public int Capacity
        {
            get { return _capacity; }
            set { _capacity = value; }
        }

        [Required]
        [MaxLength(20)]
        public string? LastName
        {
            get { return _lastName; }
            set { _lastName = value; }
        }

        [Required]
        [EndtimeEarlierThanStarttime("EndTime")]
        public string? StartTime
        {
            get { return _startTime; }
            set { _startTime = value; }
        }

        [Required]
        public string? EndTime
        {
            get { return _endTime; }
            set { _endTime = value; }
        }

        [Required]
        [DataType(DataType.Date)]
        [DateValidation()]
        public string? Date
        {
            get { return _date; }
            set { _date = value; }
        }

        //For filter
        public ReservationDto()
        {

        }

        public ReservationDto(int kapacityInput, string? lastnameInput, string? startTimeInput, string? endTimeinput, string? dateInput)
        {
            _capacity = kapacityInput;
            _lastName = lastnameInput;
            _startTime = startTimeInput;
            _endTime = endTimeinput;
            _date = dateInput;
        }

        // Operator for Unittest
        public static bool operator ==(ReservationDto reservationDto1, ReservationDto reservationDto2)
        {
            if (reservationDto1.Date == reservationDto2.Date && 
                reservationDto1.Capacity == reservationDto2.Capacity && 
                reservationDto1.StartTime == reservationDto2.StartTime && 
                reservationDto1.EndTime == reservationDto2.EndTime && 
                reservationDto1.LastName == reservationDto2.LastName)
            {
                return true;
            }
            return false;
        }

        public static bool operator !=(ReservationDto reservationDto1, ReservationDto reservationDto2)
        {
            if (reservationDto1.Date != reservationDto2.Date ||
                reservationDto1.Capacity != reservationDto2.Capacity ||
                reservationDto1.StartTime != reservationDto2.StartTime ||
                reservationDto1.EndTime != reservationDto2.EndTime ||
                reservationDto1.LastName != reservationDto2.LastName)
            {
                return true;
            }
            return false;
        }
    }
}
