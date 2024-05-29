namespace Backend.BLL
{
    public class PagedResponse<T> where T : BaseEntity
    {
        public PagedResponse(int pageNumber, int pageSize, int totalRecords, List<T> data)
        {
            Data = data;
            PageNumber = pageNumber;
            PageSize = pageSize;
            TotalRecords = totalRecords;
            TotalPages = (int)Math.Ceiling((decimal)totalRecords / (decimal)pageSize);
        }

        public int PageNumber { get; init; }
        public int PageSize { get; init; }
        public int TotalRecords { get; init; }
        public int TotalPages { get; init; }
        public List<T> Data { get; init; }
    }
}
