namespace ZData01.Values
{
	using System.Diagnostics;
	using System.Diagnostics.CodeAnalysis;
	using System.Numerics;
	using Actions;
	using Attributes;
	using Bases;
	using Exceptions;
	using MessagePack;
	using Newtonsoft.Json;

	[JsonObject(MemberSerialization.OptIn)]
	public class Date : Base<DateOnly>, IAdditionOperators<Date, Date, Date>, ISubtractionOperators<Date, Date, Date>, IModulusOperators<Date, Date, Date>, IMinMaxValue<Date>
	{
		[JsonProperty]
		public new DateOnly Value => base.Value;

		public static Date MinValue => new(DateOnly.MinValue);
		public static Date MaxValue => new(DateOnly.MaxValue);

		/// <summary>
		/// Primary constructor for the <see cref="Date"/> class
		/// </summary>
		/// <param name="value">The given value</param>
		/// <exception cref="DateException"/>
		[JsonConstructor, MainConstructor]
		public Date(DateOnly value) : base(value) => Log.Event(new StackFrame(true));

		/// <summary>
		/// Constructor for the <see cref="Date"/> class
		/// </summary>
		/// <param name="value">The given value</param>
		/// <exception cref="DateException"/>
		public Date(DateTime value) : this(new DateOnly(value.Year, value.Month, value.Day)) => Log.Event(new StackFrame(true));

		/// <inheritdoc cref="Date(DateTime)"/>
		public Date(Moment value) : this(new DateOnly(value.Value.Year, value.Value.Month, value.Value.Day)) => Log.Event(new StackFrame(true));
		
		/// <inheritdoc cref="Date(DateTime)"/>
		/// <param name="year">The given year</param>
		/// <param name="month">The given month</param>
		/// <param name="day">The given day</param>
		public Date(Year year, Month month, Day day) : this(new DateOnly(year.Value, month.Value, day.Value)) => Log.Event(new StackFrame(true));
		
		/// <inheritdoc cref="Date(Year,Month,Day)"/>
		public Date(Year year, Month month, int day) : this(new DateOnly(year.Value, month.Value, day)) => Log.Event(new StackFrame(true));
		
		/// <inheritdoc cref="Date(Year,Month,Day)"/>
		public Date(Year year, int month, Day day) : this(new DateOnly(year.Value, month, day.Value)) => Log.Event(new StackFrame(true));
		
		/// <inheritdoc cref="Date(Year,Month,Day)"/>
		public Date(Year year, int month, int day) : this(new DateOnly(year.Value, month, day)) => Log.Event(new StackFrame(true));
		
		/// <inheritdoc cref="Date(Year,Month,Day)"/>
		public Date(int year, Month month, Day day) : this(new DateOnly(year, month.Value, day.Value)) => Log.Event(new StackFrame(true));
		
		/// <inheritdoc cref="Date(Year,Month,Day)"/>
		public Date(int year, Month month, int day) : this(new DateOnly(year, month.Value, day)) => Log.Event(new StackFrame(true));
		
		/// <inheritdoc cref="Date(Year,Month,Day)"/>
		public Date(int year, int month, Day day) : this(new DateOnly(year, month, day.Value)) => Log.Event(new StackFrame(true));
		
		/// <inheritdoc cref="Date(Year,Month,Day)"/>
		public Date(int year, int month, int day) : this(new DateOnly(year, month, day)) => Log.Event(new StackFrame(true));
		
		/// <inheritdoc cref="Date(Year,Month,Day)"/>
		public Date(Year year, Month month) : this(new DateOnly(year.Value, month.Value, 1)) => Log.Event(new StackFrame(true));
		
		/// <inheritdoc cref="Date(Year,Month,Day)"/>
		public Date(Year year, int month) : this(new DateOnly(year.Value, month, 1)) => Log.Event(new StackFrame(true));
		
		/// <inheritdoc cref="Date(Year,Month,Day)"/>
		public Date(int year, Month month) : this(new DateOnly(year, month.Value, 1)) => Log.Event(new StackFrame(true));
		
		/// <inheritdoc cref="Date(Year,Month,Day)"/>
		public Date(int year, int month) : this(new DateOnly(year, month, 1)) => Log.Event(new StackFrame(true));
		
		/// <inheritdoc cref="Date(Year,Month,Day)"/>
		public Date(Year year) : this(new DateOnly(year.Value, 1, 1)) => Log.Event(new StackFrame(true));

		/// <inheritdoc cref="Date(Year,Month,Day)"/>
		public Date(int dayNumber) : this(DateOnly.FromDayNumber(dayNumber)) => Log.Event(new StackFrame(true));

		/// <summary>
		/// Empty constructor for the <see cref="Date"/> class
		/// </summary>
		[EmptyConstructor]
		public Date() : this(DateOnly.MinValue) => Log.Event(new StackFrame(true));

