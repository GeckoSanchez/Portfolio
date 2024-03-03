namespace ZData02.Identities
{
	using System.Diagnostics;
	using System.Diagnostics.CodeAnalysis;
	using Actions;
	using Attributes;
	using Bases;
	using Enums;
	using Exceptions;
	using Names;
	using IDs;
	using Newtonsoft.Json;
	using Types;

	[JsonObject(MemberSerialization.OptIn)]
	public class LinkIdentity : BaseIdentity<LinkKind>
	{
		/// <summary>
		/// Primary constructor for the <see cref="LinkIdentity"/> class
		/// </summary>
		/// <param name="name">The <see cref="LinkName"/> name</param>
		/// <param name="type">The <see cref="LinkType"/> type</param>
		/// <param name="id">The <see cref="LinkID"/> ID</param>
		/// <exception cref="IdentityException"/>
		[JsonConstructor, MainConstructor]
		public LinkIdentity([NotNull] LinkName name, [NotNull] LinkType type, [NotNull] LinkID id) : base(new BaseName<LinkKind>(name.Data), new BaseType<LinkKind>(type.Data), new BaseID<LinkKind>(id)) => Log.Event(new StackFrame(true));

		/// <summary>
		/// Constructor for the <see cref="LinkIdentity"/> class
		/// </summary>
		/// <inheritdoc cref="LinkIdentity(LinkName, LinkType, LinkID)"/>
		public LinkIdentity([NotNull] LinkName name, [NotNull] LinkKind type) : base(new BaseName<LinkKind>(name.Data), new BaseType<LinkKind>(type), new BaseID<LinkKind>(type)) => Log.Event(new StackFrame(true));

		/// <inheritdoc cref="LinkIdentity(LinkName, LinkKind)"/>
		public LinkIdentity([NotNull] LinkName name, [NotNull] LinkType type) : base(name.Data, type.Data, new LinkID(type.Data)) => Log.Event(new StackFrame(true));

		/// <inheritdoc cref="LinkIdentity(LinkName, LinkKind)"/>
		public LinkIdentity([NotNull] LinkName name) : base(name.Data, Def.LinkKind) => Log.Event(new StackFrame(true));
	}
}
