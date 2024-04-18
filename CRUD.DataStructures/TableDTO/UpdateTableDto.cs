using CRUD.DataStructures.ReservationDTO;

namespace CRUD.DataStructures.TableDTO
{
    public class UpdateTableDto : IDto
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

        private List<UpdateReservationDto> availability = new List<UpdateReservationDto>();
        public List<UpdateReservationDto> Availability
        {
            get { return availability; }
            set { availability = value; }
        }

        public UpdateTableDto(int kapacityInput, string nameInput)
        {
            Kapacity = kapacityInput;
            Name = nameInput;
        }
    }
}
