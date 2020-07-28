namespace ClassRegistration.DataAccess.Pagination
{
    /// <summary>
    /// This class limits the amount of output data.
    /// </summary>
    public class ModelPagination
    {
        // setting the maximum page size
        const int maxPageSize = 50;

        public int PageNumber { get; set; } = 1;

        private int _pageSize = 10;

        public int PageSize
        {
            get
            {
                return _pageSize;
            }
            set
            {
                // setting the value of page size to be the maximum page size if the page size is greater than the maximum.
                _pageSize = (value > maxPageSize) ? maxPageSize : value;
            }
        }
    }
}
