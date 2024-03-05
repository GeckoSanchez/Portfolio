namespace ZData02.Bases
{
	using System.Diagnostics;
	using System.Diagnostics.CodeAnalysis;
	using Actions;
	using Attributes;
	using Exceptions;
	using MessagePack;
	using Microsoft.AspNetCore.Mvc.ApplicationModels;
	using Newtonsoft.Json;

	[JsonObject(MemberSerialization.OptIn)]
	public class BaseName<TEnum> : BaseData<string> where TEnum : struct, Enum
	{
		/// <summary>
		/// The base <see cref="string"/> data for this datum
		/// </summary>
		[JsonProperty]
		public new string Data { get => base.Data; protected set => base.Data = value; }

		/// <summary>
		/// Primary constructor for the <see cref="BaseName{TEnum}"/> class
		/// </summary>
		/// <param name="name">The given name</param>
		/// <exception cref="NameException"/>
		[JsonConstructor, MainConstructor]
		public BaseName([NotNull] string name) : base("")
		{
			var sf = new StackFrame(true);
			Log.Event(sf);

			try
			{
				if (Enum.GetNames<TEnum>().Any(name.Contains))
					throw new NameException($"The given name {Format.ExcValue(name)} is not valid, since it contains a reserved keyword: {Enum.GetNames<TEnum>().First(name.Contains)}", sf);
				else
					Data = name;
			}
			catch (NameException)
			{
				throw;
			}
			catch (Exception ex)
			{
				throw new NameException(ex, sf);
			}
			finally
			{
				if (Data == "")
					Data = Def.Name;
			}
		}

		/// <summary>
		/// Constructor for the <see cref="BaseName{TEnum}"/> class
		/// </summary>
		/// <param name="data">The given <see cref="BaseName{TEnum}"/> value</param>
		/// <exception cref="NameException"/>
		public BaseName(BaseName<TEnum> data) : this(data.Data) => Log.Event(new StackFrame(true));

		public static implicit operator BaseName<TEnum>(string v) => new(v);

		/// <inheritdoc cref="BaseData{TData}.ToMessagePack(MessagePackSerializerOptions?)"/>
		/// <exception cref="NameException"/>
		public override byte[] ToMessagePack(MessagePackSerializerOptions? opts = null)
		{
			var sf = new StackFrame(true);
			Log.Event(sf);

			byte[]? Out = null;

			try
			{
				Out = MessagePackSerializer.Serialize(Data);
			}
			catch (MessagePackSerializationException ex)
			{
				throw new NameException(new Exception($"This data could not be serialized into the MessagePack format", ex), sf);
			}
			catch (Exception ex)
			{
				throw new NameException(ex, sf);
			}
			finally
			{
				try
				{
					Out ??= base.ToMessagePack(opts);
				}
				catch (Exception)
				{
					Out = Def.MsgPack;
				}
			}

			return Out;
		}
	}
}
