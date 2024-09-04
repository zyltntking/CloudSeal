namespace CloudSeal.Data.Core;

/// <summary>
///     模型定义接口
/// </summary>
/// <typeparam name="TKey">标识类型</typeparam>
/// <typeparam name="TTimeStamp">时间戳类型</typeparam>
/// <typeparam name="THandler">操作标识类型</typeparam>
/// <typeparam name="TConcurrencyStamp">并发戳类型</typeparam>
public interface IModelDefinition<TKey, TConcurrencyStamp, TTimeStamp, THandler> :
    IKeySlot<TKey>,
    IConcurrencyStampSlot<TConcurrencyStamp>,
    ITimeStampSlot<TTimeStamp>,
    IHandlerSlot<THandler>
    where TKey : IEquatable<TKey>
    where TConcurrencyStamp : IEquatable<TConcurrencyStamp>
    where TTimeStamp : IEquatable<TTimeStamp>
    where THandler : IEquatable<THandler>;

/// <summary>
///     分区模型定义接口
/// </summary>
/// <typeparam name="TKey">标识类型</typeparam>
/// <typeparam name="TTimeStamp">时间戳类型</typeparam>
/// <typeparam name="THandler">操作标识类型</typeparam>
/// <typeparam name="TConcurrencyStamp">并发戳类型</typeparam>
/// <typeparam name="TPartition">分区标识类型</typeparam>
public interface IPartitionModelDefinition<
    TKey, TConcurrencyStamp, TTimeStamp,
    THandler, TPartition> :
    IModelDefinition<TKey, TConcurrencyStamp, TTimeStamp, THandler>,
    IPartitionSlot<TPartition>
    where TKey : IEquatable<TKey>
    where TConcurrencyStamp : IEquatable<TConcurrencyStamp>
    where TTimeStamp : IEquatable<TTimeStamp>
    where THandler : IEquatable<THandler>
    where TPartition : IEquatable<TPartition>;

/// <summary>
///     软删除模型定义接口
/// </summary>
/// <typeparam name="TKey">标识类型</typeparam>
/// <typeparam name="TTimeStamp">时间戳类型</typeparam>
/// <typeparam name="THandler">操作标识类型</typeparam>
/// <typeparam name="TSoftDeleteTimeStamp">软删除时间戳类型</typeparam>
/// <typeparam name="TSoftDeleteHandler">软删除操作标识类型</typeparam>
/// <typeparam name="TConcurrencyStamp">并发戳类型</typeparam>
public interface ISoftDeleteModelDefinition<
    TKey, TConcurrencyStamp, TTimeStamp,
    TSoftDeleteTimeStamp, THandler, TSoftDeleteHandler> :
    IModelDefinition<TKey, TConcurrencyStamp, TTimeStamp, THandler>,
    ISoftDeleteTimeStampSlot<TSoftDeleteTimeStamp>,
    ISoftDeleteHandlerSlot<TSoftDeleteHandler>
    where TKey : IEquatable<TKey>
    where TConcurrencyStamp : IEquatable<TConcurrencyStamp>
    where TTimeStamp : IEquatable<TTimeStamp>
    where THandler : IEquatable<THandler>;

/// <summary>
///     软删除模型定义接口
/// </summary>
/// <typeparam name="TKey">标识类型</typeparam>
/// <typeparam name="TTimeStamp">时间戳类型</typeparam>
/// <typeparam name="THandler">操作标识类型</typeparam>
/// <typeparam name="TSoftDeleteTimeStamp">软删除时间戳类型</typeparam>
/// <typeparam name="TSoftDeleteHandler">软删除操作标识类型</typeparam>
/// <typeparam name="TConcurrencyStamp">并发戳类型</typeparam>
/// <typeparam name="TPartition">分区标识类型</typeparam>
public interface ISoftDeleteModelDefinition<
    TKey, TConcurrencyStamp, TTimeStamp,
    TSoftDeleteTimeStamp, THandler, TSoftDeleteHandler, TPartition> :
    ISoftDeleteModelDefinition<TKey, TConcurrencyStamp, TTimeStamp, TSoftDeleteTimeStamp, THandler, TSoftDeleteHandler>,
    IPartitionSlot<TPartition>
    where TKey : IEquatable<TKey>
    where TConcurrencyStamp : IEquatable<TConcurrencyStamp>
    where TTimeStamp : IEquatable<TTimeStamp>
    where THandler : IEquatable<THandler>
    where TSoftDeleteTimeStamp : IEquatable<TSoftDeleteTimeStamp>
    where TSoftDeleteHandler : IEquatable<TSoftDeleteHandler>
    where TPartition : IEquatable<TPartition>;