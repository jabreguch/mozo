namespace Mozo.Model.Interfaces;

public interface IBaseFilterModel
{
    int? CoEmpresa { get; set; }
    int? PageIndex { get; set; }
    int? PageSize { get; set; }
    //int? CoUsuCre { get; set; }
    //int? CoUsuMod { get; set; }
    //int? CoUsuEli { get; set; }
}
