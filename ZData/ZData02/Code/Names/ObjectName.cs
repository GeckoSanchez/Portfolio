namespace ZData02.Names
{
	using System.Diagnostics;
	using System.Diagnostics.CodeAnalysis;
	using Actions;
	using Attributes;
	using Bases;
	using Enums;
	using Exceptions;
	using Newtonsoft.Json;

	[JsonObject(MemberSerialization.OptIn)]
	public class ObjectName : BaseName<ObjectKind>
	{
		/// <summary>
		/// Primary constructor for the <see cref="ObjectName"/> class
		/// </summary>
		/// <param name="name">The given name</param>
		/// <exception cref="NameException"/>
		[JsonConstructor, MainConstructor]
		public ObjectName([NotNull] string name) : base(name) => Log.Event(new StackFrame(true));

		/// <summary>
		/// Constructor for the <see cref="ObjectName"/> class
		/// </summary>
		/// <param name="data">The given data</param>
		/// <exception cref="NameException"/>
		public ObjectName(BaseName<ObjectKind> data) : base(data.Data) => Log.Event(new StackFrame(true));

		/// <inheritdoc cref="ObjectName(BaseName{PlatformKind})"/>
		public ObjectName(ObjectName data) : base(data.Data) => Log.Event(new StackFrame(true));

		public static implicit operator ObjectName(string v) => new(v);
	}
}
