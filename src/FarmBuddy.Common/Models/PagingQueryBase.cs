using FarmBuddy.Common.Exceptions;

namespace FarmBuddy.Common.Models;

public class PagingQueryBase
{
    private int _current;

    private int _pageSize = 10;

    /// <summary>
    /// 當前頁碼
    /// </summary>
    public virtual int Current
    {
        get => _current;
        set
        {
            if (value - 1 < 0)
                throw new BusinessException(ErrorCode.ValidationError ,"當前頁碼不能小於0");
            _current = value - 1;
        }
    }

    /// <summary>
    /// 每頁數量
    /// </summary>
    public virtual int PageSize
    {
        get => _pageSize;
        set
        {
            if (value < 0)
                throw new BusinessException(ErrorCode.ValidationError ,"每頁數量不能小於0");
            _pageSize = value == 0 ? 10 : value;
        }
    }

    /// <summary>
    /// 排序字段配置
    /// Key: 字段名, Value: 排序方向 (asc/desc/null)
    /// </summary>
    public virtual Dictionary<string, string?>? Sort { get; set; }
}