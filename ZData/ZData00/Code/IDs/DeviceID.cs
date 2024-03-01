namespace ZData00.IDs
{
	using System.Diagnostics;
	using Actions;
	using Attributes;
	using Bases;
	using Enums;
	using Exceptions;
	using Newtonsoft.Json;

	[JsonObject(MemberSerialization.OptIn)]
	public class DeviceID : BaseID<DeviceType>
	{
		[JsonConstructor, MainConstructor]
		protected DeviceID(ulong value) : base(value) => Log.Event(new StackFrame(true));
		public DeviceID(DeviceType type) : base(type) => Log.Event(new StackFrame(true));
		public DeviceID(DeviceType type, ulong id) : base(type, id) => Log.Event(new StackFrame(true));

		public static implicit operator DeviceID(DeviceType v) => new(v);

		/// <inheritdoc cref="BaseID{DeviceType}.Generate(DeviceType)"/>
		public new static DeviceID Generate(DeviceType type)
		{
			var sf = new StackFrame(true);
			Log.Event(sf);

			DeviceID? Out = null;

			try
			{
				Out = new(type);
			}
			catch (Exception ex)
			{
				throw new IDException(ex, sf);
			}
			finally
			{
				Out ??= Def.DevType;
			}

			return Out;
		}

		public new static DeviceID Generate(BaseType<DeviceType> type)
		{
			var sf = new StackFrame(true);
			Log.Event(sf);

			DeviceID? Out = null;

			try
			{
				Out = new(type.Value);
			}
			catch (Exception ex)
			{
				throw new IDException(ex, sf);
			}
			finally
			{
				Out ??= Def.DevType;
			}

			return Out;
		}
	}
}
