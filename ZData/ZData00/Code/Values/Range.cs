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
	public class Range<TRange> : Base<TRange[]> where TRange : notnull, INumberBase<TRange>, IComparisonOperators<TRange, TRange, bool>, IMinMaxValue<TRange>
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
		/// <exception cref="NumberException"é>
		[JsonConstructor]
		public Range(TRange start, TRange end, TRange step) : base([])
		{
			var sf = new StackFrame(true);
			Log.Event(sf);

			try
			{
				if (start > end)
					throw new Exception($"The start value {Format<TRange>.ExcValue(start)} is greater than the end value {Format<TRange>.ExcValue(end)}");
				else if (start < step)
					throw new Exception($"The difference between the start value {Format<TRange>.ExcValue(start)} and the end value {Format<TRange>.ExcValue(end)} {Format<TRange>.ExcValue(end - start)} is greater than the step value {Format<TRange>.ExcValue(step)}");
				else
				{
					End = end;
					Start = start;
					Step = step;

					for (TRange i = Start; i < End; i += Step)
						base.Value = [.. base.Value, i];
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
				throw new NumberException($"This data could not serialized into the JSON format", sf);
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

		/// <inheritdoc cref="Base{T}.ToMessagePack(MessagePackSerializerOptions?)"/>
		/// <exception cref="NumberException"/>
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
				throw new NumberException($"This data could not serialized into the MessagePack format", sf);
			}
			catch (Exception ex)
			{
				throw new NumberException(ex, sf);
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

		/// <inheritdoc cref="Base{T}.ToString"/>
		/// <exception cref="NumberException"/>
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

		public string ToArrayString()
		{
			var sf = new StackFrame(true);
			Log.Event(sf);

			string Out = "[";

			try
			{
				for (TRange i = Start; i < End; i++)
					Out += $"{i},";

				Out = Out.TrimEnd(',') + "]";
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
