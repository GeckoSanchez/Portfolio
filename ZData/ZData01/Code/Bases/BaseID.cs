namespace ZData01.Bases
{
	using System.Diagnostics;
	using System.Globalization;
	using System.Xml.Linq;
	using Actions;
	using Attributes;
	using Exceptions;
	using MessagePack;
	using Newtonsoft.Json;

	[JsonObject(MemberSerialization.OptIn)]
	public class BaseID : Base<UInt128>, IEquatable<BaseID>
	{
		/// <summary>
		/// Base <see cref="UInt128"/> value
		/// </summary>
		[JsonProperty]
		public new UInt128 Value { get => base.Value; set => base.Value = value; }

		/// <summary>
		/// Primary constructor for the <see cref="BaseID"/> class
		/// </summary>
		/// <param name="value">The given ID</param>
		/// <exception cref="IDException"/>
		[JsonConstructor, MainConstructor]
		protected BaseID(UInt128 value) : base(value) => Log.Event(new StackFrame(true));

		/// <inheritdoc cref="Base{UInt128}.Equals(object?)"/>
		/// <exception cref="IDException"/>
		public override bool Equals(object? obj)
		{
			var sf = new StackFrame(true);
			Log.Event(sf);

			bool? Out = null;

			try
			{
				Out = Equals(obj as Base<UInt128>);
			}
			catch (BaseException)
			{
				throw;
			}
			catch (Exception ex)
			{
				throw new IDException(ex, sf);
			}
			finally
			{
				try
				{
					Out ??= base.Equals(obj);
				}
				catch (BaseException) { }
			}

			return Out ?? false;
		}

		/// <inheritdoc cref="Base{T}.Equals(Base{T}?)"/>
		/// <exception cref="IDException"/>
		public bool Equals(BaseID? other)
		{
			var sf = new StackFrame(true);
			Log.Event(sf);

			bool? Out = null;

			try
			{
				Out = other is not null && EqualityComparer<UInt128>.Default.Equals(Value, other.Value);
			}
			catch (BaseException)
			{
				throw;
			}
			catch (Exception ex)
			{
				throw new IDException(ex, sf);
			}
			finally
			{
				try
				{
					Out ??= base.Equals(other);
				}
				catch (BaseException) { }
			}

			return Out ?? false;
		}

		/// <inheritdoc cref="Base{T}.Equals(Base{T}?)"/>
		/// <exception cref="IDException"/>
		public bool Equals(UInt128? other)
		{
			var sf = new StackFrame(true);
			Log.Event(sf);

			bool? Out = null;

			try
			{
				Out = other is not null && EqualityComparer<UInt128>.Default.Equals(other);
			}
			catch (BaseException)
			{
				throw;
			}
			catch (Exception ex)
			{
				throw new IDException(ex, sf);
			}
			finally
			{
				try
				{
					Out ??= base.Equals(other);
				}
				catch (BaseException) { }
			}

			return Out ?? false;
		}

		/// <inheritdoc cref="Base{T}.GetHashCode"/>
		/// <exception cref="IDException"/>
		public override int GetHashCode()
		{
			var sf = new StackFrame(true);
			Log.Event(sf);

			int? Out = null;

			try
			{
				Out = HashCode.Combine(Value, base.GetHashCode());
			}
			catch (Exception ex)
			{
				throw new BaseException(ex, sf);
			}
			finally
			{
				try
				{
					Out ??= base.GetHashCode();
				}
				catch (Exception) { }
			}

			return Out ?? 0;
		}

		/// <inheritdoc cref="Base{UInt128}.ToJSON(Formatting)"/>
		/// <exception cref="IDException"/>
		public override string ToJSON(Formatting formatting = Formatting.Indented)
		{
			var sf = new StackFrame(true);
			Log.Event(sf);

			string? Out = null;

			try
			{
				Out = JsonConvert.SerializeObject(this, formatting);
			}
			catch (JsonException)
			{
				throw new IDException($"This data could not be serialized into the JSON format", sf);
			}
			catch (Exception ex)
			{
				throw new IDException(ex, sf);
			}
			finally
			{
				try
				{
					Out ??= base.ToJSON(formatting);
				}
				catch (BaseException)
				{
					Out = Def.JSON;
				}
			}

			return Out;
		}

		/// <inheritdoc cref="Base{T}.ToString"/>
		/// <exception cref="IDException"/>
		public override string ToString()
		{
			var sf = new StackFrame(true);
			Log.Event(sf);

			string? Out = null;

			try
			{
				Out = $"{Value:X32}";
			}
			catch (Exception ex)
			{
				throw new BaseException(ex, sf);
			}
			finally
			{
				try
				{
					Out ??= base.ToString();
				}
				catch (BaseException)
				{
					Out = "";
				}
			}

			return Out;
		}
	}

	/// <summary>
	/// A <typeparamref name="TEnum"/>-type version of an ID
	/// </summary>
	/// <typeparam name="TEnum">An <see cref="Enum"/> in the <see cref="Enums"/> namespace</typeparam>
	[JsonObject(MemberSerialization.OptIn)]
	public class BaseID<TEnum> : BaseID, IEquatable<BaseID<TEnum>?> where TEnum : struct, Enum
	{
		/// <summary>
		/// Primary constructor for the <see cref="BaseID{TEnum}"/> class
		/// </summary>
		/// <param name="value">The given ID</param>
		[JsonConstructor, MainConstructor]
		internal BaseID(UInt128 value) : base(value) => Log.Event(new StackFrame(true));

		/// <summary>
		/// Constructor for the <see cref="BaseID{TEnum}"/> class
		/// </summary>
		/// <param name="type">The given <typeparamref name="TEnum"/></param>
		/// <param name="id"></param>
		/// <exception cref="IDException"/>
		public BaseID(TEnum type, ulong id) : this(0)
		{
			var sf = new StackFrame(true);
			Log.Event(sf);

			try
			{
				var tid = Def.TopIDs[typeof(TEnum)];
				Value = UInt128.Parse($"{tid:X8}{type.GetHashCode():X8}{id:X16}", NumberStyles.HexNumber);
			}
			catch (Exception ex)
			{
				throw new IDException(ex, sf);
			}
			finally
			{
				if (Value == 0)
					Value = id;
			}
		}

		/// <inheritdoc cref="BaseID{TEnum}.BaseID(TEnum, ulong)"/>
		public BaseID(TEnum type) : this(0)
		{
			var sf = new StackFrame(true);
			Log.Event(sf);

			try
			{
				var tid = Def.TopIDs[typeof(TEnum)];
				Value = UInt128.Parse($"{tid:X8}{type.GetHashCode():X8}{Random.Shared.NextInt64() + Random.Shared.NextInt64():X16}", NumberStyles.HexNumber);
			}
			catch (Exception ex)
			{
				throw new IDException(ex, sf);
			}
			finally
			{
				if (Value == 0)
					Value = (ulong)Random.Shared.NextInt64();
			}
		}

		/// <inheritdoc cref="BaseID{TEnum}.BaseID(TEnum, ulong)"/>
		public BaseID(BaseID<TEnum> value) : this(value.Value) => Log.Event(new StackFrame(true));

		/// <inheritdoc cref="BaseID{TEnum}.BaseID(TEnum, ulong)"/>
		public BaseID(BaseID value) : this(value.Value) => Log.Event(new StackFrame(true));

		public static implicit operator BaseID<TEnum>(TEnum v) => new(v);

		/// <inheritdoc cref="Base{UInt128}.ToMessagePack(MessagePackSerializerOptions?)"/>
		/// <exception cref="IdentityException"/>
		public override byte[] ToMessagePack(MessagePackSerializerOptions? opts = null)
		{
			var sf = new StackFrame(true);
			Log.Event(sf);

			byte[]? Out = null;

			try
			{
				Out = MessagePackSerializer.Serialize($"{Value:X32}", opts);
			}
			catch (MessagePackSerializationException)
			{
				throw new IDException($"This data could not be serialized into the MessagePack format", sf);
			}
			catch (Exception ex)
			{
				throw new IDException(ex, sf);
			}
			finally
			{
				try
				{
					Out ??= base.ToMessagePack();
				}
				catch (Exception) { }
			}

			return Out;
		}

		/// <inheritdoc cref="BaseID.ToJSON(Formatting)"/>
		public override string ToJSON(Formatting formatting = Formatting.Indented)
		{
			var sf = new StackFrame(true);
			Log.Event(sf);

			string? Out = null;

			try
			{
				Out = JsonConvert.SerializeObject(this, formatting);
			}
			catch (JsonException)
			{
				throw new IDException($"This data could not be serialized into the JSON format", sf);
			}
			catch (Exception ex)
			{
				throw new IDException(ex, sf);
			}
			finally
			{
				try
				{
					Out ??= base.ToJSON(formatting);
				}
				catch (BaseException)
				{
					Out = Def.JSON;
				}
			}

			return Out;
		}

		/// <inheritdoc cref="BaseID.Equals(object?)"/>
		public override bool Equals(object? obj)
		{
			var sf = new StackFrame(true);
			Log.Event(sf);

			bool? Out = null;

			try
			{
				Out = Equals(obj as BaseID<TEnum>);
			}
			catch (Exception ex)
			{
				throw new IDException(ex, sf);
			}
			finally
			{
				try
				{
					Out ??= base.Equals(obj);
				}
				catch (Exception) { }
			}

			return Out ?? false;
		}

		/// <inheritdoc cref="BaseID.Equals(BaseID?)"/>
		public bool Equals(BaseID<TEnum>? other)
		{
			var sf = new StackFrame(true);
			Log.Event(sf);

			bool? Out = null;

			try
			{
				Out = other is not null && base.Equals(other) && Value.Equals(other.Value);
			}
			catch (Exception ex)
			{
				throw new IDException(ex, sf);
			}
			finally
			{
				try
				{
					Out ??= base.Equals(other);
				}
				catch (Exception) { }
			}

			return Out ?? false;
		}

		/// <inheritdoc cref="BaseID.GetHashCode"/>
		public override int GetHashCode()
		{
			var sf = new StackFrame(true);
			Log.Event(sf);

			int? Out = null;

			try
			{
				Out = HashCode.Combine(base.GetHashCode(), Value);
			}
			catch (Exception ex)
			{
				throw new IDException(ex, sf);
			}
			finally
			{
				try
				{
					Out ??= base.GetHashCode();
				}
				catch (Exception) { }
			}

			return Out ?? 0;
		}

		public static bool operator ==(BaseID<TEnum>? left, BaseID<TEnum>? right) => EqualityComparer<BaseID>.Default.Equals(left, right);
		public static bool operator !=(BaseID<TEnum>? left, BaseID<TEnum>? right) => !(left == right);

		public static bool operator ==(BaseID<TEnum>? left, BaseID? right) => EqualityComparer<BaseID>.Default.Equals(left, right);
		public static bool operator !=(BaseID<TEnum>? left, BaseID? right) => !(left == right);
	}
}
