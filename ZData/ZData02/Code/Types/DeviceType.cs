namespace ZData02.Types
{
	using System.Diagnostics;
	using Actions;
	using Attributes;
	using Bases;
	using Enums;
	using Newtonsoft.Json;

	[JsonObject(MemberSerialization.OptIn)]
	public class DeviceType : BaseType<DeviceKind>
	{
		/// <summary>
		/// Primary constructor for the <see cref="DeviceType"/> class
		/// </summary>
		/// <param name="type">The <see cref="ObjectKind"/> type data</param>
		[JsonConstructor, MainConstructor]
		public DeviceType(DeviceKind type) : base(type) => Log.Event(new StackFrame(true));

		/// <summary>
		/// Constructor for the <see cref="DeviceType"/> class
		/// </summary>
		/// <param name="data">The <see cref="DeviceType"/> data</param>
		public DeviceType(DeviceType data) : this(data.Data) => Log.Event(new StackFrame(true));

		public static implicit operator DeviceType(DeviceKind v) => new(v);
	}
}
