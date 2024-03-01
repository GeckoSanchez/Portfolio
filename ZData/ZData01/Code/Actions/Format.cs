namespace ZData01.Actions
{
	using Attributes;

	[method: Unlogged]
	public class Format(string value)
	{
		[Unlogged]
		public static string ExcValue(string value) => $"('{value}')";
		[Unlogged]
		public override string ToString() => $"'{value}'";
	}

	[method: Unlogged]
	public class Format<T>(T value)
	{
		[Unlogged]
		public static string ExcValue(T value) => $"('{value}')";
		[Unlogged]
		public override string ToString() => $"'{value}'";
	}
}
