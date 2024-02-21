namespace ZData00.Bases
{
	using System.Diagnostics;
	using Actions;
	using Attributes;
	using Enums;
	using Exceptions;
	using MessagePack;
	using Newtonsoft.Json;

	[JsonObject(MemberSerialization.OptIn)]
	public class BaseType : Base<Enum>
	{
		[JsonProperty]
		public new Enum Value { get => base.Value; protected set => base.Value = value; }

		[JsonConstructor, PrimaryConstructor]
		public BaseType(Enum value) : base(value)
		{
			var sf = new StackFrame(true);
			Log.Event(sf);

			try
			{
				if (value.GetType() != typeof(BlockType) &&
						value.GetType() != typeof(DeviceType) &&
						value.GetType() != typeof(ExType) &&
						value.GetType() != typeof(LinkType) &&
						value.GetType() != typeof(LogType) &&
						value.GetType() != typeof(Months) &&
						value.GetType() != typeof(ObjectType) &&
						value.GetType() != typeof(PageType) &&
						value.GetType() != typeof(PlatformType) &&
						value.GetType() != typeof(UserType))
					throw new Exception($"The given value {Format<Enum>.ExcValue(value)} (Type: {value.GetType().Name}) does not have one of the valid types");
				else
					Value = value;
			}
			catch (Exception ex)
			{
				throw new TypeException(ex, sf);
			}
		}

		public BaseType(BaseType<BlockType> value) : this((Enum)value.Value) => Log.Event(new StackFrame(true));
		public BaseType(BaseType<DeviceType> value) : this((Enum)value.Value) => Log.Event(new StackFrame(true));
		public BaseType(BaseType<ExType> value) : this((Enum)value.Value) => Log.Event(new StackFrame(true));
		public BaseType(BaseType<LinkType> value) : this((Enum)value.Value) => Log.Event(new StackFrame(true));
		public BaseType(BaseType<LogType> value) : this((Enum)value.Value) => Log.Event(new StackFrame(true));
		public BaseType(BaseType<Months> value) : this((Enum)value.Value) => Log.Event(new StackFrame(true));
		public BaseType(BaseType<PageType> value) : this((Enum)value.Value) => Log.Event(new StackFrame(true));
		public BaseType(BaseType<PlatformType> value) : this((Enum)value.Value) => Log.Event(new StackFrame(true));
		public BaseType(BaseType<UserType> value) : this((Enum)value.Value) => Log.Event(new StackFrame(true));

		public static implicit operator BaseType(BlockType value) => new((Enum)value);
		public static implicit operator BaseType(DeviceType value) => new((Enum)value);
		public static implicit operator BaseType(ExType value) => new((Enum)value);
		public static implicit operator BaseType(LinkType value) => new((Enum)value);
		public static implicit operator BaseType(LogType value) => new((Enum)value);
		public static implicit operator BaseType(Months value) => new((Enum)value);
		public static implicit operator BaseType(PageType value) => new((Enum)value);
		public static implicit operator BaseType(PlatformType value) => new((Enum)value);
		public static implicit operator BaseType(UserType value) => new((Enum)value);

		/// <inheritdoc cref="Base{Enum}.ToMessagePack(MessagePackSerializerOptions?)"/>
		/// <exception cref="TypeException"/>
		public override byte[] ToMessagePack(MessagePackSerializerOptions? opts = null)
		{
			var sf = new StackFrame(true);
			Log.Event(sf);

			byte[]? Out = null;

			try
			{
				Out = MessagePackSerializer.Serialize($"{Value}", opts);
			}
			catch (MessagePackSerializationException)
			{
				throw new TypeException($"This data could not serialized into the MessagePack format", sf);
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
					Out = [];
				}
			}

			return Out;
		}
	}

	[JsonObject(MemberSerialization.OptIn)]
	public class BaseType<TEnum> : BaseType where TEnum : struct, Enum
	{
		[JsonProperty]
		public new TEnum Value { get => (TEnum)base.Value; protected set => base.Value = value; }

		[JsonConstructor, PrimaryConstructor]
		public BaseType(TEnum value) : base((TEnum)default!)
		{
			var sf = new StackFrame(true);
			Log.Event(sf);

			try
			{
				if (typeof(TEnum) != typeof(BlockType) &&
						typeof(TEnum) != typeof(DeviceType) &&
						typeof(TEnum) != typeof(ExType) &&
						typeof(TEnum) != typeof(LinkType) &&
						typeof(TEnum) != typeof(LogType) &&
						typeof(TEnum) != typeof(Months) &&
						typeof(TEnum) != typeof(ObjectType) &&
						typeof(TEnum) != typeof(PageType) &&
						typeof(TEnum) != typeof(PlatformType) &&
						typeof(TEnum) != typeof(UserType))
					throw new Exception($"The given value {Format<TEnum>.ExcValue(value)} (Type: {typeof(TEnum)}) does not have one of the valid types");
				else
					Value = value;
			}
			catch (Exception ex)
			{
				throw new TypeException(ex, sf);
			}
		}

		public static implicit operator BaseType<TEnum>(TEnum v) => new(v);

		/// <inheritdoc cref="BaseType.ToMessagePack(MessagePackSerializerOptions?)"/>
		public override byte[] ToMessagePack(MessagePackSerializerOptions? opts = null)
		{
			var sf = new StackFrame(true);
			Log.Event(sf);

			byte[]? Out = null;

			try
			{
				Out = MessagePackSerializer.Serialize(Enum.GetName(Value) ?? "", opts);
			}
			catch (InvalidOperationException)
			{
				throw new TypeException($"This name for the given type could not retrieved", sf);
			}
			catch (MessagePackSerializationException)
			{
				throw new TypeException($"This data could not serialized into the MessagePack format", sf);
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
					Out = [];
				}
			}

			return Out;
		}
	}
}
