using Mozo.MaestroData;
using Mozo.Model.Maestro;

namespace Mozo.MaestroBusiness;

public interface IDocumentoIdentidadBusiness
{
    Task<IEnumerable<DocumentoIdentidadModel>> SelAllActiveAsync();
}

public class DocumentoIdentidadBusiness : IDocumentoIdentidadBusiness
{
    private readonly IDocumentoIdentidadData _data;
    public DocumentoIdentidadBusiness(IDocumentoIdentidadData data)
    {
        _data = data;
    }
    public async Task<IEnumerable<DocumentoIdentidadModel>> SelAllActiveAsync()
    {
        var r = await _data.SelAllActiveAsync();
        return r.OrderBy(x => x.NoCodigo);
    }


}
