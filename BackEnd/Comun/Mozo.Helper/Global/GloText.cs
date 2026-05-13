using System.Collections;
using System.Data;
using System.Globalization;
using System.Security.Cryptography;
using System.Text;

namespace Mozo.Helper.Global;

public static partial class Glo
{

    private static readonly Dictionary<string, string> foreignCharacters = new()
    {
        { "äæǽ", "ae" },
        { "öœ", "oe" },
        { "ü", "ue" },
        { "Ä", "Ae" },
        { "Ü", "Ue" },
        { "Ö", "Oe" },
        { "ÀÁÂÃÄÅǺĀĂĄǍΑΆẢẠẦẪẨẬẰẮẴẲẶА", "A" },
        { "àáâãåǻāăąǎªαάảạầấẫẩậằắẵẳặа", "a" },
        { "Б", "B" },
        { "б", "b" },
        { "ÇĆĈĊČ", "C" },
        { "çćĉċč", "c" },
        { "Д", "D" },
        { "д", "d" },
        { "ÐĎĐΔ", "Dj" },
        { "ðďđδ", "dj" },
        { "ÈÉÊËĒĔĖĘĚΕΈẼẺẸỀẾỄỂỆЕЭ", "E" },
        { "èéêëēĕėęěέεẽẻẹềếễểệеэ", "e" },
        { "Ф", "F" },
        { "ф", "f" },
        { "ĜĞĠĢΓГҐ", "G" },
        { "ĝğġģγгґ", "g" },
        { "ĤĦ", "H" },
        { "ĥħ", "h" },
        { "ÌÍÎÏĨĪĬǏĮİΗΉΊΙΪỈỊИЫ", "I" },
        { "ìíîïĩīĭǐįıηήίιϊỉịиыї", "i" },
        { "Ĵ", "J" },
        { "ĵ", "j" },
        { "ĶΚК", "K" },
        { "ķκк", "k" },
        { "ĹĻĽĿŁΛЛ", "L" },
        { "ĺļľŀłλл", "l" },
        { "М", "M" },
        { "м", "m" },
        { "ÑŃŅŇΝН", "N" },
        { "ñńņňŉνн", "n" },
        { "ÒÓÔÕŌŎǑŐƠØǾΟΌΩΏỎỌỒỐỖỔỘỜỚỠỞỢО", "O" },
        { "òóôõōŏǒőơøǿºοόωώỏọồốỗổộờớỡởợо", "o" },
        { "П", "P" },
        { "п", "p" },
        { "ŔŖŘΡР", "R" },
        { "ŕŗřρр", "r" },
        { "ŚŜŞȘŠΣС", "S" },
        { "śŝşșšſσςс", "s" },
        { "ȚŢŤŦτТ", "T" },
        { "țţťŧт", "t" },
        { "ÙÚÛŨŪŬŮŰŲƯǓǕǗǙǛŨỦỤỪỨỮỬỰУ", "U" },
        { "ùúûũūŭůűųưǔǖǘǚǜυύϋủụừứữửựу", "u" },
        { "ÝŸŶΥΎΫỲỸỶỴЙ", "Y" },
        { "ýÿŷỳỹỷỵй", "y" },
        { "В", "V" },
        { "в", "v" },
        { "Ŵ", "W" },
        { "ŵ", "w" },
        { "ŹŻŽΖЗ", "Z" },
        { "źżžζз", "z" },
        { "ÆǼ", "AE" },
        { "ß", "ss" },
        { "Ĳ", "IJ" },
        { "ĳ", "ij" },
        { "Œ", "OE" },
        { "ƒ", "f" },
        { "ξ", "ks" },
        { "π", "p" },
        { "β", "v" },
        { "μ", "m" },
        { "ψ", "ps" },
        { "Ё", "Yo" },
        { "ё", "yo" },
        { "Є", "Ye" },
        { "є", "ye" },
        { "Ї", "Yi" },
        { "Ж", "Zh" },
        { "ж", "zh" },
        { "Х", "Kh" },
        { "х", "kh" },
        { "Ц", "Ts" },
        { "ц", "ts" },
        { "Ч", "Ch" },
        { "ч", "ch" },
        { "Ш", "Sh" },
        { "ш", "sh" },
        { "Щ", "Shch" },
        { "щ", "shch" },
        { "ЪъЬь", "" },
        { "Ю", "Yu" },
        { "ю", "yu" },
        { "Я", "Ya" },
        { "я", "ya" }
    };

    public static bool In<T>(this T value, params T[] args)
    {
        return args.Contains(value);
    }

    public static string? Mayuscula(this object value)
    {
        if (value.NoNulo() == false) return null;
        return Convert.ToString(value)!.Trim().ToUpper();
    }


