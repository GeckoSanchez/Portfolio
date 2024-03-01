namespace ZData01.Properties
{
	using Bases;
	using Categories;
	using Values;

	public interface IEditable : IProperty
	{
		ICollection<Moment>? EMoments { get; }

		/// <summary>
		/// Function to change the ID of this <see cref="IEditable"/>
		/// </summary>
		/// <remarks>The new ID will be randomly generated for the correct datum type</remarks>
		/// <exception cref="BaseException"/>
		void ChangeID();
	}

	public interface IEditable<TEnum> : IEditable where TEnum: struct, Enum
	{
		/// <summary>
		/// Function to change the <paramref name="name"/> of this <see cref="IEditable{TEnum}"/>
		/// </summary>
		/// <param name="name">The new name of this <see cref="IEditable{TEnum}"/></param>
		/// <exception cref="BaseException"/>
		void ChangeName(BaseName<TEnum> name);

		/// <summary>
		/// Function to change the <paramref name="type"/> of this <see cref="IEditable{TEnum}"/>
		/// </summary>
		/// <param name="type">The new type of this <see cref="IEditable{TEnum}"/></param>
		/// <exception cref="BaseException"/>
		void ChangeType(BaseType<TEnum> type);

		/// <summary>
		/// Function to change the <paramref name="type"/> of this <see cref="IEditable{TEnum}"/>
		/// </summary>
		/// <param name="type">The new type of this <see cref="IEditable{TEnum}"/></param>
		/// <exception cref="BaseException"/>
		void ChangeType(TEnum name);

		/// <summary>
		/// Function to change the <paramref name="id"/> of this <see cref="IEditable{TEnum}"/>
		/// </summary>
		/// <param name="id">The new type of this <see cref="IEditable{TEnum}"/></param>
		/// <exception cref="BaseException"/>
		void ChangeID(BaseID<TEnum> id);
	}
}
