//using Mozo.Model.Interfaces;

namespace Mozo.Model;

public class PaginationModel<T> //: //IBasePaginationModel
{
    public PaginationModel(IEnumerable<T> data, int? rowsCount, int? pageSize, int? pageIndex)
    {
        Data = data;
        RowsCount = rowsCount;
        PageSize = pageSize;
        PageIndex = pageIndex ?? 0;
        PageCount = pageSize > 0 ? (int)Math.Ceiling((decimal)((decimal)(rowsCount ?? 0) / (decimal)(pageSize ?? 0))) : 0;
    }
    public int? RowsCount { get; set; }
    public IEnumerable<T>? Data { get; set; }

    public int? PageSize { get; set; }

    #region KEYSET
    public int? FirstId { get; set; }
    public int? LastId { get; set; }
    #endregion

    #region OFFSET + LIMIT
    public int? PageIndex { get; set; }
    public int PageCount { get; set; }
    #endregion
}