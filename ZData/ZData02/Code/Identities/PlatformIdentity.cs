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
	public class PlatformIdentity : BaseIdentity<PlatformKind>
	{
		/// <summary>
		/// Primary constructor for the <see cref="PlatformIdentity"/> class
		/// </summary>
		/// <param name="name">The <see cref="PlatformName"/> name</param>
		/// <param name="type">The <see cref="PlatformType"/> type</param>
		/// <param name="id">The <see cref="PlatformID"/> ID</param>
		/// <exception cref="IdentityException"/>
		[JsonConstructor, MainConstructor]
		public PlatformIdentity([NotNull] PlatformName name, [NotNull] PlatformType type, [NotNull] PlatformID id) : base(name, type, new(id)) => Log.Event(new StackFrame(true));

		/// <summary>
		/// Constructor for the <see cref="PlatformIdentity"/> class
		/// </summary>
		/// <inheritdoc cref="PlatformIdentity(PlatformName,PlatformType,PlatformID)"/>
		public PlatformIdentity([NotNull] PlatformName name, [NotNull] PlatformKind type) : base(name, type) => Log.Event(new StackFrame(true));

		/// <inheritdoc cref="PlatformIdentity(PlatformName,PlatformKind)"/>
		public PlatformIdentity([NotNull] PlatformName name) : base(name) => Log.Event(new StackFrame(true));

		/// <inheritdoc cref="PlatformIdentity(PlatformName,PlatformKind)"/>
		public PlatformIdentity([NotNull] PlatformName name, [NotNull] PlatformType type) : base(name, type) => Log.Event(new StackFrame(true));
	}
}
