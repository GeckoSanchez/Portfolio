namespace ZData01.IDs
{
	using System.Diagnostics;
	using Actions;
	using Attributes;
	using Bases;
	using Enums;
	using Exceptions;
	using Newtonsoft.Json;

	/// <summary>
	/// A <see cref="ObjectType"/>-type version of an ID
	/// </summary>
	[JsonObject(MemberSerialization.OptIn)]
	public class ObjectID : BaseID<ObjectType>
	{
		/// <summary>
		/// Primary constructor for the <see cref="ObjectID"/> class
		/// </summary>
		/// <param name="value">The given value</param>
		[JsonConstructor, MainConstructor]
		private ObjectID(UInt128 value) : base(value) => Log.Event(new StackFrame(true));

		/// <summary>
		/// Constructor for the <see cref="ObjectID"/> class
		/// </summary>
		/// <param name="type">The given <see cref="ObjectType"/> type</param>
		/// <param name="id">The given sub-ID</param>
		/// <exception cref="IDException"/>
		public ObjectID(ObjectType type, ulong id) : base(type, id) => Log.Event(new StackFrame(true));

		/// <inheritdoc cref="ObjectID(ObjectType, ulong)"/>
		public ObjectID(ObjectType type) : base(type) => Log.Event(new StackFrame(true));

		/// <inheritdoc cref="ObjectID(ObjectType, ulong)"/>
		public ObjectID(BaseType<ObjectType> type) : base(type.Value) => Log.Event(new StackFrame(true));

		/// <inheritdoc cref="ObjectID(ObjectType, ulong)"/>
		public ObjectID() : base(Def.OType) => Log.Event(new StackFrame(true));

		public static implicit operator ObjectID(ObjectType v) => new(v);
		public static implicit operator ObjectID(BaseType<ObjectType> v) => new(v);
	}
}
