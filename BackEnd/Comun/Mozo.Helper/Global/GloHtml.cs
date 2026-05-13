namespace Mozo.Helper.Global;

public static partial class Glo
{
    public static string[] StringToParagraph(this string value, string? separate = null)
    {
        //            var mobileNos = number.Replace(" ", "")
        //.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries).ToList();

        if (value.EsNulo()) return new[] { "" };
        string[] lines;
        if (separate == null)
            lines = value.Split(
                new[] { "\r\n", "\r", "\n" },
                StringSplitOptions.RemoveEmptyEntries
            );
        else
            lines = value.Split(
                new[] { separate },
                StringSplitOptions.RemoveEmptyEntries
            );


        return lines;
    }
}