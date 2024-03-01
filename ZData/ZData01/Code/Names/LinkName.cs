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
	public class LinkName : BaseName<LinkType>
	{
		/// <summary>
		/// Primary constructor for the <see cref="LinkName"/> class
		/// </summary>
		/// <param name="value">The given value</param>
		/// <exception cref="NameException"/>
		[JsonConstructor, MainConstructor]
		public LinkName(string value) : base(value) => Log.Event(new StackFrame(true));

		/// <summary>
		/// Constructor for the <see cref="LinkName"/> class
		/// </summary>
		/// <inheritdoc cref="LinkName(string)"/>
		public LinkName(BaseName<LinkType> value) : this(value.Value) => Log.Event(new StackFrame(true));

		/// <inheritdoc cref="LinkName(BaseName{LinkType})"/>
		public LinkName(Base<string> value) : this(value.Value) => Log.Event(new StackFrame(true));

		public static implicit operator LinkName(string v) => new(v);
	}
}
