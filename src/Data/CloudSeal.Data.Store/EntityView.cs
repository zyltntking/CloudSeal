using CloudSeal.Data.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CloudSeal.Data.Store;

/// <summary>
///     抽象实体视图接口
/// </summary>
/// <typeparam name="TEntity">实体类型</typeparam>
/// <typeparam name="TKey">键类型</typeparam>
public abstract class EntityView<TEntity, TKey> : KeyLessEntityView<TEntity>, IEntityView<TEntity, TKey>
    where TEntity : class, IKeySlot<TKey>
    where TKey : IComparable, IComparable<TKey>, IEquatable<TKey>
{
    /// <summary>
    ///     无键实体视图构造
    /// </summary>
    /// <param name="context">数据库上下文</param>
    /// <param name="cacheProxy">缓存代理</param>
    /// <param name="logger">日志依赖</param>
    protected EntityView(
        DbContext context,
        ICacheProxy? cacheProxy,
        ILogger? logger = null) : base(context, logger)
    {
        CacheProxy = cacheProxy;
    }

    #region Access

    /// <summary>
    ///     缓存代理
    /// </summary>
    protected ICacheProxy? CacheProxy { get; }

    /// <summary>
    ///     实体键生成委托
    /// </summary>
    protected abstract string EntityKey(TKey key);

    /// <summary>
    ///     实体键生成委托
    /// </summary>
    protected override string EntityKey(TEntity entity)
    {
        return EntityKey(entity.Id);
    }

    /// <summary>
    ///     键适配查询
    /// </summary>
    public IQueryable<TEntity> KeyMatchQuery(TKey key)
    {
        return EntityQuery.Where(entity => entity.Id.Equals(key));
    }

    /// <summary>
    ///     键适配查询
    /// </summary>
    /// <param name="keys"></param>
    /// <returns></returns>
    public IQueryable<TEntity> KeyMatchQuery(IEnumerable<TKey> keys)
    {
        return EntityQuery.Where(entity => keys.Contains(entity.Id));
    }

    #endregion

    #region GetId

    /// <summary>
    ///     获取指定实体Id
    /// </summary>
    /// <param name="entity">实体</param>
    /// <returns>Id</returns>
    public TKey GetId(TEntity entity)
    {
        OnActionExecuting(entity, nameof(entity));
        return entity.Id;
    }

    /// <summary>
    ///     获取指定实体Id
    /// </summary>
    /// <param name="entity">实体</param>
    /// <param name="cancellationToken">取消异步操作的信号</param>
    /// <returns>Id</returns>
    public Task<TKey> GetIdAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        OnAsyncActionExecuting(entity, nameof(entity), cancellationToken);
        return Task.FromResult(entity.Id);
    }

    #endregion

    #region IsDeleted

    /// <summary>
    ///     是否被删除
    /// </summary>
    /// <param name="entity">实体</param>
    /// <returns>判断结果</returns>
    public bool IsDeleted(TEntity entity)
    {
        OnActionExecuting(entity, nameof(entity));
        return IsDeleted(entity.Id);
    }

    /// <summary>
    ///     是否被删除
    /// </summary>
    /// <param name="key">键</param>
    /// <returns></returns>
    public bool IsDeleted(TKey key)
    {
        OnActionExecuting(key, nameof(key));
        return !KeyMatchQuery(key).Any();
    }

    /// <summary>
    ///     是否被删除
    /// </summary>
    /// <param name="entity">实体</param>
    /// <param name="cancellationToken">操作取消信号</param>
    /// <returns>判断结果</returns>
    public Task<bool> IsDeletedAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        OnAsyncActionExecuting(entity, nameof(entity), cancellationToken);

        return IsDeletedAsync(entity.Id, cancellationToken);
    }

    /// <summary>
    ///     是否被删除
    /// </summary>
    /// <param name="key">键</param>
    /// <param name="cancellationToken">操作取消信号</param>
    /// <returns></returns>
    public async Task<bool> IsDeletedAsync(TKey key, CancellationToken cancellationToken = default)
    {
        OnAsyncActionExecuting(key, nameof(key), cancellationToken);

        var exists = await KeyMatchQuery(key).AnyAsync(cancellationToken);

        return !exists;
    }

    #endregion

    #region FindEntity

    /// <summary>
    ///     根据Id查找实体
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public TEntity? FindEntity(TKey id)
    {
        OnActionExecuting(id, nameof(id));

        var cacheKey = CacheKey(id);

        var entity = CacheProxy?.Get<TEntity>(cacheKey);

        if (entity is not null)
        {
            Logger?.LogDebug("Read {cacheKey} From Cache", cacheKey);
            return entity;
        }

        entity = FindById(id);

        if (entity is not null)
        {
            CacheProxy?.Set(cacheKey, entity, CacheProxy.DefaultExpire);
            Logger?.LogDebug("{cacheKey} Cached", cacheKey);
        }

        return entity;
    }

    /// <summary>
    ///     根据Id查找实体
    /// </summary>
    /// <param name="ids"></param>
    /// <returns></returns>
    public IEnumerable<TEntity> FindEntities(IEnumerable<TKey> ids)
    {
        var idArray = ids as TKey[] ?? ids.ToArray();

        OnActionExecuting(idArray, nameof(ids));

        var entities = new List<TEntity>();

        if (CacheProxy is not null)
            foreach (var id in idArray)
            {
                var cacheKey = CacheKey(id);

                var entity = CacheProxy.Get<TEntity>(cacheKey);

                if (entity is not null)
                {
                    entities.Add(entity);
                    Logger?.LogDebug("Read {cacheKey} From Cache", cacheKey);
                }
            }

        if (entities.Count > 0) return entities;

        entities = FindByIds(idArray);

        if (entities.Count > 0)
            foreach (var entity in entities)
            {
                var cacheKey = CacheKey(entity.Id);

                CacheProxy?.Set(cacheKey, entity, CacheProxy.DefaultExpire);
                Logger?.LogDebug("{cacheKey} Cached", cacheKey);
            }

        return entities;
    }

    /// <summary>
    ///     根据Id查找实体
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken">取消信号</param>
    /// <returns></returns>
    public async Task<TEntity?> FindEntityAsync(TKey id, CancellationToken cancellationToken = default)
    {
        OnAsyncActionExecuting(id, nameof(id), cancellationToken);

        var cacheKey = CacheKey(id);

        TEntity? entity = null;

        if (CacheProxy is not null) entity = await CacheProxy.GetAsync<TEntity>(cacheKey, cancellationToken);

        if (entity is not null)
        {
            Logger?.LogDebug("Read {cacheKey} From Cache", cacheKey);
            return entity;
        }

        entity = await FindByIdAsync(id, cancellationToken);

        if (entity is not null)
        {
            CacheProxy?.SetAsync(cacheKey, entity, CacheProxy.DefaultExpire, cancellationToken);
            Logger?.LogDebug("{cacheKey} Cached", cacheKey);
        }

        return entity;
    }

    /// <summary>
    ///     根据Id查找实体
    /// </summary>
    /// <param name="ids"></param>
    /// <param name="cancellationToken">取消信号</param>
    /// <returns></returns>
    public async Task<IEnumerable<TEntity>> FindEntitiesAsync(IEnumerable<TKey> ids,
        CancellationToken cancellationToken = default)
    {
        var idArray = ids as TKey[] ?? ids.ToArray();

        OnAsyncActionExecuting(idArray, nameof(ids), cancellationToken);

        var entities = new List<TEntity>();

        if (CacheProxy is not null)
            foreach (var id in idArray)
            {
                var cacheKey = CacheKey(id);

                var entity = await CacheProxy.GetAsync<TEntity>(cacheKey, cancellationToken);

                if (entity is not null)
                {
                    entities.Add(entity);
                    Logger?.LogDebug("Read {cacheKey} From Cache", cacheKey);
                }
            }

        if (entities.Count > 0) return entities;

        entities = await FindByIdsAsync(idArray, cancellationToken);

        if (entities.Count > 0)
            foreach (var entity in entities)
            {
                var cacheKey = CacheKey(entity.Id);

                CacheProxy?.SetAsync(cacheKey, entity, CacheProxy.DefaultExpire, cancellationToken);
                Logger?.LogDebug("{cacheKey} Cached", cacheKey);
            }

        return entities;
    }

    #endregion

    #region Exists

    /// <summary>
    ///     判断实体是否存在
    /// </summary>
    /// <param name="key">实体键</param>
    /// <returns></returns>
    public bool Exists(TKey key)
    {
        OnActionExecuting(key, nameof(key));
        return KeyMatchQuery(key).Any();
    }

    /// <summary>
    ///     判断实体是否存在
    /// </summary>
    /// <param name="key">实体键</param>
    /// <param name="cancellationToken">操作取消信号</param>
    /// <returns></returns>
    public Task<bool> ExistsAsync(TKey key, CancellationToken cancellationToken = default)
    {
        OnAsyncActionExecuting(key, nameof(key), cancellationToken);
        return KeyMatchQuery(key).AnyAsync(cancellationToken);
    }

    #endregion

    #region Action

    #region CacheKey

    /// <summary>
    ///     生成键
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    protected string CacheKey(TEntity entity)
    {
        return $"{EntityName}:{EntityKey(entity)}";
    }

    /// <summary>
    ///     生成键
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    protected string CacheKey(TKey key)
    {
        return $"{EntityName}:{EntityKey(key)}";
    }

    #endregion

    #region FindById

    /// <summary>
    ///     根据Id查询实体
    /// </summary>
    /// <param name="id">id</param>
    /// <returns></returns>
    private TEntity? FindById(TKey id)
    {
        return KeyMatchQuery(id).FirstOrDefault();
    }

    /// <summary>
    ///     根据Id查询实体
    /// </summary>
    /// <param name="ids">id表</param>
    /// <returns></returns>
    private List<TEntity> FindByIds(IEnumerable<TKey> ids)
    {
        return KeyMatchQuery(ids).ToList();
    }

    /// <summary>
    ///     根据Id查找实体
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken">取消信号</param>
    /// <returns></returns>
    private Task<TEntity?> FindByIdAsync(
        TKey id,
        CancellationToken cancellationToken = default)
    {
        return KeyMatchQuery(id).FirstOrDefaultAsync(cancellationToken);
    }

    /// <summary>
    ///     根据Id查找实体
    /// </summary>
    /// <param name="ids"></param>
    /// <param name="cancellationToken">取消信号</param>
    /// <returns></returns>
    private Task<List<TEntity>> FindByIdsAsync(
        IEnumerable<TKey> ids,
        CancellationToken cancellationToken = default)
    {
        return KeyMatchQuery(ids).ToListAsync(cancellationToken);
    }

    #endregion

    #endregion
}

