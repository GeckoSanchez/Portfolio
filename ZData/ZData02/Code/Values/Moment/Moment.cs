namespace ZData02.Values
{
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.Diagnostics;
	using System.Numerics;
	using Actions;
	using Attributes;
	using Bases;
	using Exceptions;
	using MessagePack;
	using Newtonsoft.Json;
	using ZData02.IDs;

	[JsonObject(MemberSerialization.OptIn)]
	public class Moment : BaseData<DateTime>, IMinMaxValue<Moment>, IComparisonOperators<Moment, DateTime, bool>, IComparisonOperators<Moment, Date, bool>, IComparisonOperators<Moment, Time, bool>, IEquatable<Moment?>
	{
		[JsonProperty]
		public new DateTime Data { get => base.Data; protected set => base.Data = value; }

		/// <summary>
		/// Represents the smallest possible value of <see cref="Moment"/>. This property is get-only.
		/// </summary>
		public static Moment MinValue => new(DateTime.MinValue);

		/// <summary>
		/// Represents the largest possible value of <see cref="Moment"/>. This property is get-only.
		/// </summary>
		public static Moment MaxValue => new(DateTime.MaxValue);

		/// <summary>
		/// Gets a <see cref="Moment"/> object that is set to the current <see cref="Date"/> and <see cref="Time"/>
		/// on this computer, expressed as the local time.
		/// </summary>
		public static Moment Now => new(DateTime.Now.ToLocalTime());

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
		public Moment(Moment value) : this(value.Data) => Log.Event(new StackFrame(true));

		/// <inheritdoc cref="Moment(Moment)"/>
		/// <param name="date">The given date</param>
		/// <param name="time">The given time</param>
		public Moment(DateOnly date, TimeOnly time) : this(new DateTime(date, time)) => Log.Event(new StackFrame(true));

		/// <inheritdoc cref="Moment(DateOnly,TimeOnly)"/>
		public Moment(DateOnly date, Time time) : this(new DateTime(date, time.Data)) => Log.Event(new StackFrame(true));

		/// <inheritdoc cref="Moment(DateOnly,TimeOnly)"/>
		public Moment(Date date, TimeOnly time) : this(new DateTime(date.Data, time)) => Log.Event(new StackFrame(true));

		/// <inheritdoc cref="Moment(DateOnly,TimeOnly)"/>
		public Moment(Date date, Time time) : this(new DateTime(date.Data, time.Data)) => Log.Event(new StackFrame(true));

		/// <inheritdoc cref="Moment(DateOnly,TimeOnly)"/>
		public Moment(DateOnly date) : this(new DateTime(date, TimeOnly.MinValue)) => Log.Event(new StackFrame(true));

		/// <inheritdoc cref="Moment(DateOnly,TimeOnly)"/>
		public Moment(Date date) : this(new DateTime(date.Data, TimeOnly.MinValue)) => Log.Event(new StackFrame(true));

		/// <inheritdoc cref="Moment(DateOnly,TimeOnly)"/>
		public Moment(TimeOnly time) : this(new DateTime(DateOnly.MinValue, time)) => Log.Event(new StackFrame(true));

		/// <inheritdoc cref="Moment(DateOnly,TimeOnly)"/>
		public Moment(Time time) : this(new DateTime(DateOnly.MinValue, time.Data)) => Log.Event(new StackFrame(true));

		/// <inheritdoc cref="Moment(DateOnly,TimeOnly)"/>
		/// <param name="year">The given year</param>
		/// <param name="month">The given month</param>
		/// <param name="day">The given day</param>
		/// <param name="hour">The given hour</param>
		/// <param name="minute">The given minute</param>
		/// <param name="second">The given second</param>
		/// <param name="millisecond">The given millisecond</param>
		public Moment(Year year, Month month, Day day, Hour hour, Minute minute, Second second, Millisecond millisecond) : this(new DateTime(year.Data, month.Data, day.Data, hour.Data, minute.Data, second.Data, millisecond.Data)) => Log.Event(new StackFrame(true));

		/// <inheritdoc cref="Moment(Year,Month,Day,Hour,Minute,Second,Millisecond)"/>
		public Moment(Year year, Month month, Day day, Hour hour, Minute minute, Second second) : this(new DateTime(year.Data, month.Data, day.Data, hour.Data, minute.Data, second.Data)) => Log.Event(new StackFrame(true));

		/// <inheritdoc cref="Moment(Year,Month,Day,Hour,Minute,Second,Millisecond)"/>
		public Moment(Year year, Month month, Day day, Hour hour, Minute minute) : this(new DateTime(year.Data, month.Data, day.Data, hour.Data, minute.Data, 0, 0, 0)) => Log.Event(new StackFrame(true));

		/// <inheritdoc cref="Moment(Year,Month,Day,Hour,Minute,Second,Millisecond)"/>
		public Moment(Year year, Month month, Day day, Hour hour) : this(new DateTime(year.Data, month.Data, day.Data, hour.Data, 0, 0, 0)) => Log.Event(new StackFrame(true));

		/// <inheritdoc cref="Moment(Year,Month,Day,Hour,Minute,Second,Millisecond)"/>
		public Moment(Year year, Month month, Day day) : this(new DateTime(year.Data, month.Data, day.Data, 0, 0, 0, 0)) => Log.Event(new StackFrame(true));

		/// <inheritdoc cref="Moment(Year,Month,Day,Hour,Minute,Second,Millisecond)"/>
		public Moment(Year year, Month month) : this(new DateTime(year.Data, month.Data, 1, 0, 0, 0, 0)) => Log.Event(new StackFrame(true));

		/// <inheritdoc cref="Moment(Year,Month,Day,Hour,Minute,Second,Millisecond)"/>
		public Moment(Year year) : this(new DateTime(year.Data, 1, 0, 0, 0, 0)) => Log.Event(new StackFrame(true));

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

		/// <inheritdoc cref="BaseData{DateTime}.Equals(object?)"/>
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

		/// <inheritdoc cref="BaseData{DateTime}.Equals(BaseData{DateTime}?)"/>
		/// <exception cref="MomentException"/>
		public bool Equals(Moment? other)
		{
			var sf = new StackFrame(true);
			Log.Event(sf);

			bool? Out;

			try
			{
				Out = other is not null && base.Equals(other) && Data == other.Data;
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
				Out = HashCode.Combine(base.GetHashCode(), Data);
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

		/// <inheritdoc cref="BaseData{TData}.ToMessagePack(MessagePackSerializerOptions?)"/>
		/// <exception cref="MomentException"/>
		public override byte[] ToMessagePack(MessagePackSerializerOptions? opts = null)
		{
			var sf = new StackFrame(true);
			Log.Event(sf);

			byte[]? Out = null;

			try
			{
				Out = MessagePackSerializer.Serialize(Data.Year, opts);
				Out = MessagePackSerializer.Serialize(Data.Month, opts);
				Out = MessagePackSerializer.Serialize(Data.Day, opts);
				Out = MessagePackSerializer.Serialize(Data.Hour, opts);
				Out = MessagePackSerializer.Serialize(Data.Minute, opts);
				Out = MessagePackSerializer.Serialize(Data.Second, opts);
				Out = MessagePackSerializer.Serialize(Data.Millisecond, opts);
			}
			catch (MessagePackSerializationException ex)
			{
				throw new MomentException(new Exception($"This data could not be serialized into the MessagePack format", ex), sf);
			}
			catch (Exception ex)
			{
				throw new MomentException(ex, sf);
			}
			finally
			{
				try
				{
					Out ??= base.ToMessagePack(opts);
				}
				catch (Exception)
				{
					Out = [];
				}
			}

			return Out;
		}

		public override IEnumerable<ValidationResult> Validate(ValidationContext? validationContext = null)
		{

			return base.Validate(validationContext);
		}

		public static bool operator >(Moment left, DateTime right) => left.Data > right;
		public static bool operator >=(Moment left, DateTime right) => left.Data >= right;
		public static bool operator <(Moment left, DateTime right) => left.Data < right;
		public static bool operator <=(Moment left, DateTime right) => left.Data <= right;
		public static bool operator ==(Moment? left, DateTime right) => left is not null && EqualityComparer<DateTime>.Default.Equals(left.Data, right);
		public static bool operator !=(Moment? left, DateTime right) => !(left == right);

		public static bool operator >(Moment left, Date right) => left.Data > right.ToMoment().Data;
		public static bool operator >=(Moment left, Date right) => left.Data >= right.ToMoment().Data;
		public static bool operator <(Moment left, Date right) => left.Data < right.ToMoment().Data;
		public static bool operator <=(Moment left, Date right) => left.Data <= right.ToMoment().Data;
		public static bool operator ==(Moment? left, Date? right) => left is not null && right is not null && EqualityComparer<DateTime>.Default.Equals(left.Data, right.ToMoment().Data);
		public static bool operator !=(Moment? left, Date? right) => !(left == right);

		public static bool operator >(Moment left, Time right) => left.Data > right.ToMoment().Data;
		public static bool operator >=(Moment left, Time right) => left.Data >= right.ToMoment().Data;
		public static bool operator <(Moment left, Time right) => left.Data < right.ToMoment().Data;
		public static bool operator <=(Moment left, Time right) => left.Data <= right.ToMoment().Data;
		public static bool operator ==(Moment? left, Time? right) => left is not null && right is not null && EqualityComparer<DateTime>.Default.Equals(left.Data, right.ToMoment().Data);
		public static bool operator !=(Moment? left, Time? right) => !(left == right);

		public static bool operator ==(Moment? left, Moment? right) => EqualityComparer<Moment>.Default.Equals(left, right);
		public static bool operator !=(Moment? left, Moment? right) => !(left == right);
	}
}
