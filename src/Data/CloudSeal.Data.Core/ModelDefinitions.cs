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
    where TKey : IComparable, IComparable<TKey>, IEquatable<TKey>
    where TConcurrencyStamp : IComparable, IComparable<TConcurrencyStamp>, IEquatable<TConcurrencyStamp>
    where TTimeStamp : IComparable, IComparable<TTimeStamp>, IEquatable<TTimeStamp>
    where THandler : IComparable, IComparable<THandler>, IEquatable<THandler>;

/// <summary>
///     模型定义接口
/// </summary>
public interface IModelDefinition : IModelDefinition<Guid, string, DateTime, Guid>,
    IKeySlot,
    IConcurrencyStampSlot,
    ITimeStampSlot,
    IHandlerSlot;

/// <summary>
///     分区模型定义接口
/// </summary>
/// <typeparam name="TKey">标识类型</typeparam>
/// <typeparam name="TTimeStamp">时间戳类型</typeparam>
/// <typeparam name="THandler">操作标识类型</typeparam>
/// <typeparam name="TConcurrencyStamp">并发戳类型</typeparam>
/// <typeparam name="TPartition">分区标识类型</typeparam>
public interface IPartitionModelDefinition<TKey, TConcurrencyStamp, TTimeStamp, THandler, TPartition> :
    IModelDefinition<TKey, TConcurrencyStamp, TTimeStamp, THandler>,
    IPartitionSlot<TPartition>
    where TKey : IComparable, IComparable<TKey>, IEquatable<TKey>
    where TConcurrencyStamp : IComparable, IComparable<TConcurrencyStamp>, IEquatable<TConcurrencyStamp>
    where TTimeStamp : IComparable, IComparable<TTimeStamp>, IEquatable<TTimeStamp>
    where THandler : IComparable, IComparable<THandler>, IEquatable<THandler>
    where TPartition : IComparable, IComparable<TPartition>, IEquatable<TPartition>;

/// <summary>
///     分区模型定义接口
/// </summary>
public interface IPartitionModelDefinition :
    IPartitionModelDefinition<Guid, string, DateTime, Guid, int>,
    IModelDefinition,
    IPartitionSlot;

/// <summary>
///     软删除模型定义接口
/// </summary>
/// <typeparam name="TKey">标识类型</typeparam>
/// <typeparam name="TTimeStamp">时间戳类型</typeparam>
/// <typeparam name="THandler">操作标识类型</typeparam>
/// <typeparam name="TSoftDeleteTimeStamp">软删除时间戳类型</typeparam>
/// <typeparam name="TSoftDeleteHandler">软删除操作标识类型</typeparam>
/// <typeparam name="TConcurrencyStamp">并发戳类型</typeparam>
public interface ISoftDeleteModelDefinition<TKey, TConcurrencyStamp, TTimeStamp, THandler, TSoftDeleteTimeStamp,
    TSoftDeleteHandler> :
    IModelDefinition<TKey, TConcurrencyStamp, TTimeStamp, THandler>,
    ISoftDeleteTimeStampSlot<TSoftDeleteTimeStamp>,
    ISoftDeleteHandlerSlot<TSoftDeleteHandler>
    where TKey : IComparable, IComparable<TKey>, IEquatable<TKey>
    where TConcurrencyStamp : IComparable, IComparable<TConcurrencyStamp>, IEquatable<TConcurrencyStamp>
    where TTimeStamp : IComparable, IComparable<TTimeStamp>, IEquatable<TTimeStamp>
    where THandler : IComparable, IComparable<THandler>, IEquatable<THandler>;

/// <summary>
///     软删除模型定义接口
/// </summary>
public interface ISoftDeleteModelDefinition :
    ISoftDeleteModelDefinition<Guid, string, DateTime, Guid, DateTime?, Guid?>,
    IModelDefinition,
    ISoftDeleteTimeStampSlot,
    ISoftDeleteHandlerSlot;

/// <summary>
///     软删除分区模型定义接口
/// </summary>
/// <typeparam name="TKey">标识类型</typeparam>
/// <typeparam name="TTimeStamp">时间戳类型</typeparam>
/// <typeparam name="THandler">操作标识类型</typeparam>
/// <typeparam name="TSoftDeleteTimeStamp">软删除时间戳类型</typeparam>
/// <typeparam name="TSoftDeleteHandler">软删除操作标识类型</typeparam>
/// <typeparam name="TConcurrencyStamp">并发戳类型</typeparam>
/// <typeparam name="TPartition">分区标识类型</typeparam>
public interface ISoftDeletePartitionModelDefinition<TKey, TConcurrencyStamp, TTimeStamp, THandler,
    TSoftDeleteTimeStamp, TSoftDeleteHandler, TPartition> :
    ISoftDeleteModelDefinition<TKey, TConcurrencyStamp, TTimeStamp, THandler, TSoftDeleteTimeStamp, TSoftDeleteHandler>,
    IPartitionModelDefinition<TKey, TConcurrencyStamp, TTimeStamp, THandler, TPartition>
    where TKey : IEquatable<TKey>, IComparable, IComparable<TKey>
    where TConcurrencyStamp : IEquatable<TConcurrencyStamp>, IComparable, IComparable<TConcurrencyStamp>
    where TTimeStamp : IEquatable<TTimeStamp>, IComparable, IComparable<TTimeStamp>
    where THandler : IEquatable<THandler>, IComparable, IComparable<THandler>
    where TPartition : IEquatable<TPartition>, IComparable, IComparable<TPartition>;

/// <summary>
///     软删除分区模型定义接口
/// </summary>
public interface ISoftDeletePartitionModelDefinition :
    ISoftDeletePartitionModelDefinition<Guid, string, DateTime, Guid, DateTime?, Guid?, int>,
    ISoftDeleteModelDefinition,
    IPartitionModelDefinition;

/// <summary>
///     抽象默认模型定义
/// </summary>
public abstract class DefaultModelDefinition : IModelDefinition
{
    /// <summary>
    ///     标识
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    ///     并发戳
    /// </summary>
    public string ConcurrencyStamp { get; set; } = Normalize.ConcurrencyStamp;

    /// <summary>
    ///     创建时间
    /// </summary>
    public DateTime CreatedAt { get; set; }

    /// <summary>
    ///     更新时间
    /// </summary>
    public DateTime UpdatedAt { get; set; }

    /// <summary>
    ///     创建标识
    /// </summary>
    public Guid CreateBy { get; set; }

    /// <summary>
    ///     修改标识
    /// </summary>
    public Guid ModifyBy { get; set; }
}

/// <summary>
///     抽象默认分区模型定义
/// </summary>
public abstract class DefaultPartitionModelDefinition : DefaultModelDefinition, IPartitionModelDefinition
{
    /// <summary>
    ///     分区标识
    /// </summary>
    public int Partition { get; set; }
}

/// <summary>
///     抽象默认软删除模型定义
/// </summary>
public abstract class DefaultSoftDeleteModelDefinition : DefaultModelDefinition, ISoftDeleteModelDefinition
{
    /// <summary>
    ///     删除时间
    /// </summary>
    public DateTime? DeletedAt { get; set; }

    /// <summary>
    ///     移除标识
    /// </summary>
    public Guid? RemoveBy { get; set; }
}

/// <summary>
///     抽象默认软删除分区模型定义
/// </summary>
public abstract class DefaultSoftDeletePartitionModelDefinition : DefaultSoftDeleteModelDefinition,
    ISoftDeletePartitionModelDefinition
{
    /// <summary>
    ///     分区标识
    /// </summary>
    public int Partition { get; set; }
}