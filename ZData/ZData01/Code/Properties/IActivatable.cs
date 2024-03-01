namespace ZData01.Properties
{
	using Categories;

	public interface IActivatable : IProperty
	{
		bool IsActive { get; }

		void Activate();
		void Deactivate();
	}
}
