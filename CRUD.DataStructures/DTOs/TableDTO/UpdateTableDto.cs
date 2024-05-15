using System.ComponentModel.DataAnnotations;

namespace CRUD.DataStructures.DTOs.TableDTO
{
    public class UpdateTableDto : ITableDto
    {
        private int _capacity;
        private string _name;

        [Required]
        [Range(2, 10)]
        public int Capacity
        {
            get { return _capacity; }
            set { _capacity = value; }
        }

        [Required]
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public UpdateTableDto(int kapacityInput, string nameInput)
        {
            _capacity = kapacityInput;
            _name = nameInput;
        }
    }
}
