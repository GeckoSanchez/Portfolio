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
	public class DeviceName : BaseName<DeviceKind>
	{
		/// <summary>
		/// Primary constructor for the <see cref="DeviceName"/> class
		/// </summary>
		/// <param name="name">The given name</param>
		/// <exception cref="NameException"/>
		[JsonConstructor, MainConstructor]
		public DeviceName([NotNull] string name) : base(name) => Log.Event(new StackFrame(true));

		/// <summary>
		/// Constructor for the <see cref="DeviceName"/> class
		/// </summary>
		/// <param name="data">The given data</param>
		/// <exception cref="NameException"/>
		public DeviceName(BaseName<DeviceKind> data) : base(data.Data) => Log.Event(new StackFrame(true));

		/// <inheritdoc cref="DeviceName(BaseName{PlatformKind})"/>
		public DeviceName(DeviceName data) : base(data.Data) => Log.Event(new StackFrame(true));

		public static implicit operator DeviceName(string v) => new(v);
	}
}
