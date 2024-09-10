namespace CloudSeal.Data.Core;

/// <summary>
///     标准化
/// </summary>
public static class Normalize
{
    /// <summary>
    ///     并发戳
    /// </summary>
    public static string ConcurrencyStamp => Guid.NewGuid().NormalizeGuid();

    /// <summary>
    ///     机密戳
    /// </summary>
    public static string SecurityStamp => Guid.NewGuid().NormalizeGuid();

    /// <summary>
    ///     Guid转字符串(标准化)
    /// </summary>
    /// <param name="id">Guid</param>
    /// <returns>string</returns>
    public static string NormalizeGuid(this Guid id)
    {
        return id.ToString("N");
    }
}