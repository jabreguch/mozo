using System.Collections;

namespace Mozo.Model;
public class PagingModelBase<T> : IEnumerable<T>
{
    public IEnumerable<T>? items { get; set; }

    public IEnumerator<T> GetEnumerator()
    {
        int? count = items!.Count();
        for (var i = 0; i < count; i++)
            yield return items!.ElementAt(i); // [i];
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

}