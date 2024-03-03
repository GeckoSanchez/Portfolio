namespace ZData02.Values
{
	using System.ComponentModel;
	using System.Diagnostics;
	using Actions;
	using Attributes;
	using Bases;
	using Exceptions;
	using MessagePack;
	using Newtonsoft.Json;

	[JsonObject(MemberSerialization.OptIn)]
	public class LString : BaseData<string>
	{
		[JsonProperty]
		public new string Data => base.Data;

		[DefaultValue(0)]
		[JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
		public Range Range { get; set; }

		/// <summary>
		/// Primary constructor for the <see cref="LString"/> class
		/// </summary>
		/// <param name="data">The given value</param>
		/// <param name="range">The given range</param>
		/// <exception cref="ValueException"/>
		[JsonConstructor, MainConstructor]
		public LString(string data, Range range) : base("")
		{
			var sf = new StackFrame(true);
			Log.Event(sf);

			try
			{
				if (data.Length < range.Start.Value || data.Length > range.End.Value)
					throw new Exception($"The string data {Format.ExcValue(data)} does not fit into the length range {Format<Range>.ExcValue(range)}");
				else
				{
					base.Data = data;
					Range = new(range.Start, int.Min(range.End.Value, data.Length));
				}
			}
			catch (Exception ex)
			{
				throw new ValueException(ex, sf);
			}
			finally
			{
				if (Range.Equals(new Range(0, 0)))
					Range = new(0, data.Length);

				if (base.Data == "")
					for (int i = 0; i < range.Start.Value; i++)
						base.Data += ' ';
			}
		}

		/// <summary>
		/// Constructor for the <see cref="LString"/> class
		/// </summary>
		/// <param name="data">The given value</param>
		/// <param name="min">The minimum size for <paramref name="data"/></param>
		/// <param name="max">The maximum size for <paramref name="data"/></param>
		/// <exception cref="ValueException"/>
		public LString(string data, int min, int max) : this(data, new Range(int.Min(min, max), int.Max(min, max))) => Log.Event(new StackFrame(true));

		/// <inheritdoc cref="LString(string,int,int)"/>
		public LString(string data, int max) : this(data, new Range(0, max)) => Log.Event(new StackFrame(true));

		/// <inheritdoc cref="LString(string,int,int)"/>
		public LString(string data) : this(data, new Range(0, data.Length)) => Log.Event(new StackFrame(true));

		/// <inheritdoc cref="BaseData{string}.ToString"/>
		/// <exception cref="ValueException"/>
		public override string ToString()
		{
			var sf = new StackFrame(true);
			Log.Event(sf);

			string? Out = null;

			try
			{
				Out = Data[Range.Start.Value..int.Min(Range.End.Value, Data.Length)];
			}
			catch (Exception ex)
			{
				throw new ValueException(ex, sf);
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

		/// <inheritdoc cref="BaseData{string}.ToJSON(Formatting)"/>
		/// <exception cref="ValueException"/>
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
				throw new ValueException($"This data could not be serialized into the JSON format", sf);
			}
			catch (Exception ex)
			{
				throw new ValueException(ex, sf);
			}
			finally
			{
				try
				{
					Out ??= base.ToJSON(formatting);
				}
				catch (Exception)
				{
					Out = Def.JSON;
				}
			}

			return Out;
		}

		/// <inheritdoc cref="BaseData{string}.ToMessagePack(MessagePackSerializerOptions?)"/>
		/// <exception cref="ValueException"/>
		public override byte[] ToMessagePack(MessagePackSerializerOptions? opts = null)
		{
			var sf = new StackFrame(true);
			Log.Event(sf);

			byte[] Out;

			try
			{
				Out = MessagePackSerializer.Serialize(Data, opts);
				Out = [.. Out, .. MessagePackSerializer.Serialize(Range.Start.Value, opts)];
				Out = [.. Out, .. MessagePackSerializer.Serialize(Range.End.Value, opts)];
			}
			catch (MessagePackSerializationException ex)
			{
				throw new MessagePackSerializationException($"This data could not be serialized into the MessagePack format", ex);
			}
			catch (Exception ex)
			{
				throw new ValueException(ex, sf);
			}

			return Out;
		}
	}
}
