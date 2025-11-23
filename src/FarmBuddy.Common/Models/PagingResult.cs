namespace FarmBuddy.Common.Models;

public class PagingResult<T>
{
    public int Current { get; set; }
    
    public int PageSize { get; set; }
    
    public int Total { get; set; }

    public List<T> Items { get; set; } = [];
}