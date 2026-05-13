namespace Mozo.Model.Seguridad;

public record IngresoFilterDto : BaseFilterDto //IngresoFilter>
{

    public int? CoEmpresa { get; set; }
    public int? CoIngreso { get; set; }
    public string? NoRefreshToken { get; set; }
}
[Serializable]
public partial class IngresoModel : BaseModel //<IngresoModel>
{
    public int? CoIngreso { get; set; }
    public int? CoPersona { get; set; }
    public int? CoPermiso { get; set; }
    public string? NoIp { get; set; }
    public string? NoRefreshToken { get; set; }
    public string? NoRefreshTokenPrevious { get; set; }
}
public partial class IngresoModel
{
    public int? CoEmpresa { get; set; }
}

//public partial class IngresoDto : BaseModel
//{
//    public static ValueTask<PermisoModel?> BindAsync(HttpContext ctx, ParameterInfo pr) => ValueTask.FromResult(ctx.GetModel<PermisoModel>());
//    public static ValueTask<IngresoDto?> BindAsync(HttpContext ctx, ParameterInfo parameter)
//    {
//        IngresoDto result = new IngresoDto
//        {
//            CoEmpresa = ctx.GetValue<int>("CoEmpresa"),
//            CoIngreso = ctx.GetValue<int>("CoIngreso"),
//            CoPermiso = ctx.GetValue<int>("CoPermiso"),
//            NoRefreshToken = ctx.GetValue<string>("NoRefreshToken")
//        };

//        return ValueTask.FromResult<IngresoDto?>(result);
//    }
//    public int? CoIngreso { get; set; }
//    public int? CoPermiso { get; set; }
//    public string? NoRefreshToken { get; set; }
//}


//public static class IngresoMapper
//{
//    public static IngresoModel? Map(IngresoDto dto)
//    {
//        return dto == null ? null : new IngresoModel()
//        {
//            //CoEmpresa = dto.CoEmpresa
//        }.SetValueDefault(dto);
//    }
//}

////CoPersona