namespace Mozo.Model.Interfaces;

public interface IBasePaginationModel
{
    public int? PageIndex { get; set; }
    public int? PageSize { get; set; }
    public int? PageCount { get; set; }
    public int? RowsCount { get; set; }
}
