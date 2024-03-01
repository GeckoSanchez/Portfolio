namespace ZData01.Values
{
	using System.Diagnostics;
	using Actions;
	using Attributes;
	using Bases;
	using Enums;
	using Exceptions;
	using Newtonsoft.Json;

	[JsonObject(MemberSerialization.OptIn)]
	public class ObjectName : BaseName<ObjectType>
	{
		/// <summary>
		/// Primary constructor for the <see cref="ObjectName"/> class
		/// </summary>
		/// <param name="value">The given value</param>
		/// <exception cref="NameException"/>
		[JsonConstructor, MainConstructor]
		public ObjectName(string value) : base(value) => Log.Event(new StackFrame(true));

		/// <summary>
		/// Constructor for the <see cref="ObjectName"/> class
		/// </summary>
		/// <inheritdoc cref="ObjectName(string)"/>
		public ObjectName(BaseName<ObjectType> value) : this(value.Value) => Log.Event(new StackFrame(true));

		/// <inheritdoc cref="ObjectName(BaseName{ObjectType})"/>
		public ObjectName(Base<string> value) : this(value.Value) => Log.Event(new StackFrame(true));

		public static implicit operator ObjectName(string v) => new(v);
	}
}
