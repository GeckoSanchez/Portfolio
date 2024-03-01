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
	public class Millisecond : BaseNumber<int>, IMinMaxValue<Millisecond>
	{
		public new static Millisecond MinValue => new(TimeOnly.MinValue.Millisecond);
		public new static Millisecond MaxValue => new(TimeOnly.MaxValue.Millisecond);

		/// <summary>
		/// Primary constructor for the <see cref="Millisecond"/> class
		/// </summary>
		/// <param name="value">The given value</param>
		/// <exception cref="NumberException"/>
		[JsonConstructor, MainConstructor]
		public Millisecond(int value) : base(value, DateTime.MinValue.Millisecond, DateTime.MaxValue.Millisecond) => Log.Event(new StackFrame(true));

		/// <summary>
		/// Constructor for the <see cref="Millisecond"/> class
		/// </summary>
		/// <inheritdoc cref="Millisecond(int)"/>
		public Millisecond(uint value) : this((int)value) => Log.Event(new StackFrame(true));

		/// <inheritdoc cref="Millisecond(uint)"/>
		public Millisecond(TimeOnly value) : this(value.Millisecond) => Log.Event(new StackFrame(true));

		/// <inheritdoc cref="Millisecond(uint)"/>
		public Millisecond(DateTime value) : this(value.Millisecond) => Log.Event(new StackFrame(true));

		/// <inheritdoc cref="Millisecond(uint)"/>
		public Millisecond(Millisecond value) : this(value.Value) => Log.Event(new StackFrame(true));

		/// <inheritdoc cref="Millisecond(uint)"/>
		public Millisecond(Time value) : this(value.Value.Millisecond) => Log.Event(new StackFrame(true));

		/// <inheritdoc cref="Millisecond(uint)"/>
		public Millisecond(Moment value) : this(value.Value.Millisecond) => Log.Event(new StackFrame(true));

		public static implicit operator Millisecond(int v) => new(v);
		public static implicit operator Millisecond(uint v) => new(v);
		public static implicit operator Millisecond(TimeOnly v) => new(v);
		public static implicit operator Millisecond(DateTime v) => new(v);
		public static implicit operator Millisecond(Time v) => new(v);
		public static implicit operator Millisecond(Moment v) => new(v);

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
				Out = $"{Value:N3}";
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