    public static string? Minuscula(this object value)
    {
        if (value.NoNulo() == false) return null;
        return Convert.ToString(value)!.Trim().ToLower();
    }


    public static string? NombresAltasYBajas(this object XobjValue)
    {
        if (XobjValue.NoNulo() == false) return null;
        StringBuilder s = new StringBuilder();
        string[] cadenas = Convert.ToString(XobjValue)!.Minuscula()!.Split(Convert.ToChar(" "));
        for (var i = 0; i < cadenas.Length; i++)
            if (cadenas[i].Length <= 2 || cadenas[i].Trim() == "del")
                s.Append(string.Concat(cadenas[i].Trim(), " "));
            else
                s.Append(string.Concat(cadenas[i].Substring(0, 1).ToUpper(), cadenas[i].Substring(1).Trim(), " "));
        return s.ToString().Trim();
    }

    public static string? PriLetraMayuscula(this object value)
    {
        if (value.NoNulo() == false) return null;
        return CultureInfo.InvariantCulture.TextInfo.ToTitleCase(value.Text()!.ToLower());
    }

    public static string? SinAcento(this object value)
    {
        if (value.NoNulo() == false) return null;

        var TX_BUSCA = (string)value;
        TX_BUSCA = TX_BUSCA.Replace("Á", "A");
        TX_BUSCA = TX_BUSCA.Replace("É", "E");
        TX_BUSCA = TX_BUSCA.Replace("Í", "I");
        TX_BUSCA = TX_BUSCA.Replace("Ó", "O");
        TX_BUSCA = TX_BUSCA.Replace("Ú", "U");
        //ÀÈÌÒÙ
        TX_BUSCA = TX_BUSCA.Replace("À", "A");
        TX_BUSCA = TX_BUSCA.Replace("È", "E");
        TX_BUSCA = TX_BUSCA.Replace("Ì", "I");
        TX_BUSCA = TX_BUSCA.Replace("Ò", "O");
        TX_BUSCA = TX_BUSCA.Replace("Ù", "U");

        TX_BUSCA = TX_BUSCA.Replace("á", "a");
        TX_BUSCA = TX_BUSCA.Replace("é", "e");
        TX_BUSCA = TX_BUSCA.Replace("í", "i");
        TX_BUSCA = TX_BUSCA.Replace("ó", "o");
        TX_BUSCA = TX_BUSCA.Replace("ú", "u");

        value = TX_BUSCA;
        return Convert.ToString(value)!.Trim();
    }

    //public static string CheckSlashEnd(this object value)
    //{
    //    if (value.NoNulo() == false) return null;
    //    if (Convert.ToString(value).Trim().IndexOf("/", Convert.ToString(value).Trim().Length - 1) == -1)
    //        value = Convert.ToString(value).Trim() + "/";

    //    return Convert.ToString(value);
    //}


    public static string? Text(this object? objValue, int qtCaracter = 0, string? txRellenaCharacter = null)
    {
        if (objValue.NoNulo() == false) return null;
        int QtLenth = Convert.ToString(objValue)!.Length;
        if ((qtCaracter > 0) & (QtLenth > qtCaracter))
        {
            objValue = Convert.ToString(objValue)!.Substring(0, qtCaracter) + "...";
        }

        if (txRellenaCharacter != null)
        {
            if (qtCaracter > 0)
            {
                if (QtLenth > qtCaracter)
                {
                    return Convert.ToString(objValue)!.Trim();
                }
                else
                {
                    return Convert.ToString(objValue)!.Trim().PadRight(qtCaracter, char.Parse(txRellenaCharacter));
                }
            }
            else if (qtCaracter < 0)
            {
                qtCaracter = qtCaracter * -1;
                if (QtLenth > qtCaracter)
                {
                    return Convert.ToString(objValue)!.Trim();
                }
                else
                {
                    return Convert.ToString(objValue)!.Trim().PadLeft(qtCaracter, char.Parse(txRellenaCharacter));
                }
            }
        }
        return Convert.ToString(objValue)!.Trim();
    }

