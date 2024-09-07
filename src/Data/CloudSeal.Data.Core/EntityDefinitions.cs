namespace CloudSeal.Data.Core;

/// <summary>
///     实体定义接口
/// </summary>
public interface IEntityDefinition<TKey> : IEntityInfoDefinition<TKey> where TKey : IEquatable<TKey>;

/// <summary>
///     实体信息定义接口
/// </summary>
public interface IEntityInfoDefinition<TKey> : IEntityPackageDefinition, IKeySlot<TKey> where TKey : IEquatable<TKey>;

/// <summary>
///     实体数据包定义接口
/// </summary>
public interface IEntityPackageDefinition;