namespace ZData01.Properties
{
	using Categories;
	using Values;

	public interface IDeletable : IProperty
	{
		/// <summary>
		/// Moment at which the <see cref="IDeletable"/> <c>will</c> be deleted
		/// </summary>
		Moment? DelMoment { get; }
		bool CanDelete { get; }

		/// <summary>
		/// Function to delete the save file of this <see cref="IDeletable"/>, as well as remove any & all links it could have to other data
		/// </summary>
		void Delete();
	}
}
