namespace ZData02.Properties
{
	using Categories;
	using Values;

	public interface IDeletable : IProperty
	{
		Moment? DMoment { get; }

		/// <summary>
		/// Function to delete the *<c>EXISTING</c>* save file for this <see cref="ICreatable"/>
		/// </summary>
		void Delete();
	}
}
