namespace CRUD.Contracts.QueryParams
{
    public class OptionsParameter
    {
        public OptionsParameter(string? filter, string? sortBy)
        {
            _filter = filter;
            _sortBy = sortBy;
        }

        private string? _filter;
        private string? _sortBy;

        public string? Filter
        {
            get { return _filter; }
            set { _filter = value; }
        }
        public string? SortBy
        {
            get { return _sortBy; }
            set { _sortBy = value; }
        }
    }
}