		public static implicit operator Date(DateTime v) => new(v);
		public static implicit operator Date(DateOnly v) => new(v);
		public static implicit operator Date(Moment v) => new(v);

		/// <inheritdoc cref="Base{DateOnly}.ToString"/>
		/// <exception cref="DateException"/>
		public override string ToString()
		{
			var sf = new StackFrame(true);
			Log.Event(sf);

			string? Out = null;

			try
			{
				Out = $"{Value:yyyy-MM-dd}";
			}
			catch (Exception ex)
			{
				throw new DateException(ex, sf);
			}
			finally
			{
				try
				{
					Out ??= base.ToString();
				}
				catch (Exception ex)
				{
					Debug.WriteLine($"{ex}");
					Out = "";
				}
			}

			return Out;
		}

		public string ToString([StringSyntax(StringSyntaxAttribute.DateOnlyFormat)] string? format)
		{
			var sf = new StackFrame(true);
			Log.Event(sf);

			string? Out = null;

			try
			{
				Out = string.Format(format ?? "d", Value);
			}
			catch (Exception ex)
			{
				throw new DateException(ex, sf);
			}
			finally
			{
				try
				{
					Out ??= base.ToString();
				}
				catch (Exception ex)
				{
					Debug.WriteLine($"{ex}");
					Out = "";
				}
			}

			return Out;
		}

		/// <inheritdoc cref="Base{DateOnly}.ToJSON(Formatting)"/>
		/// <exception cref="DateException"/>
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
				throw new DateException(ex, sf);
			}
			finally
			{
				try
				{
					Out ??= base.ToJSON(formatting);
				}
				catch (Exception ex)
				{
					Debug.WriteLine($"{ex}");
					Out = "";
				}
			}

			return Out;
		}

		/// <inheritdoc cref="Base{DateOnly}.ToMessagePack(MessagePackSerializerOptions?)"/>
		/// <exception cref="DateException"/>
		public override byte[] ToMessagePack(MessagePackSerializerOptions? opts = null)
		{
			var sf = new StackFrame(true);
			Log.Event(sf);

			byte[]? Out = null;

			try
			{
				Out = MessagePackSerializer.Serialize(Value.Year, opts);
				Out = [.. Out, .. MessagePackSerializer.Serialize(Value.Month, opts)];
				Out = [.. Out, .. MessagePackSerializer.Serialize(Value.Day, opts)];
			}
			catch (MessagePackSerializationException ex)
			{
				throw new MessagePackSerializationException($"This data could not be serialized into the MessagePack format", ex);
			}
			catch (Exception ex)
			{
				throw new DateException(ex, sf);
			}
			finally
			{
				try
				{
					Out ??= base.ToMessagePack(opts);
				}
				catch (Exception ex)
				{
					Debug.WriteLine($"{ex}");
					Out = Def.MsgPack;
				}
			}

			return Out;
		}

		public Moment ToMoment()
		{
			var sf = new StackFrame(true);
			Log.Event(sf);

			Moment? Out = null;

			try
			{
				Out = new(this);
			}
			catch (Exception ex)
			{
				throw new DateException(ex, sf);
			}
			finally
			{
				Out ??= new();
			}

			return Out;
		}

		/// <inheritdoc cref="IAdditionOperators{TSelf, TOther, TResult}.op_Addition"/>
		/// <exception cref="TimeException"/>
		public static Date operator +(Date left, Date right)
		{
			var sf = new StackFrame(true);
			Log.Event(sf);

			Date? Out = null;

			try
			{
				Out = new(left.Value.DayNumber + right.Value.DayNumber);
			}
			catch (DateException)
			{
				throw;
			}
			catch (Exception ex)
			{
				throw new DateException(ex, sf);
			}
			finally
			{
				Out ??= left;
			}

			return Out;
		}

		/// <inheritdoc cref="ISubtractionOperators{TSelf, TOther, TResult}.op_Subtraction"/>
		/// <exception cref="TimeException"/>
		public static Date operator -(Date left, Date right)
		{
			var sf = new StackFrame(true);
			Log.Event(sf);

			Date? Out = null;

			try
			{
				Out = new(left.Value.DayNumber - right.Value.DayNumber);
			}
			catch (DateException)
			{
				throw;
			}
			catch (Exception ex)
			{
				throw new DateException(ex, sf);
			}
			finally
			{
				Out ??= left;
			}

			return Out;
		}

		/// <inheritdoc cref="IModulusOperators{TSelf, TOther, TResult}.op_Modulus"/>
		/// <exception cref="TimeException"/>
		public static Date operator %(Date left, Date right)
		{
			var sf = new StackFrame(true);
			Log.Event(sf);

			Date? Out = null;

			try
			{
				Out = new(left.Value.DayNumber % right.Value.DayNumber);
			}
			catch (DateException)
			{
				throw;
			}
			catch (Exception ex)
			{
				throw new DateException(ex, sf);
			}
			finally
			{
				Out ??= left;
			}

			return Out;
		}
	}
}
