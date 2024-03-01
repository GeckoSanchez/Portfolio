namespace ZData01.Properties
{
	using Categories;
	using Values;

	public interface ICreatable : IProperty
	{
		Moment CMoment { get; }
		bool IsCreated { get; }

		void Create();
	}
}
