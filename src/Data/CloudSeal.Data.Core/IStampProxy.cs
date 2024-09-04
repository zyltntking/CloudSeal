namespace CloudSeal.Data.Core;

/// <summary>
///     数据戳代理接口
/// </summary>
/// <typeparam name="TStamp"></typeparam>
public interface IStampProxy<out TStamp>
{
    /// <summary>
    ///     生成数据戳
    /// </summary>
    TStamp GenerateStamp { get; }
}