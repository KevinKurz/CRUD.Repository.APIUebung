using CRUD.DataStructures.AttributeService;
using CRUD.DataStructures.DTOs.ReservationDTO;
using System.Diagnostics.CodeAnalysis;

namespace CRUD.DataStructures.DTOs.TableDTO
{
    public class TableDto : IDto
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
