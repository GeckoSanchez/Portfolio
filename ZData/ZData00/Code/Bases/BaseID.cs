namespace ZData00.Bases
{
	using System.Diagnostics;
	using System.Globalization;
	using Actions;
	using Attributes;
	using Exceptions;
	using MessagePack;
	using Newtonsoft.Json;

	[JsonObject(MemberSerialization.OptIn)]
	public class BaseID<TEnum> : Base<ulong> where TEnum : struct, Enum
	{
		[JsonProperty]
		public new ulong Value { get => base.Value; protected set => base.Value = value; }

		[JsonConstructor, MainConstructor]
		protected BaseID(ulong value) : base(value) => Log.Event(new StackFrame(true));

		public BaseID(TEnum type, ulong id) : this(id)
		{
			var sf = new StackFrame(true);
			Log.Event(sf);

			try
			{
				int midID = 0;

				if (!Def.TopIDs.TryGetValue(typeof(TEnum), out byte topID))
					throw new Exception($"The given type {Format<string>.ExcValue($"{type.GetType().FullName}:{type}")} could not be used to create an id");

				foreach (var i in Enum.GetValues<TEnum>().Where(i => type.HasFlag(i) && i.GetHashCode() != 0))
					midID += i.GetHashCode();

				if (id > (1 << 48))
					throw new Exception($"The given id {Format<ulong>.ExcValue(Value)} was greater than its maximum value {Format<ulong>.ExcValue(1 << 48)}");

				Value = ulong.Parse($"{topID:B5}{midID % (1 << 11):B11}{id % (1 << 48):B48}", NumberStyles.BinaryNumber);
			}
			catch (Exception ex)
			{
				throw new IDException(ex, sf);
			}
		}

		public BaseID(TEnum type) : this(type, (ulong)(Random.Shared.NextInt64() % (1 << 48))) => Log.Event(new StackFrame(true));

		/// <summary>
		/// Function to generate a new version of this type of ID
		/// </summary>
		/// <param name="type">The given type for a new ID</param>
		/// <returns>An ID</returns>
		/// <exception cref="IDException"/>
		public static BaseID<TEnum> Generate(TEnum type)
		{
			var sf = new StackFrame(true);
			Log.Event(sf);

			BaseID<TEnum>? Out = null;

			try
			{
				Out = new(type);
			}
			catch (Exception ex)
			{
				throw new IDException(ex, sf);
			}
			finally
			{
				Out ??= new((TEnum)default!);
			}

			return Out;
		}

		public static BaseID<TEnum> Generate(BaseType<TEnum> type)
		{
			var sf = new StackFrame(true);
			Log.Event(sf);

			BaseID<TEnum>? Out = null;

			try
			{
				Out = new(type.Value);
			}
			catch (Exception ex)
			{
				throw new IDException(ex, sf);
			}
			finally
			{
				Out ??= new((TEnum)default!);
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
				Out = $"{Value:X16}";
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
				catch (BaseException)
				{
					Out = "";
				}
			}

			return Out;
		}

		/// <inheritdoc cref="Base{ulong}.ToJSON(Formatting)"/>
		/// <exception cref="IDException"/>
		public override string ToJSON(Formatting formatting = Formatting.None)
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
				throw new IDException($"This data could not serialized into the JSON format", sf);
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

		public override byte[] ToMessagePack(MessagePackSerializerOptions? opts = null)
		{
			var sf = new StackFrame(true);
			Log.Event(sf);

			byte[]? Out = null;

			try
			{
				Out = MessagePackSerializer.Serialize(Value, opts);
			}
			catch (MessagePackSerializationException)
			{
				throw new IDException($"This data could not serialized into the MessagePack format", sf);
			}
			catch (Exception ex)
			{
				throw new IDException(ex, sf);
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

		/// <inheritdoc cref="Base{ulong}.Dispose()"/>
		/// <exception cref="IDException"/>
		protected override void Dispose(bool disposing)
		{
			var sf = new StackFrame(true);
			Log.Event(sf);

			try
			{
				Value = default!;
				GC.Collect(GC.MaxGeneration, GCCollectionMode.Aggressive);
			}
			catch (Exception ex)
			{
				throw new IDException(ex, sf);
			}
		}
	}
}
