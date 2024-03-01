namespace ZData01.Values
{
	using System.Diagnostics;
	using System.Numerics;
	using Actions;
	using Attributes;
	using Bases;
	using Exceptions;
	using Newtonsoft.Json;

	[JsonObject(MemberSerialization.OptIn)]
	public class Time : Base<TimeOnly>, IAdditionOperators<Time, Time, Time>, ISubtractionOperators<Time, Time, Time>, IModulusOperators<Time, Time, Time>
	{
		[JsonProperty]
		public new TimeOnly Value => base.Value;

		/// <summary>
		/// Primary constructor for the <see cref="Time"/> class
		/// </summary>
		/// <param name="value">The given value</param>
		/// <exception cref="TimeException"/>
		[JsonConstructor, MainConstructor]
		public Time(TimeOnly value) : base(value) => Log.Event(new StackFrame(true));

		/// <summary>
		/// Constructor for the <see cref="Time"/> class
		/// </summary>
		/// <inheritdoc cref="Time(TimeOnly)"/>
		public Time(DateTime value) : this(new TimeOnly(value.Hour, value.Minute, value.Second, value.Millisecond, value.Microsecond)) => Log.Event(new StackFrame(true));

		/// <inheritdoc cref="Time(DateTime)"/>
		/// <param name="hour">The given hour</param>
		/// <param name="minute">The given minute</param>
		/// <param name="second">The given second</param>
		/// <param name="millisecond">The given millisecond</param>
		public Time(Hour hour, Minute minute, Second second, Millisecond millisecond) : this(new TimeOnly(hour.Value, minute.Value, second.Value, millisecond.Value)) => Log.Event(new StackFrame(true));

		/// <inheritdoc cref="Time(DateTime)"/>
		public Time(Moment value) : this(new TimeOnly(value.Value.Hour, value.Value.Minute, value.Value.Second, value.Value.Millisecond, value.Value.Microsecond)) => Log.Event(new StackFrame(true));

		/// <inheritdoc cref="Time(Hour,Minute,Second,Millisecond)"/>
		public Time(Hour hour, Minute minute, Second second, int millisecond) : this(new TimeOnly(hour.Value, minute.Value, second.Value, millisecond)) => Log.Event(new StackFrame(true));

		/// <inheritdoc cref="Time(Hour,Minute,Second,Millisecond)"/>
		public Time(Hour hour, Minute minute, int second, Millisecond millisecond) : this(new TimeOnly(hour.Value, minute.Value, second, millisecond.Value)) => Log.Event(new StackFrame(true));

		/// <inheritdoc cref="Time(Hour,Minute,Second,Millisecond)"/>
		public Time(Hour hour, Minute minute, int second, int millisecond) : this(new TimeOnly(hour.Value, minute.Value, second, millisecond)) => Log.Event(new StackFrame(true));

		/// <inheritdoc cref="Time(Hour,Minute,Second,Millisecond)"/>
		public Time(Hour hour, int minute, Second second, Millisecond millisecond) : this(new TimeOnly(hour.Value, minute, second.Value, millisecond.Value)) => Log.Event(new StackFrame(true));

		/// <inheritdoc cref="Time(Hour,Minute,Second,Millisecond)"/>
		public Time(Hour hour, int minute, Second second, int millisecond) : this(new TimeOnly(hour.Value, minute, second.Value, millisecond)) => Log.Event(new StackFrame(true));

		/// <inheritdoc cref="Time(Hour,Minute,Second,Millisecond)"/>
		public Time(Hour hour, int minute, int second, Millisecond millisecond) : this(new TimeOnly(hour.Value, minute, second, millisecond.Value)) => Log.Event(new StackFrame(true));

		/// <inheritdoc cref="Time(Hour,Minute,Second,Millisecond)"/>
		public Time(Hour hour, int minute, int second, int millisecond) : this(new TimeOnly(hour.Value, minute, second, millisecond)) => Log.Event(new StackFrame(true));

		/// <inheritdoc cref="Time(Hour,Minute,Second,Millisecond)"/>
		public Time(int hour, Minute minute, Second second, Millisecond millisecond) : this(new TimeOnly(hour, minute.Value, second.Value, millisecond.Value)) => Log.Event(new StackFrame(true));

		/// <inheritdoc cref="Time(Hour,Minute,Second,Millisecond)"/>
		public Time(int hour, Minute minute, Second second, int millisecond) : this(new TimeOnly(hour, minute.Value, second.Value, millisecond)) => Log.Event(new StackFrame(true));

		/// <inheritdoc cref="Time(Hour,Minute,Second,Millisecond)"/>
		public Time(int hour, Minute minute, int second, Millisecond millisecond) : this(new TimeOnly(hour, minute.Value, second, millisecond.Value)) => Log.Event(new StackFrame(true));

		/// <inheritdoc cref="Time(Hour,Minute,Second,Millisecond)"/>
		public Time(int hour, Minute minute, int second, int millisecond) : this(new TimeOnly(hour, minute.Value, second, millisecond)) => Log.Event(new StackFrame(true));

		/// <inheritdoc cref="Time(Hour,Minute,Second,Millisecond)"/>
		public Time(int hour, int minute, Second second, Millisecond millisecond) : this(new TimeOnly(hour, minute, second.Value, millisecond.Value)) => Log.Event(new StackFrame(true));

		/// <inheritdoc cref="Time(Hour,Minute,Second,Millisecond)"/>
		public Time(int hour, int minute, Second second, int millisecond) : this(new TimeOnly(hour, minute, second.Value, millisecond)) => Log.Event(new StackFrame(true));
		/// <inheritdoc cref="Time(Hour,Minute,Second,Millisecond)"/>
		public Time(int hour, int minute, int second, Millisecond millisecond) : this(new TimeOnly(hour, minute, second, millisecond.Value)) => Log.Event(new StackFrame(true));

		/// <inheritdoc cref="Time(Hour,Minute,Second,Millisecond)"/>
		public Time(int hour, int minute, int second, int millisecond) : this(new TimeOnly(hour, minute, second, millisecond)) => Log.Event(new StackFrame(true));

		/// <inheritdoc cref="Time(Hour,Minute,Second,Millisecond)"/>
		public Time(Hour hour, Minute minute, Second second) : this(new TimeOnly(hour.Value, minute.Value, second.Value)) => Log.Event(new StackFrame(true));

		/// <inheritdoc cref="Time(Hour,Minute,Second,Millisecond)"/>
		public Time(Hour hour, Minute minute, int second) : this(new TimeOnly(hour.Value, minute.Value, second)) => Log.Event(new StackFrame(true));

		/// <inheritdoc cref="Time(Hour,Minute,Second,Millisecond)"/>
		public Time(Hour hour, int minute, Second second) : this(new TimeOnly(hour.Value, minute, second.Value)) => Log.Event(new StackFrame(true));

		/// <inheritdoc cref="Time(Hour,Minute,Second,Millisecond)"/>
		public Time(Hour hour, int minute, int second) : this(new TimeOnly(hour.Value, minute, second)) => Log.Event(new StackFrame(true));

		/// <inheritdoc cref="Time(Hour,Minute,Second,Millisecond)"/>
		public Time(int hour, Minute minute, Second second) : this(new TimeOnly(hour, minute.Value, second.Value)) => Log.Event(new StackFrame(true));

		/// <inheritdoc cref="Time(Hour,Minute,Second,Millisecond)"/>
		public Time(int hour, Minute minute, int second) : this(new TimeOnly(hour, minute.Value, second)) => Log.Event(new StackFrame(true));

		/// <inheritdoc cref="Time(Hour,Minute,Second,Millisecond)"/>
		public Time(int hour, int minute, Second second) : this(new TimeOnly(hour, minute, second.Value)) => Log.Event(new StackFrame(true));

		/// <inheritdoc cref="Time(Hour,Minute,Second,Millisecond)"/>
		public Time(int hour, int minute, int second) : this(new TimeOnly(hour, minute, second)) => Log.Event(new StackFrame(true));

		/// <inheritdoc cref="Time(Hour,Minute,Second,Millisecond)"/>
		public Time(Hour hour, Minute minute) : this(new TimeOnly(hour.Value, minute.Value)) => Log.Event(new StackFrame(true));

		/// <inheritdoc cref="Time(Hour,Minute,Second,Millisecond)"/>
		public Time(Hour hour, int minute) : this(new TimeOnly(hour.Value, minute)) => Log.Event(new StackFrame(true));

		/// <inheritdoc cref="Time(Hour,Minute,Second,Millisecond)"/>
		public Time(int hour, Minute minute) : this(new TimeOnly(hour, minute.Value)) => Log.Event(new StackFrame(true));

		/// <inheritdoc cref="Time(Hour,Minute,Second,Millisecond)"/>
		public Time(int hour, int minute) : this(new TimeOnly(hour, minute)) => Log.Event(new StackFrame(true));

		/// <inheritdoc cref="Time(Hour,Minute,Second,Millisecond)"/>
		public Time(Hour hour) : this(new TimeOnly(hour.Value, TimeOnly.MinValue.Minute)) => Log.Event(new StackFrame(true));

		/// <inheritdoc cref="Time(Hour,Minute,Second,Millisecond)"/>
		/// <param name="ticks">The given number of ticks since 00:00:00.000</param>
		public Time(long ticks) : this(new TimeOnly(ticks)) => Log.Event(new StackFrame(true));

		/// <summary>
		/// Empty constructor for the <see cref="Time"/> class
		/// </summary>
		[EmptyConstructor]
		public Time() : this(TimeOnly.MinValue) => Log.Event(new StackFrame(true));

		public override string ToString()
		{
			var sf = new StackFrame(true);
			Log.Event(sf);

			string? Out = null;

			try
			{
				Out = $"{Value:HH:mm:ss.fff}";
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
				throw new TimeException(ex, sf);
			}
			finally
			{
				Out ??= new();
			}

			return Out;
		}

		/// <summary>
		/// Function to add hours to the given <see cref="Time"/> object
		/// </summary>
		/// <param name="time">The given <see cref="Time"/> object</param>
		/// <param name="value">The number of hours to be added to <paramref name="time"/></param>
		/// <exception cref="TimeException"/>
		public void AddHours(ref Time time, int value)
		{
			var sf = new StackFrame(true);
			Log.Event(sf);

			try
			{
				time = time.Value.AddHours(value);
			}
			catch (Exception ex)
			{
				throw new TimeException(ex, sf);
			}
		}

		/// <summary>
		/// Function to add minutes to the given <see cref="Time"/> object
		/// </summary>
		/// <param name="time">The given <see cref="Time"/> object</param>
		/// <param name="value">The number of minutes to be added to <paramref name="time"/></param>
		/// <exception cref="TimeException"/>
		public void AddMinutes(ref Time time, int value)
		{
			var sf = new StackFrame(true);
			Log.Event(sf);

			try
			{
				time = time.Value.AddMinutes(value);
			}
			catch (Exception ex)
			{
				throw new TimeException(ex, sf);
			}
		}

		/// <summary>
		/// Function to add seconds to the given <see cref="Time"/> object
		/// </summary>
		/// <param name="time">The given <see cref="Time"/> object</param>
		/// <param name="value">The number of seconds to be added to <paramref name="time"/></param>
		/// <exception cref="TimeException"/>
		public void AddSeconds(ref Time time, int value)
		{
			var sf = new StackFrame(true);
			Log.Event(sf);

			try
			{
				time = time.Value.Add(new(0, 0, 0, value));
			}
			catch (Exception ex)
			{
				throw new TimeException(ex, sf);
			}
		}

		/// <summary>
		/// Function to add milliseconds to the given <see cref="Time"/> object
		/// </summary>
		/// <param name="time">The given <see cref="Time"/> object</param>
		/// <param name="value">The number of milliseconds to be added to <paramref name="time"/></param>
		/// <exception cref="TimeException"/>
		public void AddMilliseconds(ref Time time, int value)
		{
			var sf = new StackFrame(true);
			Log.Event(sf);

			try
			{
				time = time.Value.Add(new(0, 0, 0, 0, value));
			}
			catch (Exception ex)
			{
				throw new TimeException(ex, sf);
			}
		}

		/// <summary>
		/// Function to add microseconds to the given <see cref="Time"/> object
		/// </summary>
		/// <param name="time">The given <see cref="Time"/> object</param>
		/// <param name="value">The number of microseconds to be added to <paramref name="time"/></param>
		/// <exception cref="TimeException"/>
		public void AddMicroseconds(ref Time time, int value)
		{
			var sf = new StackFrame(true);
			Log.Event(sf);

			try
			{
				time = time.Value.Add(new(0, 0, 0, 0, 0, value));
			}
			catch (Exception ex)
			{
				throw new TimeException(ex, sf);
			}
		}

		public static implicit operator Time(TimeOnly v) => new(v);
		public static implicit operator Time(DateTime v) => new(v);
		public static implicit operator Time(Moment v) => new(v);

		/// <inheritdoc cref="IAdditionOperators{TSelf, TOther, TResult}.op_Addition"/>
		/// <remarks>If an exception is encountered, it will return the <paramref name="left"/> value</remarks>
		/// <exception cref="TimeException"/>
		public static Time operator +(Time left, Time right)
		{
			var sf = new StackFrame(true);
			Log.Event(sf);

			Time? Out = null;

			try
			{
				Out = new(left.Value.Ticks + right.Value.Ticks);
			}
			catch (TimeException)
			{
				throw;
			}
			catch (Exception ex)
			{
				throw new TimeException(ex, sf);
			}
			finally
			{
				Out ??= left;
			}

			return Out;
		}

		/// <inheritdoc cref="ISubtractionOperators{TSelf, TOther, TResult}.op_Subtraction"/>
		/// <exception cref="TimeException"/>
		public static Time operator -(Time left, Time right)
		{
			var sf = new StackFrame(true);
			Log.Event(sf);

			Time? Out = null;

			try
			{
				Out = new(left.Value.Ticks - right.Value.Ticks);
			}
			catch (TimeException)
			{
				throw;
			}
			catch (Exception ex)
			{
				throw new TimeException(ex, sf);
			}
			finally
			{
				Out ??= left;
			}

			return Out;
		}

		/// <inheritdoc cref="IModulusOperators{TSelf, TOther, TResult}.op_Modulus"/>
		/// <exception cref="TimeException"/>
		public static Time operator %(Time left, Time right)
		{
			var sf = new StackFrame(true);
			Log.Event(sf);

			Time? Out = null;

			try
			{
				Out = new(left.Value.Ticks % right.Value.Ticks);
			}
			catch (TimeException)
			{
				throw;
			}
			catch (Exception ex)
			{
				throw new TimeException(ex, sf);
			}
			finally
			{
				Out ??= left;
			}

			return Out;
		}
	}
}
