namespace ZData01.Categories
{
	using System.Numerics;

	public interface INumber : ICategory { }

	public interface INumber<TNum> : ICategory where TNum : INumberBase<TNum>, IComparisonOperators<TNum, TNum, bool>, IMinMaxValue<TNum>
	{
		/// <summary>
		/// The current <typeparamref name="TNum"/> value
		/// </summary>
		TNum Value { get; }

		/// <summary>
		/// The minimum <typeparamref name="TNum"/> value
		/// </summary>
		TNum MinValue { get; }

		/// <summary>
		/// The maximum <typeparamref name="TNum"/> value
		/// </summary>
		TNum MaxValue { get; }
	}
}
