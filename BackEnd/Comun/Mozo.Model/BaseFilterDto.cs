namespace Mozo.Model;

public record BaseFilterDto
{
    public int? FlEstReg { get; set; }

    #region Search 
    public string? NoInputSearch { get; set; }
    #endregion

    public int? PageSize { get; set; }
    /*
        #region OFFSET + LIMIT
        public int? CursorId { get; set; }
        public int? Direction { get; set; }
        #endregion
    */

    #region OFFSET + LIMIT
    public int? PageIndex { get; set; }
    #endregion

    public int? FlEmpresaNotKey { get; set; }

}

