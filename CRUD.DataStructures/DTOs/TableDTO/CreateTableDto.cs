using CRUD.DataStructures.AttributeService;
using CRUD.DataStructures.DTOs.ReservationDTO;

namespace CRUD.DataStructures.DTOs.TableDTO
{
    public class CreateTableDto : IDto
    {
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

        private List<CreateReservationDto> availability = new List<CreateReservationDto>();
        public List<CreateReservationDto> Availability
        {
            get { return availability; }
            set { availability = value; }
        }

        public CreateTableDto(int kapacityInput, string nameInput)
        {
            Kapacity = kapacityInput;
            Name = nameInput;
        }
    }
}
