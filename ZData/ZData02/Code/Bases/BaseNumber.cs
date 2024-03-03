namespace ZData02.Bases
{
	using System.Diagnostics;
	using System.Diagnostics.CodeAnalysis;
	using System.Numerics;
	using Actions;
	using Attributes;
	using Newtonsoft.Json;

	[JsonObject(MemberSerialization.OptIn)]
	public class BaseNumber<T> : BaseData<T> where T : INumberBase<T>, IComparisonOperators<T, T, bool>, IMinMaxValue<T>
	{
		[JsonProperty]
		public new T Data { get => base.Data; protected set => base.Data = value; }

		/// <summary>
		/// Primary constructor for the <see cref="BaseNumber{T}"/> class
		/// </summary>
		/// <param name="data"></param>
		[JsonConstructor, MainConstructor]
		public BaseNumber([NotNull] T data) : base(data) => Log.Event(new StackFrame(true));
	}
}
