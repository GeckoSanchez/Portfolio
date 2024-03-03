namespace ZData02.Types
{
	using System.Diagnostics;
	using Actions;
	using Attributes;
	using Bases;
	using Enums;
	using Newtonsoft.Json;

	[JsonObject(MemberSerialization.OptIn)]
	public class ObjectType : BaseType<ObjectKind>
	{
		/// <summary>
		/// Primary constructor for the <see cref="ObjectType"/> class
		/// </summary>
		/// <param name="type">The <see cref="ObjectKind"/> type data</param>
		[JsonConstructor, MainConstructor]
		public ObjectType(ObjectKind type) : base(type) => Log.Event(new StackFrame(true));

		/// <summary>
		/// Constructor for the <see cref="ObjectType"/> class
		/// </summary>
		/// <param name="data">The <see cref="ObjectType"/> data</param>
		public ObjectType(ObjectType type) : this(type.Data) => Log.Event(new StackFrame(true));

		public static implicit operator ObjectType(ObjectKind v) => new(v);
	}
}
