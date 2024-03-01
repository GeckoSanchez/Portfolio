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
	public class BaseType<TEnum> : Base<TEnum> where TEnum : struct, Enum
	{
		[JsonProperty]
		public new TEnum Value => base.Value;

		[JsonConstructor, MainConstructor]
		public BaseType(TEnum value) : base(Def<TEnum>.Value)
		{
			var sf = new StackFrame(true);
			Log.Event(sf);

			try
			{
				if (typeof(TEnum) != typeof(BlockType) && typeof(TEnum) != typeof(DeviceType) &&
						typeof(TEnum) != typeof(ExType) && typeof(TEnum) != typeof(LinkType) &&
						typeof(TEnum) != typeof(LogType) && typeof(TEnum) != typeof(Months) &&
						typeof(TEnum) != typeof(ObjectType) && typeof(TEnum) != typeof(PageType) &&
						typeof(TEnum) != typeof(PlatformType) && typeof(TEnum) != typeof(UserType))
					throw new Exception($"The given value {Format<TEnum>.ExcValue(value)} (Type: {typeof(TEnum)}) does not have one of the valid types");
				else
					base.Value = value;
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
