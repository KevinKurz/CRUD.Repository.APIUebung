using System.ComponentModel.DataAnnotations;

namespace CRUD.Contracts.Queries.TableQuery
{
    public class UpdateTableQuery : ITableQuery
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

        public UpdateTableQuery(int kapacityInput, string nameInput)
        {
            _capacity = kapacityInput;
            _name = nameInput;
        }
    }
}
