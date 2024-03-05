namespace ZData02.Links
{
	using System.Diagnostics;
	using System.Diagnostics.CodeAnalysis;
	using Actions;
	using Attributes;
	using Bases;
	using Exceptions;
	using Identities;
	using Newtonsoft.Json;

	[JsonObject(MemberSerialization.OptIn)]
	public class DeviceLink : BaseLink<BaseDevice>
	{
		/// <summary>
		/// Primary constructor for the <see cref="DeviceLink"/> class
		/// </summary>
		/// <inheritdoc cref="BaseLink{TObject}(LinkIdentity,UInt128,UInt128)"/>
		/// <exception cref="LinkException"/>
		[JsonConstructor, MainConstructor]
		private DeviceLink([NotNull] LinkIdentity identity, [NotNull] UInt128 parent, [NotNull] UInt128 child) : base(identity, parent, child) => Log.Event(new StackFrame(true));

		/// <summary>
		/// Constructor for the <see cref="DeviceLink"/> class
		/// </summary>
		/// <inheritdoc cref="BaseLink{TObject}(LinkIdentity,UInt128,UInt128)"/>
		/// <exception cref="LinkException"/>
		public DeviceLink(LinkIdentity identity, BaseDevice parent, BaseDevice child) : this(identity, parent.Identity.ID.Data, child.Identity.ID.Data) => Log.Event(new StackFrame(true));
	}
}
