namespace Mozo.Helper.Global;

public static partial class Glo
{
    public static bool EsNulo(this object? value)
    {
        if (ReferenceEquals(value, DBNull.Value)) return true;
        if (value == null)
            return true;
        if (value.ToString()!.Trim().Equals("")) return true;
        return false;
    }

    public static bool EsNulo(this string? value)
    {
        if (value == null) return true;
        if (value.Trim().Equals("")) return true;
        return false;
    }

    public static bool NoNulo(this object? value)
    {
        if (value == null) return false;
        if (value.ToString()!.Trim().Equals("")) return false;
        return true;
    }

    public static bool IsNullableType(Type type)
    {
        return type.IsGenericType && type.GetGenericTypeDefinition().Equals(typeof(Nullable<>));
    }


}