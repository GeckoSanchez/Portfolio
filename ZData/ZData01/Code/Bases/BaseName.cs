namespace ZData01.Bases
{
	using System.Diagnostics;
	using Actions;
	using Attributes;
	using Exceptions;
	using MessagePack;
	using Newtonsoft.Json;

	[JsonObject(MemberSerialization.OptIn)]
	public class BaseName<TEnum> : Base<string> where TEnum : struct, Enum
	{
		[JsonProperty]
		public new string Value => base.Value;

		/// <summary>
		/// The primary constructor for the <see cref="BaseName{TEnum}"/> class
		/// </summary>
		/// <param name="value">The given value</param>
		/// <exception cref="NameException"/>
		[JsonConstructor, MainConstructor]
		public BaseName(string value) : base("")
		{
			var sf = new StackFrame(true);
			Log.Event(sf);

			try
			{
				if (Enum.GetNames<TEnum>().Any(e => value.StartsWith(e) && value.EndsWith(e)))
					throw new Exception($"The given name {Format.ExcValue(value)} is not valid, since it either starts or ends with the following keyword {Format.ExcValue(Enum.GetNames<TEnum>().First(e => value.StartsWith(e) && value.EndsWith(e)))}");
				else
					base.Value = value;
			}
			catch (Exception ex)
			{
				throw new NameException(ex, sf);
			}
			finally
			{
				if (base.Value == "")
					base.Value = Def.Name;
			}
		}

		/// <summary>
		/// Constructor for the <see cref="BaseName{TEnum}"/> class
		/// </summary>
		/// <inheritdoc cref="BaseName{TEnum}(string)"/>
		public BaseName(BaseName<TEnum> value) : this("")
		{
			var sf = new StackFrame(true);
			Log.Event(sf);

			try
			{
				if (Enum.GetNames<TEnum>().Any(e => value.Value.StartsWith(e) && value.Value.EndsWith(e)))
					throw new Exception($"The given name {Format.ExcValue(value.Value)} is not valid, since it either starts or ends with the following keyword {Format.ExcValue(Enum.GetNames<TEnum>().First(e => value.Value.StartsWith(e) && value.Value.EndsWith(e)))}");
				else
					base.Value = value.Value;
			}
			catch (Exception ex)
			{
				throw new NameException(ex, sf);
			}
			finally
			{
				if (base.Value == "")
					base.Value = Def.Name;
			}
		}

		public static implicit operator BaseName<TEnum>(string v) => new(v);

		/// <inheritdoc cref="Base{String}.ToJSON(Formatting)"/>
		/// <exception cref="NameException"/>
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
				throw new NameException(ex, sf);
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

		/// <inheritdoc cref="Base{String}.ToMessagePack(MessagePackSerializerOptions?)"/>
		/// <exception cref="NameException"/>
		public override byte[] ToMessagePack(MessagePackSerializerOptions? opts = null)
		{
			var sf = new StackFrame(true);
			Log.Event(sf);

			byte[]? Out = null;

			try
			{
				Out = MessagePackSerializer.Serialize(Value, opts);
			}
			catch (MessagePackSerializationException ex)
			{
				throw new MessagePackSerializationException($"This data could not be serialized into the MessagePack format", ex);
			}
			catch (Exception ex)
			{
				throw new BaseException(ex, sf);
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
}
