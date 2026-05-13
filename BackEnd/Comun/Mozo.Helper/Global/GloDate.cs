using Mozo.Helper.Enu;

using System.Globalization;
using System.Text;

namespace Mozo.Helper.Global;

public static partial class Glo
{
    private static readonly string[] MesCol =
    {
        "Enero",
        "Febrero",
        "Marzo",
        "Abril",
        "Mayo",
        "Junio",
        "Julio",
        "Agosto",
        "Septiembre",
        "Octubre",
        "Noviembre",
        "Diciembre"
    };
    //public static string[] MesCol = p;


    private static string[] GetHoraYMeridiano(string Hh)
    {
        if (Hh.Num() >= 0 && Hh.Num() < 11)
            return [Hh, "am"];
        if (Hh.Num() == 12)
            return [Hh, "am"];
        return [(Hh.Num()! - 12).CerosIzquierda(2), "pm"];
    }

    //yyyy
    //MM
    //dd
    public static string? FechaBd(this object XobjValue, string? NoHora = null)
    {
        //2021-03-20T20:16
        string? NoHoraTmp = null;

        if (XobjValue.EsNulo()) return null;

        string? NoFecha = XobjValue.Text();

        if (NoFecha!.Length == 7)
        {
            NoFecha = XobjValue.Text()!.Replace("-", "");
            NoHoraTmp = NoHora;
        }
        else if (NoFecha.Length == 10)
        {
            NoFecha = XobjValue.Text()!.Replace("-", "");
            NoHoraTmp = NoHora;
        }
        else if (NoFecha.Length == 8)
        {
            NoFecha = XobjValue.Text()!.Replace("-", "");
            NoHoraTmp = NoHora;
        }
        else if (NoFecha.Length == 16)
        {
            if (NoFecha.IndexOf("T", 0) > -1)
            {
                string[] a = XobjValue.Text()!.Replace("-", "").Split("T");
                NoFecha = a[0];
                NoHoraTmp = a[1]; //.HoraBd(); ;
            }
            else
            {
                NoFecha = XobjValue.Text()!.Replace("-", "");
            }
        }
        else
        {
            return null;
        }


        if (NoHoraTmp.NoNulo())
            NoHoraTmp = NoHoraTmp.HoraBd();
        else
            NoHoraTmp = null;
        return string.Concat(NoFecha, NoHoraTmp);
    }

    public static string? HoraBd(this object? XobjValue)
    {
        if (XobjValue.EsNulo()) return null;
        if (XobjValue.Text()!.Length == 5) return XobjValue.Text()!.Replace(":", "") + "00";
        return null;
    }

