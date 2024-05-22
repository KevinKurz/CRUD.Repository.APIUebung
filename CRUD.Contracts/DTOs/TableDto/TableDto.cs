using CRUD.Contracts.DTOs.ReservationDto;
using System.ComponentModel.DataAnnotations;

namespace CRUD.Contracts.DTOs.TableDto
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

        // For TableFilterService
        public TableDto()
        {

        }

        // For Mapper
        public TableDto(int capacityInput, string? nameInput)
        {
            _capacity = capacityInput;
            _name = nameInput;
        }

        public static bool operator ==(TableDto table1, TableDto table2)
        {
            if (table1.Capacity == table2.Capacity && table1.Name == table2.Name)
            {
                return true;
            }
            return false;
        }

        public static bool operator !=(TableDto table1, TableDto table2)
        {
            if (table1.Capacity != table2.Capacity || table1.Name != table2.Name)
            {
                return true;
            }
            return false;
        }
    }
}
