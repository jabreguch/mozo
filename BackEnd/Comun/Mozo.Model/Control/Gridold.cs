using Microsoft.AspNetCore.Components;

namespace Mozo.Model.Control
{
    public static class Gridold
    {

        public class Cols
        {
            private Col? col = null;
            private int Count = 0;
            public List<Col> Columns = new();

            public MarkupString? DivSmall(int index)
            {
                //grid.separator
                col = Columns.Find(x => x.Index == index);
                if (col != null)
                {

                    return new MarkupString(string.Concat("<div class=\"", col!.CssSmall, index == Count ? " grid-separator-small" : "", "\">", col!.Title, "</div>"));
                }
                else
                    return new MarkupString("");

            }

            public void Add(Col col)
            {
                Columns.Add(col);
                Count = Columns.Count;
            }

            public string? CssNormal(int index, bool flSeparator = true)
            {
                col = Columns.Find(x => x.Index == index);
                if (col != null)
                {

                    return string.Concat(col!.CssNormal, index == Count && flSeparator == true ? " grid-separator-small" : "");
                }
                else
                    return "";
            }


            // public EventHandler<ArgumentsModel>? eMethod = null;
        }


        public class Col
        {
            public Col(int index, string? title, int sizeSmall, int sizeNormal)
            {
                this.Index = index;
                this.Title = title;
                this.SizeNormal = sizeNormal;
                this.SizeSmall = sizeSmall;
            }
            public int Index { get; set; }
            public string? Title { get; set; } = null!;
            public int SizeSmall { get; set; }
            public int SizeNormal { get; set; }

            public string? CssNormal
            {
                get
                {
                    return string.Concat("g-col-", SizeSmall, " g-col-sm-", SizeSmall, " g-col-md-", SizeNormal, " g-col-lg-", SizeNormal, " g-col-xl-", SizeNormal);
                    //return string.Concat("g-col-", SizeSmall, " g-col-sm-", SizeSmall, " g-col-md-", SizeNormal, " g-col-lg-", SizeNormal, " g-col-xl-", SizeNormal, " border");
                }
            }

            public string? CssSmall
            {
                get
                {
                    return string.Concat("d-md-none d-lg-none d-xl-none d-sm-block d-block g-col-", 12 - SizeSmall, " g-col-sm-", 12 - SizeSmall);
                    //return string.Concat("d-md-none d-lg-none d-xl-none d-sm-block d-block g-col-", 12 - SizeSmall, " g-col-sm-", 12 - SizeSmall, " border");
                }
            }


            //g-col-12 g-col-sm-12 g-col-md-3 g-col-lg-3 g-col-xl-3
        }

    }
}
