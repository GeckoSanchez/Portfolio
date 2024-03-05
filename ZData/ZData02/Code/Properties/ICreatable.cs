namespace ZData02.Properties
{
	using Categories;
	using Values;
	using ZData02.Enums;

	public interface ICreatable : IProperty
	{
		Moment? CMoment { get; }

		/// <summary>
		/// Function to create a *<c>NEW</c>* save file for this <see cref="ICreatable"/>
		/// </summary>
		void Create();
	}
}
