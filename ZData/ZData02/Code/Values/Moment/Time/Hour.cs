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
	public class Hour : LNumber<int>, IMinMaxValue<Hour>
	{
		public static Hour MinValue => new(TimeOnly.MinValue.Hour);
		public static Hour MaxValue => new(TimeOnly.MaxValue.Hour);

		/// <summary>
		/// Primary constructor for the <see cref="Hour"/> class
		/// </summary>
		/// <param name="value">The given value</param>
		/// <exception cref="NumberException"/>
		[JsonConstructor, MainConstructor]
		public Hour(int value) : base(value, DateTime.MinValue.Hour, DateTime.MaxValue.Hour) => Log.Event(new StackFrame(true));

		/// <summary>
		/// Constructor for the <see cref="Hour"/> class
		/// </summary>
		/// <inheritdoc cref="Hour(int)"/>
		public Hour(uint value) : this((int)value) => Log.Event(new StackFrame(true));

		/// <inheritdoc cref="Hour(uint)"/>
		public Hour(TimeOnly value) : this(value.Hour) => Log.Event(new StackFrame(true));

		/// <inheritdoc cref="Hour(uint)"/>
		public Hour(DateTime value) : this(value.Hour) => Log.Event(new StackFrame(true));

		/// <inheritdoc cref="Hour(uint)"/>
		public Hour(Hour value) : this(value.Data) => Log.Event(new StackFrame(true));

		/// <inheritdoc cref="Hour(uint)"/>
		public Hour(Time value) : this(value.Data.Hour) => Log.Event(new StackFrame(true));

		/// <inheritdoc cref="Hour(uint)"/>
		public Hour(Moment value) : this(value.Data.Hour) => Log.Event(new StackFrame(true));

		public static implicit operator Hour(int v) => new(v);
		public static implicit operator Hour(uint v) => new(v);
		public static implicit operator Hour(TimeOnly v) => new(v);
		public static implicit operator Hour(DateTime v) => new(v);
		public static implicit operator Hour(Time v) => new(v);
		public static implicit operator Hour(Moment v) => new(v);

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
