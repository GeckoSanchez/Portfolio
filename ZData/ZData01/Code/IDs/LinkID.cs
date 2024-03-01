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
	/// A <see cref="LinkType"/>-type version of an ID
	/// </summary>
	[JsonObject(MemberSerialization.OptIn)]
	public class LinkID : BaseID<LinkType>
	{
		/// <summary>
		/// Primary constructor for the <see cref="LinkID"/> class
		/// </summary>
		/// <param name="value">The given value</param>
		[JsonConstructor, MainConstructor]
		private LinkID(UInt128 value) : base(value) => Log.Event(new StackFrame(true));

		/// <summary>
		/// Constructor for the <see cref="LinkID"/> class
		/// </summary>
		/// <param name="type">The given <see cref="LinkType"/> type</param>
		/// <param name="id">The given sub-ID</param>
		/// <exception cref="IDException"/>
		public LinkID(LinkType type, ulong id) : base(type, id) => Log.Event(new StackFrame(true));

		/// <inheritdoc cref="LinkID(LinkType, ulong)"/>
		public LinkID(LinkType type) : base(type) => Log.Event(new StackFrame(true));

		/// <inheritdoc cref="LinkID(LinkType, ulong)"/>
		public LinkID() : base(Def.LnkType) => Log.Event(new StackFrame(true));

		public static implicit operator LinkID(LinkType v) => new(v);
	}
}
