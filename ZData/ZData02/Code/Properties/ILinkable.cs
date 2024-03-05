namespace ZData02.Properties
{
	using Categories;

	public interface ILinkable : IProperty
	{
	}

	public interface ILinkable<ILinked> : IProperty where ILinked : ILinkable
	{
	}
}
