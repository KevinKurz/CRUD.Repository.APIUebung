using CRUD.DataStructures.AttributeService;
using CRUD.DataStructures.DTOs.ReservationDTO;
using System.ComponentModel.DataAnnotations;

namespace CRUD.DataStructures.DTOs.TableDTO
{
    public class CreateTableDto : ITableDto
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

        public CreateTableDto(int kapacityInput, string nameInput)
        {
            Kapacity = kapacityInput;
            Name = nameInput;
        }
    }
}
