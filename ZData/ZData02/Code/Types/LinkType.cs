namespace ZData02.Types
{
	using System.Diagnostics;
	using Actions;
	using Attributes;
	using Bases;
	using Enums;
	using Newtonsoft.Json;

	[JsonObject(MemberSerialization.OptIn)]
	public class LinkType : BaseType<LinkKind>
	{
		/// <summary>
		/// Primary constructor for the <see cref="LinkType"/> class
		/// </summary>
		/// <param name="type">The <see cref="ObjectKind"/> type data</param>
		[JsonConstructor, MainConstructor]
		public LinkType(LinkKind type) : base(type) => Log.Event(new StackFrame(true));

		/// <summary>
		/// Constructor for the <see cref="LinkType"/> class
		/// </summary>
		/// <param name="data">The <see cref="LinkType"/> data</param>
		public LinkType(LinkType data) : this(data.Data) => Log.Event(new StackFrame(true));

		public static implicit operator LinkType(LinkKind v) => new(v);
	}
}