    public static string RandomString(int length,
        string allowedChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789")
    {
        if (length < 0) throw new ArgumentOutOfRangeException("length", "length cannot be less than zero.");
        if (string.IsNullOrEmpty(allowedChars)) throw new ArgumentException("allowedChars may not be empty.");

        const int byteSize = 0x100;
        //var allowedCharSet = new HashSet<char>(allowedChars).ToArray();
        var allowedCharSet = new HashSet<char>(allowedChars).ToArray();
        if (byteSize < allowedCharSet.Length)
            throw new ArgumentException(
                string.Format("allowedChars may contain no more than {0} characters.", byteSize));

        // Guid.NewGuid and System.Random are not particularly random. By using a
        // cryptographically-secure random number generator, the caller is always
        // protected, regardless of use.
        using (var rng = RandomNumberGenerator.Create())
        {
            StringBuilder result = new();
            var buf = new byte[128];
            while (result.Length < length)
            {
                rng.GetBytes(buf);
                for (var i = 0; i < buf.Length && result.Length < length; ++i)
                {
                    // Divide the byte into allowedCharSet-sized groups. If the
                    // random value falls into the last group and the last group is
                    // too small to choose from the entire allowedCharSet, ignore
                    // the value in order to avoid biasing the result.
                    var outOfRangeStart = byteSize - byteSize % allowedCharSet.Length;
                    if (outOfRangeStart <= buf[i]) continue;
                    result.Append(allowedCharSet[buf[i] % allowedCharSet.Length]);
                }
            }

            return result.ToString();
        }
    }

    public static string? Text(this DateTime? pr, string text)
    {
        if (pr != null) return pr.Value.ToString(text);
        return null;
    }

    public static string? Text(this IDataReader dr, string field)
    {
        return Text(dr[field]);
    }

    public static string? Text(this DataRowView dr, string field)
    {
        return Text(dr[field]);
    }

    public static string? Text(this DataRow dr, string field)
    {
        return Text(dr[field]);
    }

    public static string? Text(this IDictionary dr, string field)
    {
        return Text(dr[field]);
    }

    public static string CerosIzquierda(this object obj, int qtCero)
    {
        string? tx = Text(obj);
        if (tx == null)
        {
            string temp = "";
            return string.Concat(temp.PadLeft(qtCero, Convert.ToChar("0")), "0");
        }

        return tx.PadLeft(qtCero, Convert.ToChar("0"));
    }


    public static string Traslado(string XstrCad)
    {
        string strResp = "";
        if (XstrCad != null)
        {
            strResp = XstrCad;
            strResp = strResp.Replace("á", "&#225;");
            strResp = strResp.Replace("é", "&#233;");
            strResp = strResp.Replace("í", "&#237;");
            strResp = strResp.Replace("ó", "&#243;");
            strResp = strResp.Replace("ú", "&#250;");
            strResp = strResp.Replace("ñ", "&#241;");
            strResp = strResp.Replace("Ñ", "&#209;");
            strResp = strResp.Replace("ü", "&#252;");
            strResp = strResp.Replace("Ü", "&#220;");
            strResp = strResp.Replace("'", "&#96;");
            strResp = strResp.Replace("Á", "&#193;");
            strResp = strResp.Replace("É", "&#201;");
            strResp = strResp.Replace("Í", "&#205;");
            strResp = strResp.Replace("Ó", "&#211;");
            strResp = strResp.Replace("Ú", "&#218;");
            strResp = strResp.Replace("°", "&#176;");
            strResp = strResp.Replace("º", "&#186;");
            //strResp = strResp.Replace("""", "&quot;");
            strResp = strResp.Replace("{", "&#123;");
            strResp = strResp.Replace("}", "&#125;");
            strResp = strResp.Replace(":", "&#58;");
            strResp = strResp.Replace(".", "&#46;");
        }
        else
        {
            strResp = "";
        }

        return strResp;
    }

    //public static char RemoveDiacritics(this char c)
    //{
    //    foreach (KeyValuePair<string, string> entry in foreign_characters)
    //    {
    //        if (entry.Key.IndexOf(c) != -1)
    //        {
    //            return entry.Value[0];
    //        }
    //    }
    //    return c;
    //}

    public static string RemoveDiacritics(this string s)
    {
        //StringBuilder sb = new StringBuilder ();
        var text = "";


        foreach (var c in s)
        {
            var len = text.Length;

            foreach (var entry in foreignCharacters)
                if (entry.Key.IndexOf(c) != -1)
                {
                    text += entry.Value;
                    break;
                }

            if (len == text.Length) text += c;
        }

        return text;
    }

    public static string RestringeASCII(this string s, int tamano = -1)
    {
        s = s.Replace("  ", " ");
        s = s.Replace("   ", " ");
        s = s.Replace("    ", " ");
        s = s.Replace("     ", " ");
        s = s.Replace("      ", " ");
        s = s.Replace("       ", " ");
        var asciiBytes = Encoding.ASCII.GetBytes(s);
        var p = new StringBuilder();

        foreach (int c in asciiBytes)
        {
            if (c >= 48 && c <= 57) p.Append((char)c);
            if (c >= 65 && c <= 90) p.Append((char)c);
            if (c >= 97 && c <= 122) p.Append((char)c);
            if (c == 32) p.Append((char)c);
        }

        if (tamano != -1 && p.Length > tamano) p.Remove(tamano, p.Length - tamano);

        return p.ToString();
    }
}