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
	public class LinkID : BaseID<LinkKind>
	{
		/// <summary>
		/// Private primary constructor for the <see cref="LinkID"/> class
		/// </summary>
		/// <param name="data">The ID data</param>
		/// <exception cref="IDException"/>
		[JsonConstructor, MainConstructor]
		private LinkID([NotNull] UInt128 data) : base(data) => Log.Event(new StackFrame(true));

		/// <summary>
		/// Empty constructor for the <see cref="LinkID"/> class
		/// </summary>
		/// <exception cref="IDException"/>
		[EmptyConstructor]
		public LinkID() : this(UInt128.MinValue)
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
		/// Constructor for the <see cref="LinkID"/>
		/// </summary>
		/// <param name="type">The <see cref="LinkKind"/> type data</param>
		/// <param name="subID">The sub-ID</param>
		/// <exception cref="IDException"/>
		public LinkID(LinkKind type, ulong subID) : this(UInt128.MinValue)
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

		/// <inheritdoc cref="LinkID(DeviceKind, ulong)"/>
		public LinkID(LinkType type, ulong subID) : this(UInt128.MinValue)
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

		/// <inheritdoc cref="LinkID(DeviceKind, ulong)"/>
		public LinkID(LinkKind type) : this(UInt128.MinValue)
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

		/// <inheritdoc cref="LinkID(DeviceKind, ulong)"/>
		public LinkID(LinkType type) : this(UInt128.MinValue)
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
