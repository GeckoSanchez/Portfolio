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
	public class LinkName : BaseName<LinkKind>
	{
		/// <summary>
		/// Primary constructor for the <see cref="LinkName"/> class
		/// </summary>
		/// <param name="name">The given name</param>
		/// <exception cref="NameException"/>
		[JsonConstructor, MainConstructor]
		public LinkName([NotNull] string name) : base(name) => Log.Event(new StackFrame(true));

		/// <summary>
		/// Constructor for the <see cref="LinkName"/> class
		/// </summary>
		/// <param name="data">The given data</param>
		/// <exception cref="NameException"/>
		public LinkName(BaseName<LinkKind> data) : base(data.Data) => Log.Event(new StackFrame(true));

		/// <inheritdoc cref="LinkName(BaseName{PlatformKind})"/>
		public LinkName(LinkName data) : base(data.Data) => Log.Event(new StackFrame(true));

		public static implicit operator LinkName(string v) => new(v);
	}
}
