namespace ZData00.Attributes
{
	using System.Diagnostics;
	using Actions;

	[AttributeUsage(AttributeTargets.Constructor, AllowMultiple = false, Inherited = false)]
	public class PrimaryConstructorAttribute : Attribute
	{
		public PrimaryConstructorAttribute() => Log.Event(new StackFrame(true));
	}
}
