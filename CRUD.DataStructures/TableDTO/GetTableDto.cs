using CRUD.DataStructures.ReservationDTO;

namespace CRUD.DataStructures.TableDTO
{
    public class GetTableDto : IDto
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

        private List<GetReservationDto> availability = new List<GetReservationDto>();
        public List<GetReservationDto> Availability
        {
            get { return availability; }
            set { availability = value; }
        }

        public GetTableDto(int kapacityInput, string nameInput)
        {
            Kapacity = kapacityInput;
            Name = nameInput;
        }
    }
}
