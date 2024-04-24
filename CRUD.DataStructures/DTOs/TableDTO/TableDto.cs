using CRUD.DataStructures.DTOs.ReservationDTO;
using System.ComponentModel.DataAnnotations;

namespace CRUD.DataStructures.DTOs.TableDTO
{
    public class TableDto : ITableDto
    {
        private int kapacity;
        private string name;

        [Required]
        [Range(2, 10)]
        public int Kapacity
        {
            get { return kapacity; }
            set { kapacity = value; }
        }

        [Required]
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        private List<ReservationDto> availability = new List<ReservationDto>();
        public List<ReservationDto> Availability
        {
            get { return availability; }
            set { availability = value; }
        }

        public TableDto(int kapacityInput, string nameInput)
        {
            Kapacity = kapacityInput;
            Name = nameInput;
        }
    }
}
