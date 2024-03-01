namespace ZData01.Links
{
	using System.Diagnostics;
	using Actions;
	using Attributes;
	using Bases;
	using Enums;
	using Identities;
	using IDs;
	using Newtonsoft.Json;

	[JsonObject(MemberSerialization.OptIn)]
	public class DeviceLink : BaseLink<DeviceType, DeviceType>
	{
		[JsonConstructor, MainConstructor]
		private DeviceLink(BaseIdentity<DeviceType> parent, BaseIdentity<DeviceType> child) : base(parent, child) => Log.Event(new StackFrame(true));
		public DeviceLink(DeviceIdentity parent, DeviceIdentity child) : this(new BaseIdentity<DeviceType>(parent.Name, parent.Type.Value, parent.ID), new BaseIdentity<DeviceType>(child.Name, child.Type.Value, child.ID)) => Log.Event(new StackFrame(true));
		public DeviceLink(DeviceID parent, DeviceID child) : base(new Tuple<UInt128, UInt128>(parent.Value, child.Value)) => Log.Event(new StackFrame(true));
	}
}
