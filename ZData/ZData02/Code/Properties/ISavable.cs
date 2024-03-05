namespace ZData02.Properties
{
	using Categories;
	using Values;

	public interface ISavable : IProperty
	{
		/// <summary>
		/// Function to update an *<c>EXISTING</c>* save file with this <see cref="ISavable"/>'s data
		/// </summary>
		void Save();
	}
}
