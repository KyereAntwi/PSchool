namespace PSchool.Presentation.Blazor;

public class PaggedList<T> where T : class
{
    public int Count { get; set; }
    public int Page { get; set; }
    public int Size { get; set; }
    public ICollection<T>? ListItems { get; set; }
}
