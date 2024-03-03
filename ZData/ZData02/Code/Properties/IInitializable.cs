namespace ZData02.Properties
{
	using Categories;
	using Values;

	public interface IInitializable : IProperty
	{
		Moment IMoment { get; }
	}
}
