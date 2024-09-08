namespace CloudSeal.Data.Core;

/// <summary>
///     标准化
/// </summary>
public static class Normalize
{
    /// <summary>
    ///     并发戳
    /// </summary>
    public static string ConcurrencyStamp => Guid.NewGuid().ToString("N");

    /// <summary>
    ///     机密戳
    /// </summary>
    public static string SecurityStamp => Guid.NewGuid().ToString("N");
}