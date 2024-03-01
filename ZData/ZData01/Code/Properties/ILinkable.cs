namespace ZData01.Properties
{
	using Categories;

	public interface ILinkable<TLinked> : IProperty where TLinked : ILinkable<TLinked>
	{
	}
}
