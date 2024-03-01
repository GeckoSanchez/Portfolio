namespace ZData01.Identities
{
	using System.Diagnostics;
	using Actions;
	using Attributes;
	using Bases;
	using Enums;
	using Exceptions;
	using IDs;
	using Newtonsoft.Json;

	[JsonObject(MemberSerialization.OptIn)]
	public class DeviceIdentity : BaseIdentity<DeviceType>
	{
		[JsonProperty]
		public new DeviceID ID { get => new(base.ID); protected set => Value = value; }
		
		/// <summary>
		/// Primary constructor for the <see cref="DeviceIdentity"/> class
		/// </summary>
		/// <param name="name">The given name</param>
		/// <param name="type">The given <see cref="DeviceType"/></param>
		/// <param name="id">The given ID</param>
		/// <exception cref="IdentityException"/>
		[JsonConstructor, MainConstructor]
		public DeviceIdentity(BaseName<DeviceType> name, BaseType<DeviceType> type, DeviceID id) : base(Def.Name, Def.DvcType, new DeviceID(0))
		{
			var sf = new StackFrame(true);
			Log.Event(sf);

			try
			{
				Name = name;
				Type = type;
				ID = id;
			}
			catch (Exception ex)
			{
				throw new IdentityException(ex, sf);
			}
		}

		/// <summary>
		/// Constructor for the <see cref="DeviceIdentity"/> class
		/// </summary>
		/// <inheritdoc cref="DeviceIdentity(BaseName{DeviceType},BaseType{DeviceType},DeviceID)"/>
		public DeviceIdentity(BaseName<DeviceType> name, DeviceType type, DeviceID id) : base(Def.Name, Def.DvcType, new DeviceID(0))
		{
			var sf = new StackFrame(true);
			Log.Event(sf);

			try
			{
				Name = name;
				Type = type;
				ID = id;
			}
			catch (Exception ex)
			{
				throw new IdentityException(ex, sf);
			}
		}

		/// <inheritdoc cref="DeviceIdentity(BaseName{DeviceType},DeviceType,DeviceID)"/>
		public DeviceIdentity(BaseName<DeviceType> name, BaseType<DeviceType> type) : base(Def.Name, Def.DvcType, new DeviceID(0))
		{
			var sf = new StackFrame(true);
			Log.Event(sf);

			try
			{
				Name = name;
				Type = type;
				ID = new DeviceID();
			}
			catch (Exception ex)
			{
				throw new IdentityException(ex, sf);
			}
		}

		/// <inheritdoc cref="DeviceIdentity(BaseName{DeviceType},DeviceType,DeviceID)"/>
		public DeviceIdentity(BaseName<DeviceType> name, DeviceType type) : base(Def.Name, Def.DvcType, new DeviceID(0))
		{
			var sf = new StackFrame(true);
			Log.Event(sf);

			try
			{
				Name = name;
				Type = type;
				ID = new DeviceID();
			}
			catch (Exception ex)
			{
				throw new IdentityException(ex, sf);
			}
		}

		/// <inheritdoc cref="DeviceIdentity(BaseName{DeviceType},DeviceType,DeviceID)"/>
		public DeviceIdentity(BaseName<DeviceType> name) : base(Def.Name, Def.DvcType, new DeviceID(0))
		{
			var sf = new StackFrame(true);
			Log.Event(sf);

			try
			{
				Name = name;
				Type = Def.DvcType;
				ID = new DeviceID();
			}
			catch (Exception ex)
			{
				throw new IdentityException(ex, sf);
			}
		}

		/// <inheritdoc cref="DeviceIdentity(BaseName{DeviceType},DeviceType,DeviceID)"/>
		public DeviceIdentity(string name) : base(Def.Name, Def.DvcType, new DeviceID(0))
		{
			var sf = new StackFrame(true);
			Log.Event(sf);

			try
			{
				Name = name;
				Type = Def.DvcType;
				ID = new DeviceID();
			}
			catch (Exception ex)
			{
				throw new IdentityException(ex, sf);
			}
		}

		public static implicit operator DeviceIdentity(string v) => new(v);
		public static implicit operator DeviceIdentity(BaseName<DeviceType> v) => new(v);
	}
}
