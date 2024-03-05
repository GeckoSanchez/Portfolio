namespace ZData02.Categories
{
	using Identities;
	using Properties;

	public interface IDevice : ICategorized, IAccessible, IActivatable, ICreatable, ISavable, IDeletable, IEditable, ILinkable
	{
		DeviceIdentity Identity { get; }
	}
}
