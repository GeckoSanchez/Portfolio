namespace ZData02.Properties
{
	using Categories;
	using Values;

	public interface IEditable : IProperty
	{
		HashSet<Moment>? EMoments { get; }

		/// <summary>
		/// Function to change the name of this <see cref="IEditable"/>
		/// </summary>
		/// <param name="name"></param>
		void ChangeName(string name);
	}
}
