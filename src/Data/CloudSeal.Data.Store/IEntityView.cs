using CloudSeal.Data.Core;
using Microsoft.EntityFrameworkCore;

namespace CloudSeal.Data.Store;

/// <summary>
///     实体视图接口
/// </summary>
/// <typeparam name="TEntity">实体类型</typeparam>
/// <typeparam name="TKey">键类型</typeparam>
public interface IEntityView<TEntity, TKey> : IKeyLessEntityView<TEntity>
    where TEntity : class, IKeySlot<TKey>
    where TKey : IEquatable<TKey>, IComparable, IComparable<TKey>
{
    #region Access

    /// <summary>
    ///     键适配查询
    /// </summary>
    IQueryable<TEntity> KeyMatchQuery(TKey key);

    /// <summary>
    ///     键适配查询
    /// </summary>
    /// <param name="keys"></param>
    /// <returns></returns>
    IQueryable<TEntity> KeyMatchQuery(IEnumerable<TKey> keys);

    #endregion

    #region GetId

    /// <summary>
    ///     获取指定实体Id
    /// </summary>
    /// <param name="entity">实体</param>
    /// <returns>Id</returns>
    TKey GetId(TEntity entity);

    /// <summary>
    ///     获取指定实体Id
    /// </summary>
    /// <param name="entity">实体</param>
    /// <param name="cancellationToken">取消异步操作的信号</param>
    /// <returns>Id</returns>
    Task<TKey> GetIdAsync(
        TEntity entity,
        CancellationToken cancellationToken = default);

    #endregion

    #region IsDelete

    /// <summary>
    ///     是否被删除
    /// </summary>
    /// <param name="entity">实体</param>
    /// <returns>判断结果</returns>
    bool IsDeleted(TEntity entity);

    /// <summary>
    ///     是否被删除
    /// </summary>
    /// <param name="key">键</param>
    /// <returns></returns>
    bool IsDeleted(TKey key);

    /// <summary>
    ///     是否被删除
    /// </summary>
    /// <param name="entity">实体</param>
    /// <param name="cancellationToken">操作取消信号</param>
    /// <returns>判断结果</returns>
    Task<bool> IsDeletedAsync(
        TEntity entity,
        CancellationToken cancellationToken = default);

    /// <summary>
    ///     是否被删除
    /// </summary>
    /// <param name="key">键</param>
    /// <param name="cancellationToken">操作取消信号</param>
    /// <returns></returns>
    Task<bool> IsDeletedAsync(
        TKey key,
        CancellationToken cancellationToken = default);

    #endregion

    #region FindEntity & FindEntities

    /// <summary>
    ///     根据Id查找实体
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    TEntity? FindEntity(TKey id);

    /// <summary>
    ///     根据Id查找实体
    /// </summary>
    /// <param name="ids"></param>
    /// <returns></returns>
    IEnumerable<TEntity> FindEntities(IEnumerable<TKey> ids);

    /// <summary>
    ///     根据Id查找实体
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken">取消信号</param>
    /// <returns></returns>
    Task<TEntity?> FindEntityAsync(
        TKey id,
        CancellationToken cancellationToken = default);

    /// <summary>
    ///     根据Id查找实体
    /// </summary>
    /// <param name="ids"></param>
    /// <param name="cancellationToken">取消信号</param>
    /// <returns></returns>
    Task<IEnumerable<TEntity>> FindEntitiesAsync(
        IEnumerable<TKey> ids,
        CancellationToken cancellationToken = default);

    #endregion

    #region Exists

    /// <summary>
    ///     判断实体是否存在
    /// </summary>
    /// <param name="id">实体键</param>
    /// <returns></returns>
    bool Exists(TKey id);

    /// <summary>
    ///     判断实体是否存在
    /// </summary>
    /// <param name="id">实体键</param>
    /// <param name="cancellationToken">操作取消信号</param>
    /// <returns></returns>
    Task<bool> ExistsAsync(
        TKey id,
        CancellationToken cancellationToken = default);

    #endregion
}

/// <summary>
///     无键实体视图接口
/// </summary>
/// <typeparam name="TEntity">实体类型</typeparam>
public interface IKeyLessEntityView<TEntity> : IDisposable where TEntity : class
{
    #region Access

    /// <summary>
    ///     Entity集合访问器
    /// </summary>
    DbSet<TEntity> EntitySet { get; }

    /// <summary>
    ///     Entity有追踪访问器
    /// </summary>
    IQueryable<TEntity> TrackingQuery { get; }

    /// <summary>
    ///     Entity无追踪访问器
    /// </summary>
    IQueryable<TEntity> EntityQuery { get; }

    /// <summary>
    ///     实体标识
    /// </summary>
    string EntityName { get; }

    #endregion
}