namespace ZData01.Categories
{
	using Enums;
	using Identities;
	using Properties;

	public interface IObject : ICategory, IAccessible, IActivatable, ICreatable, IEditable<ObjectType>, IDeletable, IInitializable
	{
		ObjectIdentity Identity { get; }
	}

	public interface IObject<TObj> : IObject where TObj : IObject
	{
	}
}