/// <summary>
///     抽象无键实体视图实现
/// </summary>
public abstract class KeyLessEntityView<TEntity> : IKeyLessEntityView<TEntity> where TEntity : class
{
    /// <summary>
    ///     无键实体视图构造
    /// </summary>
    /// <param name="context">数据库上下文</param>
    /// <param name="logger">日志依赖</param>
    protected KeyLessEntityView(
        DbContext context,
        ILogger? logger = null)
    {
        ArgumentNullException.ThrowIfNull(context);
        Context = context;
        Logger = logger;
    }

    #region Implementation of IDisposable

    /// <summary>
    ///     已释放标识
    /// </summary>
    private bool _disposed;

    /// <summary>
    ///     Throws if this class has been disposed.
    /// </summary>
    protected void ThrowIfDisposed()
    {
        if (_disposed) throw new ObjectDisposedException(GetType().Name);
    }

    /// <summary>
    ///     Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
    /// </summary>
    public void Dispose()
    {
        _disposed = true;
        GC.SuppressFinalize(this);
    }

    #endregion

    #region Access

    /// <summary>
    ///     数据访问上下文
    /// </summary>
    protected DbContext Context { get; }

    /// <summary>
    ///     日志依赖
    /// </summary>
    protected ILogger? Logger { get; }

