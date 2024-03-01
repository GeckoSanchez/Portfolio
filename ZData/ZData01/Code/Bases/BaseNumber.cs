namespace ZData01.Bases
{
	using System.Diagnostics;
	using System.Globalization;
	using System.Numerics;
	using Actions;
	using Attributes;
	using Exceptions;
	using MessagePack;
	using Newtonsoft.Json;

	[JsonObject(MemberSerialization.OptIn)]
	public class BaseNumber<TNum> : Base<TNum>, Categories.INumber<TNum> where TNum : INumberBase<TNum>, IComparisonOperators<TNum, TNum, bool>, IMinMaxValue<TNum>
	{
		[JsonProperty]
		public new TNum Value { get => base.Value; protected init => base.Value = value; }
		public TNum MinValue { get; protected init; }
		public TNum MaxValue { get; protected init; }

		/// <summary>
		/// Primary constructor for the <see cref="BaseNumber{TNum}"/> class
		/// </summary>
		/// <param name="value">The current value</param>
		/// <param name="minimum">The minimum value</param>
		/// <param name="maximum">The maximum value</param>
		/// <exception cref="NumberException"/>
		[JsonConstructor, MainConstructor]
		public BaseNumber(TNum value, TNum minimum, TNum maximum) : base(TNum.Zero)
		{
			var sf = new StackFrame(true);
			Log.Event(sf);

			try
			{
				if (maximum == minimum && value != maximum)
					throw new Exception($"The minimum value {Format<TNum>.ExcValue(minimum)} is equal to the maximum value {Format<TNum>.ExcValue(maximum)}, but it is not equal to the current value {Format<TNum>.ExcValue(value)}");
				else if (minimum > maximum)
					throw new Exception($"The minimum value {Format<TNum>.ExcValue(minimum)} is greater than the maximum value {Format<TNum>.ExcValue(maximum)}");
				else if (value < minimum)
					throw new Exception($"The current value {Format<TNum>.ExcValue(value)} is lesser than the minimum value {Format<TNum>.ExcValue(minimum)}");
				else if (value > maximum)
					throw new Exception($"The current value {Format<TNum>.ExcValue(value)} is greater than the maximum value {Format<TNum>.ExcValue(maximum)}");
				else
				{
					Value = value;
					MinValue = minimum;
					MaxValue = maximum;
				}
			}
			catch (Exception ex)
			{
				throw new NumberException(ex, sf);
			}
			finally
			{
				MinValue ??= TNum.MinValue;
				MaxValue ??= TNum.MaxValue;
			}
		}

		/// <summary>
		/// Constructor for the <see cref="BaseNumber{TNum}"/>
		/// </summary>
		/// <inheritdoc cref="BaseNumber{TNum}(TNum, TNum, TNum)"/>
		public BaseNumber(TNum value, TNum maximum) : this(value, TNum.MinValue, maximum) => Log.Event(new StackFrame(true));

		/// <inheritdoc cref="BaseNumber{TNum}(TNum, TNum)"/>
		public BaseNumber(TNum value) : this(value, TNum.MinValue, TNum.MaxValue) => Log.Event(new StackFrame(true));

		/// <inheritdoc cref="BaseNumber{TNum}(TNum, TNum)"/>
		/// <param name="value">The given value</param>
		public BaseNumber(BaseNumber<TNum> value) : this(value.Value, value.MinValue, value.MaxValue) => Log.Event(new StackFrame(true));

		/// <inheritdoc cref="BaseNumber{TNum}(TNum, TNum)"/>
		public BaseNumber() : this(TNum.Zero, TNum.MinValue, TNum.MaxValue) => Log.Event(new StackFrame(true));

		public static implicit operator BaseNumber<TNum>(TNum v) => new(v);

		/// <inheritdoc cref="Base{TNum}.ToString"/>
		/// <exception cref="NumberException"/>
		public override string ToString()
		{
			var sf = new StackFrame(true);
			Log.Event(sf);

			string? Out = null;

			try
			{
				Out = $"({Value})[{MinValue},{MaxValue}]";
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
				catch (Exception)
				{
					Out = "";
				}
			}

			return Out;
		}

		/// <inheritdoc cref="Base{TNum}.ToMessagePack(MessagePackSerializerOptions?)"/>
		/// <exception cref="NumberException"/>
		public override byte[] ToMessagePack(MessagePackSerializerOptions? opts = null)
		{
			var sf = new StackFrame(true);
			Log.Event(sf);

			byte[] Out;

			try
			{
				if (typeof(TNum) != typeof(sbyte) && typeof(TNum) != typeof(byte) && typeof(TNum) != typeof(short) &&
						typeof(TNum) != typeof(ushort) && typeof(TNum) != typeof(int) && typeof(TNum) != typeof(uint) &&
						typeof(TNum) != typeof(long) && typeof(TNum) != typeof(ulong) && typeof(TNum) != typeof(float) &&
						typeof(TNum) != typeof(double) && typeof(TNum) != typeof(decimal) && typeof(TNum) != typeof(Half) &&
						typeof(TNum) != typeof(bool) && typeof(TNum) != typeof(string) && typeof(TNum) != typeof(char) &&
						typeof(TNum) != typeof(Int128) && typeof(TNum) != typeof(UInt128) && Value is null)
					throw new Exception($"The given value {Format<BaseNumber<TNum>>.ExcValue(this)} could not be serialized into a MessagePack format, since its type {Format<string>.ExcValue(typeof(TNum).Name)} is not valid");
				else if (typeof(TNum) == typeof(UInt128))
				{
					ulong top = ulong.Parse($"{(UInt128)(object)Value:X32}"[..16], NumberStyles.HexNumber);
					ulong btm = ulong.Parse($"{(UInt128)(object)Value:X32}"[16..], NumberStyles.HexNumber);

					Out = MessagePackSerializer.Serialize(top, opts);
					Out = [.. Out, .. MessagePackSerializer.Serialize(btm, opts)];
				}
				else if (typeof(TNum) == typeof(Int128))
				{
					ulong top = ulong.Parse($"{(Int128)(object)Value:X32}"[..16], NumberStyles.HexNumber);
					ulong btm = ulong.Parse($"{(Int128)(object)Value:X32}"[16..], NumberStyles.HexNumber);

					Out = MessagePackSerializer.Serialize(top, opts);
					Out = [.. Out, .. MessagePackSerializer.Serialize(btm, opts)];
				}
				else
					Out = MessagePackSerializer.Serialize(Value, opts);
			}
			catch (MessagePackSerializationException ex)
			{
				throw new MessagePackSerializationException($"This data could not be serialized into the MessagePack format", ex);
			}
			catch (Exception ex)
			{
				throw new NumberException(ex, sf);
			}

			return Out;
		}

		/// <inheritdoc cref="Base{TNum}.ToJSON(Formatting)"/>
		/// <exception cref="NumberException"/>
		public override string ToJSON(Formatting formatting = Formatting.Indented)
		{
			var sf = new StackFrame(true);
			Log.Event(sf);

			string? Out = null;

			try
			{
				Out = JsonConvert.SerializeObject(this, formatting);
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
				catch (Exception)
				{
					Out = Def.JSON;
				}
			}

			return Out;
		}
	}
}
