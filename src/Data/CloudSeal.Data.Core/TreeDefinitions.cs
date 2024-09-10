namespace CloudSeal.Data.Core;

/// <summary>
///     树结构项目定义接口
/// </summary>
/// <typeparam name="TKey">标识类型</typeparam>
/// <typeparam name="TParentKey">父项目标识类型</typeparam>
public interface ITreeItemDefinition<TKey, TParentKey> : IKeySlot<TKey>, IParentKeySlot<TParentKey>
    where TKey : IEquatable<TKey>, IComparable, IComparable<TKey>;

/// <summary>
///     树结构项目定义接口
/// </summary>
public interface ITreeItemDefinition : ITreeItemDefinition<Guid, Guid?>;

/// <summary>
///     树结构节点信息定义接口
/// </summary>
/// <typeparam name="TTreeEntityInfo">树结构项目信息类型</typeparam>
/// <typeparam name="TKey">标识类型</typeparam>
public interface ITreeNodeInfoDefinition<TTreeEntityInfo, TKey> : IKeySlot<TKey>
    where TTreeEntityInfo : IKeySlot<TKey>
    where TKey : IEquatable<TKey>, IComparable, IComparable<TKey>
{
    /// <summary>
    ///     子节点集合
    /// </summary>
    ICollection<TTreeEntityInfo>? Children { get; set; }
}

/// <summary>
///     树结构节点信息定义接口
/// </summary>
/// <typeparam name="TTreeEntityInfo">树结构项目信息类型</typeparam>
public interface ITreeNodeInfoDefinition<TTreeEntityInfo> :
    ITreeNodeInfoDefinition<TTreeEntityInfo, Guid>
    where TTreeEntityInfo : IKeySlot;

/// <summary>
///     二叉树结构节点信息定义接口
/// </summary>
/// <typeparam name="TTreeEntityInfo">树结构项目信息类型</typeparam>
/// <typeparam name="TKey">标识类型</typeparam>
public interface IBinaryTreeNodeInfoDefinition<TTreeEntityInfo, TKey> : IKeySlot<TKey>
    where TTreeEntityInfo : IKeySlot<TKey>
    where TKey : IEquatable<TKey>, IComparable, IComparable<TKey>
{
    /// <summary>
    ///     左子节点
    /// </summary>
    TTreeEntityInfo? LeftChild { get; set; }

    /// <summary>
    ///     右子节点
    /// </summary>
    TTreeEntityInfo? RightChild { get; set; }
}

/// <summary>
///     二叉树结构节点信息定义接口
/// </summary>
/// <typeparam name="TTreeEntityInfo">树结构项目信息类型</typeparam>
public interface IBinaryTreeNodeInfoDefinition<TTreeEntityInfo> :
    IBinaryTreeNodeInfoDefinition<TTreeEntityInfo, Guid>
    where TTreeEntityInfo : IKeySlot;

/// <summary>
///     树结构节点定义接口
/// </summary>
/// <typeparam name="TTreeEntity">树结构项目类型</typeparam>
/// <typeparam name="TKey">标识类型</typeparam>
/// <typeparam name="TParentKey">父项目标识类型</typeparam>
public interface ITreeNodeDefinition<TTreeEntity, TKey, TParentKey> : ITreeItemDefinition<TKey, TParentKey>,
    ITreeNodeInfoDefinition<TTreeEntity, TKey>
    where TTreeEntity : ITreeItemDefinition<TKey, TParentKey>
    where TKey : IEquatable<TKey>, IComparable, IComparable<TKey>;

/// <summary>
///     树结构节点定义接口
/// </summary>
/// <typeparam name="TTreeEntity">树结构项目类型</typeparam>
public interface ITreeNodeDefinition<TTreeEntity> :
    ITreeNodeDefinition<TTreeEntity, Guid, Guid?>
    where TTreeEntity : ITreeItemDefinition<Guid, Guid?>;

/// <summary>
///     二叉树结构节点定义接口
/// </summary>
/// <typeparam name="TTreeEntity">二叉树结构项目类型</typeparam>
/// <typeparam name="TKey">标识类型</typeparam>
/// <typeparam name="TParentKey">父项目标识类型</typeparam>
public interface IBinaryTreeNodeDefinition<TTreeEntity, TKey, TParentKey> : ITreeItemDefinition<TKey, TParentKey>,
    IBinaryTreeNodeInfoDefinition<TTreeEntity, TKey>
    where TTreeEntity : ITreeItemDefinition<TKey, TParentKey>
    where TKey : IEquatable<TKey>, IComparable, IComparable<TKey>;

/// <summary>
///     二叉树结构节点定义接口
/// </summary>
/// <typeparam name="TTreeEntity">二叉树结构项目类型</typeparam>
public interface IBinaryTreeNodeDefinition<TTreeEntity> :
    IBinaryTreeNodeDefinition<TTreeEntity, Guid, Guid?>
    where TTreeEntity : ITreeItemDefinition<Guid, Guid?>;