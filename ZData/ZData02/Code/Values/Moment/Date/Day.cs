namespace ZData02.Values
{
	using System.Diagnostics;
	using System.Numerics;
	using Actions;
	using Attributes;
	using Bases;
	using Exceptions;
	using Newtonsoft.Json;

	[JsonObject(MemberSerialization.OptIn)]
	public class Day : LNumber<int>, IMinMaxValue<Day>
	{
		public static Day MinValue => new(DateOnly.MinValue.Day);
		public static Day MaxValue => new(DateOnly.MaxValue.Day);

		/// <summary>
		/// Primary constructor for the <see cref="Day"/> class
		/// </summary>
		/// <param name="value">The given value</param>
		/// <exception cref="NumberException"/>
		[JsonConstructor, MainConstructor]
		public Day(int value) : base(value, DateTime.MinValue.Day, DateTime.MaxValue.Day) => Log.Event(new StackFrame(true));

		/// <summary>
		/// Constructor for the <see cref="Day"/> class
		/// </summary>
		/// <inheritdoc cref="Day(int)"/>
		public Day(uint value) : this((int)value) => Log.Event(new StackFrame(true));

		/// <inheritdoc cref="Day(uint)"/>
		public Day(DateOnly value) : this(value.Day) => Log.Event(new StackFrame(true));

		/// <inheritdoc cref="Day(uint)"/>
		public Day(DateTime value) : this(value.Day) => Log.Event(new StackFrame(true));

		/// <inheritdoc cref="Day(uint)"/>
		public Day(Day value) : this(value.Data) => Log.Event(new StackFrame(true));

		/// <inheritdoc cref="Day(uint)"/>
		public Day(Date value) : this(value.Data.Day) => Log.Event(new StackFrame(true));

		/// <inheritdoc cref="Day(uint)"/>
		public Day(Moment value) : this(value.Data.Day) => Log.Event(new StackFrame(true));

		public static implicit operator Day(int v) => new(v);
		public static implicit operator Day(uint v) => new(v);
		public static implicit operator Day(DateOnly v) => new(v);
		public static implicit operator Day(DateTime v) => new(v);
		public static implicit operator Day(Date v) => new(v);
		public static implicit operator Day(Moment v) => new(v);

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

		public override string ToString()
		{
			var sf = new StackFrame(true);
			Log.Event(sf);

			string? Out = null;

			try
			{
				Out = $"{Data:N2}";
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
	}
}
