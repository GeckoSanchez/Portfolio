namespace ZData00.Attributes
{
	using System.Diagnostics;
	using Actions;

	[AttributeUsage(AttributeTargets.Constructor, AllowMultiple = false, Inherited = false)]
	public sealed class MainConstructorAttribute : Attribute
	{
		public MainConstructorAttribute() => Log.Event(new StackFrame(true));
	}
}