    /// <summary>
    ///     Entity集合访问器
    /// </summary>
    public DbSet<TEntity> EntitySet => Context.Set<TEntity>();

    /// <summary>
    ///     Entity有追踪访问器
    /// </summary>
    public IQueryable<TEntity> TrackingQuery => EntitySet.AsQueryable();

    /// <summary>
    ///     Entity无追踪访问器
    /// </summary>
    public IQueryable<TEntity> EntityQuery => EntitySet.AsNoTracking();

    /// <summary>
    ///     实体标识
    /// </summary>
    public string EntityName => GetType().Name;

    /// <summary>
    ///     实体键生成委托
    /// </summary>
    protected abstract string EntityKey(TEntity entity);

    #endregion

    #region Action

    #region OnActionExecution

    /// <summary>
    ///     方法执行前
    /// </summary>
    /// <typeparam name="TValue">值类型</typeparam>
    /// <param name="value">值</param>
    /// <param name="name">值名称</param>
    protected void OnActionExecuting<TValue>(TValue value, string? name)
    {
        ThrowIfDisposed();
        ArgumentNullException.ThrowIfNull(value, name);
    }

    /// <summary>
    ///     异步方法执行前
    /// </summary>
    /// <typeparam name="TValue">值类型</typeparam>
    /// <param name="value">值</param>
    /// <param name="name">值名称</param>
    /// <param name="cancellationToken">操作取消信号</param>
    protected void OnAsyncActionExecuting<TValue>(TValue value, string name,
        CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        OnActionExecuting(value, name);
    }

    #endregion

    #endregion
}