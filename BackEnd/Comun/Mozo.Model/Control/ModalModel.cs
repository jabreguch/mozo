using Mozo.Helper.Enu;

namespace Mozo.Model.Control
{
    public class ModalModel
    {
        //private string? id;
        public string? Id { get; set; }
        //{
        //    get
        //    {
        //        if (id == null) id = Guid.NewGuid().ToString();
        //        return id;
        //    }
        //    set => id = value;
        //}
        public string? Title { get; set; }
        public string? SubTitle { get; set; }

        public int Size { get; set; } = EnuCommon.Modal.Size.Default;

        public string? GetSize()
        {
            if (Size == EnuCommon.Modal.Size.Small)
                return "modal-sm";
            else if (Size == EnuCommon.Modal.Size.Default)
                return "";
            else if (Size == EnuCommon.Modal.Size.Large)
                return "modal-lg";
            else if (Size == EnuCommon.Modal.Size.ExtraLarge)
                return "modal-xl";
            else if (Size == EnuCommon.Modal.Size.Fullscreen)
                return "modal-fullscreen";

            return "";
        }

        public bool Validation { get; set; } = false;

        public bool Visible { get; set; } = false;

        public ComponentModel? form { get; set; }

    }
}
