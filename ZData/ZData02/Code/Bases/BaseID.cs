namespace ZData02.Bases
{
	using System.Diagnostics;
	using System.Diagnostics.CodeAnalysis;
	using System.Globalization;
	using System.Xml.Linq;
	using Actions;
	using Attributes;
	using Enums;
	using Exceptions;
	using MessagePack;
	using Newtonsoft.Json;
	using ZData02.IDs;

	[JsonObject(MemberSerialization.OptIn)]
	public class BaseID<TEnum> : BaseData<UInt128> where TEnum : struct, Enum
	{
		/// <summary>
		/// The base <see cref="UInt128"/> data for this datum
		/// </summary>
		[JsonProperty]
		public new UInt128 Data { get => base.Data; protected init => base.Data = value; }

		/// <summary>
		/// Protected primary constructor for the <see cref="BaseID{TEnum}"/>
		/// </summary>
		/// <param name="data">The given data</param>
		[JsonConstructor, MainConstructor]
		protected BaseID([NotNull] UInt128 data) : base(data) => Log.Event(new StackFrame(true));

		/// <summary>
		/// Constructor for the <see cref="BaseID{TEnum}"/> class
		/// </summary>
		/// <param name="type">The <see cref="TEnum"/> type</param>
		/// <param name="subID">The sub-ID</param>
		/// <exception cref="IDException"/>
		public BaseID(TEnum type, ulong subID) : this(UInt128.MinValue)
		{
			var sf = new StackFrame(true);
			Log.Event(sf);

			try
			{
				if (type.GetType() != typeof(DeviceKind) && type.GetType() != typeof(LinkKind) &&
						type.GetType() != typeof(ObjectKind) && type.GetType() != typeof(PlatformKind))
					throw new Exception($"The type for this ID {Format.ExcValue(typeof(TEnum).Name)} is not valid");
				else
					Data = UInt128.Parse($"{type.GetType().GetHashCode():X8}{type.GetHashCode():X8}{subID:X16}", NumberStyles.HexNumber);
			}
			catch (Exception ex)
			{
				throw new IDException(ex, sf);
			}
		}

		/// <inheritdoc cref="BaseID{TEnum}(TEnum,ulong)"/>
		public BaseID(BaseType<TEnum> type, ulong subID) : this(UInt128.MinValue)
		{
			var sf = new StackFrame(true);
			Log.Event(sf);

			try
			{
				if (type.Data.GetType() != typeof(DeviceKind) && type.Data.GetType() != typeof(LinkKind) &&
						type.Data.GetType() != typeof(ObjectKind) && type.Data.GetType() != typeof(PlatformKind))
					throw new Exception($"The type for this ID {Format.ExcValue(typeof(TEnum).Name)} is not valid");
				else
					Data = UInt128.Parse($"{type.GetType().GetHashCode():X8}{type.GetHashCode():X8}{subID:X16}", NumberStyles.HexNumber);
			}
			catch (Exception ex)
			{
				throw new IDException(ex, sf);
			}
		}

		/// <inheritdoc cref="BaseID{TEnum}(TEnum,ulong)"/>
		public BaseID(TEnum type) : this(UInt128.MinValue)
		{
			var sf = new StackFrame(true);
			Log.Event(sf);

			try
			{
				Data = new BaseID<TEnum>(type, (ulong)Random.Shared.NextInt64()).Data;
			}
			catch (IDException)
			{
				throw;
			}
			catch (Exception ex)
			{
				throw new IDException(ex, sf);
			}
		}

		/// <inheritdoc cref="BaseID{TEnum}(TEnum,ulong)"/>
		public BaseID(BaseType<TEnum> type) : this(UInt128.MinValue)
		{
			var sf = new StackFrame(true);
			Log.Event(sf);

			try
			{
				Data = new BaseID<TEnum>(type.Data, (ulong)Random.Shared.NextInt64()).Data;
			}
			catch (IDException)
			{
				throw;
			}
			catch (Exception ex)
			{
				throw new IDException(ex, sf);
			}
		}

		/// <summary>
		/// Empty constructor for the <see cref="BaseID{TEnum}"/> class
		/// </summary>
		/// <exception cref="IDException"/>
		public BaseID() : this(UInt128.MinValue)
		{
			var sf = new StackFrame(true);
			Log.Event(sf);

			try
			{
				Data = new BaseID<TEnum>(Def<TEnum>.Value, (ulong)Random.Shared.NextInt64()).Data;
			}
			catch (IDException)
			{
				throw;
			}
			catch (Exception ex)
			{
				throw new IDException(ex, sf);
			}
		}

		/// <inheritdoc cref="BaseID{TEnum}(TEnum,ulong)"/>
		public BaseID(DeviceID value) : this(value.Data) => Log.Event(new StackFrame(true));

		/// <inheritdoc cref="BaseID{TEnum}(TEnum,ulong)"/>
		public BaseID(LinkID value) : this(value.Data) => Log.Event(new StackFrame(true));

		/// <inheritdoc cref="BaseID{TEnum}(TEnum,ulong)"/>
		public BaseID(ObjectID value) : this(value.Data) => Log.Event(new StackFrame(true));

		/// <inheritdoc cref="BaseID{TEnum}(TEnum,ulong)"/>
		public BaseID(PlatformID value) : this(value.Data) => Log.Event(new StackFrame(true));

		/// <inheritdoc cref="BaseData{TData}.ToString"/>
		/// <exception cref="IDException"/>
		public override string ToString()
		{
			var sf = new StackFrame(true);
			Log.Event(sf);

			string? Out = null;

			try
			{
				Out = $"{Data:X32}";
			}
			catch (Exception ex)
			{
				throw new IDException(ex, sf);
			}
			finally
			{
				try
				{
					Out ??= base.ToString();
				}
				catch (Exception)
				{
					Out = "";
				}
			}

			return Out;
		}

		/// <inheritdoc cref="BaseData{TData}.ToMessagePack(MessagePackSerializerOptions?)"/>
		/// <exception cref="IDException"/>
		public override byte[] ToMessagePack(MessagePackSerializerOptions? opts = null)
		{
			var sf = new StackFrame(true);
			Log.Event(sf);

			byte[] Out;
			string s;
			ulong top, btm;

			try
			{
				s = $"{Data:X32}";
				top = ulong.Parse(s[..16], NumberStyles.HexNumber);
				btm = ulong.Parse(s[16..], NumberStyles.HexNumber);

				Out = [.. MessagePackSerializer.Serialize(top, opts), .. MessagePackSerializer.Serialize(btm, opts)];
			}
			catch (MessagePackSerializationException ex)
			{
				throw new IDException(new Exception($"This data could not be serialized into the MessagePack format", ex), sf);
			}
			catch (Exception ex)
			{
				throw new IDException(ex, sf);
			}

			return Out;
		}
	}
}
