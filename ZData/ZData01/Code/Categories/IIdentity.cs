namespace ZData01.Categories
{
	using ZData01.Bases;

	public interface IIdentity<TEnum> : ICategory where TEnum : struct, Enum
	{
		/// <summary>
		/// This Identity's base name
		/// </summary>
		BaseName<TEnum> Name { get; }

		/// <summary>
		/// This Identity's base <typeparamref name="TEnum"/> type
		/// </summary>
		BaseType<TEnum> Type { get; }

		/// <summary>
		/// This Identity's base <typeparamref name="TEnum"/> ID
		/// </summary>
		BaseID<TEnum> ID { get; }
	}
}
