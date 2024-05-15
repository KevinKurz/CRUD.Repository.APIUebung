using CRUD.DataStructures.DTOs.ReservationDTO;
using System.ComponentModel.DataAnnotations;

namespace CRUD.DataStructures.DTOs.TableDTO
{
    public class TableDto : ITableDto
    {
        private int _capacity;
        private string? _name;

        [Required]
        [Range(2, 10)]
        public int Capacity
        {
            get { return _capacity; }
            set { _capacity = value; }
        }

        [Required]
        public string? Name
        {
            get { return _name; }
            set { _name = value; }
        }

        private List<ReservationDto> _availability = new List<ReservationDto>();
        public List<ReservationDto> Availability
        {
            get { return _availability; }
            set { _availability = value; }
        }

        // For TableFilterService
        public TableDto()
        {
            
        }

        // For Mapper
        public TableDto(int capacityInput, string nameInput)
        {
            _capacity = capacityInput;
            _name = nameInput;
        }
    }
}
