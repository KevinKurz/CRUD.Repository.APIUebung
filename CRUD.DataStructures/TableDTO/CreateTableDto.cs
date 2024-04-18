using CRUD.DataStructures.ReservationDTO;

namespace CRUD.DataStructures.TableDTO
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
