namespace ZData02.Types
{
	using System.Diagnostics;
	using Actions;
	using Attributes;
	using Bases;
	using Enums;
	using Newtonsoft.Json;

	[JsonObject(MemberSerialization.OptIn)]
	public class PlatformType : BaseType<PlatformKind>
	{
		/// <summary>
		/// Primary constructor for the <see cref="PlatformType"/> class
		/// </summary>
		/// <param name="type">The <see cref="PlatformKind"/> type data</param>
		[JsonConstructor, MainConstructor]
		public PlatformType(PlatformKind type) : base(type) => Log.Event(new StackFrame(true));

		/// <summary>
		/// Constructor for the <see cref="PlatformType"/> class
		/// </summary>
		/// <param name="data">The <see cref="PlatformKind"/> data</param>
		public PlatformType(PlatformType data) : this(data.Data) => Log.Event(new StackFrame(true));

		public static implicit operator PlatformType(DeviceKind v) => new(v);
	}
}
