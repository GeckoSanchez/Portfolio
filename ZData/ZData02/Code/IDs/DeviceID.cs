namespace ZData02.IDs
{
	using System.Diagnostics;
	using System.Diagnostics.CodeAnalysis;
	using System.Globalization;
	using Actions;
	using Attributes;
	using Bases;
	using Enums;
	using Exceptions;
	using Newtonsoft.Json;
	using Types;

	[JsonObject(MemberSerialization.OptIn)]
	public class DeviceID : BaseID<DeviceKind>
	{
		/// <summary>
		/// Private primary constructor for the <see cref="DeviceID"/> class
		/// </summary>
		/// <param name="data">The ID data</param>
		/// <exception cref="IDException"/>
		[JsonConstructor, MainConstructor]
		private DeviceID([NotNull] UInt128 data) : base(data) => Log.Event(new StackFrame(true));

		/// <summary>
		/// Empty constructor for the <see cref="DeviceID"/> class
		/// </summary>
		/// <exception cref="IDException"/>
		[EmptyConstructor]
		public DeviceID() : this(UInt128.MinValue)
		{
			var sf = new StackFrame(true);
			Log.Event(sf);

			try
			{
				Data = UInt128.Parse($"{Def.DeviceKind.GetType().GetHashCode():X8}{Def.DeviceKind.GetHashCode():X8}{Random.Shared.NextInt64():X16}", NumberStyles.HexNumber);
			}
			catch (Exception ex)
			{
				throw new IDException(ex, sf);
			}
		}

		/// <summary>
		/// Constructor for the <see cref="DeviceID"/>
		/// </summary>
		/// <param name="type">The <see cref="DeviceKind"/> type data</param>
		/// <param name="subID">The sub-ID</param>
		/// <exception cref="IDException"/>
		public DeviceID(DeviceKind type, ulong subID) : this(UInt128.MinValue)
		{
			var sf = new StackFrame(true);
			Log.Event(sf);

			try
			{
				Data = UInt128.Parse($"{type.GetType().GetHashCode():X8}{type.GetHashCode():X8}{subID:X16}", NumberStyles.HexNumber);
			}
			catch (Exception ex)
			{
				throw new IDException(ex, sf);
			}
		}

		/// <inheritdoc cref="DeviceID(DeviceKind, ulong)"/>
		public DeviceID(DeviceType type, ulong subID) : this(UInt128.MinValue)
		{
			var sf = new StackFrame(true);
			Log.Event(sf);

			try
			{
				Data = UInt128.Parse($"{type.GetType().GetHashCode():X8}{type.GetHashCode():X8}{subID:X16}", NumberStyles.HexNumber);
			}
			catch (Exception ex)
			{
				throw new IDException(ex, sf);
			}
		}

		/// <inheritdoc cref="DeviceID(DeviceKind, ulong)"/>
		public DeviceID(DeviceKind type) : this(UInt128.MinValue)
		{
			var sf = new StackFrame(true);
			Log.Event(sf);

			try
			{
				Data = UInt128.Parse($"{type.GetType().GetHashCode():X8}{type.GetHashCode():X8}{Random.Shared.NextInt64():X16}", NumberStyles.HexNumber);
			}
			catch (Exception ex)
			{
				throw new IDException(ex, sf);
			}
		}

		/// <inheritdoc cref="DeviceID(DeviceKind, ulong)"/>
		public DeviceID(DeviceType type) : this(UInt128.MinValue)
		{
			var sf = new StackFrame(true);
			Log.Event(sf);

			try
			{
				Data = UInt128.Parse($"{type.GetType().GetHashCode():X8}{type.GetHashCode():X8}{Random.Shared.NextInt64():X16}", NumberStyles.HexNumber);
			}
			catch (Exception ex)
			{
				throw new IDException(ex, sf);
			}
		}
	}
}
