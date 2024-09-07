namespace CloudSeal.Data.Core;

/// <summary>
///     数据戳代理接口
/// </summary>
/// <typeparam name="TStamp">戳类型</typeparam>
public interface IStampProxy<out TStamp>
{
    /// <summary>
    ///     生成数据戳
    /// </summary>
    TStamp GenerateStamp { get; }
}

/// <summary>
/// 时间戳代理接口
/// </summary>
/// <typeparam name="TStamp">戳类型</typeparam>
public interface ITimeStampProxy<out TStamp> : IStampProxy<TStamp>;

/// <summary>
/// 操作标识代理接口
/// </summary>
/// <typeparam name="TStamp">戳类型</typeparam>
public interface IHandlerStampProxy<out TStamp> : IStampProxy<TStamp>;

/// <summary>
/// 并发戳代理接口
/// </summary>
/// <typeparam name="TStamp">戳类型</typeparam>
public interface IConcurrencyStampProxy<out TStamp> : IStampProxy<TStamp>;

/// <summary>
/// 分区标识代理接口
/// </summary>
/// <typeparam name="TStamp">戳类型</typeparam>
public interface IPartitionProxy<out TStamp> : IStampProxy<TStamp>;

/// <summary>
/// 默认时间戳代理实现
/// </summary>
public class DefaultTimeStampProxy : ITimeStampProxy<DateTime>
{
    /// <summary>
    /// 生成时间戳
    /// </summary>
    public DateTime GenerateStamp => DateTime.Now;
}

/// <summary>
/// 默认操作标识代理实现
/// </summary>
public class DefaultHandlerStampProxy : IHandlerStampProxy<Guid>
{
    /// <summary>
    /// 生成操作标识
    /// </summary>
    public Guid GenerateStamp => Guid.Empty;
}

/// <summary>
/// 默认并发戳代理实现
/// </summary>
public class DefaultConcurrencyStampProxy : IConcurrencyStampProxy<string>
{
    /// <summary>
    /// 生成并发戳
    /// </summary>
    public string GenerateStamp => Normalize.ConcurrencyStamp;
}

/// <summary>
/// 默认分区标识代理实现
/// </summary>
public class DefaultPartitionProxy : IPartitionProxy<int>
{
    /// <summary>
    /// 生成分区标识
    /// </summary>
    public int GenerateStamp => 0;
}