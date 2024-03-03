namespace ZData02.Properties
{
	using Categories;

	public interface IActivatable : IProperty
	{
		bool IsActive { get; }

		/// <summary>
		/// Function to activate this <see cref="IActivatable"/>
		/// </summary>
		void Activate();

		/// <summary>
		/// Function to deactivate this <see cref="IActivatable"/>
		/// </summary>
		void Deactivate();
	}
}
