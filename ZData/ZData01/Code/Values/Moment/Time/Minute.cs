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
	public class Minute : BaseNumber<int>, IMinMaxValue<Minute>
	{
		public new static Minute MinValue => new(TimeOnly.MinValue.Minute);
		public new static Minute MaxValue => new(TimeOnly.MaxValue.Minute);

		/// <summary>
		/// Primary constructor for the <see cref="Minute"/> class
		/// </summary>
		/// <param name="value">The given value</param>
		/// <exception cref="NumberException"/>
		[JsonConstructor, MainConstructor]
		public Minute(int value) : base(value, DateTime.MinValue.Minute, DateTime.MaxValue.Minute) => Log.Event(new StackFrame(true));

		/// <summary>
		/// Constructor for the <see cref="Minute"/> class
		/// </summary>
		/// <inheritdoc cref="Minute(int)"/>
		public Minute(uint value) : this((int)value) => Log.Event(new StackFrame(true));

		/// <inheritdoc cref="Minute(uint)"/>
		public Minute(TimeOnly value) : this(value.Minute) => Log.Event(new StackFrame(true));

		/// <inheritdoc cref="Minute(uint)"/>
		public Minute(DateTime value) : this(value.Minute) => Log.Event(new StackFrame(true));

		/// <inheritdoc cref="Minute(uint)"/>
		public Minute(Minute value) : this(value.Value) => Log.Event(new StackFrame(true));

		/// <inheritdoc cref="Minute(uint)"/>
		public Minute(Time value) : this(value.Value.Minute) => Log.Event(new StackFrame(true));

		/// <inheritdoc cref="Minute(uint)"/>
		public Minute(Moment value) : this(value.Value.Minute) => Log.Event(new StackFrame(true));

		public static implicit operator Minute(int v) => new(v);
		public static implicit operator Minute(uint v) => new(v);
		public static implicit operator Minute(TimeOnly v) => new(v);
		public static implicit operator Minute(DateTime v) => new(v);
		public static implicit operator Minute(Time v) => new(v);
		public static implicit operator Minute(Moment v) => new(v);

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
