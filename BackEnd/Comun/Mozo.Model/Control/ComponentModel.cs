namespace Mozo.Model.Control
{
    //public class ModalModel
    //{
    //    public string? Id { get; set; }
    //    public string? Title { get; set; }
    //    public string? SubTitle { get; set; }

    //    public int Size { get; set; } = EnuCommon.ModalSize.Default;


    //    public string? ModalDisplay = "none;";
    //    public string? ModalClass = string.Empty;
    //    public bool ShowBackdrop = false;

    //    public string? GetSize(int size)
    //    {
    //        if (size == EnuCommon.ModalSize.Small)
    //            return "modal-sm";
    //        else if (size == EnuCommon.ModalSize.Default)
    //            return "";
    //        else if (size == EnuCommon.ModalSize.Large)
    //            return "modal-lg";
    //        else if (size == EnuCommon.ModalSize.ExtraLarge)
    //            return "modal-xl";
    //        else if (size == EnuCommon.ModalSize.Fullscreen)
    //            return "modal-fullscreen";

    //        return "";
    //    }


    //}

    public class ComponentModel : IDisposable
    {
        public required Type Type { get; set; }
        public Dictionary<string, object> Params { get; set; } = [];

        public void Dispose()
        {
            Params = [];
        }
    }

}
