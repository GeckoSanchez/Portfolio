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
	/// A <see cref="DeviceType"/>-type version of an ID
	/// </summary>
	[JsonObject(MemberSerialization.OptIn)]
	public class DeviceID : BaseID<DeviceType>
	{
		/// <summary>
		/// Primary constructor for the <see cref="DeviceID"/> class
		/// </summary>
		/// <param name="value">The given value</param>
		[JsonConstructor, MainConstructor]
		private DeviceID(UInt128 value) : base(value) => Log.Event(new StackFrame(true));

		/// <summary>
		/// Constructor for the <see cref="DeviceID"/> class
		/// </summary>
		/// <param name="type">The given <see cref="DeviceType"/> type</param>
		/// <param name="id">The given sub-ID</param>
		/// <exception cref="IDException"/>
		public DeviceID(DeviceType type, ulong id) : base(type, id) => Log.Event(new StackFrame(true));

		/// <inheritdoc cref="DeviceID(DeviceType, ulong)"/>
		public DeviceID(DeviceType type) : base(type) => Log.Event(new StackFrame(true));

		/// <inheritdoc cref="DeviceID(DeviceType, ulong)"/>
		public DeviceID(BaseID<DeviceType> id) : base(id) => Log.Event(new StackFrame(true));

		/// <summary>
		/// Empty constructor for the <see cref="DeviceID"/> class
		/// </summary>
		[EmptyConstructor]
		public DeviceID() : base(Def.DvcType, (ulong)Random.Shared.NextInt64()) => Log.Event(new StackFrame(true));

		public static implicit operator DeviceID(DeviceType v) => new(v);
	}
}