    public static DateTime FechaUtc(this long? unixTimeStamp)
    {
        // Unix timestamp is seconds past epoch
        DateTime dtDateTime = new(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
        dtDateTime = dtDateTime.AddSeconds(unixTimeStamp ?? default);
        return dtDateTime;
    }

    public static string[] InputFecha(this object? XobjValue, bool FlDateTimeLocal = false)
    {
        //2006-12-30' yyyy-MM-dd
        string Yyyy, Mm, Dd, Hh, Mi, Ss;
        string[] OutPut = { null, null };
        if (XobjValue.EsNulo()) return OutPut;
        if (XobjValue.Text()!.Length == 14)
        {
            Yyyy = XobjValue!.ToString()!.Substring(0, 4);
            Mm = XobjValue.ToString()!.Substring(4, 2);
            Dd = XobjValue.ToString()!.Substring(6, 2);

            Hh = XobjValue.ToString()!.Substring(8, 2);
            Mi = XobjValue.ToString()!.Substring(10, 2);
            Ss = XobjValue.ToString()!.Substring(12, 2);

            if (FlDateTimeLocal == false)
            {
                OutPut[0] = string.Concat(Yyyy, "-", Mm, "-", Dd);
                OutPut[1] = string.Concat(Hh, ":", Mi);
            }
            else
            {
                OutPut[0] = string.Concat(Yyyy, "-", Mm, "-", Dd, "T", Hh, ":", Mi);
                //OutPut[1] = null;
                return OutPut;
            }
        }

        if (XobjValue.Text()!.Length == 8)
        {
            Yyyy = XobjValue!.ToString()!.Substring(0, 4);
            Mm = XobjValue.ToString()!.Substring(4, 2);
            Dd = XobjValue.ToString()!.Substring(6, 2);

            OutPut[0] = string.Concat(Yyyy, "-", Mm, "-", Dd);
            //OutPut[1] = string.Concat(Hh, ":", Mi);
        }

        if (XobjValue.Text()!.Length == 6) // Time
        {
            Hh = XobjValue!.ToString()!.Substring(0, 2);
            Mi = XobjValue.ToString()!.Substring(2, 2);
            Ss = XobjValue.ToString()!.Substring(4, 2);

            OutPut[0] = string.Concat(Hh, ":", Mi);
            //OutPut[1] = string.Concat(Hh, ":", Mi);
        }

        else
        {
            return OutPut;
        }

        return OutPut;
    }


    public static string? SubtractionDateTime(this string Date1, string Date2)
    {
        if (Date1.EsNulo() || Date2.EsNulo()) return null;


        if (Date1.Trim().Length == 14 && Date2.Trim().Length == 6) Date2 = Date1.Substring(0, 8) + Date2;


        string YyyyI = "0000", MmI = "00", DdI = "00", HhI = "00", MiI = "00", SsI = "00";
        string YyyyF = "0000", MmF = "00", DdF = "00", HhF = "00", MiF = "00", SsF = "00";


        if (Date1.Trim().Length == 8)
        {
            YyyyI = Date1.Substring(0, 4);
            MmI = Date1.Substring(4, 2);
            DdI = Date1.Substring(6, 2);
        }
        else if (Date1.Trim().Length == 14)
        {
            YyyyI = Date1.Substring(0, 4);
            MmI = Date1.Substring(4, 2);
            DdI = Date1.Substring(6, 2);

            HhI = Date1.Substring(8, 2);
            MiI = Date1.Substring(10, 2);
            SsI = Date1.Substring(12, 2);
        }
        else if (Date1.Trim().Length == 6)
        {
            HhI = Date1.Substring(0, 2);
            MiI = Date1.Substring(2, 2);
            SsI = Date1.Substring(4, 2);
        }

        if (Date2.Trim().Length == 8)
        {
            YyyyF = Date2.Substring(0, 4);
            MmF = Date2.Substring(4, 2);
            DdF = Date2.Substring(6, 2);
        }
        else if (Date2.Trim().Length == 14)
        {
            YyyyF = Date2.Substring(0, 4);
            MmF = Date2.Substring(4, 2);
            DdF = Date2.Substring(6, 2);

            HhF = Date2.Substring(8, 2);
            MiF = Date2.Substring(10, 2);
            SsF = Date2.Substring(12, 2);
        }
        else if (Date2.Trim().Length == 6)
        {
            HhF = Date2.Substring(0, 2);
            MiF = Date2.Substring(2, 2);
            SsF = Date2.Substring(4, 2);
        }

        DateTime Dat1 = new(int.Parse(YyyyI), int.Parse(MmI), int.Parse(DdI), int.Parse(HhI), int.Parse(MiI),
            int.Parse(SsI));
        DateTime Dat2 = new(int.Parse(YyyyF), int.Parse(MmF), int.Parse(DdF), int.Parse(HhF), int.Parse(MiF),
            int.Parse(SsF));

        var Diff_dates = Dat2.Subtract(Dat1);
        var sb = new StringBuilder();
        if (Diff_dates.Days > 0) sb.Append(Diff_dates.Days + " dia(s) ");

        if (Diff_dates.Hours > 0) sb.Append(Diff_dates.Hours + " hr.(s) ");

        if (Diff_dates.Minutes > 0) sb.Append(Diff_dates.Minutes + " min.(s) ");

        return sb.ToString();
    }

    public static string? FechaHora(this string? XobjValue, int CoFormatoFecha = EnuCommon.FormatoFecha.SoloFecha)
    {
        if (XobjValue.EsNulo()) return null;

        string Yyyy, Mm, Dd, Hh, Mi, Ss, MmNombre, DdNombre; //, Meridiano = "am";

        if (CoFormatoFecha == EnuCommon.FormatoFecha.SoloFecha)
        {
            if (XobjValue!.Trim().Length >= 8)
            {
                Yyyy = XobjValue.Substring(0, 4);
                Mm = XobjValue.Substring(4, 2);
                Dd = XobjValue.Substring(6, 2);

                return string.Concat(Dd, "/", Mm, "/", Yyyy);
            }

            return null;
        }

        if (CoFormatoFecha == EnuCommon.FormatoFecha.SoloHora)
        {
            if (XobjValue!.Trim().Length == 6)
            {
                Hh = XobjValue.Substring(0, 2);
                Mi = XobjValue.Substring(2, 2);
                Ss = XobjValue.Substring(4, 2);
            }
            else
            {
                Hh = XobjValue.Substring(8, 2);
                Mi = XobjValue.Substring(10, 2);
                Ss = XobjValue.Substring(12, 2);
            }

            var a = GetHoraYMeridiano(Hh);

            return string.Concat(a[0], ":", Mi, " ", a[1]);
        }

        if (CoFormatoFecha == EnuCommon.FormatoFecha.FechaYHora)
        {
            Yyyy = XobjValue!.Substring(0, 4);
            Mm = XobjValue.Substring(4, 2);
            Dd = XobjValue.Substring(6, 2);

            Hh = XobjValue.Substring(8, 2);
            Mi = XobjValue.Substring(10, 2);
            Ss = XobjValue.Substring(12, 2);

            var a = GetHoraYMeridiano(Hh);

            return string.Concat(Dd, "/", Mm, "/", Yyyy, " ", a[0], ":", Mi, " ", a[1]);
        }

        if (CoFormatoFecha == EnuCommon.FormatoFecha.FechaNombre)
        {
            Yyyy = XobjValue!.Substring(0, 4);
            Mm = XobjValue.Substring(4, 2);
            Dd = XobjValue.Substring(6, 2);

            DateTime DateValue = new(Yyyy.Num() ?? 0, Mm.Num() ?? 0, Dd.Num() ?? 0);
            DdNombre = DateValue.ToString("dddd", new CultureInfo("es-ES"));
            //DdNombre = DdNombre.PriLetraMayuscula();
            MmNombre = MesCol[(Mm.Num() ?? 0) - 1];

            return string.Concat(DdNombre, " ", Dd, " de ", MmNombre, " del ", Yyyy);
        }

        if (CoFormatoFecha == EnuCommon.FormatoFecha.FechaYHoraNombre)
        {
            Yyyy = XobjValue!.Substring(0, 4);
            Mm = XobjValue.Substring(4, 2);
            Dd = XobjValue.Substring(6, 2);

            Hh = XobjValue.Substring(8, 2);
            Mi = XobjValue.Substring(10, 2);
            Ss = XobjValue.Substring(12, 2);

            var a = GetHoraYMeridiano(Hh);

            DateTime DateValue = new(Yyyy.Num() ?? 0, Mm.Num() ?? 0, Dd.Num() ?? 0);
            DdNombre = DateValue.ToString("dddd", new CultureInfo("es-ES"));
            //DdNombre = DdNombre.PriLetraMayuscula();
            MmNombre = MesCol[(Mm.Num() ?? 0) - 1];

            return string.Concat(DdNombre, " ", Dd, " de ", MmNombre, " del ", Yyyy, " ", a[0], ":", Mi, " ", a[1]);
        }

        if (CoFormatoFecha == EnuCommon.FormatoFecha.MesAnio)
        {
            Yyyy = XobjValue!.Substring(0, 4);
            Mm = XobjValue.Substring(4, 2);
            //Dd = XobjValue.ToString().Substring(6, 2);


            //DateTime DateValue = new(Yyyy.Num(), Mm.Num(), Dd.Num());
            //DdNombre = DateValue.ToString("dddd", new CultureInfo("es-ES"));
            //DdNombre = DdNombre.PriLetraMayuscula();
            MmNombre = MesCol[(Mm.Num() ?? 0) - 1];

            return string.Concat(MmNombre, " del ", Yyyy);
        }

        if (CoFormatoFecha == EnuCommon.FormatoFecha.MesNombre)
        {
            Mm = XobjValue!.Substring(4, 2);
            MmNombre = MesCol[(Mm.Num() ?? 0) - 1];
            return MmNombre;
        }

        if (CoFormatoFecha == EnuCommon.FormatoFecha.DiaNombre)
        {
            Yyyy = XobjValue!.Substring(0, 4);
            Mm = XobjValue.Substring(4, 2);
            Dd = XobjValue.Substring(6, 2);

            DateTime DateValue = new(Yyyy.Num() ?? 0, Mm.Num() ?? 0, Dd.Num() ?? 0);
            DdNombre = DateValue.ToString("dddd", new CultureInfo("es-ES"));

            return DdNombre;
        }

        if (CoFormatoFecha == EnuCommon.FormatoFecha.DiaNumero)
        {
            //Yyyy = XobjValue.ToString().Substring(0, 4);
            //Mm = XobjValue.ToString().Substring(4, 2);
            Dd = XobjValue!.Substring(6, 2);

            //DateTime DateValue = new DateTime(Yyyy.Num(), Mm.Num(), Dd.Num());
            //DdNombre = DateValue.ToString("dddd", new CultureInfo("es-ES"));

            return Dd;
        }

        return null;
    }
}