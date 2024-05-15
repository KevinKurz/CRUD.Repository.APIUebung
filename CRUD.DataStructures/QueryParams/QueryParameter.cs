namespace CRUD.Core.QueryParams
{
    public class QueryParameter
    {
        public QueryParameter() { }
        public QueryParameter(int tableId)
        {
            _tableId = tableId;
        }
        public QueryParameter(int tableId, int reservationId)
        {
            _tableId = tableId;
            _reservationId = reservationId;
        }

        private int _tableId;
        private int _reservationId;

        public int TableId
        {
            get { return _tableId; }
            set { _tableId = value; }
        }

        public int ReservationId
        {
            get { return _reservationId; }
            set { _reservationId = value; }
        }
    }
}
