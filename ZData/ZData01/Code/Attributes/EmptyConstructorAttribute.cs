namespace ZData01.Attributes
{
	using System.Diagnostics;
	using Actions;

	[AttributeUsage(AttributeTargets.Constructor, AllowMultiple = false, Inherited = false)]
	public class EmptyConstructorAttribute : Attribute
	{
		public EmptyConstructorAttribute() => Log.Event(new StackFrame(true));
	}
}
