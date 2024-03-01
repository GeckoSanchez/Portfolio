namespace ZData01.Values
{
	using System.Diagnostics;
	using Actions;
	using Attributes;
	using Bases;
	using Exceptions;
	using MessagePack;
	using Newtonsoft.Json;

	[JsonObject(MemberSerialization.OptIn)]
	public class LString : Base<string>
	{
		[JsonProperty]
		public new string Value => base.Value;

		[JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
		public Range Range { get; set; }

		/// <summary>
		/// Primary constructor for the <see cref="LString"/> class
		/// </summary>
		/// <param name="value">The given value</param>
		/// <param name="range">The given range</param>
		/// <exception cref="ValueException"/>
		[JsonConstructor, MainConstructor]
		public LString(string value, Range range) : base(value)
		{
			var sf = new StackFrame(true);
			Log.Event(sf);

			try
			{
				Range = new(range.Start, int.Min(range.End.Value, value.Length));
			}
			catch (Exception ex)
			{
				throw new ValueException(ex, sf);
			}
			finally
			{
				Range = range;
			}
		}

		/// <summary>
		/// Constructor for the <see cref="LString"/> class
		/// </summary>
		/// <param name="value">The given value</param>
		/// <param name="min">The minimum size for <paramref name="value"/></param>
		/// <param name="max">The maximum size for <paramref name="value"/></param>
		/// <exception cref="ValueException"/>
		public LString(string value, int min, int max) : this(value, new Range(int.Min(min, max), int.Max(min, max))) => Log.Event(new StackFrame(true));

		/// <inheritdoc cref="LString(string,int,int)"/>
		public LString(string value, int max) : this(value, new Range(0, max)) => Log.Event(new StackFrame(true));

		/// <inheritdoc cref="LString(string,int,int)"/>
		public LString(string value) : this(value, new Range(0, value.Length)) => Log.Event(new StackFrame(true));

		/// <inheritdoc cref="Base{string}.ToString"/>
		/// <exception cref="ValueException"/>
		public override string ToString()
		{
			var sf = new StackFrame(true);
			Log.Event(sf);

			string? Out = null;

			try
			{
				Out = Value[Range.Start.Value..int.Min(Range.End.Value, Value.Length)];
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

		/// <inheritdoc cref="Base{string}.ToJSON(Formatting)"/>
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

		/// <inheritdoc cref="Base{string}.ToMessagePack(MessagePackSerializerOptions?)"/>
		/// <exception cref="ValueException"/>
		public override byte[] ToMessagePack(MessagePackSerializerOptions? opts = null)
		{
			var sf = new StackFrame(true);
			Log.Event(sf);

			byte[] Out;

			try
			{
				Out = MessagePackSerializer.Serialize(Value, opts);
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
