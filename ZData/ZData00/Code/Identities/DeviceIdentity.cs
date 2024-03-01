namespace ZData00.Identities
{
	using System.Diagnostics;
	using Actions;
	using Attributes;
	using Bases;
	using Enums;
	using IDs;
	using Newtonsoft.Json;

	[JsonObject(MemberSerialization.OptIn)]
	public class DeviceIdentity : BaseIdentity<DeviceType>
	{
		[JsonConstructor, MainConstructor]
		public DeviceIdentity(BaseName name, BaseType<DeviceType> type, DeviceID id) : base(name, type, id) => Log.Event(new StackFrame(true));
		public DeviceIdentity(BaseName name, BaseType<DeviceType> type) : this(name, type, DeviceID.Generate(type)) => Log.Event(new StackFrame(true));
		public DeviceIdentity(BaseName name, DeviceType type) : this(name, type, DeviceID.Generate(type)) => Log.Event(new StackFrame(true));
		public DeviceIdentity(BaseName name) : this(name, Def.DevType, DeviceID.Generate(Def.DevType)) => Log.Event(new StackFrame(true));
		public DeviceIdentity(string name) : this(new(name), Def.DevType, DeviceID.Generate(Def.DevType)) => Log.Event(new StackFrame(true));
	}
}
