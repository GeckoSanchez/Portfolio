namespace ZData01.Properties
{
	using Categories;
	using Exceptions;
	using Values;

	public interface IInitializable : IProperty
	{
		/// <summary>
		/// Moment at which this <see cref="IInitializable"/> was first initialized
		/// </summary>
		/// <remarks>
		/// This property must <c>never</c> have its value changed beyond its first initialization
		/// </remarks>
		/// <exception cref="PropertyException"/>
		public Moment IMoment { get; }
	}
}
