namespace ZData01.Values
{
	using System.Diagnostics;
	using Actions;
	using Attributes;
	using Bases;
	using Enums;
	using Exceptions;
	using Newtonsoft.Json;

	[JsonObject(MemberSerialization.OptIn)]
	public class DeviceName : BaseName<DeviceType>
	{
		/// <summary>
		/// Primary constructor for the <see cref="DeviceName"/> class
		/// </summary>
		/// <param name="value">The given value</param>
		/// <exception cref="NameException"/>
		[JsonConstructor, MainConstructor]
		public DeviceName(string value) : base(value) => Log.Event(new StackFrame(true));

		/// <summary>
		/// Constructor for the <see cref="DeviceName"/> class
		/// </summary>
		/// <inheritdoc cref="DeviceName(string)"/>
		public DeviceName(BaseName<DeviceType> value) : this(value.Value) => Log.Event(new StackFrame(true));

		/// <inheritdoc cref="DeviceName(BaseName{DeviceType})"/>
		public DeviceName(Base<string> value) : this(value.Value) => Log.Event(new StackFrame(true));

		public static implicit operator DeviceName(string v) => new(v);
	}
}
