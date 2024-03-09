namespace ZData04.Actions
{
	using Attributes;

	[method: Unlogged]
	public class Format(string value)
	{
		public static string ExcValue(string value) => $"('{value}')";
		public override string ToString() => $"'{value}'";
	}

	[method: Unlogged]
	public class Format<T>(T value)
	{
		public static string ExcValue(T value) => $"('{value}')";
		public override string ToString() => $"'{value}'";
	}
}
