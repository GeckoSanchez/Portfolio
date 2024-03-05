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
	public class Year : LNumber<int>, IMinMaxValue<Year>
	{
		public static Year MinValue => new(DateOnly.MinValue.Year);
		public static Year MaxValue => new(DateOnly.MaxValue.Year);

		/// <summary>
		/// Primary constructor for the <see cref="Year"/> class
		/// </summary>
		/// <param name="value">The given value</param>
		/// <exception cref="NumberException"/>
		[JsonConstructor, MainConstructor]
		public Year(int value) : base(value, DateTime.MinValue.Year, DateTime.MaxValue.Year) => Log.Event(new StackFrame(true));

		/// <summary>
		/// Constructor for the <see cref="Year"/> class
		/// </summary>
		/// <inheritdoc cref="Year(int)"/>
		public Year(uint value) : this((int)value) => Log.Event(new StackFrame(true));

		/// <inheritdoc cref="Year(uint)"/>
		public Year(DateOnly value) : this(value.Year) => Log.Event(new StackFrame(true));

		/// <inheritdoc cref="Year(uint)"/>
		public Year(DateTime value) : this(value.Year) => Log.Event(new StackFrame(true));

		/// <inheritdoc cref="Year(uint)"/>
		public Year(Year value) : this(value.Data) => Log.Event(new StackFrame(true));

		/// <inheritdoc cref="Year(uint)"/>
		public Year(Date value) : this(value.Data.Year) => Log.Event(new StackFrame(true));

		/// <inheritdoc cref="Year(uint)"/>
		public Year(Moment value) : this(value.Data.Year) => Log.Event(new StackFrame(true));

		public static implicit operator Year(int v) => new(v);
		public static implicit operator Year(uint v) => new(v);
		public static implicit operator Year(DateOnly v) => new(v);
		public static implicit operator Year(DateTime v) => new(v);
		public static implicit operator Year(Date v) => new(v);
		public static implicit operator Year(Moment v) => new(v);

		public override string ToJSON(Formatting? formatting = null)
		{
			var sf = new StackFrame(true);
			Log.Event(sf);

			string? Out = null;

			try
			{
				Out = JsonConvert.SerializeObject(this, formatting ?? Def.JSONFormatting);
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
				Out = $"{Data:N4}";
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
