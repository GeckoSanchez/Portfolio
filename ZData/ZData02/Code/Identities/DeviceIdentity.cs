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
	public class DeviceIdentity : BaseIdentity<DeviceKind>
	{
		/// <summary>
		/// Primary constructor for the <see cref="DeviceIdentity"/> class
		/// </summary>
		/// <param name="name">The <see cref="DeviceName"/> name</param>
		/// <param name="type">The <see cref="DeviceType"/> type</param>
		/// <param name="id">The <see cref="DeviceID"/> ID</param>
		/// <exception cref="IdentityException"/>
		[JsonConstructor, MainConstructor]
		public DeviceIdentity([NotNull] DeviceName name, [NotNull] DeviceType type, [NotNull] DeviceID id) : base(new BaseName<DeviceKind>(name.Data), new BaseType<DeviceKind>(type.Data), new BaseID<DeviceKind>(id)) => Log.Event(new StackFrame(true));

		/// <summary>
		/// Constructor for the <see cref="DeviceIdentity"/> class
		/// </summary>
		/// <inheritdoc cref="DeviceIdentity(DeviceName, DeviceType, DeviceID)"/>
		public DeviceIdentity([NotNull] DeviceName name, [NotNull] DeviceKind type) : base(new BaseName<DeviceKind>(name.Data), new BaseType<DeviceKind>(type), new BaseID<DeviceKind>(type)) => Log.Event(new StackFrame(true));

		/// <inheritdoc cref="DeviceIdentity(DeviceName, DeviceKind)"/>
		public DeviceIdentity([NotNull] DeviceName name, [NotNull] DeviceType type) : base(name.Data, type.Data, new DeviceID(type.Data)) => Log.Event(new StackFrame(true));

		/// <inheritdoc cref="DeviceIdentity(DeviceName, DeviceKind)"/>
		public DeviceIdentity([NotNull] DeviceName name) : base(name.Data, Def.DeviceKind) => Log.Event(new StackFrame(true));
	}
}
