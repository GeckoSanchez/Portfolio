namespace ZData02.Categories
{
	using Identities;
	using Properties;

	public interface IObject : ICategorized, IAccessible, IActivatable, ICreatable, IInitializable, IDeletable, IEditable, ISavable
	{
		ObjectIdentity Identity { get; }
	}

	public interface IObject<TLinkable> : IObject where TLinkable : ILinkable, IObject
	{
	}
}
