using System.Collections;
using System.Data;
using System.Data.Common;
using System.Globalization;

namespace Mozo.Helper.Global;

public static partial class Glo
{
    public static decimal Deci(this IDataReader dr, string field)
    {
        return Deci(dr[field]);
    }

    public static int? Num(this object? value)
    {
        if (ReferenceEquals(value, DBNull.Value)) return null;
        if (value == null) return null;

        var str = value.ToString()!.Trim();
        if (str.Equals("")) return null;

        return Convert.ToInt32(str);
    }
    public static string NumFormatExcel(this object value, int qtDecimal = 2, bool round = false)
    {
        if (ReferenceEquals(value, DBNull.Value)) return "";
        if (value == null) return "";
        if (value.ToString()!.Trim().Equals("")) return "";
        if (round) value = Math.Round((decimal)value, 1);


        return string.Format("{0:0" + (qtDecimal > 0 ? ".".PadRight(qtDecimal + 1, Convert.ToChar("0")) : "") + "}",
            value);
    }

    // //decimalVar.ToString("0.00")
    public static string NumFormat(this object? value, int qtDecimal = 2)
    {
        if (ReferenceEquals(value, DBNull.Value)) return "";
        if (value == null) return "";
        if (value.ToString()!.Trim().Equals("")) return "";
        return string.Format("{0:#,##0" + (qtDecimal > 0 ? ".".PadRight(qtDecimal + 1, Convert.ToChar("0")) : "") + "}",
            value);
    }

    public static decimal Deci(this object value)
    {
        if (ReferenceEquals(value, DBNull.Value)) return 0;
        if (value == null) return 0;
        if (value.ToString()!.Trim().Equals("")) return 0;
        var r = new CultureInfo("es-PE");
        r.NumberFormat.CurrencyDecimalSeparator = ".";
        r.NumberFormat.CurrencyGroupSeparator = ".";
        r.NumberFormat.NumberDecimalSeparator = ".";
        r.NumberFormat.PercentDecimalSeparator = ".";
        Thread.CurrentThread.CurrentCulture = r;
        decimal.TryParse(value.ToString(), out var val);
        return val;
    }
    public static int? Num(this DbDataReader? dr, string field)
    {
        return Numero(dr![field]);
    }
    public static int? Num(this IDataReader? dr, string field)
    {
        return Numero(dr![field]);
    }

    public static int? Num(this DataRowView? dr, string field)
    {
        return Numero(dr![field]);
    }

    public static int? Num(this DataRow? dr, string field)
    {
        return Numero(dr![field]);
    }

    public static int? Num(this IDictionary? dr, string field)
    {
        return Numero(dr![field]);
    }

    public static int? Num(this string value)
    {
        return Numero(value);
    }

    //public static int Num(this object value)
    //{
    //    return Numero(value);
    //}

    private static int? Numero(object? value)
    {
        if (value == null) return null;
        if (value!.ToString()!.Trim().Equals("")) return null;
        if (value.GetType().BaseType!.FullName == "System.Enum") return Convert.ToInt32(value);
        return int.Parse(value.ToString()!);
    }

    public static bool EsNumero(this object? value)
    {
        bool isNumeric = int.TryParse(value.Text(), out var n);
        if (isNumeric) return true;
        bool isDecimal = decimal.TryParse(value.Text(), out var d);
        return isDecimal;
    }

    //public static decimal Pow(decimal x, double y)
    //{
    //    decimal A = 1m;
    //    BitArray e = new BitArray(BitConverter.GetBytes(y));
    //    int t = e.Count;

    //    for (int i = t - 1; i >= 0; --i)
    //    {
    //        A *= A;
    //        if (e[i] == true)
    //        {
    //            A *= x;
    //        }
    //    }
    //    return A;
    //}
}