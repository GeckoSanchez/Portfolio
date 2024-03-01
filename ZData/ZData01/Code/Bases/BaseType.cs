namespace ZData01.Bases
{
	using System.Diagnostics;
	using Actions;
	using Attributes;
	using Enums;
	using Exceptions;
	using MessagePack;
	using Newtonsoft.Json;

	[JsonObject(MemberSerialization.OptIn)]
	public class BaseType : Base<Enum>, IEquatable<BaseType>
	{
		/// <summary>
		/// Base <see cref="Enum"/> value
		/// </summary>
		[JsonProperty]
		public new Enum Value { get => base.Value; set => base.Value = value; }

		[JsonConstructor, MainConstructor]
		public BaseType(Enum value) : base(value) => Log.Event(new StackFrame(true));
		public BaseType(BaseType value) : base(value.Value) => Log.Event(new StackFrame(true));

		public static implicit operator BaseType(Enum v) => new(v);

		public override bool Equals(object? obj)
		{
			var sf = new StackFrame(true);
			Log.Event(sf);

			bool? Out;

			try
			{
				Out = Equals(obj as BaseType);
			}
			catch (Exception ex)
			{
				throw new TypeException(ex, sf);
			}

			return Out ?? false;
		}

		public bool Equals(BaseType? other)
		{
			var sf = new StackFrame(true);
			Log.Event(sf);

			bool? Out;

			try
			{
				Out = EqualityComparer<Enum>.Default.Equals(Value, base.Value);
			}
			catch (Exception ex)
			{
				throw new TypeException(ex, sf);
			}

			return Out ?? false;
		}

		public override int GetHashCode()
		{
			var sf = new StackFrame(true);
			Log.Event(sf);

			int? Out;

			try
			{
				Out = HashCode.Combine(Value, base.GetHashCode());
			}
			catch (BaseException)
			{
				throw;
			}
			catch (Exception ex)
			{
				throw new TypeException(ex, sf);
			}

			return Out ?? 0;
		}

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
				throw new BaseException($"This data could not be serialized into the JSON format", sf);
			}
			catch (Exception ex)
			{
				throw new BaseException(ex, sf);
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

		/// <inheritdoc cref="Base{T}.ToMessagePack(MessagePackSerializerOptions?)"/>
		/// <exception cref="TypeException"/>
		public override byte[] ToMessagePack(MessagePackSerializerOptions? opts = null)
		{
			var sf = new StackFrame(true);
			Log.Event(sf);

			byte[]? Out = null;

			try
			{
				Out = MessagePackSerializer.Serialize(Value.ToString(), opts);
			}
			catch (MessagePackSerializationException)
			{
				throw new TypeException($"This data could not be serialized into the MessagePack format", sf);
			}
			catch (Exception ex)
			{
				throw new TypeException(ex, sf);
			}
			finally
			{
				try
				{
					Out ??= base.ToMessagePack(opts);
				}
				catch (BaseException)
				{
					Out = Def.MsgPack;
				}
			}

			return Out;
		}
	}

	[JsonObject(MemberSerialization.OptIn)]
	public class BaseType<TEnum> : BaseType, IEquatable<BaseType<TEnum>> where TEnum : struct, Enum
	{
		/// <summary>
		/// Base <see cref="TEnum"/> value
		/// </summary>
		[JsonProperty]
		public new TEnum Value { get => (TEnum)base.Value; set => base.Value = value; }

		public BaseType(TEnum value) : base(value) => Log.Event(new StackFrame(true));
		public BaseType(BaseType value) : base(value.Value)
		{
			var sf = new StackFrame(true);
			Log.Event(sf);

			try
			{
				if (value.GetType() != typeof(ObjectType))
				{
					Value = Def<TEnum>.Value;
					throw new Exception($"The given type {Format<BaseType>.ExcValue(value)} is not valid for this type");
				}
			}
			catch (Exception ex)
			{
				throw new TypeException(ex, sf);
			}
		}

		public BaseType(BaseType<TEnum> value) : base(value.Value) => Log.Event(new StackFrame(true));

		public static implicit operator BaseType<TEnum>(TEnum v) => new(v);

		/// <inheritdoc cref="object.Equals(object?)"/>
		/// <exception cref="TypeException"/>
		public bool Equals(BaseType<TEnum>? other)
		{
			var sf = new StackFrame(true);
			Log.Event(sf);

			bool? Out;

			try
			{
				Out = other is not null && EqualityComparer<TEnum>.Default.Equals(other.Value, Value);
			}
			catch (Exception ex)
			{
				throw new TypeException(ex, sf);
			}

			return Out ?? false;
		}

		/// <inheritdoc cref="object.Equals(object?)"/>
		/// <exception cref="TypeException"/>
		public override bool Equals(object? obj)
		{
			var sf = new StackFrame(true);
			Log.Event(sf);

			bool? Out = null;

			try
			{
				Out = Equals(obj as BaseType<TEnum>);
			}
			catch (Exception ex)
			{
				throw new TypeException(ex, sf);
			}
			finally
			{
				try
				{
					Out ??= Equals(obj as BaseType<TEnum>);
				}
				catch (BaseException) { }
			}

			return Out ?? false;
		}

		/// <inheritdoc cref="Base{TEnum}.GetHashCode"/>
		/// <exception cref="TypeException"/>
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
				throw new TypeException(ex, sf);
			}
			finally
			{
				try
				{
					Out ??= base.GetHashCode();
				}
				catch (BaseException) { }
			}

			return Out ?? 0;
		}
	}
}
