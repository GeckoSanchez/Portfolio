namespace ZData00.Bases
{
	using System.Diagnostics;
	using Actions;
	using Attributes;
	using Enums;
	using Exceptions;
	using Identities;
	using IDs;
	using Newtonsoft.Json;
	using Values;

	[JsonObject(MemberSerialization.OptIn, ItemNullValueHandling = NullValueHandling.Ignore)]
	public class BaseDevice : Base<DeviceIdentity>
	{
		[JsonProperty]
		public DeviceIdentity Identity => Value;

		[JsonProperty("MAC address")]
		public MACAddress MACAddress { get; protected init; }

		[JsonConstructor, MainConstructor]
		protected BaseDevice(DeviceIdentity value, MACAddress macaddress) : base(value)
		{
			var sf = new StackFrame(true);
			Log.Event(sf);

			try
			{
				MACAddress = macaddress;
			}
			catch (Exception ex)
			{
				throw new DeviceException(ex, sf);
			}
		}

		public BaseDevice(BaseName name, BaseType<DeviceType> type, DeviceID id) : this(new DeviceIdentity(name, type, id), new()) => Log.Event(new StackFrame(true));
		public BaseDevice(BaseName name, BaseType<DeviceType> type) : this(new DeviceIdentity(name, type, DeviceID.Generate(type)), new()) => Log.Event(new StackFrame(true));
	}
}
