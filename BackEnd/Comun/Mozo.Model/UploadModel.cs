using System;
using System.Text;
using System.Text.Json.Serialization;

using Mozo.Comun.Helper.Enu;
using Mozo.Comun.Helper.Global;

namespace Mozo.Model;

[Serializable]
public struct UploadChunkFileMergeModel
{
    public int Index { get; set; }
    public string Name { get; set; }
}

[Serializable]
public class UploadFilePathAndConfigModel
{
    public UploadFilePathAndConfigModel(
        string fileNameWithPartAndPrefij
    )
    {
        FileNameWithPartAndPrefij = fileNameWithPartAndPrefij;
        TrailingTokens =
            FileNameWithPartAndPrefij.Substring(FileNameWithPartAndPrefij.IndexOf(PartToken) + PartToken.Length);
        FileNameNotPart = FileNameWithPartAndPrefij.Substring(0, FileNameWithPartAndPrefij.IndexOf(PartToken));
        FileNameNotPartAndPrefij = FileNameNotPart.Substring(10);
    }

    public string PartToken => ".part_";
    public string FileNameWithPartAndPrefij { get; }

    public string TrailingTokens { get; }

    public string FileNameNotPart { get; }

    public string FileNameNotPartAndPrefij { get; }

    public string PathFolderTemporary { get; set; }
}

[Serializable]
public class UploadModel
{
    private int? _FlMultiple;


    private int? _FlPreview;

    private int? _FlRequired;

    //[JsonPropertyName("Archivo")] public ArchivoModel Archivo { get; set; }

    //[JsonPropertyName("ArchivoCol")] public List<ArchivoModel> ArchivoCol { get; set; }

    private string _TxAccept;

    public UploadModel()
    {
        //FileParts = new List<string>();
        TxPrefij = "CtlUpload";
        TxTitle = "Selecciona";
        //FlPrefijo = 0;
        CoSendTo = EnuCommon.CoEnviarA.CarpetaTemporal;
        //ArchivoCol = new List<ArchivoModel>();
        //Archivo = new ArchivoModel();
        Id = NumeroRandom();
    }

    [JsonPropertyName("CoSendTo")] public int? CoSendTo { get; set; }
    [JsonPropertyName("TipoArchivo")] public TipoSeleccionModel TipoArchivo { get; set; }
    [JsonPropertyName("TxPrefij")] public string TxPrefij { get; set; }

    [JsonPropertyName("Id")] public string Id { get; set; }

    public int? FlPreview
    {
        set => _FlPreview = value;
    }

    public HtmlString Preview
    {
        get
        {
            if (_FlPreview == 1)
                return new HtmlString("preview=\"true\"");
            return new HtmlString("preview=\"false\"");
        }
    }

    public int? FlRequired
    {
        set => _FlRequired = value;
    }

    public HtmlString Required
    {
        get
        {
            if (_FlRequired == 1)
                return new HtmlString("required");
            return new HtmlString("");
        }
    }

    public int? FlMultiple
    {
        set => _FlMultiple = value;
    }

    public HtmlString Multiple
    {
        get
        {
            if (_FlMultiple == 1)
                return new HtmlString("multiple");
            return new HtmlString("");
        }
    }

    [JsonPropertyName("TxEndProcessFunction")]
    public string TxEndProcessFunction { get; set; }

    //[JsonPropertyName("TxRemoveProcessFunction")] public string TxRemoveProcessFunction { get; set; }

    //[JsonPropertyName("MaxFileSizeMB")] public int? MaxFileSizeMB { get; set; }
    //[JsonPropertyName("FileParts")] public List<String> FileParts { get; set; }
    // [JsonPropertyName("baseFileName")] public string baseFileName { get; set; }

    [JsonPropertyName("TxTitle")] public string TxTitle { get; set; }

    public string TxAccept
    {
        set => _TxAccept = value;
    }


    public HtmlString Accept
    {
        get
        {
            var sb = new StringBuilder("");
            if (_TxAccept.NoNulo())
            {
                var ss = _TxAccept.Split(char.Parse("|"));
                foreach (var s in ss) sb.Append(string.Concat(".", s.Minuscula(), ","));
                return new HtmlString(string.Concat("accept=\"", sb.ToString(), "\""));
            }

            return new HtmlString("");
        }
    }

    private string NumeroRandom()
    {
        var rnd = new Random();

        var PrefixId = 1;

        EnuCommon.RandomListUnica.Add(PrefixId);

        while (EnuCommon.RandomListUnica.Contains(PrefixId))
        {
            PrefixId = rnd.Next(15239);
            if (!EnuCommon.RandomListUnica.Contains(PrefixId))
            {
                EnuCommon.RandomListUnica.Add(PrefixId);
                break;
            }
        }

        return PrefixId.Text();
    }
}