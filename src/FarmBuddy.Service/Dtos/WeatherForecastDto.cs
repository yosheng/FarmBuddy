namespace FarmBuddy.Service.Dtos;

/// <summary>
/// 优化后的天气预报数据结构 - 按时间维度聚合，消除冗余的时间字段
/// 原理：每个时间段内聚合5个关键气象要素(Wx,PoP,MinT,MaxT,CI)
/// 相比原始结构，节省了4倍的重复时间字段
/// </summary>
public class OptimizedWeatherForecast
{
    public string LocationName { get; set; }
    public List<TimePeriodForecast> TimePeriods { get; set; }
}

/// <summary>
/// 单个时间周期的完整气象数据
/// </summary>
public class TimePeriodForecast
{
    /// <summary>预报开始时间</summary>
    public string StartTime { get; set; }

    /// <summary>预报结束时间</summary>
    public string EndTime { get; set; }

    /// <summary>天气现象（晴、雨等）</summary>
    public WeatherCondition Wx { get; set; }

    /// <summary>降水概率 (Probability of Precipitation)</summary>
    public NumberValue PoP { get; set; }

    /// <summary>最低温度</summary>
    public NumberValue MinT { get; set; }

    /// <summary>最高温度</summary>
    public NumberValue MaxT { get; set; }

    /// <summary>舒适度指数 (Comfort Index)</summary>
    public TextValue CI { get; set; }
}

/// <summary>数字类型的天气参数（温度、降水概率等）</summary>
public class NumberValue
{
    public string Value { get; set; }
    public string Unit { get; set; }
}

/// <summary>文本类型的天气参数（舒适度等）</summary>
public class TextValue
{
    public string Value { get; set; }
}

/// <summary>天气现象（包含代码和描述）</summary>
public class WeatherCondition
{
    public string Description { get; set; }
    public string Code { get; set; }
}
