namespace ZData02.Categories
{
	using Identities;
	using ZData02.Properties;

	public interface IObject : ICategorized, IAccessible, IActivatable, ICreatable, IInitializable, IDeletable
	{
		ObjectIdentity Identity { get; }
	}

	public interface IObject<TLinkable> : IObject where TLinkable : IObject
	{
	}
}
