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
	public class Second : BaseNumber<int>, IMinMaxValue<Second>
	{
		public new static Second MinValue => new(TimeOnly.MinValue.Second);
		public new static Second MaxValue => new(TimeOnly.MaxValue.Second);

		/// <summary>
		/// Primary constructor for the <see cref="Second"/> class
		/// </summary>
		/// <param name="value">The given value</param>
		/// <exception cref="NumberException"/>
		[JsonConstructor, MainConstructor]
		public Second(int value) : base(value, DateTime.MinValue.Second, DateTime.MaxValue.Second) => Log.Event(new StackFrame(true));

		/// <summary>
		/// Constructor for the <see cref="Second"/> class
		/// </summary>
		/// <inheritdoc cref="Second(int)"/>
		public Second(uint value) : this((int)value) => Log.Event(new StackFrame(true));

		/// <inheritdoc cref="Second(uint)"/>
		public Second(TimeOnly value) : this(value.Second) => Log.Event(new StackFrame(true));

		/// <inheritdoc cref="Second(uint)"/>
		public Second(DateTime value) : this(value.Second) => Log.Event(new StackFrame(true));

		/// <inheritdoc cref="Second(uint)"/>
		public Second(Second value) : this(value.Value) => Log.Event(new StackFrame(true));

		/// <inheritdoc cref="Second(uint)"/>
		public Second(Time value) : this(value.Value.Second) => Log.Event(new StackFrame(true));

		/// <inheritdoc cref="Second(uint)"/>
		public Second(Moment value) : this(value.Value.Second) => Log.Event(new StackFrame(true));

		public static implicit operator Second(int v) => new(v);
		public static implicit operator Second(uint v) => new(v);
		public static implicit operator Second(TimeOnly v) => new(v);
		public static implicit operator Second(DateTime v) => new(v);
		public static implicit operator Second(Time v) => new(v);
		public static implicit operator Second(Moment v) => new(v);

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
				Out = $"{Value:N2}";
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
