namespace ZData00.Values
{
	using System.Diagnostics;
	using System.Numerics;
	using Actions;
	using Bases;
	using Exceptions;
	using MessagePack;
	using Newtonsoft.Json;

	[JsonObject(MemberSerialization.OptIn)]
	public class Range<TRange> : Base<TRange[]> where TRange : notnull, INumber<TRange>, IMinMaxValue<TRange>
	{
		[JsonProperty]
		public TRange Start { get; init; }
		[JsonProperty]
		public TRange End { get; protected set; }
		[JsonProperty]
		public TRange Step { get; set; }

		public new TRange[] Value
		{
			get
			{
				TRange[] Out = [];

				try
				{
					for (TRange i = Start; i < End; i += Step)
						Out = [.. Out, i];
				}
				catch (Exception ex)
				{
					throw new NumberException(ex, new StackFrame(true));
				}
				finally
				{
					Out ??= [];
				}

				return Out;
			}
		}

		/// <summary>
		/// Primary constructor for the <see cref="Range{TRange}"/> class
		/// </summary>
		/// <param name="start">The minimum value for this <see cref="Range{TRange}"/></param>
		/// <param name="end">The maximum value for this <see cref="Range{TRange}"/></param>
		/// <param name="step">The difference between each value of this <see cref="Range{TRange}"/></param>
		/// <exception cref="NumberException"></exception>
		[JsonConstructor]
		public Range(TRange start, TRange end, TRange step) : base([])
		{
			var sf = new StackFrame(true);
			Log.Event(sf);

			try
			{
				if (start < end)
					throw new Exception($"The start value {Format<TRange>.ExcValue(start)} is lesser than the end value {Format<TRange>.ExcValue(end)}");
				else if (TRange.CreateTruncating(start.CompareTo(end)) < step)
					throw new Exception($"The difference between the start value {Format<TRange>.ExcValue(start)} and the end value {Format<TRange>.ExcValue(end)} {Format<int>.ExcValue(start.CompareTo(end))} ");
				else
				{
					End = end;
					Start = start;
					Step = step;
				}
			}
			catch (Exception ex)
			{
				throw new NumberException(ex, sf);
			}
		}

		/// <summary>
		/// Constructor for the <see cref="Range{TRange}"/> class
		/// </summary>
		/// <inheritdoc cref="Range{TRange}.Range(TRange, TRange, TRange)"/>
		public Range(TRange end, TRange step) : this(TRange.Zero, end, step) => Log.Event(new StackFrame(true));
		/// <inheritdoc cref="Range{TRange}.Range(TRange, TRange)"/>
		public Range(TRange end) : this(TRange.Zero, end, TRange.One) => Log.Event(new StackFrame(true));

		public TRange[] ToArray()
		{
			var sf = new StackFrame(true);
			Log.Event(sf);

			TRange[] Out = [];

			try
			{
				for (TRange i = Start; i < End; i += Step)
					Out = [.. Out, i];
			}
			catch (Exception ex)
			{
				throw new NumberException(ex, sf);
			}
			finally
			{
				Out ??= [];
			}

			return Out;
		}

		public IEnumerable<TRange> ToEnumerable()
		{
			var sf = new StackFrame(true);
			Log.Event(sf);

			IEnumerable<TRange> Out = [];

			try
			{
				for (TRange i = Start; i < End; i += Step)
					Out = [.. Out, i];
			}
			catch (Exception ex)
			{
				throw new NumberException(ex, sf);
			}
			finally
			{
				Out ??= [];
			}

			return Out;
		}

		public Range ToRange()
		{
			var sf = new StackFrame(true);
			Log.Event(sf);

			Range? Out;

			try
			{
				Out = new(int.CreateTruncating(Start), int.CreateTruncating(End));
			}
			catch (Exception ex)
			{
				throw new NumberException(ex, sf);
			}

			return Out ?? new();
		}

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
				throw new BaseException($"This data could not serialized into the JSON format", sf);
			}
			catch (Exception ex)
			{
				throw new NumberException(ex, sf);
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
				Out = MessagePackSerializer.Serialize(Start, opts);
				Out = [.. Out, .. MessagePackSerializer.Serialize(End, opts)];
				Out = [.. Out, .. MessagePackSerializer.Serialize(Step, opts)];
			}
			catch (MessagePackSerializationException)
			{
				throw new BaseException($"This data could not serialized into the MessagePack format", sf);
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

		public override string ToString()
		{
			var sf = new StackFrame(true);
			Log.Event(sf);

			string? Out = null;

			try
			{
				Out = $"[{Start},{End}]({Step})";
			}
			catch (Exception ex)
			{
				throw new NumberException(ex, sf);
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
}
