using Microsoft.AspNetCore.Components;

namespace Mozo.Model.Control
{

    public static class GridModel
    {
        public class PageModel
        {
            public int? RowCount { get; set; }

            private int? pageIndex;
            public int? PageIndex { get => pageIndex ?? 0; set => pageIndex = value; }

            private int? pageSize;
            public int? PageSize { get => pageSize ?? 10; set => pageSize = value; }

            public int? PageCount { get; set; }
            public MarkupString Text { get; set; }
            public bool Enabled = true;
            public bool Current = false;
        }

        public class TemplateModel<T>
        {
            public RenderFragment<T>? Template { get; set; }

            public ColumnModel? Col { get; set; }
        }

        public class ColumnModel
        {
            public ColumnModel() { }
            public ColumnModel(int index, string title, int sizeSmall, int sizeNormal)
            {
                Index = index;
                Title = title;
                SizeSmall = sizeSmall;
                SizeNormal = sizeNormal;
            }
            public string? Title { get; set; }
            public int QtColumn { get; set; }
            public int Index { get; set; }
            public int SizeSmall { get; set; }
            public int SizeNormal { get; set; }

            public string? CssNormal
            {
                get => string.Concat("g-col-", SizeSmall, " g-col-sm-", SizeSmall, " g-col-md-", SizeNormal, " g-col-lg-", SizeNormal, " g-col-xl-", SizeNormal);
            }

            public string? CssSmall
            {
                get => string.Concat("d-md-none d-lg-none d-xl-none d-sm-block d-block g-col-", 12 - SizeSmall, " g-col-sm-", 12 - SizeSmall);
            }

        }

        public class ColumnsModel
        {
            public List<ColumnModel> Columns = new();

            public MarkupString? DivSmall(int index)
            {
                ColumnModel? col = Columns.Find(x => x.Index == index);
                if (col != null)
                    return new MarkupString(string.Concat("<div class=\"", col!.CssSmall, "\">", col!.Title, "</div>"));
                else
                    return new MarkupString("");
            }

            public void Add(ColumnModel col) => Columns.Add(col);

            public string? CssNormal(int index)
            {
                ColumnModel? col = Columns.Find(x => x.Index == index);
                if (col != null)
                    return col!.CssNormal;
                else
                    return "";
            }
        }


    }
}
