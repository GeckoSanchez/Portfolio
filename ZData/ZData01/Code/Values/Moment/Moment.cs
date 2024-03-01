namespace ZData01.Values
{
	using System.Collections.Generic;
	using System.Diagnostics;
	using System.Numerics;
	using Actions;
	using Attributes;
	using Bases;
	using Exceptions;
	using Newtonsoft.Json;

	[JsonObject(MemberSerialization.OptIn)]
	public class Moment : Base<DateTime>, IMinMaxValue<Moment>, IComparisonOperators<Moment, DateTime, bool>, IComparisonOperators<Moment, Date, bool>, IComparisonOperators<Moment, Time, bool>, IEquatable<Moment?>
	{
		[JsonProperty]
		public new DateTime Value { get => base.Value; protected set => base.Value = value; }

		public static Moment MinValue => new(DateTime.MinValue);
		public static Moment MaxValue => new(DateTime.MaxValue);

		/// <summary>
		/// Primary constructor for the <see cref="Moment"/> class
		/// </summary>
		/// <param name="value">The given value</param>
		/// <exception cref="NumberException"/>
		[JsonConstructor, MainConstructor]
		public Moment(DateTime value) : base(value) => Log.Event(new StackFrame(true));

		/// <summary>
		/// Constructor for the <see cref="Moment"/> class
		/// </summary>
		/// <param name="value">The given value</param>
		public Moment(Moment value) : this(value.Value) => Log.Event(new StackFrame(true));

		/// <inheritdoc cref="Moment(Moment)"/>
		/// <param name="date">The given date</param>
		/// <param name="time">The given time</param>
		public Moment(DateOnly date, TimeOnly time) : this(new DateTime(date, time)) => Log.Event(new StackFrame(true));

		/// <inheritdoc cref="Moment(DateOnly,TimeOnly)"/>
		public Moment(DateOnly date, Time time) : this(new DateTime(date, time.Value)) => Log.Event(new StackFrame(true));

		/// <inheritdoc cref="Moment(DateOnly,TimeOnly)"/>
		public Moment(Date date, TimeOnly time) : this(new DateTime(date.Value, time)) => Log.Event(new StackFrame(true));

		/// <inheritdoc cref="Moment(DateOnly,TimeOnly)"/>
		public Moment(Date date, Time time) : this(new DateTime(date.Value, time.Value)) => Log.Event(new StackFrame(true));

		/// <inheritdoc cref="Moment(DateOnly,TimeOnly)"/>
		public Moment(DateOnly date) : this(new DateTime(date, TimeOnly.MinValue)) => Log.Event(new StackFrame(true));

		/// <inheritdoc cref="Moment(DateOnly,TimeOnly)"/>
		public Moment(Date date) : this(new DateTime(date.Value, TimeOnly.MinValue)) => Log.Event(new StackFrame(true));

		/// <inheritdoc cref="Moment(DateOnly,TimeOnly)"/>
		public Moment(TimeOnly time) : this(new DateTime(DateOnly.MinValue, time)) => Log.Event(new StackFrame(true));

		/// <inheritdoc cref="Moment(DateOnly,TimeOnly)"/>
		public Moment(Time time) : this(new DateTime(DateOnly.MinValue, time.Value)) => Log.Event(new StackFrame(true));

		/// <inheritdoc cref="Moment(DateOnly,TimeOnly)"/>
		/// <param name="year">The given year</param>
		/// <param name="month">The given month</param>
		/// <param name="day">The given day</param>
		/// <param name="hour">The given hour</param>
		/// <param name="minute">The given minute</param>
		/// <param name="second">The given second</param>
		/// <param name="millisecond">The given millisecond</param>
		public Moment(Year year, Month month, Day day, Hour hour, Minute minute, Second second, Millisecond millisecond) : this(new DateTime(year.Value, month.Value, day.Value, hour.Value, minute.Value, second.Value, millisecond.Value)) => Log.Event(new StackFrame(true));

		/// <inheritdoc cref="Moment(Year,Month,Day,Hour,Minute,Second,Millisecond)"/>
		public Moment(Year year, Month month, Day day, Hour hour, Minute minute, Second second) : this(new DateTime(year.Value, month.Value, day.Value, hour.Value, minute.Value, second.Value)) => Log.Event(new StackFrame(true));

		/// <inheritdoc cref="Moment(Year,Month,Day,Hour,Minute,Second,Millisecond)"/>
		public Moment(Year year, Month month, Day day, Hour hour, Minute minute) : this(new DateTime(year.Value, month.Value, day.Value, hour.Value, minute.Value, 0, 0, 0)) => Log.Event(new StackFrame(true));

		/// <inheritdoc cref="Moment(Year,Month,Day,Hour,Minute,Second,Millisecond)"/>
		public Moment(Year year, Month month, Day day, Hour hour) : this(new DateTime(year.Value, month.Value, day.Value, hour.Value, 0, 0, 0)) => Log.Event(new StackFrame(true));

		/// <inheritdoc cref="Moment(Year,Month,Day,Hour,Minute,Second,Millisecond)"/>
		public Moment(Year year, Month month, Day day) : this(new DateTime(year.Value, month.Value, day.Value, 0, 0, 0, 0)) => Log.Event(new StackFrame(true));

		/// <inheritdoc cref="Moment(Year,Month,Day,Hour,Minute,Second,Millisecond)"/>
		public Moment(Year year, Month month) : this(new DateTime(year.Value, month.Value, 1, 0, 0, 0, 0)) => Log.Event(new StackFrame(true));

		/// <inheritdoc cref="Moment(Year,Month,Day,Hour,Minute,Second,Millisecond)"/>
		public Moment(Year year) : this(new DateTime(year.Value, 1, 0, 0, 0, 0)) => Log.Event(new StackFrame(true));

		/// <summary>
		/// Constructor for the <see cref="Moment"/> class
		/// </summary>
		/// <param name="ticks">The number of ticks since the start of the Unix Epoch</param>
		public Moment(long ticks) : this(new DateTime(ticks)) => Log.Event(new StackFrame(true));

		/// <summary>
		/// Empty constructor for the <see cref="Moment"/> class
		/// </summary>
		public Moment() : this(DateTime.MinValue) => Log.Event(new StackFrame(true));

		public static implicit operator Moment(DateTime v) => new(v);
		public static implicit operator Moment(DateOnly v) => new(v);
		public static implicit operator Moment(TimeOnly v) => new(v);
		public static implicit operator Moment(Date v) => new(v);
		public static implicit operator Moment(Time v) => new(v);
		public static implicit operator Moment(long v) => new(v);

		/// <inheritdoc cref="Base{DateTime}.Equals(object?)"/>
		/// <exception cref="MomentException"/>
		public override bool Equals(object? obj)
		{
			var sf = new StackFrame(true);
			Log.Event(sf);

			bool? Out;

			try
			{
				Out = Equals(obj as Moment);
			}
			catch (BaseException)
			{
				throw;
			}
			catch (Exception ex)
			{
				throw new MomentException(ex, sf);
			}

			return Out ?? false;
		}

		/// <inheritdoc cref="Base{DateTime}.Equals(Base{DateTime}?)"/>
		/// <exception cref="MomentException"/>
		public bool Equals(Moment? other)
		{
			var sf = new StackFrame(true);
			Log.Event(sf);

			bool? Out;

			try
			{
				Out = other is not null && base.Equals(other) && Value == other.Value;
			}
			catch (BaseException)
			{
				throw;
			}
			catch (Exception ex)
			{
				throw new MomentException(ex, sf);
			}

			return Out ?? false;
		}

		public override int GetHashCode()
		{
			var sf = new StackFrame(true);
			Log.Event(sf);

			int? Out = null;

			try
			{
				Out = HashCode.Combine(base.GetHashCode(), Value);
			}
			catch (BaseException)
			{
				throw;
			}
			catch (Exception ex)
			{
				throw new MomentException(ex, sf);
			}
			finally
			{
				try
				{
					Out ??= base.GetHashCode();
				}
				catch (Exception) { }
			}

			return Out ?? 0;
		}

		public static bool operator >(Moment left, DateTime right) => left.Value > right;
		public static bool operator >=(Moment left, DateTime right) => left.Value >= right;
		public static bool operator <(Moment left, DateTime right) => left.Value < right;
		public static bool operator <=(Moment left, DateTime right) => left.Value <= right;
		public static bool operator ==(Moment? left, DateTime right) => left is not null && EqualityComparer<DateTime>.Default.Equals(left.Value, right);
		public static bool operator !=(Moment? left, DateTime right) => !(left == right);

		public static bool operator >(Moment left, Date right) => left.Value > right.ToMoment().Value;
		public static bool operator >=(Moment left, Date right) => left.Value >= right.ToMoment().Value;
		public static bool operator <(Moment left, Date right) => left.Value < right.ToMoment().Value;
		public static bool operator <=(Moment left, Date right) => left.Value <= right.ToMoment().Value;
		public static bool operator ==(Moment? left, Date? right) => left is not null && right is not null && EqualityComparer<DateTime>.Default.Equals(left.Value, right.ToMoment().Value);
		public static bool operator !=(Moment? left, Date? right) => !(left == right);

		public static bool operator >(Moment left, Time right) => left.Value > right.ToMoment().Value;
		public static bool operator >=(Moment left, Time right) => left.Value >= right.ToMoment().Value;
		public static bool operator <(Moment left, Time right) => left.Value < right.ToMoment().Value;
		public static bool operator <=(Moment left, Time right) => left.Value <= right.ToMoment().Value;
		public static bool operator ==(Moment? left, Time? right) => left is not null && right is not null && EqualityComparer<DateTime>.Default.Equals(left.Value, right.ToMoment().Value);
		public static bool operator !=(Moment? left, Time? right) => !(left == right);

		public static bool operator ==(Moment? left, Moment? right) => EqualityComparer<Moment>.Default.Equals(left, right);
		public static bool operator !=(Moment? left, Moment? right) => !(left == right);
	}
}
