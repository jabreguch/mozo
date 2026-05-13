using System.Collections.Generic;

namespace Mozo.Model.Control;

public class TitleModalModel
{
    public string TxTitle { get; set; }
    public string TxDescription { get; set; }
    public List<BreadCrumbModalItemModel> BreadCrumbModalItemList { get; set; }
}

public class BreadCrumbModalItemModel
{
    public string TxTitle { get; set; }
}