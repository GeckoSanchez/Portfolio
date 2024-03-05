namespace ZData02.Values
{
	using System.Diagnostics;
	using System.Numerics;
	using Actions;
	using Attributes;
	using Bases;
	using Enums;
	using Exceptions;
	using Newtonsoft.Json;

	[JsonObject(MemberSerialization.OptIn)]
	public class Month : LNumber<int>, IMinMaxValue<Month>
	{
		public static Month MinValue => new(DateOnly.MinValue.Month);
		public static Month MaxValue => new(DateOnly.MaxValue.Month);

		/// <summary>
		/// Primary constructor for the <see cref="Month"/> class
		/// </summary>
		/// <param name="value">The given value</param>
		/// <exception cref="NumberException"/>
		[JsonConstructor, MainConstructor]
		public Month(int value) : base(value, DateTime.MinValue.Month, DateTime.MaxValue.Month) => Log.Event(new StackFrame(true));

		/// <summary>
		/// Constructor for the <see cref="Month"/> class
		/// </summary>
		/// <inheritdoc cref="Month(int)"/>
		public Month(uint value) : this((int)value) => Log.Event(new StackFrame(true));

		/// <inheritdoc cref="Month(uint)"/>
		public Month(DateOnly value) : this(value.Month) => Log.Event(new StackFrame(true));

		/// <inheritdoc cref="Month(uint)"/>
		public Month(DateTime value) : this(value.Month) => Log.Event(new StackFrame(true));

		/// <inheritdoc cref="Month(uint)"/>
		public Month(Months value) : this(value.GetHashCode()) => Log.Event(new StackFrame(true));

		/// <inheritdoc cref="Month(uint)"/>
		public Month(Month value) : this(value.Data) => Log.Event(new StackFrame(true));

		/// <inheritdoc cref="Month(uint)"/>
		public Month(Date value) : this(value.Data.Month) => Log.Event(new StackFrame(true));

		/// <inheritdoc cref="Month(uint)"/>
		public Month(Moment value) : this(value.Data.Month) => Log.Event(new StackFrame(true));

		public static implicit operator Month(int v) => new(v);
		public static implicit operator Month(uint v) => new(v);
		public static implicit operator Month(DateOnly v) => new(v);
		public static implicit operator Month(DateTime v) => new(v);
		public static implicit operator Month(Months v) => new(v);
		public static implicit operator Month(Date v) => new(v);
		public static implicit operator Month(Moment v) => new(v);

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
