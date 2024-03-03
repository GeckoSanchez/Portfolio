namespace ZData02.Values
{
	using System.Diagnostics;
	using System.Diagnostics.CodeAnalysis;
	using System.Numerics;
	using Actions;
	using Attributes;
	using Bases;
	using Exceptions;
	using Newtonsoft.Json;

	[JsonObject(MemberSerialization.OptIn)]
	public class LNumber<T> : BaseNumber<T> where T : INumberBase<T>, IComparisonOperators<T, T, bool>, IMinMaxValue<T>
	{
		[JsonProperty]
		public T Minimum { get; protected set; }

		[JsonProperty]
		public T Maximum { get; protected set; }

		/// <summary>
		/// Primary constructor for the <see cref="LNumber{T}"/> class
		/// </summary>
		/// <param name="current">The current <typeparamref name="T"/> value</param>
		/// <param name="min">The minimum <typeparamref name="T"/> value</param>
		/// <param name="max">The maximum <typeparamref name="T"/> value</param>
		/// <exception cref="NumberException"/>
		[JsonConstructor, MainConstructor]
		public LNumber([NotNull] T current, [AllowNull] T? min, [AllowNull] T? max) : base(current)
		{
			var sf = new StackFrame(true);
			Log.Event(sf);

			try
			{
				min ??= T.MinValue;
				max ??= T.MaxValue;

				if (min == max && current != min)
					throw new Exception($"The minimum value {Format<T>.ExcValue(min)} is equal to the maximum value {Format<T>.ExcValue(max)}, and the current value {Format<T>.ExcValue(current)} also different to the minimum/maximum value");

				Minimum = min;
				Maximum = max;
			}
			catch (Exception ex)
			{
				throw new NumberException(ex, sf);
			}
			finally
			{
				Minimum ??= T.MinValue;
				Maximum ??= T.MaxValue;
			}
		}

		/// <summary>
		/// Constructor for the <see cref="LNumber{T}"/> class
		/// </summary>
		/// <param name="current">The current <typeparamref name="T"/> value</param>
		/// <param name="min">The minimum <typeparamref name="T"/> value</param>
		/// <param name="max">The maximum <typeparamref name="T"/> value</param>
		/// <exception cref="NumberException"/>
		public LNumber([NotNull] T current, [AllowNull] T? max) : this(current, T.MinValue, max) => Log.Event(new StackFrame(true));

		/// <summary>
		/// Constructor for the <see cref="LNumber{T}"/> class
		/// </summary>
		/// <param name="current">The current <typeparamref name="T"/> value</param>
		/// <exception cref="NumberException"/>
		public LNumber([NotNull] T current) : this(current, T.MinValue, T.MaxValue) => Log.Event(new StackFrame(true));

		/// <summary>
		/// Empty constructor for the <see cref="LNumber{T}"/> class
		/// </summary>
		/// <exception cref="NumberException"/>
		public LNumber() : this(T.Zero, T.MinValue, T.MaxValue) => Log.Event(new StackFrame(true));
	}
}
