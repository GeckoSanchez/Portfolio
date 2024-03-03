namespace ZData02.Categories
{
	public interface IBase : ICategorized { }
	public interface IBase<T> : IBase where T : notnull
	{
		T Data { get; }
	}
}
