using System.Collections.Generic;

namespace Mozo.Model.Control;

public class ContentModel
{
    public string Id { get; set; }
    public ToolbarModel Toolbar { get; set; }
    public TitleModel Title { get; set; }


}

public class ToolbarModel
{
    public string Id { get; set; }
    public string TxTitle { get; set; }
    public FilterControlModel Filter { get; set; }

    public List<BreadCrumbItemModel> BreadCrumbItemList { get; set; }

}

public class TitleModel
{
    public string Id { get; set; }
    public string TxTitle { get; set; }
    public string TxDescription { get; set; }
    public List<BreadCrumbItemModel> BreadCrumbItemList { get; set; }
}

public class BreadCrumbItemModel
{
    public string Id { get; set; }
    public string TxValue { get; set; }
    public string TxTitle { get; set; }
}

public class FilterControlModel
{
    public FilterControlModel()
    {
        TxTitle = "Opciones de busqueda";
    }

    //public FilterModel(string title) {

    //}



    public string TxTitle { get; set; }
}