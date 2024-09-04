namespace CloudSeal.Core;

/// <summary>
/// 树结构项目定义
/// </summary>
/// <typeparam name="TKey">标识类型</typeparam>
/// <typeparam name="TParentKey">父项目标识类型</typeparam>
public interface ITreeItemDefinition<TKey, TParentKey> : IKeySlot<TKey>, IParentKeySlot<TParentKey>
    where TKey : IEquatable<TKey>;

/// <summary>
/// 树结构节点信息定义
/// </summary>
/// <typeparam name="TTreeEntityInfo">树结构项目信息类型</typeparam>
/// <typeparam name="TKey">标识类型</typeparam>
public interface ITreeNodeInfoDefinition<TTreeEntityInfo, TKey> : IKeySlot<TKey>
    where TTreeEntityInfo : IKeySlot<TKey>
    where TKey : IEquatable<TKey>
{
    /// <summary>
    /// 子节点集合
    /// </summary>
    ICollection<TTreeEntityInfo>? Children { get; set; }
}

/// <summary>
/// 树结构节点定义
/// </summary>
/// <typeparam name="TTreeEntity">树结构项目类型</typeparam>
/// <typeparam name="TKey">标识类型</typeparam>
/// <typeparam name="TParentKey">父项目标识类型</typeparam>
public interface ITreeNodeDefinition<TTreeEntity, TKey, TParentKey> : ITreeItemDefinition<TKey, TParentKey>, ITreeNodeInfoDefinition<TTreeEntity, TKey>
    where TTreeEntity : ITreeItemDefinition<TKey, TParentKey>
    where TKey : IEquatable<TKey>;