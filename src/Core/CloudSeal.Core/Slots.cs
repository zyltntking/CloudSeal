﻿namespace CloudSeal.Core;

/// <summary>
///     键插槽
/// </summary>
/// <typeparam name="TKey">键类型</typeparam>
public interface IKeySlot<TKey> where TKey : IEquatable<TKey>
{
    /// <summary>
    ///     标识
    /// </summary>
    TKey Id { get; set; }
}

/// <summary>
///     父项目键插槽
/// </summary>
/// <typeparam name="TParentKey">夫项目键类型</typeparam>
public interface IParentKeySlot<TParentKey>
{
    /// <summary>
    ///     父项目标识
    /// </summary>
    TParentKey ParentId { get; set; }
}

/// <summary>
///     分区标识插槽
/// </summary>
/// <typeparam name="TPartition">分区标识类型</typeparam>
public interface IPartitionSlot<TPartition> where TPartition : IEquatable<TPartition>
{
    /// <summary>
    ///     分区标识
    /// </summary>
    TPartition Partition { get; set; }
}

/// <summary>
///     校验戳插槽
/// </summary>
/// <typeparam name="TCheckStamp">校验戳类型</typeparam>
public interface ICheckStampSlot<TCheckStamp> where TCheckStamp : IEquatable<TCheckStamp>
{
    /// <summary>
    ///     校验戳
    /// </summary>
    TCheckStamp CheckStamp { get; set; }
}

/// <summary>
///     安全戳插槽
/// </summary>
/// <typeparam name="TSecurityStamp">安全戳类型</typeparam>
public interface ISecurityStampSlot<TSecurityStamp>
{
    /// <summary>
    ///     安全戳
    /// </summary>
    TSecurityStamp SecurityStamp { get; set; }
}

/// <summary>
///     并发戳插槽
/// </summary>
/// <typeparam name="TConcurrencyStamp">并发戳类型</typeparam>
public interface IConcurrencyStampSlot<TConcurrencyStamp> where TConcurrencyStamp : IEquatable<TConcurrencyStamp>
{
    /// <summary>
    ///     并发戳
    /// </summary>
    TConcurrencyStamp ConcurrencyStamp { get; set; }
}

/// <summary>
///     时间戳插槽
/// </summary>
/// <typeparam name="TTimeStamp">时间戳类型</typeparam>
public interface ITimeStampSlot<TTimeStamp> where TTimeStamp : IEquatable<TTimeStamp>
{
    /// <summary>
    ///     创建时间
    /// </summary>
    TTimeStamp CreatedAt { get; set; }

    /// <summary>
    ///     更新时间
    /// </summary>
    TTimeStamp UpdatedAt { get; set; }
}

/// <summary>
///     软删除时间戳插槽
/// </summary>
/// <typeparam name="TTimeStamp">时间戳类型</typeparam>
public interface ISoftDeleteTimeStampSlot<TTimeStamp>
{
    /// <summary>
    ///     删除时间
    /// </summary>
    TTimeStamp DeletedAt { get; set; }
}

/// <summary>
///     操作标识插槽
/// </summary>
/// <typeparam name="THandler">操作标识类型</typeparam>
public interface IHandlerSlot<THandler> where THandler : IEquatable<THandler>
{
    /// <summary>
    ///     创建标识
    /// </summary>
    THandler CreateBy { get; set; }

    /// <summary>
    ///     修改标识
    /// </summary>
    THandler ModifyBy { get; set; }
}

/// <summary>
///     软删除操作标识插槽
/// </summary>
/// <typeparam name="THandler"></typeparam>
public interface ISoftDeleteHandlerSlot<THandler>
{
    /// <summary>
    ///     移除标识
    /// </summary>
    THandler RemoveBy { get; set; }
}