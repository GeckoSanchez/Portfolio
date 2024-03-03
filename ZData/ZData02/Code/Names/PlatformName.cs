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
	public class PlatformName : BaseName<PlatformKind>
	{
		/// <summary>
		/// Primary constructor for the <see cref="PlatformName"/> class
		/// </summary>
		/// <param name="name">The given name</param>
		/// <exception cref="NameException"/>
		[JsonConstructor, MainConstructor]
		public PlatformName([NotNull] string name) : base(name) => Log.Event(new StackFrame(true));

		/// <summary>
		/// Constructor for the <see cref="PlatformName"/> class
		/// </summary>
		/// <param name="data">The given data</param>
		/// <exception cref="NameException"/>
		public PlatformName(BaseName<PlatformKind> data) : base(data.Data) => Log.Event(new StackFrame(true));

		/// <inheritdoc cref="PlatformName(BaseName{PlatformKind})"/>
		public PlatformName(PlatformName data) : base(data.Data) => Log.Event(new StackFrame(true));

		public static implicit operator PlatformName(string v) => new(v);
	}
}
