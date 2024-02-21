namespace ZData00.Bases
{
	using System.Diagnostics;
	using System.Numerics;
	using Actions;
	using Attributes;
	using Exceptions;
	using Newtonsoft.Json;

	public class BaseNumber<TNum> : Base<TNum> where TNum : notnull, INumber<TNum>, IMinMaxValue<TNum>
	{
		[JsonProperty]
		public new TNum Value { get => base.Value; protected set => base.Value = value; }
		[JsonProperty]
		public TNum Minimum { get; protected init; }
		[JsonProperty]
		public TNum Maximum { get; protected init; }

		[JsonConstructor, PrimaryConstructor]
		public BaseNumber(TNum value, TNum minimum, TNum maximum) : base(TNum.Zero)
		{
			var sf = new StackFrame(true);
			Log.Event(sf);

			try
			{
				if (minimum > maximum)
					throw new Exception($"The minimum value {Format<TNum>.ExcValue(minimum)} is greater than the maximum value {Format<TNum>.ExcValue(maximum)}");
				else if (value < minimum)
					throw new Exception($"The current value {Format<TNum>.ExcValue(value)} is lesser than the minimum value {Format<TNum>.ExcValue(minimum)}");
				else if (value > maximum)
					throw new Exception($"The current value {Format<TNum>.ExcValue(value)} is greater than the maximum value {Format<TNum>.ExcValue(maximum)}");
				else
				{
					Value = value;
					Minimum = minimum;
					Maximum = maximum;
				}
			}
			catch (Exception ex)
			{
				throw new NumberException(ex, sf);
			}
			finally
			{
				Minimum ??= TNum.Zero;
				Maximum ??= TNum.MaxValue;
			}
		}

		public BaseNumber(TNum value, TNum maximum) : this(value, TNum.MinValue, maximum) => Log.Event(new StackFrame(true));
		public BaseNumber(TNum value) : this(value, TNum.MinValue, TNum.MaxValue) => Log.Event(new StackFrame(true));
	}
}
