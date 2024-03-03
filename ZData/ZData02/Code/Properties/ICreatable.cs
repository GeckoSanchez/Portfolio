namespace ZData02.Properties
{
	using Categories;
	using Values;

	public interface ICreatable : IProperty
	{
		Moment? CMoment { get; }

		/// <summary>
		/// Function to create a *<c>NEW</c>* save file for this <see cref="ICreatable"/>
		/// </summary>
		/// <remarks>Both a JSON and a <see cref="MessagePack"/> file will be created by default</remarks>
		void Create(bool json = true, bool msgpack = true);
	}
}
