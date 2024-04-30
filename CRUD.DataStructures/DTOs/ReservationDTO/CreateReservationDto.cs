using System.ComponentModel.DataAnnotations;
using CRUD.DataStructures.AttributeService;

namespace CRUD.DataStructures.DTOs.ReservationDTO
{
    public class CreateReservationDto : IReservationDto
    {
        private int kapacity;
        private string? lastName;
        private string? startTime;
        private string? endTime;
        private string? date;

        [Range(1, 10)]
        public int Kapacity
        {
            get { return kapacity; }
            set { kapacity = value; }
        }

        [Required]
        [MaxLength(20)]
        public string? LastName
        {
            get { return lastName; }
            set { lastName = value; }
        }

        [Required]
        [DataType(DataType.Time)]
        [EndtimeEarlierThanStarttime("EndTime")]
        public string StartTime
        {
            get { return startTime; }
            set { startTime = value; }
        }

        [Required]
        [DataType(DataType.Time)]
        public string EndTime
        {
            get { return endTime; }
            set { endTime = value; }
        }

        [Required]
        [DataType(DataType.Date)]
        [DateValidation()]
        public string Date
        {
            get { return date; }
            set { date = value; }
        }

        public CreateReservationDto(int kapacityInput, string lastnameInput, string startTimeInput, string endTimeinput, string dateInput)
        {
            kapacity = kapacityInput;
            lastName = lastnameInput;
            startTime = startTimeInput;
            endTime = endTimeinput;
            date = dateInput;
        }
    }
}
