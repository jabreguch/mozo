using Mozo.Helper.Enu;
using Mozo.Helper.Global;

namespace Mozo.Model.Maestro;


public static class RedSocialPaging
{

    private static Func<int, List<RedSocialModel>, bool, string?> RedSocialConcat = (coTipoRedSocial, lst, first) =>
    {
        if (lst == null) return null;
        if (lst.Count() == 0) return null;
        List<RedSocialModel> r = lst.FindAll(x => x.CoTipoRedSocial == coTipoRedSocial);
        string? s = null;
        if (first == false)
        {
            if (r.Count() > 0)
            {
                foreach (var item in r) s = s + item.NoRedSocial + ", ";
                s = s.Text();
                if (s!.Substring(s.Length - 1, 1) == ",") s = s.Substring(0, s.Length - 1);
            }
            return s;
        }

        if (r.Count() > 0)
            return r.First()?.NoRedSocial;

        return null;
    };

    private static Func<int, int?, List<RedSocialModel>, List<RedSocialModel>?> RedSocialAll = (coTipoRedSocial, flWhatsapp, lst) =>
    {
        if (lst == null) return null;
        if (lst.Count() == 0) return null;
        List<RedSocialModel> r = null!;
        if (flWhatsapp == null)
        {
            r = lst.FindAll(x => x.CoTipoRedSocial == coTipoRedSocial);
        }
        else if (flWhatsapp == 1)
        {
            r = lst.FindAll(x => x.CoTipoRedSocial == coTipoRedSocial && x.FlWhatsapp == 1);
        }

        return r;
    };

    public static string? NoCorreoElectronicoAll(this List<RedSocialModel> lst) => RedSocialConcat(EnuTipoGeneral.Maestro.RedSocial.CorreoElectronico, lst, false);
    public static string? NoTelefonoMovilAl(this List<RedSocialModel> lst) => RedSocialConcat(EnuTipoGeneral.Maestro.RedSocial.TelefonoMovil, lst, false);
    public static string? NoTelefonoFijoAll(this List<RedSocialModel> lst) => RedSocialConcat(EnuTipoGeneral.Maestro.RedSocial.TelefonoFijo, lst, false);

    public static string? NoCorreoElectronicoFirst(this List<RedSocialModel> lst) => RedSocialConcat(EnuTipoGeneral.Maestro.RedSocial.CorreoElectronico, lst, true);
    public static string? NoTelefonoMovilFirst(this List<RedSocialModel> lst) => RedSocialConcat(EnuTipoGeneral.Maestro.RedSocial.TelefonoMovil, lst, true);
    public static string? NoTelefonoFijoFirst(this List<RedSocialModel> lst) => RedSocialConcat(EnuTipoGeneral.Maestro.RedSocial.TelefonoFijo, lst, true);

    public static List<RedSocialModel>? CorreoElectronicoAll(this List<RedSocialModel> lst) => RedSocialAll(EnuTipoGeneral.Maestro.RedSocial.CorreoElectronico, null, lst);
    public static List<RedSocialModel>? TelefonoMovilAll(this List<RedSocialModel> lst) => RedSocialAll(EnuTipoGeneral.Maestro.RedSocial.TelefonoMovil, null, lst);
    public static List<RedSocialModel>? TelefonoFijoAll(this List<RedSocialModel> lst) => RedSocialAll(EnuTipoGeneral.Maestro.RedSocial.TelefonoFijo, null, lst);

    public static List<RedSocialModel>? WhatsappAll(this List<RedSocialModel> lst) => RedSocialAll(EnuTipoGeneral.Maestro.RedSocial.TelefonoMovil, 1, lst);


}